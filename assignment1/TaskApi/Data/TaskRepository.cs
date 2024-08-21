using System.Collections.Generic;
using System.Data.SqlClient;
using TaskApi.Models;
using Task = TaskApi.Models.Task;

namespace TaskApi.Data
{
    public class TaskRepository
    {
        private readonly string _connectionString;

        public TaskRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("TaskDbConnection");
        }

        public IEnumerable<Models.Task> GetAllTasks()
        {
            var tasks = new List<Models.Task>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Task", connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tasks.Add(new Models.Task
                        {
                            TaskID = (int)reader["TaskID"],
                            TaskName = reader["TaskName"].ToString(),
                            Description = reader["Description"].ToString(),
                            Duration = (int)reader["Duration"],
                            AssignedBy = reader["AssignedBy"].ToString()
                        });
                    }
                }
            }

            return tasks;
        }

        public Models.Task GetTaskById(int id)
        {
            Task task = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Task WHERE TaskID = @TaskID", connection);
                command.Parameters.AddWithValue("@TaskID", id);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        task = new Task
                        {
                            TaskID = (int)reader["TaskID"],
                            TaskName = reader["TaskName"].ToString(),
                            Description = reader["Description"].ToString(),
                            Duration = (int)reader["Duration"],
                            AssignedBy = reader["AssignedBy"].ToString()
                        };
                    }
                }
            }

            return task;
        }

        public void AddTask(Task task)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("INSERT INTO Task (TaskName, Description, Duration, AssignedBy) VALUES (@TaskName, @Description, @Duration, @AssignedBy)", connection);
                command.Parameters.AddWithValue("@TaskName", task.TaskName);
                command.Parameters.AddWithValue("@Description", task.Description);
                command.Parameters.AddWithValue("@Duration", task.Duration);
                command.Parameters.AddWithValue("@AssignedBy", task.AssignedBy);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdateTask(Task task)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("UPDATE Task SET TaskName = @TaskName, Description = @Description, Duration = @Duration, AssignedBy = @AssignedBy WHERE TaskID = @TaskID", connection);
                command.Parameters.AddWithValue("@TaskID", task.TaskID);
                command.Parameters.AddWithValue("@TaskName", task.TaskName);
                command.Parameters.AddWithValue("@Description", task.Description);
                command.Parameters.AddWithValue("@Duration", task.Duration);
                command.Parameters.AddWithValue("@AssignedBy", task.AssignedBy);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void DeleteTask(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("DELETE FROM Task WHERE TaskID = @TaskID", connection);
                command.Parameters.AddWithValue("@TaskID", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
