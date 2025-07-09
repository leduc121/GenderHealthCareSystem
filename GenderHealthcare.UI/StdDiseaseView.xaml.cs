using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using GenderHealthcare.BLL.Interfaces;
using GenderHealthcare.BLL.DTOs;
using GenderHealthcare.UI.ViewModels;

namespace GenderHealthcare.UI.Views
{
    public partial class StdDiseaseView : UserControl
    {
        private readonly IStdDiseaseService _diseaseSvc;
        private readonly IOfferService _offerSvc;
        private readonly ICurrentUserService _userCtx;

        public StdDiseaseView()
        {
            InitializeComponent();

            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                var sp = App.AppHost.Services;
                _diseaseSvc = sp.GetRequiredService<IStdDiseaseService>();
                _offerSvc = sp.GetRequiredService<IOfferService>();
                _userCtx = sp.GetRequiredService<ICurrentUserService>();
                Loaded += StdDiseaseView_Loaded;
            }
        }

        private async void StdDiseaseView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            await LoadServicesAsync();
        }

        public async Task LoadServicesAsync()
        {
            var diseases = await _diseaseSvc.GetAllAsync();
            var offers = await _offerSvc.GetActiveOffersAsync();
            var currentUserId = _userCtx.UserId;

            var vmList = new List<StdDiseaseViewModel>();
            foreach (var d in diseases)
            {
                var vm = new StdDiseaseViewModel
                {
                    Id = d.Id,
                    DiseaseName = d.DiseaseName,
                    OriginalPrice = d.TestPrice
                };

                // Tìm offer áp dụng
                var match = offers.FirstOrDefault(o =>
                    (o.ApplicableServices ?? "")
                        .Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(s => s.Trim())
                        .Any(s => s.Equals(d.DiseaseName, StringComparison.OrdinalIgnoreCase))
                );

                if (match != null)
                {
                    vm.OfferName = match.OfferName;
                    decimal discount = 0;

                    // Kiểm tra nếu người dùng là khách hàng tiềm năng
                    var targetUsers = match.ApplicableServices?.Split(new[] { "TargetUsers=" }, StringSplitOptions.None)[1]
                        ?.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    if (targetUsers != null && targetUsers.Contains(currentUserId))
                    {
                        if (match.OfferType.Equals("Percentage", StringComparison.OrdinalIgnoreCase))
                        {
                            discount = vm.OriginalPrice * match.DiscountValue / 100m;
                            if (match.MaxDiscount.HasValue)
                                discount = Math.Min(discount, match.MaxDiscount.Value);
                        }
                        else // Fixed
                        {
                            discount = match.DiscountValue;
                        }
                    }

                    vm.DiscountValue = discount;
                    vm.FinalPrice = vm.OriginalPrice - discount;
                }
                else
                {
                    vm.FinalPrice = vm.OriginalPrice;
                }

                vmList.Add(vm);
            }

            dgServices.ItemsSource = vmList;
        }
    }
}