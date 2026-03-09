using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatusController : ControllerBase
{
    private readonly AppDbContext _context;

    // 1. Inject the Database Context
    public StatusController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetStatus()
    {
        return Ok(new { message = "Hello from the .NET Backend!" });
    }

    // 2. Add the Login Method to save to SQL Server
    [HttpPost("login")]
    public async Task<IActionResult> SaveLogin([FromBody] string name)
    {
        if (string.IsNullOrWhiteSpace(name)) 
            return BadRequest(new { message = "Name cannot be empty" });

        // This creates a new entry using the model we added to AppDbContext
        var entry = new UserLogin 
        { 
            Name = name, 
            LoginTime = DateTime.Now 
        };

        _context.UserLogins.Add(entry);
        await _context.SaveChangesAsync();

        return Ok(new { message = $"Successfully saved {name} to SQL Server at {entry.LoginTime}!" });
    }
}
