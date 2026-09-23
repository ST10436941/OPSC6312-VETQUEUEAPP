namespace VetQueue.Api.Models;

public class Paid
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string Name { get; set; } = string.Empty;

    public ICollection<Appointment> Appointments { get; set; }
        = new List<Appointment>();
}