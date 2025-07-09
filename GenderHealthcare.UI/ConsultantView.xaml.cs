using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using GenderHealthcare.BLL.DTOs;
using GenderHealthcare.BLL.Interfaces;
using GenderHealthcare.UI.Services;
using GenderHealthcare.UI.Views;

namespace GenderHealthcare.UI.Views
{
    public partial class ConsultantView : UserControl
    {
        private readonly IConsultantProfileService _profileSvc;
        private readonly IAppointmentService _appointmentSvc;
        private readonly ICurrentUserService _userCtx;
        private readonly IRoleService _roleSvc;

        public ConsultantView()
        {
            InitializeComponent();
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                var sp = App.AppHost.Services;
                _profileSvc = sp.GetRequiredService<IConsultantProfileService>();
                _appointmentSvc = sp.GetRequiredService<IAppointmentService>();
                _userCtx = sp.GetRequiredService<ICurrentUserService>();
                _roleSvc = sp.GetRequiredService<IRoleService>();
                Loaded += ConsultantView_Loaded;
            }
        }

        private async void ConsultantView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            var userId = _userCtx.UserId;
            if (string.IsNullOrEmpty(userId) || !await IsConsultantAsync(userId))
            {
                this.Visibility = System.Windows.Visibility.Collapsed;
                MessageBox.Show("Bạn không có quyền truy cập trang này.", "Lỗi quyền", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            await LoadProfileAsync(userId);
            await LoadAppointmentsAsync(userId);
        }

        private async Task<bool> IsConsultantAsync(string userId)
        {
            var roles = await _roleSvc.GetRolesByUserIdAsync(userId);
            return roles != null && roles.Any(r => r.Name == "Consultant");
        }

        public async Task LoadProfileAsync(string consultantId)
        {
            var profile = await _profileSvc.GetConsultantProfileByIdAsync(consultantId);
            if (profile != null)
            {
                txtSpecialization.Text = profile.Specialization ?? string.Empty;
                txtQualification.Text = profile.Qualification ?? string.Empty;
                txtExperience.Text = profile.Experience ?? string.Empty;
                txtConsultationFee.Text = profile.ConsultationFee?.ToString("F2") ?? "0.00";
                txtProfileStatus.Text = profile.ProfileStatus ?? string.Empty;
                chkIsAvailable.IsChecked = profile.IsAvailable;
            }
            else
            {
                MessageBox.Show("Không tìm thấy hồ sơ cố vấn.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async Task LoadAppointmentsAsync(string consultantId)
        {
            var appointments = await _appointmentSvc.GetAppointmentsByConsultantIdAsync(consultantId);
            dgAppointments.ItemsSource = appointments ?? new List<AppointmentDTO>();
        }

        private async void BtnUpdateProfile_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var consultantId = _userCtx.UserId;
            if (string.IsNullOrEmpty(consultantId))
            {
                MessageBox.Show("Không thể xác định ID cố vấn.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!decimal.TryParse(txtConsultationFee.Text, out var fee) || fee < 0)
            {
                MessageBox.Show("Phí tư vấn phải là số hợp lệ và không âm.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var profile = new ConsultantProfileDTO
            {
                ConsultantId = consultantId,
                Specialization = txtSpecialization.Text,
                Qualification = txtQualification.Text,
                Experience = txtExperience.Text,
                ConsultationFee = fee,
                ProfileStatus = txtProfileStatus.Text,
                IsAvailable = chkIsAvailable.IsChecked ?? false,
                Status = true,
                UpdatedAt = DateTime.Now
            };

            try
            {
                await _profileSvc.UpdateConsultantProfileAsync(profile);
                await LoadProfileAsync(consultantId);
                MessageBox.Show("Cập nhật hồ sơ thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật hồ sơ: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = App.AppHost.Services.GetRequiredService<LoginWindow>();
            loginWindow.Show();
            var window = Window.GetWindow(this);
            if (window != null) window.Close();
        }
    }
}