using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VetQueue.Api.Data;
using VetQueue.Api.Models;

namespace VetQueue.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly VetQueueDbContext _context;

    public AppointmentsController(VetQueueDbContext context)
    {
        _context = context;
    }

    // GET: api/Appointments
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointments()
    {
        return await _context.Appointments.ToListAsync();
    }

    // GET: api/Appointments/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Appointment>> GetAppointment(string id)
    {
        var appointment = await _context.Appointments.FindAsync(id);

        if (appointment == null)
        {
            return NotFound();
        }

        return appointment;
    }

    // POST: api/Appointments
    [HttpPost]
    public async Task<ActionResult<Appointment>> CreateAppointment(
        Appointment appointment)
    {
        appointment.Id = Guid.NewGuid().ToString();

        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetAppointment),
            new { id = appointment.Id },
            appointment
        );
    }

    // PUT: api/Appointments/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAppointment(
        string id,
        Appointment appointment)
    {
        if (id != appointment.Id)
        {
            return BadRequest();
        }

        _context.Entry(appointment).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AppointmentExists(id))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }

    // DELETE: api/Appointments/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAppointment(string id)
    {
        var appointment = await _context.Appointments.FindAsync(id);

        if (appointment == null)
        {
            return NotFound();
        }

        _context.Appointments.Remove(appointment);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool AppointmentExists(string id)
    {
        return _context.Appointments.Any(e => e.Id == id);
    }
}