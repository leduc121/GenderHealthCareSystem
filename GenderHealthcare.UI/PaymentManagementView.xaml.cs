using GenderHealthcare.BLL.DTOs;
using GenderHealthcare.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GenderHealthcare.UI.Views
{
    public partial class PaymentManagementView : UserControl
    {
        private IPaymentService _paymentService; // Bỏ readonly

        // Constructor không tham số cho XAML
        public PaymentManagementView()
        {
            InitializeComponent();
        }

        // Constructor với dependency injection
        public PaymentManagementView(IPaymentService paymentService) : this()
        {
            _paymentService = paymentService ?? throw new ArgumentNullException(nameof(paymentService));
            Loaded += PaymentManagementView_Loaded;
        }

        // Method để set service từ bên ngoài
        public void SetPaymentService(IPaymentService paymentService)
        {
            if (_paymentService == null)
            {
                _paymentService = paymentService ?? throw new ArgumentNullException(nameof(paymentService));
                Loaded += PaymentManagementView_Loaded;
            }
        }

        private async void PaymentManagementView_Loaded(object sender, RoutedEventArgs e)
        {
            if (_paymentService != null)
            {
                await LoadPayments();
            }
        }

        public async Task LoadPayments()
        {
            try
            {
                if (_paymentService == null)
                {
                    MessageBox.Show("Service chưa được khởi tạo!");
                    return;
                }

                var payments = await _paymentService.GetAllPaymentsAsync();
                PaymentGrid.ItemsSource = payments ?? new List<PaymentDTO>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải thanh toán: {ex.Message}");
            }
        }

        private async void BtnAddPayment_Click(object sender, RoutedEventArgs e)
        {
            if (_paymentService == null)
            {
                MessageBox.Show("Service chưa được khởi tạo!");
                return;
            }

            try
            {
                var paymentDto = new PaymentDTO
                {
                    Id = Guid.NewGuid().ToString(),
                    Status = true,
                    UpdatedAt = DateTime.Now
                };
                var createdPayment = await _paymentService.CreatePaymentAsync(paymentDto);
                if (createdPayment != null) await LoadPayments();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi thêm thanh toán: {ex.Message}");
            }
        }

        private async void BtnUpdatePayment_Click(object sender, RoutedEventArgs e)
        {
            if (_paymentService == null)
            {
                MessageBox.Show("Service chưa được khởi tạo!");
                return;
            }

            try
            {
                var selectedPayment = PaymentGrid.SelectedItem as PaymentDTO;
                if (selectedPayment != null)
                {
                    selectedPayment.UpdatedAt = DateTime.Now;
                    var updatedPayment = await _paymentService.UpdatePaymentAsync(selectedPayment);
                    if (updatedPayment != null) await LoadPayments();
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn thanh toán để cập nhật!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi cập nhật thanh toán: {ex.Message}");
            }
        }

        private async void BtnDeletePayment_Click(object sender, RoutedEventArgs e)
        {
            if (_paymentService == null)
            {
                MessageBox.Show("Service chưa được khởi tạo!");
                return;
            }

            try
            {
                var selectedPayment = PaymentGrid.SelectedItem as PaymentDTO;
                if (selectedPayment != null)
                {
                    var result = await _paymentService.DeletePaymentAsync(selectedPayment.Id);
                    if (result) await LoadPayments();
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn thanh toán để xóa!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xóa thanh toán: {ex.Message}");
            }
        }
    }
}