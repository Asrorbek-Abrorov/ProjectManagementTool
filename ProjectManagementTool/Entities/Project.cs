namespace ProjectManagementTool.Entities;

public class Project
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Task> Tasks { get; set; }

    public Project()
    {
        Tasks = new List<Task>();
    }
}
