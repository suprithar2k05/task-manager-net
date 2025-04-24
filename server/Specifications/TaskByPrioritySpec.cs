using server.Models;

namespace server.Specifications;

public static class TaskSpecifications
{
    public static Func<TaskItem, bool> ByPriority(string priority) =>
        t => t.Priority.Equals(priority, StringComparison.OrdinalIgnoreCase);

    public static Func<TaskItem, bool> ByTag(string tag) =>
        t => t.Tags != null && t.Tags.Contains(tag, StringComparer.OrdinalIgnoreCase);
}
