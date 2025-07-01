using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GenderHealthcare.DAL.Entities;

public partial class GenderHealthcareContext : DbContext
{
    public GenderHealthcareContext()
    {
    }

    public GenderHealthcareContext(DbContextOptions<GenderHealthcareContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<ConsultantFeedback> ConsultantFeedbacks { get; set; }

    public virtual DbSet<ConsultantProfile> ConsultantProfiles { get; set; }

    public virtual DbSet<ContraceptiveReminder> ContraceptiveReminders { get; set; }

    public virtual DbSet<MenstrualCycle> MenstrualCycles { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Offer> Offers { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<StdDisease> StdDiseases { get; set; }

    public virtual DbSet<StdTestAppointment> StdTestAppointments { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-13VQ2HGC\\SQLEXPRESS;;uid=sa;pwd=12345;database=gender_healthcare_db;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.ConsultantId, e.AppointmentDate }).HasName("PK__appointm__0D4FC4B4D7D5A477");

            entity.ToTable("appointments");

            entity.Property(e => e.UserId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("user_id");
            entity.Property(e => e.ConsultantId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("consultant_id");
            entity.Property(e => e.AppointmentDate)
                .HasColumnType("datetime")
                .HasColumnName("appointment_date");
            entity.Property(e => e.AppointmentLocation)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("appointment_location");
            entity.Property(e => e.AppointmentStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("appointment_status");
            entity.Property(e => e.EndTime).HasColumnName("end_time");
            entity.Property(e => e.FixedPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("fixed_price");
            entity.Property(e => e.StartTime).HasColumnName("start_time");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Consultant).WithMany(p => p.AppointmentConsultants)
                .HasForeignKey(d => d.ConsultantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__appointme__consu__2EDAF651");

            entity.HasOne(d => d.User).WithMany(p => p.AppointmentUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__appointme__user___2DE6D218");
        });

        modelBuilder.Entity<ConsultantFeedback>(entity =>
        {
            entity.HasKey(e => new { e.ConsultantId, e.UserId, e.FeedbackDate }).HasName("PK__consulta__D047896D597027FE");

            entity.ToTable("consultant_feedback");

            entity.Property(e => e.ConsultantId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("consultant_id");
            entity.Property(e => e.UserId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("user_id");
            entity.Property(e => e.FeedbackDate)
                .HasColumnType("datetime")
                .HasColumnName("feedback_date");
            entity.Property(e => e.FeedbackContent)
                .HasColumnType("text")
                .HasColumnName("feedback_content");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Consultant).WithMany(p => p.ConsultantFeedbackConsultants)
                .HasForeignKey(d => d.ConsultantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__consultan__consu__4F47C5E3");

            entity.HasOne(d => d.User).WithMany(p => p.ConsultantFeedbackUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__consultan__user___503BEA1C");
        });

        modelBuilder.Entity<ConsultantProfile>(entity =>
        {
            entity.HasKey(e => e.ConsultantId).HasName("PK__consulta__680695C416C83141");

            entity.ToTable("consultant_profiles");

            entity.Property(e => e.ConsultantId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("consultant_id");
            entity.Property(e => e.ConsultationFee)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("consultation_fee");
            entity.Property(e => e.Experience)
                .HasColumnType("text")
                .HasColumnName("experience");
            entity.Property(e => e.IsAvailable)
                .HasDefaultValue(true)
                .HasColumnName("is_available");
            entity.Property(e => e.ProfileStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("profile_status");
            entity.Property(e => e.Qualification)
                .HasColumnType("text")
                .HasColumnName("qualification");
            entity.Property(e => e.Specialization)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("specialization");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Consultant).WithOne(p => p.ConsultantProfile)
                .HasForeignKey<ConsultantProfile>(d => d.ConsultantId)
                .HasConstraintName("FK__consultan__consu__25518C17");
        });

        modelBuilder.Entity<ContraceptiveReminder>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__contrace__B9BE370F543D5284");

            entity.ToTable("contraceptive_reminders");

            entity.Property(e => e.UserId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("user_id");
            entity.Property(e => e.ContraceptiveType)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("contraceptive_type");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.Frequency)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("frequency");
            entity.Property(e => e.ReminderMessage)
                .HasColumnType("text")
                .HasColumnName("reminder_message");
            entity.Property(e => e.ReminderStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("reminder_status");
            entity.Property(e => e.ReminderTime).HasColumnName("reminder_time");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.User).WithOne(p => p.ContraceptiveReminder)
                .HasForeignKey<ContraceptiveReminder>(d => d.UserId)
                .HasConstraintName("FK__contracep__user___3D2915A8");
        });

        modelBuilder.Entity<MenstrualCycle>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__menstrua__B9BE370F5708FA41");

            entity.ToTable("menstrual_cycles");

            entity.Property(e => e.UserId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("user_id");
            entity.Property(e => e.CycleEndDate).HasColumnName("cycle_end_date");
            entity.Property(e => e.CycleLength).HasColumnName("cycle_length");
            entity.Property(e => e.CycleStartDate).HasColumnName("cycle_start_date");
            entity.Property(e => e.FlowIntensity).HasColumnName("flow_intensity");
            entity.Property(e => e.Notes)
                .HasColumnType("text")
                .HasColumnName("notes");
            entity.Property(e => e.PainLevel).HasColumnName("pain_level");
            entity.Property(e => e.PeriodLength).HasColumnName("period_length");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.User).WithOne(p => p.MenstrualCycle)
                .HasForeignKey<MenstrualCycle>(d => d.UserId)
                .HasConstraintName("FK__menstrual__user___395884C4");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.ConsultantId, e.SentAt }).HasName("PK__messages__6F449A7F590E0D48");

            entity.ToTable("messages");

            entity.Property(e => e.UserId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("user_id");
            entity.Property(e => e.ConsultantId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("consultant_id");
            entity.Property(e => e.SentAt)
                .HasColumnType("datetime")
                .HasColumnName("sent_at");
            entity.Property(e => e.MessageContent)
                .HasColumnType("text")
                .HasColumnName("message_content");
            entity.Property(e => e.SenderType)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("sender_type");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Consultant).WithMany(p => p.MessageConsultants)
                .HasForeignKey(d => d.ConsultantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__messages__consul__4B7734FF");

            entity.HasOne(d => d.User).WithMany(p => p.MessageUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__messages__user_i__4A8310C6");
        });

        modelBuilder.Entity<Offer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__offers__3213E83F588958B5");

            entity.ToTable("offers");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("id");
            entity.Property(e => e.ApplicableServices)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("applicable_services");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DiscountValue)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("discount_value");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("end_date");
            entity.Property(e => e.MaxDiscount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("max_discount");
            entity.Property(e => e.MinAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("min_amount");
            entity.Property(e => e.OfferName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("offer_name");
            entity.Property(e => e.OfferType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("offer_type");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("start_date");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UsageLimit).HasColumnName("usage_limit");
            entity.Property(e => e.UsedCount)
                .HasDefaultValue(0)
                .HasColumnName("used_count");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__payments__3213E83F0A6BE848");

            entity.ToTable("payments");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("amount");
            entity.Property(e => e.AppointmentConsultantId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("appointment_consultant_id");
            entity.Property(e => e.AppointmentDate)
                .HasColumnType("datetime")
                .HasColumnName("appointment_date");
            entity.Property(e => e.AppointmentUserId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("appointment_user_id");
            entity.Property(e => e.DiscountAmount)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("discount_amount");
            entity.Property(e => e.OfferId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("offer_id");
            entity.Property(e => e.OriginalAmount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("original_amount");
            entity.Property(e => e.PaymentDate)
                .HasColumnType("datetime")
                .HasColumnName("payment_date");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("payment_method");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("payment_status");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("user_id");

            entity.HasOne(d => d.Offer).WithMany(p => p.Payments)
                .HasForeignKey(d => d.OfferId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__payments__offer___3587F3E0");

            entity.HasOne(d => d.User).WithMany(p => p.Payments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__payments__user_i__339FAB6E");

            entity.HasOne(d => d.Appointment).WithMany(p => p.Payments)
                .HasForeignKey(d => new { d.AppointmentUserId, d.AppointmentConsultantId, d.AppointmentDate })
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__payments__3493CFA7");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__roles__3213E83FD8ED7704");

            entity.ToTable("roles");

            entity.HasIndex(e => e.Name, "UQ__roles__72E12F1B2D48B245").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<StdDisease>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__std_dise__3213E83F2B361360");

            entity.ToTable("std_diseases");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("id");
            entity.Property(e => e.DiseaseName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("disease_name");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
            entity.Property(e => e.TestPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("test_price");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<StdTestAppointment>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.DiseaseId, e.TestDate }).HasName("PK__std_test__2EB955B0EB9F578E");

            entity.ToTable("std_test_appointments");

            entity.Property(e => e.UserId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("user_id");
            entity.Property(e => e.DiseaseId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("disease_id");
            entity.Property(e => e.TestDate)
                .HasColumnType("datetime")
                .HasColumnName("test_date");
            entity.Property(e => e.DiscountAmount)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("discount_amount");
            entity.Property(e => e.OfferId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("offer_id");
            entity.Property(e => e.OriginalPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("original_price");
            entity.Property(e => e.ResultDate)
                .HasColumnType("datetime")
                .HasColumnName("result_date");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
            entity.Property(e => e.TestLocation)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("test_location");
            entity.Property(e => e.TestPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("test_price");
            entity.Property(e => e.TestResult)
                .HasColumnType("text")
                .HasColumnName("test_result");
            entity.Property(e => e.TestStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("test_status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Disease).WithMany(p => p.StdTestAppointments)
                .HasForeignKey(d => d.DiseaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__std_test___disea__45BE5BA9");

            entity.HasOne(d => d.Offer).WithMany(p => p.StdTestAppointments)
                .HasForeignKey(d => d.OfferId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__std_test___offer__46B27FE2");

            entity.HasOne(d => d.User).WithMany(p => p.StdTestAppointments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__std_test___user___44CA3770");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__users__3213E83F9893FFA2");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "UQ__users__AB6E6164034B30BF").IsUnique();

            entity.HasIndex(e => e.Phone, "UQ__users__B43B145F0EC8BC8A").IsUnique();

            entity.HasIndex(e => e.Username, "UQ__users__F3DBC5727B68556E").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("id");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.Email)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("gender");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.ProfilePicture)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("profile_picture");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK__user_role__role___208CD6FA"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__user_role__user___1F98B2C1"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId").HasName("PK__user_rol__6EDEA153CDC7C635");
                        j.ToTable("user_roles");
                        j.IndexerProperty<string>("UserId")
                            .HasMaxLength(36)
                            .IsUnicode(false)
                            .IsFixedLength()
                            .HasColumnName("user_id");
                        j.IndexerProperty<string>("RoleId")
                            .HasMaxLength(36)
                            .IsUnicode(false)
                            .IsFixedLength()
                            .HasColumnName("role_id");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
