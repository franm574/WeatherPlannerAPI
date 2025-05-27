using Microsoft.EntityFrameworkCore;

namespace WeatherPlannerAPI
{
    // Handles database connection and maps TaskItem to the Tasks table
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options) { }

        public DbSet<TaskItem> Tasks => Set<TaskItem>();
    }
}