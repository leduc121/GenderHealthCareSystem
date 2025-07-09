using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using GenderHealthcare.BLL.Interfaces;

namespace GenderHealthcare.UI.Views
{
    public partial class UserNotificationsView : UserControl
    {
        private readonly IContraceptiveReminderService _reminderSvc;
        private readonly ICurrentUserService _userCtx;

        public UserNotificationsView()
        {
            InitializeComponent();
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                var sp = App.AppHost.Services;
                _reminderSvc = sp.GetRequiredService<IContraceptiveReminderService>();
                _userCtx = sp.GetRequiredService<ICurrentUserService>();
            }
        }

        /// <summary>
        /// Load danh sách reminders của user hiện tại từ BLL
        /// </summary>
        public async Task LoadNotificationsAsync()
        {
            if (string.IsNullOrEmpty(_userCtx.UserId))
            {
                dgNotifications.ItemsSource = null;
                return;
            }

            var list = await _reminderSvc
                .GetActiveRemindersByUserAsync(_userCtx.UserId);
            dgNotifications.ItemsSource = list;
        }
    }
}
