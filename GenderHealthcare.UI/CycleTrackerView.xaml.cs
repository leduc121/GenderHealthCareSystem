using GenderHealthcare.BLL.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using System.Windows.Controls;

namespace GenderHealthcare.UI.Views
{
    public partial class CycleTrackerView : UserControl
    {
        private readonly ICycleService _cycleService;

        public CycleTrackerView()
        {
            InitializeComponent();

            // Chỉ lấy service khi chương trình đang chạy, không phải ở chế độ thiết kế
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                // Lấy service từ DI container thông qua AppHost đã tạo ở App.xaml.cs
                _cycleService = App.AppHost?.Services.GetRequiredService<ICycleService>();

                if (_cycleService == null)
                {
                    // Lỗi này không nên xảy ra nếu bạn đã đăng ký service đúng
                    throw new InvalidOperationException("Không thể lấy ICycleService từ DI container.");
                }
            }
        }

        private async void LoadCycleHistory()
        {
            if (_cycleService == null) return;
            try
            {
                cycleDataGrid.ItemsSource = await _cycleService.GetMyCyclesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}");
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (_cycleService == null) return;
            if (datePickerStart.SelectedDate == null)
            {
                MessageBox.Show("Vui lòng chọn ngày bắt đầu.");
                return;
            }

            try
            {
                DateOnly cycleStartDate = DateOnly.FromDateTime(datePickerStart.SelectedDate.Value);
                DateOnly? cycleEndDate = datePickerEnd.SelectedDate.HasValue
                    ? DateOnly.FromDateTime(datePickerEnd.SelectedDate.Value)
                    : null;

                await _cycleService.RecordCycleAsync(cycleStartDate, cycleEndDate, txtNotes.Text);

                MessageBox.Show("Lưu chu kỳ thành công!");
                LoadCycleHistory();

                datePickerStart.SelectedDate = null;
                datePickerEnd.SelectedDate = null;
                txtNotes.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu: {ex.Message}");
            }
        }

        private void CycleTrackerView_OnLoaded(object sender, RoutedEventArgs e)
        {
            LoadCycleHistory();
        }
    }
}