using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using GenderHealthcare.BLL.Interfaces;
using GenderHealthcare.BLL.DTOs;
using GenderHealthcare.UI.ViewModels;

namespace GenderHealthcare.UI.Views
{
    public partial class UserAppointmentsView : UserControl
    {
        private readonly IAppointmentService _appointmentSvc;
        private readonly IUserService _userSvc;
        private readonly ICurrentUserService _userCtx;

        public UserAppointmentsView()
        {
            InitializeComponent();
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                var sp = App.AppHost.Services;
                _appointmentSvc = sp.GetRequiredService<IAppointmentService>();
                _userSvc = sp.GetRequiredService<IUserService>();
                _userCtx = sp.GetRequiredService<ICurrentUserService>();
            }
        }

        public async void LoadAppointments()
        {
            try
            {
                var uid = _userCtx.UserId;
                if (string.IsNullOrEmpty(uid))
                {
                    dgAppointments.ItemsSource = null;
                    return;
                }

                // 1) Lấy appointments
                var appts = (await _appointmentSvc.GetAppointmentsByUserIdAsync(uid)).ToList();

                // 2) Lấy tất cả user, build dict Id→FullName
                var allUsers = (await _userSvc.GetAllUsersAsync()).ToList();
                var dict = allUsers.ToDictionary(
                    u => u.Id,
                    u => $"{u.FirstName} {u.LastName}");

                // 3) Map sang VM
                var vmList = appts.Select(a => new AppointmentVM
                {
                    ConsultantName = dict.TryGetValue(a.ConsultantId, out var nm) ? nm : a.ConsultantId,
                    AppointmentDate = a.AppointmentDate,
                    AppointmentStatus = a.AppointmentStatus ?? "",
                    AppointmentLocation = a.AppointmentLocation ?? ""
                }).ToList();

                dgAppointments.ItemsSource = vmList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi load lịch hẹn: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
