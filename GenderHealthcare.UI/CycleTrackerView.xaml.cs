using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using GenderHealthcare.BLL.Interfaces;
using GenderHealthcare.BLL.DTOs;
using GenderHealthcare.UI.Services;

namespace GenderHealthcare.UI.Views
{
    public partial class CycleTrackerView : UserControl
    {
        private readonly IReminderService _reminderSvc;
        private readonly IContraceptiveReminderService _dbReminderSvc;  // ← thêm
        private readonly ICurrentUserService _userContext;
        private readonly ICycleService _cycleService;

        public CycleTrackerView()
        {
            InitializeComponent();

            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                var sp = App.AppHost.Services;
                _reminderSvc = sp.GetRequiredService<IReminderService>();
                _dbReminderSvc = sp.GetRequiredService<IContraceptiveReminderService>(); // ← thêm
                _userContext = sp.GetRequiredService<ICurrentUserService>();
                _cycleService = sp.GetRequiredService<ICycleService>();
                Loaded += CycleTrackerView_OnLoaded;
            }
        }

        private async void CycleTrackerView_OnLoaded(object sender, RoutedEventArgs e)
        {
            await LoadCycleHistoryAsync();
        }

        private async Task LoadCycleHistoryAsync()
        {
            try
            {
                if (string.IsNullOrEmpty(_userContext.UserId))
                {
                    cycleDataGrid.ItemsSource = null;
                    return;
                }

                var cycles = await _cycleService.GetMyCyclesAsync();
                cycleDataGrid.ItemsSource = cycles;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[LoadCycleHistory] {ex}");
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // 1) Validate ngày bắt đầu
            if (datePickerStart.SelectedDate == null)
            {
                MessageBox.Show("Vui lòng chọn ngày bắt đầu.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // 2) Kiểm tra session user
            if (string.IsNullOrEmpty(_userContext.UserId))
            {
                MessageBox.Show("Phiên đăng nhập đã hết hạn, vui lòng đăng nhập lại.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // 3) Chuyển DateTimePicker sang DateOnly
                var cycleStart = DateOnly.FromDateTime(datePickerStart.SelectedDate.Value);
                DateOnly? cycleEnd = datePickerEnd.SelectedDate.HasValue
                    ? DateOnly.FromDateTime(datePickerEnd.SelectedDate.Value)
                    : null;
                var notes = txtNotes.Text;

                // 4) Gọi service lưu chu kỳ
                await _cycleService.RecordCycleAsync(cycleStart, cycleEnd, notes);

                MessageBox.Show("Lưu chu kỳ thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);

                var userId = _userContext.UserId!;

                // 5a) Lưu + schedule nhắc uống thuốc (08:00 hàng ngày)
                var pillDto = new ContraceptiveReminderDTO
                {
                    UserId = userId,
                    ContraceptiveType = "Pill",
                    StartDate = cycleStart,
                    EndDate = cycleEnd,
                    ReminderTime = TimeOnly.Parse("08:00"),
                    Frequency = "Daily",
                    ReminderStatus = "Scheduled",
                    ReminderMessage = "Đến giờ uống thuốc tránh thai rồi!",
                    Status = true,
                    UpdatedAt = DateTime.Now
                };
                await _dbReminderSvc.CreateReminderAsync(pillDto);
                _reminderSvc.ScheduleDaily(pillDto.ReminderTime.Value.ToTimeSpan(), pillDto.ReminderMessage!);

                // 5b) Lưu + schedule nhắc rụng trứng (ngày +14 lúc 09:00)
                var ovulationDate = cycleStart.AddDays(14);
                var ovulDto = new ContraceptiveReminderDTO
                {
                    UserId = userId,
                    ContraceptiveType = "Ovulation",
                    StartDate = ovulationDate,
                    ReminderTime = TimeOnly.Parse("09:00"),
                    Frequency = "OneTime",
                    ReminderStatus = "Scheduled",
                    ReminderMessage = "Hôm nay bạn có khả năng rụng trứng!",
                    Status = true,
                    UpdatedAt = DateTime.Now
                };
                await _dbReminderSvc.CreateReminderAsync(ovulDto);
                _reminderSvc.ScheduleOneTime(ovulationDate.ToDateTime(ovulDto.ReminderTime.Value), ovulDto.ReminderMessage!);

                // 5c) Lưu + schedule fertile window (ngày 12–16 lúc 10:00)
                for (int offset = 12; offset <= 16; offset++)
                {
                    var fertileDay = cycleStart.AddDays(offset);
                    var fertDto = new ContraceptiveReminderDTO
                    {
                        UserId = userId,
                        ContraceptiveType = "FertileWindow",
                        StartDate = fertileDay,
                        ReminderTime = TimeOnly.Parse("10:00"),
                        Frequency = "OneTime",
                        ReminderStatus = "Scheduled",
                        ReminderMessage = "Hôm nay là ngày khả năng thụ thai cao.",
                        Status = true,
                        UpdatedAt = DateTime.Now
                    };
                    await _dbReminderSvc.CreateReminderAsync(fertDto);
                    _reminderSvc.ScheduleOneTime(fertileDay.ToDateTime(fertDto.ReminderTime.Value), fertDto.ReminderMessage!);
                }

                // 6) Refresh và clear form
                await LoadCycleHistoryAsync();
                datePickerStart.SelectedDate = null;
                datePickerEnd.SelectedDate = null;
                txtNotes.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu chu kỳ: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
