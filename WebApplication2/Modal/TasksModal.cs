using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace testTaskManagement.Modal;
[Table("tasks")]
[PrimaryKey(nameof(taskId))]
public class TasksModal
{
    public Guid taskId { get; set; }
    public String taskName { get; set; }
    public String taskDescription { get; set; }
    public DateTime dueDate { get; set; }
    public  Guid userId { get; set; }
}