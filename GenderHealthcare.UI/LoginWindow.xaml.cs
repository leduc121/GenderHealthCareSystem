using GenderHealthcare.BLL.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace GenderHealthcare.UI.Views
{
    public partial class LoginWindow : Window
    {
        private readonly IUserService _userService;
        private readonly IServiceProvider _serviceProvider;

        public LoginWindow(IUserService userService, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            if (userService == null || serviceProvider == null)
            {
                throw new ArgumentNullException("Dịch vụ hoặc ServiceProvider không được cung cấp.");
            }
            _userService = userService;
            _serviceProvider = serviceProvider;
        }

        private async void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                lblMessage.Text = "Vui lòng nhập tên đăng nhập và mật khẩu!";
                return;
            }

            try
            {
                bool isValid = await _userService.ValidateLoginAsync(username, password);
                if (isValid)
                {
                    var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    lblMessage.Text = "Tên đăng nhập hoặc mật khẩu không đúng!";
                }
            }
            catch (System.Exception ex)
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