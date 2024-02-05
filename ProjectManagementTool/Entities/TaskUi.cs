using Spectre.Console;

namespace ProjectManagementTool.Entities;

public class TaskUi
{
    private Project _project;

    public TaskUi(Project project)
    {
        _project = project;
    }

    public void Run()
    {
        while (true)
        {
            var menu = new SelectionPrompt<string>()
                .Title("Tasks for project " + _project.Name)
                .AddChoices(new[] { "Create Task", "Update Task", "Delete Task", "Show All Tasks", "Exit" });

            string selectedAction = AnsiConsole.Prompt(menu);

            switch (selectedAction)
            {
                case "Create Task":
                    CreateTask();
                    break;

                case "Update Task":
                    UpdateTask();
                    break;

                case "Delete Task":
                    DeleteTask();
                    break;

                case "Show All Tasks":
                    ShowAllTasks();
                    break;

                case "Exit":
                    return;
            }
        }
    }

    private void CreateTask()
    {
        var taskNameInputBox = new TextPrompt<string>("Task Name:");

        string taskName = AnsiConsole.Prompt(taskNameInputBox);

        var taskDescriptionInputBox = new TextPrompt<string>("Task Description:");

        string taskDescription = AnsiConsole.Prompt(taskDescriptionInputBox);

        Task task = new Task { Name = taskName, Description = taskDescription };

        _project.Tasks.Add(task);

        AnsiConsole.MarkupLine("[green]Task created successfully![/]");
    }

    private void UpdateTask()
    {
        var taskSelectionPrompt = new SelectionPrompt<Task>()
            .Title("Select a task to update:")
            .AddChoices(_project.Tasks);

        Task taskToUpdate = AnsiConsole.Prompt(taskSelectionPrompt);

        var newTaskNameInputBox = new TextPrompt<string>("New Task Name:");

        string newTaskName = AnsiConsole.Prompt(newTaskNameInputBox);

        var newTaskDescriptionInputBox = new TextPrompt<string>("New Task Description:");

        string newTaskDescription = AnsiConsole.Prompt(newTaskDescriptionInputBox);

        taskToUpdate.Name = newTaskName;
        taskToUpdate.Description = newTaskDescription;

        AnsiConsole.MarkupLine("[green]Task updated successfully![/]");
    }

    private void DeleteTask()
    {
        var taskSelectionPrompt = new SelectionPrompt<Task>()
            .Title("Select a task to delete:")
            .AddChoices(_project.Tasks);

        Task taskToDelete = AnsiConsole.Prompt(taskSelectionPrompt);

        _project.Tasks.Remove(taskToDelete);

        AnsiConsole.MarkupLine("[green]Task deleted successfully![/]");
    }

    private void ShowAllTasks()
    {
        var table = new Table();

        table.AddColumn(new TableColumn("Name").Centered());
        table.AddColumn(new TableColumn("Description").Centered());

        foreach (var task in _project.Tasks)
        {
            table.AddRow(task.Name, task.Description);
        }

        AnsiConsole.Render(table);
    }
}
