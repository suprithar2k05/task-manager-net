using server.DTOs;
using server.Models;
using server.Repositories;

namespace server.Services;

public class TaskService
{
    private readonly ITaskRepository _repo;

    public TaskService(ITaskRepository repo) => _repo = repo;

    public Task<List<TaskItem>> GetTasks(string userId) => _repo.GetByUserAsync(userId);
    public Task<List<TaskItem>> GetTasksBySpec(string userId, Func<TaskItem, bool> spec) => _repo.GetBySpecAsync(userId, spec);

    public Task<TaskItem> GetById(string id) => _repo.GetByIdAsync(id);
    
    
    public Task Create(string userId, TaskDto dto) => _repo.CreateAsync(new TaskItem
    {
        Title = dto.Title,
        Priority = dto.Priority,
        Tags = dto.Tags,
        Description = dto.Description,
        DueDate = dto.DueDate,
        IsCompleted = dto.IsCompleted,
        UserId = userId,
    });

    public async Task Update(string id, TaskDto dto)
    {
        var task = await _repo.GetByIdAsync(id);
        if (task != null)
        {
            task.Title = dto.Title;
            task.Priority = dto.Priority;
            task.Description = dto.Description;
            task.Tags = dto.Tags;
            task.DueDate = dto.DueDate;
            task.IsCompleted = dto.IsCompleted;
            await _repo.UpdateAsync(task);
        }
    }

    public Task Delete(string id) => _repo.DeleteAsync(id);
}
