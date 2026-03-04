using CleanTeeth.Domain.Exceptions;
using CleanTeeth.Domain.Enums;
using CleanTeeth.Domain.ValueObjects;

namespace CleanTeeth.Domain.Entities;

public class Appointment
{
    public Guid Id { get; private set; }
    public Guid PatientId { get; private set; }
    public Guid DentistId { get; private set; }
    public Guid DentalOfficeId { get; private set; }
    public AppointmentStatus Status { get; private set; }
    //public DateTime StartDate { get; private set; }
    //public DateTime EndDate { get; private set; }
    public TimeInterval TimeInterval { get; private set; }
    // Propiedades que facilitan la navegación 
    public Patient? Patient { get; private set; }
    public Dentist? Dentist { get; private set; }
    public DentalOffice? DentalOffice { get; private set; }

    public Appointment(Guid patientId, Guid dentistId, Guid dentalOfficeId,
        TimeInterval timeInterval)
    {

        if ( timeInterval.Start < DateTime.UtcNow)
        {
            throw new BusinessRuleException($"La  fecha de inicio no puede anterior a la fecha actual");
        }

        PatientId = patientId;
        DentistId = dentistId;
        DentalOfficeId = dentalOfficeId;
        Status = AppointmentStatus.Scheduled;
        //StartDate = startDate;
        //EndDate = endDate;
        TimeInterval = timeInterval;
        Id = Guid.CreateVersion7();
    }

    public void Cancel()
    {
        if (Status != AppointmentStatus.Scheduled)
        {
            throw new BusinessRuleException($"Solo puede ser cancelar una cita programada");
        }

        Status = AppointmentStatus.Cancelled;
    }

    public void Complete()
    {
        if (Status != AppointmentStatus.Scheduled)
        {
            throw new BusinessRuleException($"Solo puede ser completada una cita programada");
        }

        Status = AppointmentStatus.Completed;
    }
}
