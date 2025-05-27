using Microsoft.EntityFrameworkCore;
using WeatherPlannerAPI;

namespace WeatherPlannerAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add database services
            builder.Services.AddDbContext<TaskDbContext>(options =>
                options.UseSqlite("Data Source=tasks.db"));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddHttpClient<WeatherService>();

            var app = builder.Build();

            // Seed data
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<TaskDbContext>();
                DataSeeder.SeedData(dbContext);
            }

            // Endpoints
            app.MapGet("/tasks", async (TaskDbContext db) =>
                await db.Tasks.ToListAsync());

            app.MapGet("/tasks/{id}", async (int id, TaskDbContext db) =>
                await db.Tasks.FindAsync(id) is TaskItem task
                    ? Results.Ok(task)
                    : Results.NotFound());

            app.MapPost("/tasks", async (TaskItem task, TaskDbContext db) =>
            {
                db.Tasks.Add(task);
                await db.SaveChangesAsync();
                return Results.Created($"/tasks/{task.Id}", task);
            });

            app.MapPut("/tasks/{id}", async (int id, TaskItem input, TaskDbContext db) =>
            {
                var task = await db.Tasks.FindAsync(id);
                if (task is null) return Results.NotFound();

                task.Title = input.Title;
                task.Description = input.Description;
                task.ScheduledDate = input.ScheduledDate;
                task.WeatherCondition = input.WeatherCondition;
                task.IsComplete = input.IsComplete;

                await db.SaveChangesAsync();
                return Results.NoContent();
            });

            app.MapDelete("/tasks/{id}", async (int id, TaskDbContext db) =>
            {
                var task = await db.Tasks.FindAsync(id);
                if (task is null) return Results.NotFound();

                db.Tasks.Remove(task);
                await db.SaveChangesAsync();
                return Results.NoContent();
            });

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.MapGet("/weather", async (WeatherService weatherService) =>
            {
                var weather = await weatherService.GetWeatherAsync();
                if (weather == null)
                    return Results.Problem("Failed to fetch weather data.");

                return Results.Ok(new
                {
                    Temperature = weather.Main.Temp,
                    Condition = weather.Weather.FirstOrDefault()?.Main ?? "Unknown"
                });
            });

            app.MapGet("/recommendation", async (WeatherService weatherService) =>
            {
                var weather = await weatherService.GetWeatherAsync();
                if (weather == null)
                    return Results.Problem("Unable to fetch weather data.");

                var condition = weather.Weather.FirstOrDefault()?.Main?.ToLower() ?? "";

                string recommendation = condition switch
                {
                    "clear" or "sunny" => "Good day for watering or planting seeds.",
                    "clouds" => "Ideal for pruning and general maintenance.",
                    "rain" => "Let nature water the garden. Check drainage.",
                    "snow" => "Avoid most outdoor tasks. Protect sensitive plants.",
                    "thunderstorm" => "Stay indoors and avoid working outside.",
                    _ => "No specific recommendation. Use your judgment."
                };

                return Results.Ok(new
                {
                    condition = weather.Weather.FirstOrDefault()?.Main ?? "Unknown",
                    recommendation
                });
            });

            app.Run();
        }
    }
}