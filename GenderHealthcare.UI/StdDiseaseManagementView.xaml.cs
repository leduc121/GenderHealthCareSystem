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
    public partial class StdDiseaseManagementView : UserControl
    {
        private readonly IStdDiseaseService _diseaseSvc;

        public StdDiseaseManagementView()
        {
            InitializeComponent();
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                var sp = App.AppHost.Services;
                _diseaseSvc = sp.GetRequiredService<IStdDiseaseService>();
                Loaded += StdDiseaseManagementView_Loaded;
            }
        }

        private async void StdDiseaseManagementView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            await LoadDataAsync();
        }

        private async Task LoadDataAsync()
        {
            var diseases = await _diseaseSvc.GetAllAsync();
            dgDiseases.ItemsSource = diseases;
        }

        private async void BtnAddDisease_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var dto = new StdDiseaseDTO { Id = Guid.NewGuid().ToString(), DiseaseName = "New Disease", TestPrice = 100m };
            await _diseaseSvc.CreateAsync(dto);
            await LoadDataAsync();
        }

        private async void BtnUpdateDisease_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var selected = dgDiseases.SelectedItem as StdDiseaseDTO;
            if (selected != null)
            {
                selected.TestPrice += 10m;
                await _diseaseSvc.UpdateAsync(selected.Id, selected);
                await LoadDataAsync();
            }
        }

        private async void BtnDeleteDisease_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var selected = dgDiseases.SelectedItem as StdDiseaseDTO;
            if (selected != null)
            {
                await _diseaseSvc.DeleteAsync(selected.Id);
                await LoadDataAsync();
            }
        }
    }
}