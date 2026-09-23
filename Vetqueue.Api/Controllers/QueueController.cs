using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VetQueue.Api.Data;
using VetQueue.Api.Models;

namespace VetQueue.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QueueController : ControllerBase
{
    private readonly VetQueueDbContext _context;

    public QueueController(VetQueueDbContext context)
    {
        _context = context;
    }

    // GET: api/Queue/clinic/{clinicId}
    [HttpGet("clinic/{clinicId}")]
    public async Task<ActionResult<IEnumerable<QueueEntry>>> GetClinicQueue(
        string clinicId)
    {
        var queue = await _context.QueueEntries
            .Where(q => q.ClinicId == clinicId)
            .OrderBy(q => q.JoinedAt)
            .ToListAsync();

        return Ok(queue);
    }

    // GET: api/Queue/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<QueueEntry>> GetQueueEntry(string id)
    {
        var entry = await _context.QueueEntries.FindAsync(id);

        if (entry == null)
        {
            return NotFound();
        }

        return Ok(entry);
    }

    // POST: api/Queue
    [HttpPost]
    public async Task<ActionResult<QueueEntry>> JoinQueue(
        QueueEntry entry)
    {
        entry.Id = Guid.NewGuid().ToString();

        entry.JoinedAt = DateTime.UtcNow;

        entry.Status = "Waiting";

        _context.QueueEntries.Add(entry);

        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetQueueEntry),
            new { id = entry.Id },
            entry);
    }

    // PUT: api/Queue/{id}/status
    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateQueueStatus(
        string id,
        [FromBody] string status)
    {
        var entry = await _context.QueueEntries.FindAsync(id);

        if (entry == null)
        {
            return NotFound();
        }

        entry.Status = status;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Queue/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> LeaveQueue(string id)
    {
        var entry = await _context.QueueEntries.FindAsync(id);

        if (entry == null)
        {
            return NotFound();
        }

        _context.QueueEntries.Remove(entry);

        await _context.SaveChangesAsync();

        return NoContent();
    }
}