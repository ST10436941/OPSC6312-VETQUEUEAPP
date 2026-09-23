namespace VetQueue.Api.Models;

public class Clinic
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string Name { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string OpeningHours { get; set; } = string.Empty;

    public ICollection<Appointment> Appointments { get; set; }
        = new List<Appointment>();

    public ICollection<Paid> PaidOptions { get; set; }
        = new List<Paid>();
}