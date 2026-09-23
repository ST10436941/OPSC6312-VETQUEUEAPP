namespace VetQueue.Api.DTOs;

public class RegisterRequest
{
    public string FirstName { get; set; } = string.Empty;

    public string Surname { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;
}