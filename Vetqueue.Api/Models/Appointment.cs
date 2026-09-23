namespace VetQueue.Api.Models;

public class Appointment
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string UserId { get; set; } = string.Empty;

    public string PetId { get; set; } = string.Empty;

    public string ClinicId { get; set; } = string.Empty;

    public string PaidId { get; set; } = string.Empty;

    public string Service { get; set; } = string.Empty;

    public DateTime AppointmentDate { get; set; }

    public string Status { get; set; } = "Upcoming";

    public User? User { get; set; }

    public Pet? Pet { get; set; }

    public Clinic? Clinic { get; set; }

    public Paid? Pay { get; set; }
}