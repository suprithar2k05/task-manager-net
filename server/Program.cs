using server.Data;
using server.Repositories;
using server.Security;
using server.Services;
using server.Settings;


var builder = WebApplication.CreateBuilder(args);

// Define allowed origin
// var allowedOrigin = "http://localhost:5173";

// Add services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
        policy.WithOrigins("https://taskmanager-front-alpha.vercel.app")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials());
});


builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.AddSingleton<MongoContext>();
builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<TaskService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDistributedMemoryCache();

var app = builder.Build();

app.UseSession();
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseCors("AllowFrontend");

app.UseAuthentication(); // if using auth
app.UseAuthorization();
app.Run();
