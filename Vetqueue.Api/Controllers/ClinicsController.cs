using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VetQueue.Api.Data;
using VetQueue.Api.Models;

namespace VetQueue.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClinicsController : ControllerBase
{
    private readonly VetQueueDbContext _context;

    public ClinicsController(VetQueueDbContext context)
    {
        _context = context;
    }

    // GET: api/Clinics
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Clinic>>> GetClinics()
    {
        var clinics = await _context.Clinics.ToListAsync();

        return Ok(clinics);
    }

    // GET: api/Clinics/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Clinic>> GetClinic(string id)
    {
        var clinic = await _context.Clinics
            .FirstOrDefaultAsync(c => c.Id == id);

        if (clinic == null)
        {
            return NotFound(new
            {
                message = "Clinic not found."
            });
        }

        return Ok(clinic);
    }

    // POST: api/Clinics
    [HttpPost]
    public async Task<ActionResult<Clinic>> CreateClinic(Clinic clinic)
    {
        clinic.Id = Guid.NewGuid().ToString();

        _context.Clinics.Add(clinic);

        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetClinic),
            new { id = clinic.Id },
            clinic
        );
    }

    // PUT: api/Clinics/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateClinic(
        string id,
        Clinic clinic)
    {
        if (id != clinic.Id)
        {
            return BadRequest(new
            {
                message = "The clinic ID in the URL does not match the clinic ID in the request."
            });
        }

        var existingClinic = await _context.Clinics
            .FirstOrDefaultAsync(c => c.Id == id);

        if (existingClinic == null)
        {
            return NotFound(new
            {
                message = "Clinic not found."
            });
        }

        existingClinic.Name = clinic.Name;
        existingClinic.Address = clinic.Address;
        existingClinic.PhoneNumber = clinic.PhoneNumber;
        existingClinic.Email = clinic.Email;
        existingClinic.OpeningHours = clinic.OpeningHours;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Clinics/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClinic(string id)
    {
        var clinic = await _context.Clinics
            .FirstOrDefaultAsync(c => c.Id == id);

        if (clinic == null)
        {
            return NotFound(new
            {
                message = "Clinic not found."
            });
        }

        _context.Clinics.Remove(clinic);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}