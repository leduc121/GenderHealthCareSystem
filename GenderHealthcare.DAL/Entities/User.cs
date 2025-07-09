using System;
using System.Collections.Generic;

namespace GenderHealthcare.DAL.Entities;

public partial class User
{
    public string Id { get; set; } = null!;

    public string? Email { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? Gender { get; set; }

    public string? Phone { get; set; }

    public string? ProfilePicture { get; set; }

    public bool? Status { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Appointment> AppointmentConsultants { get; set; } = new List<Appointment>();

    public virtual ICollection<Appointment> AppointmentUsers { get; set; } = new List<Appointment>();

    public virtual ICollection<ConsultantFeedback> ConsultantFeedbackConsultants { get; set; } = new List<ConsultantFeedback>();

    public virtual ICollection<ConsultantFeedback> ConsultantFeedbackUsers { get; set; } = new List<ConsultantFeedback>();

    public virtual ConsultantProfile? ConsultantProfile { get; set; }

    //public virtual ContraceptiveReminder? ContraceptiveReminder { get; set; }

    public virtual ICollection<MenstrualCycle> MenstrualCycles { get; set; }
        = new List<MenstrualCycle>();

    public virtual ICollection<Message> MessageConsultants { get; set; } = new List<Message>();

    public virtual ICollection<Message> MessageUsers { get; set; } = new List<Message>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<StdTestAppointment> StdTestAppointments { get; set; } = new List<StdTestAppointment>();


    public ICollection<UserRole> UserRoles { get; set; }

    public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>(); // Thêm thuộc tính Blogs

    public virtual ICollection<ContraceptiveReminder> ContraceptiveReminders { get; set; }
        = new List<ContraceptiveReminder>();

}
