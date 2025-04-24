using Microsoft.Extensions.Options;
using MongoDB.Driver;
using server.Models;
using server.Settings;

namespace server.Data;

public class MongoContext
{
    private readonly IMongoDatabase _database;

    public MongoContext(IOptions<MongoDbSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        _database = client.GetDatabase(settings.Value.DatabaseName);
    }

    public IMongoCollection<User> Users => _database.GetCollection<User>("users");
    public IMongoCollection<TaskItem> Tasks => _database.GetCollection<TaskItem>("tasks");
}