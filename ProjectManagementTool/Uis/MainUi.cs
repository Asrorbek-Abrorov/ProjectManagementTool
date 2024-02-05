using Newtonsoft.Json;
using ProjectManagementTool.Entities;
using Spectre.Console;

namespace ProjectManagementTool.Uis;

public class MainUi
{
    private List<Project> projects;

    public MainUi()
    {
        projects = ReadProjectsFromFile("../../../../ProjectManagementTool/Data/Projects.json");
    }

    public void Run()
    {
        while (true)
        {
            Console.Clear();
            var menu = new SelectionPrompt<string>()
                .Title("Select an action:")
                .AddChoices(new[] { "Create Project", "Update Project", "Delete Project", "Show All Projects", "Exit" });

            string selectedAction = AnsiConsole.Prompt(menu);

            switch (selectedAction)
            {
                case "Create Project":
                    CreateProject();
                    break;

                case "Update Project":
                    UpdateProject();
                    break;

                case "Delete Project":
                    DeleteProject();
                    break;

                case "Show All Projects":
                    ShowAllProjects();
                    break;

                case "Exit":
                    SaveProjectsToFile(projects, "../../../../ProjectManagementTool/Data/Projects.json");
                    return;
            }
            Console.WriteLine();
            Console.WriteLine("Press enter to continue...");
            Console.ReadKey();
        }
    }

    private void CreateProject()
    {
        var projectNameInputBox = new TextPrompt<string>("Project Name:");

        string projectName = AnsiConsole.Prompt(projectNameInputBox);

        var projectDescriptionInputBox = new TextPrompt<string>("Project Description:");

        string projectDescription = AnsiConsole.Prompt(projectDescriptionInputBox);

        Project project = new Project { Name = projectName, Description = projectDescription };

        AddTasksToProject(project);

        projects.Add(project);

        SaveProjectsToFile(projects, "../../../../ProjectManagementTool/Data/Projects.json");

        AnsiConsole.MarkupLine("[green]Project created successfully![/]");
    }

    private void AddTasksToProject(Project project)
    {
        while (true)
        {

            Console.WriteLine("Task Name (leave empty to finish adding tasks):");

            string taskName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(taskName))
            {
                break;
            }

            var taskDescriptionInputBox = new TextPrompt<string>("Task Description:");

            string taskDescription = AnsiConsole.Prompt(taskDescriptionInputBox);

            Entities.Task task = new Entities.Task { Name = taskName, Description = taskDescription };

            project.Tasks.Add(task);

            AnsiConsole.MarkupLine("[green]Task added successfully![/]");
        }
    }

    private void UpdateProject()
    {
        var projectSelectionPrompt = new SelectionPrompt<Project>()
            .Title("Select a project to update:")
            .AddChoices(projects);

        Project projectToUpdate = AnsiConsole.Prompt(projectSelectionPrompt);

        var newProjectNameInputBox = new TextPrompt<string>("New Project Name:");

        string newProjectName = AnsiConsole.Prompt(newProjectNameInputBox);

        var newProjectDescriptionInputBox = new TextPrompt<string>("New Project Description:");

        string newProjectDescription = AnsiConsole.Prompt(newProjectDescriptionInputBox);

        projectToUpdate.Name = newProjectName;
        projectToUpdate.Description = newProjectDescription;

        AnsiConsole.MarkupLine("[green]Project updated successfully![/]");
    }

    private void DeleteProject()
    {
        var projectSelectionPrompt = new SelectionPrompt<Project>()
            .Title("Select a project to delete:")
            .AddChoices(projects)
            .UseConverter(project => project.Name);

        Project projectToDelete = AnsiConsole.Prompt(projectSelectionPrompt);

        projects.Remove(projectToDelete);

        AnsiConsole.MarkupLine("[green]Project deleted successfully![/]");
    }

    private void ShowAllProjects()
    {
        Console.WriteLine("All Projects:\n");

        if (projects.Count == 0)
        {
            Console.WriteLine("No projects found.");
        }
        else
        {
            for (int i = 0; i < projects.Count; i++)
            {
                Project project = projects[i];
                Console.WriteLine($"Project {i + 1}:");
                Console.WriteLine($"Name: {project.Name}");
                Console.WriteLine($"Description: {project.Description}");

                if (project.Tasks.Count > 0)
                {
                    Console.WriteLine("Tasks:");
                    for (int j = 0; j < project.Tasks.Count; j++)
                    {
                        Entities.Task task = project.Tasks[j];
                        Console.WriteLine($"- Task {j + 1}:");
                        Console.WriteLine($"  Name: {task.Name}");
                        Console.WriteLine($"  Description: {task.Description}");
                    }
                }
                else
                {
                    Console.WriteLine("No tasks found.");
                }

                Console.WriteLine();
            }
        }
    }

    private List<Project> ReadProjectsFromFile(string filePath)
    {
        string jsonContent = File.ReadAllText(filePath);

        List<Project> projects = JsonConvert.DeserializeObject<List<Project>>(jsonContent);

        return projects;
    }

    private void SaveProjectsToFile(List<Project> projects, string filePath)
    {
        string jsonContent = JsonConvert.SerializeObject(projects, Formatting.Indented);

        File.WriteAllText(filePath, jsonContent);
    }

}
