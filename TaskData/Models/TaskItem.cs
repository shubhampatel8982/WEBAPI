// TaskItem.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskData.Models
{
    public class TaskItem
    {
        [Key]
        public int TaskID { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public string AssignedBy { get; set; }

        public ICollection<SubTask> SubTasks { get; set; }
    }
}
