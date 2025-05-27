namespace WeatherPlannerAPI
{
    // Represents a single gardening or farming task
    public class TaskItem
    {
        public int Id { get; set; } // Primary key
        public string? Title { get; set; } // Task name, e.g., "Water plants"
        public string? Description { get; set; } // Optional details
        public DateTime? ScheduledDate { get; set; } // When it should be done
        public string? WeatherCondition { get; set; } // e.g., "Rain", "Sunny"
        public bool IsComplete { get; set; } // Status
    }
}