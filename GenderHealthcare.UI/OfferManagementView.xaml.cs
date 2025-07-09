using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using GenderHealthcare.BLL.DTOs;
using GenderHealthcare.BLL.Interfaces;

namespace GenderHealthcare.UI.Views
{
    public partial class OfferManagementView : UserControl
    {
        private readonly IOfferService _offerSvc;
        private readonly IAppointmentService _appointmentSvc;
        private readonly IUserService _userSvc;

        public OfferManagementView()
        {
            InitializeComponent();
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                var sp = App.AppHost.Services;
                _offerSvc = sp.GetRequiredService<IOfferService>();
                _appointmentSvc = sp.GetRequiredService<IAppointmentService>();
                _userSvc = sp.GetRequiredService<IUserService>();
                Loaded += OfferManagementView_Loaded;
            }
        }

        private async void OfferManagementView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            var offers = await _offerSvc.GetActiveOffersAsync();
            dgOffers.ItemsSource = offers;
        }

        private async void BtnAddOffer_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var dto = new OfferDTO
            {
                Id = Guid.NewGuid().ToString(),
                OfferName = "STD Discount",
                OfferType = "Percentage",
                DiscountValue = 10m, // 10% giảm giá
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(1),
                ApplicableServices = "Chlamydia" // Ví dụ
            };
            // Xác định khách hàng tiềm năng
            var targetUsers = await GetPotentialUsersAsync();
            if (targetUsers.Count > 0)
            {
                dto.ApplicableServices += $",TargetUsers={string.Join(",", targetUsers)}";
            }
            await _offerSvc.CreateAsync(dto);
            await LoadDataAsync();
        }

        private async void BtnUpdateOffer_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var selected = dgOffers.SelectedItem as OfferDTO;
            if (selected != null)
            {
                selected.DiscountValue += 5m; // Tăng 5% giảm giá
                await _offerSvc.UpdateAsync(selected.Id, selected);
                await LoadDataAsync();
            }
        }

        private async void BtnDeleteOffer_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var selected = dgOffers.SelectedItem as OfferDTO;
            if (selected != null)
            {
                await _offerSvc.DeleteAsync(selected.Id);
                await LoadDataAsync();
            }
        }

        private async Task<List<string>> GetPotentialUsersAsync()
        {
            var appointments = await _appointmentSvc.GetAllAppointmentsAsync();
            var users = await _userSvc.GetAllUsersAsync();
            var userAppointments = appointments.GroupBy(a => a.UserId)
                                              .Select(g => new { UserId = g.Key, Count = g.Count() })
                                              .OrderByDescending(x => x.Count)
                                              .Take(3) // Lấy 3 khách hàng có lịch hẹn nhiều nhất
                                              .ToList();

            var potentialUserIds = new List<string>();
            foreach (var ua in userAppointments)
            {
                var user = users.FirstOrDefault(u => u.Id == ua.UserId);
                if (user != null && ua.Count > 2) // Điều kiện: ít nhất 3 lịch hẹn
                    potentialUserIds.Add(ua.UserId);
            }
            return potentialUserIds;
        }
    }
}