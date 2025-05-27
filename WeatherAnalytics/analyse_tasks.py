import sqlite3
import pandas as pd
import matplotlib.pyplot as plt

# Connect to the tasks.db SQLite database
conn = sqlite3.connect('../WeatherPlannerAPI/tasks.db')

# Load the Tasks table into a DataFrame
df = pd.read_sql_query("SELECT * FROM Tasks", conn)

# Print basic stats
print("=== Task Overview ===")
print(f"Total tasks: {len(df)}")
print(f"Completed: {df['IsComplete'].sum()}")
print(f"Incomplete: {len(df) - df['IsComplete'].sum()}")

# Show how many tasks per weather condition
print("\n=== Tasks by Weather Condition ===")
print(df['WeatherCondition'].value_counts())

# Plot a bar chart of weather conditions
df['WeatherCondition'].value_counts().plot(kind='bar', title='Tasks by Weather Condition')
plt.xlabel("Weather")
plt.ylabel("Number of Tasks")
plt.tight_layout()
plt.show()

# Close the connection
conn.close()