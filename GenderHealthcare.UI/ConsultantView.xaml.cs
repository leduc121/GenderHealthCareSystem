using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using GenderHealthcare.BLL.DTOs;
using GenderHealthcare.BLL.Interfaces;
using GenderHealthcare.UI.ViewModels;

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
            if (userId == null || !await IsConsultantAsync(userId))
            {
                this.Visibility = System.Windows.Visibility.Collapsed;
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
                txtSpecialization.Text = profile.Specialization ?? "";
                txtQualification.Text = profile.Qualification ?? "";
                txtExperience.Text = profile.Experience ?? "";
                txtConsultationFee.Text = profile.ConsultationFee?.ToString("F2") ?? "0.00";
                txtProfileStatus.Text = profile.ProfileStatus ?? "";
                chkIsAvailable.IsChecked = profile.IsAvailable;
            }
        }

        public async Task LoadAppointmentsAsync(string consultantId)
        {
            var appointments = await _appointmentSvc.GetAppointmentsByConsultantIdAsync(consultantId);
            dgAppointments.ItemsSource = appointments;
        }

        private async void BtnUpdateProfile_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var consultantId = _userCtx.UserId;
            if (consultantId != null)
            {
                var profile = new ConsultantProfileDTO
                {
                    ConsultantId = consultantId,
                    Specialization = txtSpecialization.Text,
                    Qualification = txtQualification.Text,
                    Experience = txtExperience.Text,
                    ConsultationFee = decimal.TryParse(txtConsultationFee.Text, out var fee) ? fee : 0,
                    ProfileStatus = txtProfileStatus.Text,
                    IsAvailable = chkIsAvailable.IsChecked ?? false,
                    Status = true,
                    UpdatedAt = DateTime.Now
                };

                await _profileSvc.UpdateConsultantProfileAsync(profile);
                await LoadProfileAsync(consultantId);
            }
        }
    }
}