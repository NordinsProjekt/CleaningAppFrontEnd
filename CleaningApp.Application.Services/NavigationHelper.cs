using CleaningApp.Domain.Entities;

namespace CleaningApp.Application.Services;

public static class NavigationHelper
{
    public static string GetPath(this NavigationPaths path)
    {
        return path switch
        {
            NavigationPaths.RegisterTask => "register-task",
            NavigationPaths.TaskList => "task-list",
            NavigationPaths.WeekPlanner => "week-planner",
            NavigationPaths.TaskTemplates => "task-templates",
            NavigationPaths.RoomsCrud => "roomsCrud",
            _ => "/"
        };
    }
}