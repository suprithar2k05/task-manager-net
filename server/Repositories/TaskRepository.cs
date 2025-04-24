using MongoDB.Driver;
using server.Data;
using server.Models;

namespace server.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly MongoContext _context;

    public TaskRepository(MongoContext context) => _context = context;

    public Task<List<TaskItem>> GetByUserAsync(string userId) =>
        _context.Tasks.Find(t => t.UserId == userId).ToListAsync();

    public Task<List<TaskItem>> GetBySpecAsync(string userId, Func<TaskItem, bool> spec) =>
        Task.FromResult(_context.Tasks.AsQueryable().Where(t => t.UserId == userId).Where(spec).ToList());

    public Task<TaskItem> GetByIdAsync(string id) =>
        _context.Tasks.Find(t => t.Id == id).FirstOrDefaultAsync();

    public Task CreateAsync(TaskItem task) => _context.Tasks.InsertOneAsync(task);

    public Task UpdateAsync(TaskItem task) =>
        _context.Tasks.ReplaceOneAsync(t => t.Id == task.Id, task);

    public Task DeleteAsync(string id) =>
        _context.Tasks.DeleteOneAsync(t => t.Id == id);
}
