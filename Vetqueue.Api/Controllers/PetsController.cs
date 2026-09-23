using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VetQueue.Api.Data;
using VetQueue.Api.Models;

namespace VetQueue.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PetsController : ControllerBase
{
    private readonly VetQueueDbContext _context;

    public PetsController(VetQueueDbContext context)
    {
        _context = context;
    }

    // GET: api/pets
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Pet>>> GetPets()
    {
        return await _context.Pets.ToListAsync();
    }

    // GET: api/pets/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Pet>> GetPet(string id)
    {
        var pet = await _context.Pets.FindAsync(id);

        if (pet == null)
        {
            return NotFound();
        }

        return pet;
    }

    // POST: api/pets
    [HttpPost]
    public async Task<ActionResult<Pet>> CreatePet(Pet pet)
    {
        pet.Id = Guid.NewGuid().ToString();

        _context.Pets.Add(pet);
        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetPet),
            new { id = pet.Id },
            pet
        );
    }

    // PUT: api/pets/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePet(string id, Pet pet)
    {
        if (id != pet.Id)
        {
            return BadRequest();
        }

        _context.Entry(pet).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PetExists(id))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }

    // DELETE: api/pets/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePet(string id)
    {
        var pet = await _context.Pets.FindAsync(id);

        if (pet == null)
        {
            return NotFound();
        }

        _context.Pets.Remove(pet);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool PetExists(string id)
    {
        return _context.Pets.Any(e => e.Id == id);
    }
}