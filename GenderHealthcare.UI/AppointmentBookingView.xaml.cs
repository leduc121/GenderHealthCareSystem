using GenderHealthcare.BLL.DTOs;
using GenderHealthcare.BLL.Interfaces;
using GenderHealthcare.UI.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace GenderHealthcare.UI.Views
{
    public partial class AppointmentBookingView : UserControl
    {
        private readonly IUserService _userSvc;
        private readonly IAppointmentService _appointmentSvc;
        private readonly ICurrentUserService _userCtx;
        private readonly IRoleService _roleSvc;

        public AppointmentBookingView()
        {
            InitializeComponent();
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                var sp = App.AppHost.Services;
                _userSvc = sp.GetRequiredService<IUserService>();
                _appointmentSvc = sp.GetRequiredService<IAppointmentService>();
                _userCtx = sp.GetRequiredService<ICurrentUserService>();
                _roleSvc = sp.GetRequiredService<IRoleService>();
                LoadConsultants();
            }
        }

        private async void LoadConsultants()
        {
            try
            {
                // 1) Lấy tất cả người dùng
                var allUsers = await _userSvc.GetAllUsersAsync();
                var consultants = new List<UserDTO>();

                // 2) Với mỗi người dùng, kiểm tra vai trò "Consultant"
                foreach (var user in allUsers)
                {
                    var roles = await _roleSvc.GetRolesByUserIdAsync(user.Id); // IEnumerable<RoleDTO>
                    if (roles.Any(r => string.Equals(r.Name, "Consultant", StringComparison.OrdinalIgnoreCase)))
                    {
                        consultants.Add(user);
                    }
                }

                // 3) Bind danh sách cố vấn lên ComboBox
                cmbConsultants.ItemsSource = consultants;
                cmbConsultants.DisplayMemberPath = "Username"; // Hiển thị Username trong ComboBox
                cmbConsultants.SelectedValuePath = "Id"; // Giá trị được chọn là Id
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách cố vấn: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void BookAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (cmbConsultants.SelectedValue == null || dpDate.SelectedDate == null)
            {
                MessageBox.Show("Vui lòng chọn cố vấn và ngày hẹn.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!TimeSpan.TryParse(txtTime.Text, out var time))
            {
                MessageBox.Show("Giờ phải đúng định dạng HH:mm.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var userId = _userCtx.UserId;
            if (string.IsNullOrEmpty(userId))
            {
                MessageBox.Show("Không tìm thấy thông tin người dùng hiện tại.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var consultantId = cmbConsultants.SelectedValue.ToString();
            var apptDate = dpDate.SelectedDate.Value.Date + time;

            // Kiểm tra lịch hẹn trùng
            try
            {
                var existing = await _appointmentSvc.GetAppointmentsByUserIdAsync(userId);
                if (existing.Any(a => a.AppointmentDate == apptDate))
                {
                    MessageBox.Show("Bạn đã có lịch hẹn vào thời gian này rồi.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var dto = new AppointmentDTO
                {
                    UserId = userId,
                    ConsultantId = consultantId,
                    AppointmentDate = apptDate,
                    AppointmentStatus = "Scheduled" // Giá trị mặc định
                };

                await _appointmentSvc.CreateAppointmentAsync(dto);
                MessageBox.Show("Đặt lịch thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);

                // Reset form
                cmbConsultants.SelectedIndex = -1;
                dpDate.SelectedDate = null;
                txtTime.Text = "09:00";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi đặt lịch: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}