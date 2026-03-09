using backend;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Add Database Context
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// 2. Add Controllers
builder.Services.AddControllers();

builder.WebHost.UseUrls("http://0.0.0.0:5000");

var app = builder.Build();

// --- START DATABASE INITIALIZATION LOGIC ---
Console.WriteLine(">>> BACKEND STARTING: Initializing DB Check...");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();

    int maxRetries = 60; // 10 minutes total wait
    int delaySeconds = 10;
    bool success = false;

    for (int i = 1; i <= maxRetries; i++)
    {
        try
        {
            Console.WriteLine($">>> [Attempt {i}/{maxRetries}] Probing SQL Server at: {connectionString?.Split(';')[0]}...");

            // NOTE: CanConnect() may fail if the 'SwarmDb' doesn't exist yet.
            // We use EnsureCreated() directly within the try-catch to create the DB 
            // once the SQL Engine is ready to accept the 'sa' login.
            if (context.Database.EnsureCreated())
            {
                Console.WriteLine(">>> SUCCESS: Database created for the first time!");
            }
            else
            {
                Console.WriteLine(">>> SUCCESS: Database already exists and is connected!");
            }

            success = true;
            break;
        }
        catch (Exception ex)
        {
            // This captures the 'Login failed for user sa' error while the engine boots
            Console.WriteLine($">>> [Wait] SQL Server is still booting or recovering: {ex.Message.Split('.')[0]}");
        }

        if (i < maxRetries)
        {
            Thread.Sleep(TimeSpan.FromSeconds(delaySeconds));
        }
    }

    if (!success)
    {
        Console.WriteLine(">>> FATAL: Reached max retries. SQL Server never became available. Exiting...");
        Environment.Exit(1); 
    }
}
// --- END DATABASE INITIALIZATION LOGIC ---

// 3. Map the API routes
app.MapControllers();

app.Run();
