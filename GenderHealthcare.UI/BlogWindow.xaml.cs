using GenderHealthcare.BLL.DTOs;
using GenderHealthcare.BLL.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GenderHealthcare.UI.Views
{
    public partial class BlogWindow : UserControl
    {
        private readonly IBlogService _blogService;
        private readonly ICurrentUserService _currentUserService;
        private ObservableCollection<BlogDTO> _blogs = new ObservableCollection<BlogDTO>();
        private BlogDTO _blog;

        public BlogDTO Blog
        {
            get => _blog;
            set
            {
                _blog = value;
                DataContext = this;
            }
        }

        public BlogWindow()
        {
            InitializeComponent();
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                var sp = App.AppHost.Services;
                _blogService = sp.GetRequiredService<IBlogService>();
                _currentUserService = sp.GetRequiredService<ICurrentUserService>();
                BlogGrid.ItemsSource = _blogs;
                Blog = new BlogDTO
                {
                    Id = Guid.NewGuid().ToString(),
                    PublishedDate = DateTime.Now,
                    Status = true,
                    AuthorId = _currentUserService.UserId
                };
            }
        }

        private async void BlogWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            await LoadBlogsAsync();
        }

        private async Task LoadBlogsAsync()
        {
            try
            {
                if (string.IsNullOrEmpty(_currentUserService.UserId))
                {
                    BlogGrid.ItemsSource = null;
                    return;
                }

                var blogs = await _blogService.GetAllBlogsAsync();
                _blogs.Clear();
                if (blogs != null) foreach (var blog in blogs) _blogs.Add(blog);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách blog: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Blog.Title) || string.IsNullOrWhiteSpace(Blog.Content) || Blog.PublishedDate == null)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin (Tiêu đề, Nội dung, Ngày đăng).", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrEmpty(_currentUserService.UserId))
            {
                MessageBox.Show("Phiên đăng nhập đã hết hạn, vui lòng đăng nhập lại.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                Blog.AuthorId = _currentUserService.UserId;
                Blog.UpdatedAt = DateTime.Now;

                await _blogService.CreateBlogAsync(Blog);

                MessageBox.Show("Lưu blog thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);

                await LoadBlogsAsync();
                Blog = new BlogDTO
                {
                    Id = Guid.NewGuid().ToString(),
                    PublishedDate = DateTime.Now,
                    Status = true,
                    AuthorId = _currentUserService.UserId
                };
                TitleTextBox.Text = string.Empty;
                ContentTextBox.Text = string.Empty;
                PublishedDatePicker.SelectedDate = DateTime.Now;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu blog: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ViewDetails_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is BlogDTO selectedBlog)
            {
                var detailView = new BlogDetailView { Blog = selectedBlog };
                var window = new Window
                {
                    Content = detailView,
                    Title = "Chi tiết Blog",
                    Width = 600,
                    Height = 450,
                    WindowStartupLocation = WindowStartupLocation.CenterScreen
                };
                window.ShowDialog();
            }
        }
    }
}