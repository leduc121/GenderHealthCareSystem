using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using GenderHealthcare.BLL.DTOs;
using GenderHealthcare.BLL.Interfaces;
using GenderHealthcare.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace GenderHealthcare.UI.Views
{
    public partial class UserView : UserControl
    {
        private readonly IConsultantFeedbackService _feedbackService;
        private readonly IAppointmentService _appointmentSvc;
        private readonly IUserService _userSvc;
        private readonly ICurrentUserService _userCtx;
        private readonly IRoleService _roleSvc;
        private readonly IBlogService _blogService;
        private readonly ObservableCollection<BlogDTO> _blogs = new ObservableCollection<BlogDTO>();

        public UserView()
        {
            InitializeComponent();

            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                var sp = App.AppHost.Services;
                _feedbackService = sp.GetRequiredService<IConsultantFeedbackService>();
                _appointmentSvc = sp.GetRequiredService<IAppointmentService>();
                _userSvc = sp.GetRequiredService<IUserService>();
                _userCtx = sp.GetRequiredService<ICurrentUserService>();
                _roleSvc = sp.GetRequiredService<IRoleService>();
                _blogService = sp.GetRequiredService<IBlogService>();
                Loaded += UserView_Loaded;
                BlogGrid.ItemsSource = _blogs;
            }
        }

        private async void UserView_Loaded(object sender, RoutedEventArgs e)
        {
            var userId = _userCtx.UserId;
            if (userId != null && await IsConsultantAsync(userId))
            {
                tabConsultant.Visibility = Visibility.Visible;
                await consultantView.LoadProfileAsync(userId);
                await consultantView.LoadAppointmentsAsync(userId);
            }
        }

        private async void mainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tabAppointments.IsSelected)
                appointmentsView.LoadAppointments();

            if (tabFeedback.IsSelected)
                LoadFeedbackConsultants();

            if (tabStdTests.IsSelected)
            {
                await stdServiceView.LoadServicesAsync();
            }

            if (tabNotifications.IsSelected)
            {
                _ = notificationsView.LoadNotificationsAsync();
            }

            if (tabBlogs.IsSelected)
            {
                await LoadBlogsAsync();
            }
        }

        private async Task LoadBlogsAsync()
        {
            try
            {
                var blogs = await _blogService.GetAllBlogsAsync();
                _blogs.Clear();
                if (blogs != null) foreach (var blog in blogs) _blogs.Add(blog);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải blog: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task<bool> IsConsultantAsync(string userId)
        {
            var roles = await _roleSvc.GetRolesByUserIdAsync(userId);
            return roles != null && roles.Any(r => r.Name == "Consultant");
        }

        private async void LoadFeedbackConsultants()
        {
            var userId = _userCtx.UserId;
            var appts = (await _appointmentSvc.GetAppointmentsByUserIdAsync(userId)).ToList();

            var users = (await _userSvc.GetAllUsersAsync()).ToList();
            var dict = users.ToDictionary(u => u.Id,
                                           u => $"{u.FirstName} {u.LastName}");

            var list = appts
                .Select(a => new AppointmentVM
                {
                    ConsultantId = a.ConsultantId,
                    ConsultantName = dict.TryGetValue(a.ConsultantId, out var n) ? n : a.ConsultantId
                })
                .GroupBy(vm => vm.ConsultantId)
                .Select(g => g.First())
                .ToList();

            cmbFeedbackConsultants.ItemsSource = list;
            cmbFeedbackConsultants.SelectedIndex = -1;
        }

        private async void SubmitFeedback_Click(object sender, RoutedEventArgs e)
        {
            if (cmbFeedbackConsultants.SelectedValue == null ||
                string.IsNullOrWhiteSpace(txtRating.Text) ||
                string.IsNullOrWhiteSpace(txtFeedback.Text))
            {
                MessageBox.Show("Vui lòng chọn cố vấn, điểm đánh giá và nội dung phản hồi.",
                                "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(txtRating.Text, out int rating) || rating < 1 || rating > 5)
            {
                MessageBox.Show("Điểm đánh giá phải là số từ 1 đến 5.",
                                "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var feedbackDto = new ConsultantFeedbackDTO
            {
                ConsultantId = cmbFeedbackConsultants.SelectedValue.ToString()!,
                UserId = _userCtx.UserId!,
                FeedbackDate = DateTime.Now,
                Rating = rating,
                FeedbackContent = txtFeedback.Text
            };

            try
            {
                await _feedbackService.SubmitFeedbackAsync(feedbackDto);
                MessageBox.Show("Gửi phản hồi thành công!",
                                "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                txtRating.Clear();
                txtFeedback.Clear();
                cmbFeedbackConsultants.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi gửi phản hồi: {ex.Message}",
                                "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            var r = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?",
                                     "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (r != MessageBoxResult.Yes) return;

            _userCtx.UserId = null;
            var login = App.AppHost.Services.GetRequiredService<LoginWindow>();
            login.Show();
            Application.Current.MainWindow = login;
            Window.GetWindow(this)?.Close();
        }
    }
}