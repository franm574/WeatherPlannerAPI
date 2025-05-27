using WeatherPlannerAPI;
using Xunit;
using System;

namespace WeatherPlannerAPI.Tests.UnitTests
{
    public class TaskItemTests
    {
        //Test to check TaskItem model stores values correctly
        [Fact]
        public void TaskItem_CanStoreAndRetrieveProperties()
        {
            var task = new TaskItem
            {
                Title = "Test Task",
                Description = "Description here",
                ScheduledDate = new DateTime(2025, 5, 10),
                WeatherCondition = "Sunny",
                IsComplete = true
            };

            Assert.Equal("Test Task", task.Title);
            Assert.Equal("Description here", task.Description);
            Assert.Equal(new DateTime(2025, 5, 10), task.ScheduledDate);
            Assert.Equal("Sunny", task.WeatherCondition);
            Assert.True(task.IsComplete);
        }
    }
}