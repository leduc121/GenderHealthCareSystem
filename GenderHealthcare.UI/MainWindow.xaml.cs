using GenderHealthcare.BLL.DTOs;
using GenderHealthcare.BLL.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GenderHealthcare.UI.Views
{
    public partial class MainWindow : Window
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IAppointmentService _appointmentService;
        private readonly IConsultantProfileService _consultantProfileService;
        private readonly IServiceProvider _serviceProvider;

        public MainWindow(IUserService userService, IRoleService roleService, IAppointmentService appointmentService, IConsultantProfileService consultantProfileService, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _roleService = roleService ?? throw new ArgumentNullException(nameof(roleService));
            _appointmentService = appointmentService ?? throw new ArgumentNullException(nameof(appointmentService));
            _consultantProfileService = consultantProfileService ?? throw new ArgumentNullException(nameof(consultantProfileService));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            try
            {
                await LoadUsers();
                await LoadRoles();
                await LoadAppointments();
                await LoadConsultantProfiles();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}");
            }
        }

        private async Task LoadUsers()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                UserGrid.ItemsSource = users ?? new List<UserDTO>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải người dùng: {ex.Message}");
            }
        }

        private async Task LoadRoles()
        {
            try
            {
                var roles = await _roleService.GetAllRolesAsync();
                RoleGrid.ItemsSource = roles ?? new List<RoleDTO>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải vai trò: {ex.Message}");
            }
        }

        private async Task LoadAppointments()
        {
            try
            {
                var appointments = await _appointmentService.GetAllAppointmentsAsync();
                AppointmentGrid.ItemsSource = appointments ?? new List<AppointmentDTO>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải lịch hẹn: {ex.Message}");
            }
        }

        private async Task LoadConsultantProfiles()
        {
            try
            {
                var profiles = await _consultantProfileService.GetAllConsultantProfilesAsync();
                ConsultantProfileGrid.ItemsSource = profiles ?? new List<ConsultantProfileDTO>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải hồ sơ cố vấn: {ex.Message}");
            }
        }

        private async void BtnAddUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var userDto = new UserDTO { Id = Guid.NewGuid().ToString() };
                var createdUser = await _userService.CreateUserAsync(userDto);
                if (createdUser != null) await LoadUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi thêm người dùng: {ex.Message}");
            }
        }

        private async void BtnUpdateUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedUser = UserGrid.SelectedItem as UserDTO;
                if (selectedUser != null)
                {
                    var updatedUser = await _userService.UpdateUserAsync(selectedUser);
                    if (updatedUser != null) await LoadUsers();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi cập nhật người dùng: {ex.Message}");
            }
        }

        private async void BtnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedUser = UserGrid.SelectedItem as UserDTO;
                if (selectedUser != null)
                {
                    var result = await _userService.DeleteUserAsync(selectedUser.Id);
                    if (result) await LoadUsers();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xóa người dùng: {ex.Message}");
            }
        }

        private async void BtnAddRole_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var roleDto = new RoleDTO { Id = Guid.NewGuid().ToString() };
                var createdRole = await _roleService.CreateRoleAsync(roleDto);
                if (createdRole != null) await LoadRoles();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi thêm vai trò: {ex.Message}");
            }
        }

        private async void BtnUpdateRole_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedRole = RoleGrid.SelectedItem as RoleDTO;
                if (selectedRole != null)
                {
                    var updatedRole = await _roleService.UpdateRoleAsync(selectedRole);
                    if (updatedRole != null) await LoadRoles();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi cập nhật vai trò: {ex.Message}");
            }
        }

        private async void BtnDeleteRole_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedRole = RoleGrid.SelectedItem as RoleDTO;
                if (selectedRole != null)
                {
                    var result = await _roleService.DeleteRoleAsync(selectedRole.Id);
                    if (result) await LoadRoles();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xóa vai trò: {ex.Message}");
            }
        }

        private async void BtnAddAppointment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var appointmentDto = new AppointmentDTO
                {
                    UserId = Guid.NewGuid().ToString(),
                    ConsultantId = Guid.NewGuid().ToString(),
                    AppointmentDate = DateTime.Now
                };
                var createdAppointment = await _appointmentService.CreateAppointmentAsync(appointmentDto);
                if (createdAppointment != null) await LoadAppointments();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi thêm lịch hẹn: {ex.Message}");
            }
        }

        private async void BtnUpdateAppointment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedAppointment = AppointmentGrid.SelectedItem as AppointmentDTO;
                if (selectedAppointment != null)
                {
                    var updatedAppointment = await _appointmentService.UpdateAppointmentAsync(selectedAppointment);
                    if (updatedAppointment != null) await LoadAppointments();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi cập nhật lịch hẹn: {ex.Message}");
            }
        }

        private async void BtnDeleteAppointment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedAppointment = AppointmentGrid.SelectedItem as AppointmentDTO;
                if (selectedAppointment != null)
                {
                    var result = await _appointmentService.DeleteAppointmentAsync(selectedAppointment.UserId, selectedAppointment.ConsultantId, selectedAppointment.AppointmentDate);
                    if (result) await LoadAppointments();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xóa lịch hẹn: {ex.Message}");
            }
        }

        private async void BtnAddConsultantProfile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var profileDto = new ConsultantProfileDTO { ConsultantId = Guid.NewGuid().ToString() };
                var createdProfile = await _consultantProfileService.CreateConsultantProfileAsync(profileDto);
                if (createdProfile != null) await LoadConsultantProfiles();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi thêm hồ sơ cố vấn: {ex.Message}");
            }
        }

        private async void BtnUpdateConsultantProfile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedProfile = ConsultantProfileGrid.SelectedItem as ConsultantProfileDTO;
                if (selectedProfile != null)
                {
                    var updatedProfile = await _consultantProfileService.UpdateConsultantProfileAsync(selectedProfile);
                    if (updatedProfile != null) await LoadConsultantProfiles();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi cập nhật hồ sơ cố vấn: {ex.Message}");
            }
        }

        private async void BtnDeleteConsultantProfile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedProfile = ConsultantProfileGrid.SelectedItem as ConsultantProfileDTO;
                if (selectedProfile != null)
                {
                    var result = await _consultantProfileService.DeleteConsultantProfileAsync(selectedProfile.ConsultantId);
                    if (result) await LoadConsultantProfiles();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xóa hồ sơ cố vấn: {ex.Message}");
            }
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = _serviceProvider.GetRequiredService<LoginWindow>();
            loginWindow.Show();
            this.Close();
        }
    }
}