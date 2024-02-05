namespace ProjectManagementTool.Entities;

public class Task
{
    public string Name { get; set; }
    public string Description { get; set; }
    public User AssignedTo { get; set; }
}
