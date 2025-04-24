using MongoDB.Driver;
using server.Data;
using server.Models;

namespace server.Repositories;

public class UserRepository : IUserRepository
{
    private readonly MongoContext _context;

    public UserRepository(MongoContext context) => _context = context;

    public async Task<User> GetByIdAsync(string id)
    {
        return await _context.Users.Find(u => u.Id == id).FirstOrDefaultAsync();
    }

    public Task<User> GetByEmailAsync(string email) =>
        _context.Users.Find(u => u.Email == email).FirstOrDefaultAsync();

    public Task CreateAsync(User user) => _context.Users.InsertOneAsync(user);
}
