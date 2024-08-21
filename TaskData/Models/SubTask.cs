// SubTask.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskData.Models
{
    public class SubTask
    {
        [Key]
        public int SubTaskId { get; set; }
        public string SubTaskName { get; set; }
        public string SubTaskDescription { get; set; }

        [ForeignKey("TaskItem")]
        public int TaskID { get; set; }
        public TaskItem Task { get; set; }
    }
}
