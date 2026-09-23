using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VetQueue.Api.Data;
using VetQueue.Api.Models;

namespace VetQueue.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly VetQueueDbContext _context;

        public AuthController(VetQueueDbContext context)
        {
            _context = context;
        }

        // REGISTER
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.FullName) ||
                string.IsNullOrWhiteSpace(request.Email) ||
                string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new
                {
                    message = "Full name, email and password are required."
                });
            }

            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (existingUser != null)
            {
                return BadRequest(new
                {
                    message = "A user with this email already exists."
                });
            }

            // Hash the password before storing it
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                PasswordHash = passwordHash
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Registration successful.",
                userId = user.Id,
                name = user.FullName,
                email = user.Email
            });
        }

        // LOGIN
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Email) ||
                string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new
                {
                    message = "Email and password are required."
                });
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
            {
                return Unauthorized(new
                {
                    message = "Invalid email or password."
                });
            }

            // Verify the entered password against the stored hash
            var passwordValid = BCrypt.Net.BCrypt.Verify(
                request.Password,
                user.PasswordHash
            );

            if (!passwordValid)
            {
                return Unauthorized(new
                {
                    message = "Invalid email or password."
                });
            }

            return Ok(new
            {
                message = "Login successful.",
                userId = user.Id,
                name = user.FullName,
                email = user.Email
            });
        }
    }

    public class RegisterRequest
    {
        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }

    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}