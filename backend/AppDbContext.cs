using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<StatusMessage> StatusMessages { get; set; }
    
    // ADD THIS LINE:
    public DbSet<UserLogin> UserLogins { get; set; } 
}

// Define the UserLogin Model
public class UserLogin
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime LoginTime { get; set; } = DateTime.Now;
}

