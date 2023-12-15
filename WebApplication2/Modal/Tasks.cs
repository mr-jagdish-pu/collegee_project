namespace testTaskManagement.Modal
{
    public class Tasks
    {
        public String taskName { get; set; }
        public String taskDescription { get; set; }

        // public String status
        // {
        //     get;
        //     set;
        // }
        public DateTime dueDate { get; set; }
        public Guid userId { get; set; }
    }
}
