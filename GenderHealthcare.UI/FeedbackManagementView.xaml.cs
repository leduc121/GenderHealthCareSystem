using GenderHealthcare.BLL.DTOs;
using GenderHealthcare.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GenderHealthcare.UI.Views
{
    public partial class FeedbackManagementView : UserControl
    {
        private IConsultantFeedbackService _feedbackService;

        // Constructor không tham số cho XAML
        public FeedbackManagementView()
        {
            InitializeComponent();
        }

        // Constructor với dependency injection
        public FeedbackManagementView(IConsultantFeedbackService feedbackService) : this()
        {
            _feedbackService = feedbackService ?? throw new ArgumentNullException(nameof(feedbackService));
            Loaded += FeedbackManagementView_Loaded;
        }

        // Property để set service từ bên ngoài
        public void SetFeedbackService(IConsultantFeedbackService feedbackService)
        {
            if (_feedbackService == null)
            {
                _feedbackService = feedbackService ?? throw new ArgumentNullException(nameof(feedbackService));
                Loaded += FeedbackManagementView_Loaded;
            }
        }

        private async void FeedbackManagementView_Loaded(object sender, RoutedEventArgs e)
        {
            if (_feedbackService != null)
            {
                await LoadFeedbacks();
            }
        }

        public async Task LoadFeedbacks()
        {
            try
            {
                if (_feedbackService == null)
                {
                    MessageBox.Show("Service chưa được khởi tạo!");
                    return;
                }

                var feedbacks = await _feedbackService.GetAllFeedbacksAsync();
                FeedbackGrid.ItemsSource = feedbacks ?? new List<ConsultantFeedbackDTO>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải phản hồi: {ex.Message}");
            }
        }

        private async void BtnAddFeedback_Click(object sender, RoutedEventArgs e)
        {
            if (_feedbackService == null)
            {
                MessageBox.Show("Service chưa được khởi tạo!");
                return;
            }

            try
            {
                var feedbackDto = new ConsultantFeedbackDTO
                {
                    ConsultantId = Guid.NewGuid().ToString(),
                    UserId = Guid.NewGuid().ToString(),
                    FeedbackDate = DateTime.Now,
                    Rating = 5,
                    Status = true,
                    UpdatedAt = DateTime.Now
                };
                var createdFeedback = await _feedbackService.CreateFeedbackAsync(feedbackDto);
                if (createdFeedback != null) await LoadFeedbacks();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi thêm phản hồi: {ex.Message}");
            }
        }

        private async void BtnUpdateFeedback_Click(object sender, RoutedEventArgs e)
        {
            if (_feedbackService == null)
            {
                MessageBox.Show("Service chưa được khởi tạo!");
                return;
            }

            try
            {
                var selectedFeedback = FeedbackGrid.SelectedItem as ConsultantFeedbackDTO;
                if (selectedFeedback != null)
                {
                    selectedFeedback.UpdatedAt = DateTime.Now;
                    var updatedFeedback = await _feedbackService.UpdateFeedbackAsync(selectedFeedback);
                    if (updatedFeedback != null) await LoadFeedbacks();
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn phản hồi để cập nhật!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi cập nhật phản hồi: {ex.Message}");
            }
        }

        private async void BtnDeleteFeedback_Click(object sender, RoutedEventArgs e)
        {
            if (_feedbackService == null)
            {
                MessageBox.Show("Service chưa được khởi tạo!");
                return;
            }

            try
            {
                var selectedFeedback = FeedbackGrid.SelectedItem as ConsultantFeedbackDTO;
                if (selectedFeedback != null)
                {
                    var result = await _feedbackService.DeleteFeedbackAsync(selectedFeedback.ConsultantId, selectedFeedback.UserId, selectedFeedback.FeedbackDate);
                    if (result) await LoadFeedbacks();
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn phản hồi để xóa!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xóa phản hồi: {ex.Message}");
            }
        }
    }
}