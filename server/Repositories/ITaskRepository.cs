using server.Models;

namespace server.Repositories;

public interface ITaskRepository
{
    Task<List<TaskItem>> GetByUserAsync(string userId);
    Task<List<TaskItem>> GetBySpecAsync(string userId, Func<TaskItem, bool> spec);
    Task<TaskItem> GetByIdAsync(string id);
    Task CreateAsync(TaskItem task);
    Task UpdateAsync(TaskItem task);
    Task DeleteAsync(string id);
}
