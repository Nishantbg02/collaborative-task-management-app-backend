namespace MiniJiraApi.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public int ProjectId { get; set; }
        public Project? Project { get; set; }

    }
}
