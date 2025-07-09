using GenderHealthcare.BLL.DTOs;
using GenderHealthcare.BLL.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
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
        private readonly IBlogService _blogService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IServiceProvider _serviceProvider;

        private ObservableCollection<UserDTO> _users = new ObservableCollection<UserDTO>();
        private ObservableCollection<RoleDTO> _roles = new ObservableCollection<RoleDTO>();
        private ObservableCollection<AppointmentDTO> _appointments = new ObservableCollection<AppointmentDTO>();
        private ObservableCollection<ConsultantProfileDTO> _consultantProfiles = new ObservableCollection<ConsultantProfileDTO>();
        private ObservableCollection<BlogDTO> _blogs = new ObservableCollection<BlogDTO>();
        private bool _isAdmin;

        public MainWindow(
            IUserService userService,
            IRoleService roleService,
            IAppointmentService appointmentService,
            IConsultantProfileService consultantProfileService,
            IBlogService blogService,
            ICurrentUserService currentUserService,
            IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _roleService = roleService ?? throw new ArgumentNullException(nameof(roleService));
            _appointmentService = appointmentService ?? throw new ArgumentNullException(nameof(appointmentService));
            _consultantProfileService = consultantProfileService ?? throw new ArgumentNullException(nameof(consultantProfileService));
            _blogService = blogService ?? throw new ArgumentNullException(nameof(blogService));
            _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            Loaded += MainWindow_Loaded;

            UserGrid.ItemsSource = _users;
            RoleGrid.ItemsSource = _roles;
            AppointmentGrid.ItemsSource = _appointments;
            ConsultantProfileGrid.ItemsSource = _consultantProfiles;
            BlogGrid.ItemsSource = _blogs;

            DataContext = this;
        }

        public ObservableCollection<BlogDTO> Blogs => _blogs;
        public bool IsAdmin => _isAdmin;

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            try
            {
                _isAdmin = await CheckIsAdminAsync();
                await LoadUsers();
                await LoadRoles();
                await LoadAppointments();
                await LoadConsultantProfiles();
                await LoadBlogs();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}");
            }
        }

        private async Task<bool> CheckIsAdminAsync()
        {
            try
            {
                var roles = await _currentUserService.GetUserRolesAsync();
                return roles.Any(r => r.Name == "Admin");
            }
            catch
            {
                return false;
            }
        }

        private async Task LoadUsers()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                _users.Clear();
                if (users != null) foreach (var user in users) _users.Add(user);
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
                _roles.Clear();
                if (roles != null) foreach (var role in roles) _roles.Add(role);
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
                _appointments.Clear();
                if (appointments != null) foreach (var appointment in appointments) _appointments.Add(appointment);
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
                _consultantProfiles.Clear();
                if (profiles != null) foreach (var profile in profiles) _consultantProfiles.Add(profile);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải hồ sơ cố vấn: {ex.Message}");
            }
        }

        private async Task LoadBlogs()
        {
            try
            {
                var blogs = await _blogService.GetAllBlogsAsync();
                _blogs.Clear();
                if (blogs != null) foreach (var blog in blogs) _blogs.Add(blog);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải blog: {ex.Message}");
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
            var selectedUser = UserGrid.SelectedItem as UserDTO;
            if (selectedUser == null)
            {
                MessageBox.Show("Vui lòng chọn một người dùng để cập nhật!");
                return;
            }

            try
            {
                var updatedUser = await _userService.UpdateUserAsync(selectedUser);
                if (updatedUser != null)
                {
                    var index = _users.IndexOf(selectedUser);
                    if (index >= 0)
                    {
                        _users[index] = updatedUser;
                        await LoadUsers();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy người dùng để cập nhật!");
                    }
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
            var selectedRole = RoleGrid.SelectedItem as RoleDTO;
            if (selectedRole == null)
            {
                MessageBox.Show("Vui lòng chọn một vai trò để cập nhật!");
                return;
            }

            try
            {
                var updatedRole = await _roleService.UpdateRoleAsync(selectedRole);
                if (updatedRole != null)
                {
                    var index = _roles.IndexOf(selectedRole);
                    if (index >= 0)
                    {
                        _roles[index] = updatedRole;
                        await LoadRoles();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy vai trò để cập nhật!");
                    }
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
            var selectedAppointment = AppointmentGrid.SelectedItem as AppointmentDTO;
            if (selectedAppointment == null)
            {
                MessageBox.Show("Vui lòng chọn một lịch hẹn để cập nhật!");
                return;
            }

            try
            {
                var updatedAppointment = await _appointmentService.UpdateAppointmentAsync(selectedAppointment);
                if (updatedAppointment != null)
                {
                    var index = _appointments.IndexOf(selectedAppointment);
                    if (index >= 0)
                    {
                        _appointments[index] = updatedAppointment;
                        await LoadAppointments();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy lịch hẹn để cập nhật!");
                    }
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
            var selectedProfile = ConsultantProfileGrid.SelectedItem as ConsultantProfileDTO;
            if (selectedProfile == null)
            {
                MessageBox.Show("Vui lòng chọn một hồ sơ cố vấn để cập nhật!");
                return;
            }

            try
            {
                var updatedProfile = await _consultantProfileService.UpdateConsultantProfileAsync(selectedProfile);
                if (updatedProfile != null)
                {
                    var index = _consultantProfiles.IndexOf(selectedProfile);
                    if (index >= 0)
                    {
                        _consultantProfiles[index] = updatedProfile;
                        await LoadConsultantProfiles();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy hồ sơ cố vấn để cập nhật!");
                    }
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

        private async void BtnAddBlog_Click(object sender, RoutedEventArgs e)
        {
            if (!_isAdmin)
            {
                MessageBox.Show("Chỉ Admin mới có thể thêm blog!");
                return;
            }

            try
            {
                var blogWindow = _serviceProvider.GetRequiredService<BlogWindow>();
                blogWindow.Blog = new BlogDTO
                {
                    Id = Guid.NewGuid().ToString(),
                    PublishedDate = DateTime.Now,
                    Status = true,
                    AuthorId = _currentUserService.UserId // Giả định Admin là tác giả
                };
                if (blogWindow.ShowDialog() == true)
                {
                    await _blogService.CreateBlogAsync(blogWindow.Blog);
                    await LoadBlogs();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi thêm blog: {ex.Message}");
            }
        }

        private async void BtnUpdateBlog_Click(object sender, RoutedEventArgs e)
        {
            if (!_isAdmin)
            {
                MessageBox.Show("Chỉ Admin mới có thể cập nhật blog!");
                return;
            }

            var selectedBlog = BlogGrid.SelectedItem as BlogDTO;
            if (selectedBlog == null)
            {
                MessageBox.Show("Vui lòng chọn một blog để cập nhật!");
                return;
            }

            try
            {
                var blogWindow = _serviceProvider.GetRequiredService<BlogWindow>();
                blogWindow.Blog = selectedBlog;
                if (blogWindow.ShowDialog() == true)
                {
                    await _blogService.UpdateBlogAsync(selectedBlog.Id, blogWindow.Blog);
                    await LoadBlogs();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi cập nhật blog: {ex.Message}");
            }
        }

        private async void BtnDeleteBlog_Click(object sender, RoutedEventArgs e)
        {
            if (!_isAdmin)
            {
                MessageBox.Show("Chỉ Admin mới có thể xóa blog!");
                return;
            }

            try
            {
                var selectedBlog = BlogGrid.SelectedItem as BlogDTO;
                if (selectedBlog != null)
                {
                    var result = await _blogService.DeleteBlogAsync(selectedBlog.Id);
                    if (result) await LoadBlogs();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xóa blog: {ex.Message}");
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