using GenderHealthcare.BLL.DTOs;
using GenderHealthcare.BLL.Interfaces;
using GenderHealthcare.DAL.Entities;
using GenderHealthcare.DAL.Interfaces;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _appointmentRepository;

    public AppointmentService(IAppointmentRepository appointmentRepository)
        => _appointmentRepository = appointmentRepository;

    public async Task<IEnumerable<AppointmentDTO>> GetAllAppointmentsAsync()
        => (await _appointmentRepository.GetAllAsync()).Select(MapToDTO);

    public async Task<AppointmentDTO?> GetAppointmentByIdAsync(string userId, string consultantId, DateTime appointmentDate)
    {
        var appt = await _appointmentRepository.GetByCompositeKeyAsync(userId, consultantId, appointmentDate);
        return appt == null ? null : MapToDTO(appt);
    }

    public async Task<IEnumerable<AppointmentDTO>> GetAppointmentsByUserIdAsync(string userId)
        => (await _appointmentRepository.GetByUserIdAsync(userId)).Select(MapToDTO);

    public async Task<IEnumerable<AppointmentDTO>> GetAppointmentsByConsultantIdAsync(string consultantId)
        => (await _appointmentRepository.GetByConsultantIdAsync(consultantId)).Select(MapToDTO);

    public async Task<AppointmentDTO> CreateAppointmentAsync(AppointmentDTO dto)
    {
        var ent = MapToEntity(dto);
        var created = await _appointmentRepository.AddAsync(ent);
        return MapToDTO(created);
    }

    public async Task<AppointmentDTO> UpdateAppointmentAsync(AppointmentDTO dto)
    {
        var ent = MapToEntity(dto);
        var updated = await _appointmentRepository.UpdateAsync(ent);
        return MapToDTO(updated);
    }

    public Task<bool> DeleteAppointmentAsync(string userId, string consultantId, DateTime appointmentDate)
        => _appointmentRepository.DeleteByCompositeKeyAsync(userId, consultantId, appointmentDate);

    public async Task<IEnumerable<AppointmentDTO>> GetAppointmentsByDateRangeAsync(DateTime start, DateTime end)
        => (await _appointmentRepository.GetByDateRangeAsync(start, end)).Select(MapToDTO);

    private static AppointmentDTO MapToDTO(Appointment a) => new AppointmentDTO
    {
        UserId = a.UserId,
        ConsultantId = a.ConsultantId,
        AppointmentDate = a.AppointmentDate,
        AppointmentStatus = a.AppointmentStatus,
        AppointmentLocation = a.AppointmentLocation,
        StartTime = a.StartTime,
        EndTime = a.EndTime,
        FixedPrice = a.FixedPrice,
        Status = a.Status,
        UpdatedAt = a.UpdatedAt
    };

    private static Appointment MapToEntity(AppointmentDTO d) => new Appointment
    {
        UserId = d.UserId,
        ConsultantId = d.ConsultantId,
        AppointmentDate = d.AppointmentDate,
        AppointmentStatus = d.AppointmentStatus,
        AppointmentLocation = d.AppointmentLocation,
        StartTime = d.StartTime,
        EndTime = d.EndTime,
        FixedPrice = d.FixedPrice,
        Status = d.Status,
        UpdatedAt = d.UpdatedAt ?? DateTime.UtcNow
    };
}
