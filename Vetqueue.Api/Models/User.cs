namespace VetQueue.Api.Models;

public class User
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public string Language { get; set; } = "English";

    public bool NotificationsEnabled { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Pet> Pets { get; set; }
        = new List<Pet>();

    public ICollection<Appointment> Appointments { get; set; }
        = new List<Appointment>();
}