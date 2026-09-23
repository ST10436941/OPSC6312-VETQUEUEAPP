namespace VetQueue.Api.Models;

public class Pet
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string UserId { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Species { get; set; } = string.Empty;

    public string Breed { get; set; } = string.Empty;

    public DateTime? DateOfBirth { get; set; }

    public string Gender { get; set; } = string.Empty;

    public double? Weight { get; set; }

    public string Notes { get; set; } = string.Empty;

    public User? User { get; set; }

    public ICollection<Appointment> Appointments { get; set; }
        = new List<Appointment>();
}