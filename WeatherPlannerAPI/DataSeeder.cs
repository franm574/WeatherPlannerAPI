using Microsoft.EntityFrameworkCore;

namespace WeatherPlannerAPI
{
    public static class DataSeeder
    {
        // Adds example tasks to the database if it's empty
        public static void SeedData(TaskDbContext context)
        {
            // Check if there are any tasks already in the database
            if (!context.Tasks.Any())
            {
                context.Tasks.AddRange(
                       new TaskItem
                       {
                           Title = "Water the plants",
                           Description = "Water the plants in the garden.",
                           ScheduledDate = new DateTime(2025, 4, 10, 18, 0, 0), // Example date
                           WeatherCondition = "Sunny",
                           IsComplete = false
                       },
                    new TaskItem
                    {
                        Title = "Prune the roses",
                        Description = "Trim the rose bushes for healthy growth.",
                        ScheduledDate = new DateTime(2025, 4, 12, 9, 0, 0),
                        WeatherCondition = "Cloudy",
                        IsComplete = false
                    },
                    new TaskItem
                    {
                        Title = "Plant new seeds",
                        Description = "Add new seeds to the vegetable bed.",
                        ScheduledDate = new DateTime(2025, 4, 25),
                        WeatherCondition = "Sunny",
                        IsComplete = false
                    }
                );
                context.SaveChanges();
            }
        }
    }
}