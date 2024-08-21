// TaskDbContext.cs
using Microsoft.EntityFrameworkCore;
using TaskData.Models;

namespace TaskData.Data
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
        {
        }

        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<SubTask> SubTasks { get; set; }
    }
}
