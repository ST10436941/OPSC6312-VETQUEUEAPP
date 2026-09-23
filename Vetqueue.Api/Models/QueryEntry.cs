namespace VetQueue.Api.Models;

public class QueueEntry
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string UserId { get; set; } = string.Empty;

    public string PetId { get; set; } = string.Empty;

    public string ClinicId { get; set; } = string.Empty;

    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

    public string Status { get; set; } = "Waiting";

    public User? User { get; set; }

    public Pet? Pet { get; set; }

    public Clinic? Clinic { get; set; }
}