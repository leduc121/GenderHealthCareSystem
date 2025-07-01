using GenderHealthcare.BLL.Interfaces;
using GenderHealthcare.BLL.Services;
using GenderHealthcare.DAL.Entities;
using GenderHealthcare.DAL.Interfaces;
using GenderHealthcare.DAL.Repositories;
using GenderHealthcare.UI.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace GenderHealthcare.UI
{
    public partial class App : Application
    {
        private IHost _host;

        public App()
        {
            try
            {
                _host = Host.CreateDefaultBuilder()
                    .ConfigureServices((context, services) =>
                    {
                        services.AddDbContext<GenderHealthcareContext>(options =>
                            options.UseSqlServer("Server=LAPTOP-13VQHGC\\SQLEXPRESS;uid=sa;pwd=12345;database=gender_healthcare_db;TrustServerCertificate=True")
                                   .EnableSensitiveDataLogging(true));

                        services.AddScoped<IUserRepository, UserRepository>();
                        services.AddScoped<IRoleRepository, RoleRepository>();
                        services.AddScoped<IAppointmentRepository, AppointmentRepository>();
                        services.AddScoped<IConsultantProfileRepository, ConsultantProfileRepository>();
                        services.AddScoped<IUserService, UserService>();
                        services.AddScoped<IRoleService, RoleService>();
                        services.AddScoped<IAppointmentService, AppointmentService>();
                        services.AddScoped<IConsultantProfileService, ConsultantProfileService>();
                        services.AddTransient<MainWindow>(sp =>
                            ActivatorUtilities.CreateInstance<MainWindow>(sp,
                                sp.GetRequiredService<IUserService>(),
                                sp.GetRequiredService<IRoleService>(),
                                sp.GetRequiredService<IAppointmentService>(),
                                sp.GetRequiredService<IConsultantProfileService>(),
                                sp
                            ));
                        services.AddTransient<LoginWindow>(sp =>
                            ActivatorUtilities.CreateInstance<LoginWindow>(sp,
                                sp.GetRequiredService<IUserService>(),
                                sp
                            ));
                    })
                    .Build();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khởi tạo Host: {ex.Message}");
                Shutdown();
            }
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            if (_host == null)
            {
                MessageBox.Show("Không thể khởi tạo ứng dụng. Đóng ngay.");
                Shutdown();
                return;
            }

            try
            {
                await _host.StartAsync();
                var loginWindow = _host.Services.GetRequiredService<LoginWindow>();
                loginWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi mở cửa sổ đăng nhập: {ex.Message}");
                Shutdown();
            }

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            if (_host != null)
            {
                await _host.StopAsync();
                _host.Dispose();
            }
            base.OnExit(e);
        }
    }
}