using GenderHealthcare.BLL;
using GenderHealthcare.BLL.DTOs;
using GenderHealthcare.BLL.Interfaces;
using GenderHealthcare.BLL.Services;
using GenderHealthcare.DAL;
using GenderHealthcare.DAL.Entities;
using GenderHealthcare.DAL.Interfaces;
using GenderHealthcare.DAL.Repositories;
using GenderHealthcare.DAL.Repositories.Interfaces;
using GenderHealthcare.UI.Services;
using GenderHealthcare.UI.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;

namespace GenderHealthcare.UI
{
    public partial class App : Application
    {
        public static IHost AppHost { get; private set; }

        public App()
        {
            try
            {
                AppHost = Host.CreateDefaultBuilder()
                    .ConfigureServices((context, services) =>
                    {
                        // 1) Sử dụng DbContextFactory
                        services.AddDbContextFactory<GenderHealthcareContext>(options =>
                            options.UseSqlServer(
                                "Server=DESKTOP-ONCMOC9\\SQLEXPRESS;uid=sa;pwd=12345;database=gender_healthcare_db;TrustServerCertificate=True"
                            ).EnableSensitiveDataLogging()
                        );

                        // 2) Đăng ký repository
                        services.AddTransient<IUserRepository, UserRepository>();
                        services.AddTransient<IRoleRepository, RoleRepository>();
                        services.AddTransient<IAppointmentRepository, AppointmentRepository>();
                        services.AddTransient<IConsultantProfileRepository, ConsultantProfileRepository>();
                        services.AddTransient<IMenstrualCycleRepository, MenstrualCycleRepository>();
                        services.AddTransient<IConsultantFeedbackRepository, ConsultantFeedbackRepository>();
                        services.AddTransient<IBlogRepository, BlogRepository>();
                        services.AddTransient<IStdDiseaseRepository, StdDiseaseRepository>();
                        services.AddTransient<IOfferRepository, OfferRepository>();
                        services.AddTransient<IContraceptiveReminderRepository, ContraceptiveReminderRepository>();

                        // 3) Đăng ký services
                        services.AddSingleton<ICurrentUserService, CurrentUserService>();
                        services.AddScoped<IUserService, UserService>();
                        services.AddScoped<IRoleService, RoleService>();
                        services.AddScoped<IAppointmentService, AppointmentService>();
                        services.AddScoped<IConsultantProfileService, ConsultantProfileService>();
                        services.AddScoped<IBlogService, BlogService>();
                        services.AddScoped<ICycleService, CycleService>();
                        services.AddScoped<IConsultantFeedbackService, ConsultantFeedbackService>();
                        services.AddScoped<IContraceptiveReminderService, ContraceptiveReminderService>();
                        services.AddScoped<IStdDiseaseService, StdDiseaseService>();
                        services.AddScoped<IOfferService, OfferService>();
                        services.AddSingleton<IReminderService, ReminderService>();

                        // 4) Đăng ký Views
                        services.AddTransient<MainWindow>(sp =>
                            ActivatorUtilities.CreateInstance<MainWindow>(sp,
                                sp.GetRequiredService<IUserService>(),
                                sp.GetRequiredService<IRoleService>(),
                                sp.GetRequiredService<IAppointmentService>(),
                                sp.GetRequiredService<IConsultantProfileService>(),
                                sp.GetRequiredService<ICurrentUserService>(),
                                sp
                            )
                        );
                        services.AddTransient<BlogWindow>();
                        services.AddTransient<BlogDetailView>(); // Thêm đăng ký BlogDetailView
                        services.AddTransient<LoginWindow>(sp =>
                            ActivatorUtilities.CreateInstance<LoginWindow>(sp,
                                sp.GetRequiredService<IUserService>(),
                                sp
                            )
                        );
                        services.AddTransient<UserView>();
                        services.AddTransient<CycleTrackerView>();
                        services.AddTransient<AppointmentBookingView>();
                        services.AddTransient<UserAppointmentsView>();
                        services.AddTransient<UserNotificationsView>();
                        services.AddTransient<StdDiseaseView>();
                        services.AddTransient<StdDiseaseManagementView>();
                        services.AddTransient<OfferManagementView>();
                        services.AddTransient<ConsultantView>();
                    })
                    .Build();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khởi tạo ứng dụng: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown();
            }
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                await AppHost.StartAsync();
                var dbRemSvc = AppHost.Services.GetRequiredService<IContraceptiveReminderService>();
                var remSvc = AppHost.Services.GetRequiredService<IReminderService>();

                var all = await dbRemSvc.GetAllActiveRemindersAsync();
                foreach (var r in all)
                {
                    if (r.Frequency?.Equals("Daily", StringComparison.OrdinalIgnoreCase) == true
                        && r.ReminderTime.HasValue)
                    {
                        remSvc.ScheduleDaily(
                            r.ReminderTime.Value.ToTimeSpan(),
                            r.ReminderMessage!);
                    }
                    else if (r.Frequency?.Equals("OneTime", StringComparison.OrdinalIgnoreCase) == true
                             && r.StartDate.HasValue && r.ReminderTime.HasValue)
                    {
                        var dt = r.StartDate.Value.ToDateTime(r.ReminderTime.Value);
                        remSvc.ScheduleOneTime(dt, r.ReminderMessage!);
                    }
                }
                var loginWindow = AppHost.Services.GetRequiredService<LoginWindow>();
                loginWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khởi chạy ứng dụng: {ex.InnerException?.Message ?? ex.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown();
            }
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            if (AppHost != null)
            {
                await AppHost.StopAsync();
                AppHost.Dispose();
            }
            base.OnExit(e);
        }
    }
}