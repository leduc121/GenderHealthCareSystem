using GenderHealthcare.BLL.DTOs;
using GenderHealthcare.BLL.Interfaces;
using GenderHealthcare.UI.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace GenderHealthcare.UI.Views
{
    public partial class LoginWindow : Window
    {
        private readonly IUserService _userService;
        private readonly IServiceProvider _serviceProvider;

        public LoginWindow(IUserService userService, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        private async void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text?.Trim();
            string password = txtPassword.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                lblMessage.Text = "Vui lòng nhập tên đăng nhập và mật khẩu!";
                return;
            }

            try
            {
                bool isValid = await _userService.ValidateLoginAsync(username, password);
                if (!isValid)
                {
                    lblMessage.Text = "Tên đăng nhập hoặc mật khẩu không đúng!";
                    return;
                }

                // Lấy thông tin user
                var user = await _userService.GetByUsernameAsync(username);
                if (user == null)
                {
                    lblMessage.Text = "Không tìm thấy người dùng!";
                    return;
                }

                // Lấy danh sách vai trò
                var roleService = _serviceProvider.GetRequiredService<IRoleService>();
                var roles = await roleService.GetRolesByUserIdAsync(user.Id);

                // Gán UserId cho CurrentUserService
                var currentUserService = _serviceProvider.GetRequiredService<ICurrentUserService>();
                currentUserService.UserId = user.Id;

                // Điều hướng dựa trên vai trò
                if (roles.Any(r => r.Name.Equals("Admin", StringComparison.OrdinalIgnoreCase)))
                {
                    var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
                    mainWindow.Show();
                }
                else if (roles.Any(r => r.Name.Equals("Consultant", StringComparison.OrdinalIgnoreCase)))
                {
                    var consultantWindow = new Window
                    {
                        Content = _serviceProvider.GetRequiredService<ConsultantView>(),
                        Title = "Bảng điều khiển cố vấn",
                        MinHeight = 600,
                        MinWidth = 800,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen
                    };
                    consultantWindow.Show();
                }
                else
                {
                    var userWindow = new Window
                    {
                        Content = _serviceProvider.GetRequiredService<UserView>(),
                        Title = "Bảng điều khiển người dùng",
                        MinHeight = 600,
                        MinWidth = 800,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen
                    };
                    userWindow.Show();
                }

                // Đóng cửa sổ Login
                this.Close();
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Lỗi đăng nhập: {ex.Message}";
            }
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}