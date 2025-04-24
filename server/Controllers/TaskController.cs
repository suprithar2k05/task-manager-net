using Microsoft.AspNetCore.Mvc;
using server.DTOs;
using server.Helpers;
using server.Services;
using server.Specifications;

namespace server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly TaskService _service;

    public TaskController(TaskService service) => _service = service;

    private string UserId => HttpContext.Session.GetString("UserId");

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? priority, [FromQuery] string? tag)
    {
        if (string.IsNullOrEmpty(UserId))
            return Unauthorized(new { message = "Not logged in" });

        if (!string.IsNullOrEmpty(priority))
            return Ok(new ApiResponse("Filtered by priority", await _service.GetTasksBySpec(UserId, TaskSpecifications.ByPriority(priority))));

        if (!string.IsNullOrEmpty(tag))
            return Ok(new ApiResponse("Filtered by tag", await _service.GetTasksBySpec(UserId, TaskSpecifications.ByTag(tag))));

        return Ok(new ApiResponse("All tasks", await _service.GetTasks(UserId)));
    }

    [HttpPost]
    public async Task<IActionResult> Create(TaskDto dto)
    {
        if (UserId == null) return Unauthorized("User not logged in");
        var user = await _service.Create(UserId, dto);
        return Ok(new ApiResponse("Task created", user));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, TaskDto dto)
    {
        if (UserId == null) return Unauthorized();
        var user = await _service.Update(id, dto);
        return Ok(new ApiResponse("Task updated", user));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        if (UserId == null) return Unauthorized();
        await _service.Delete(id);
        return Ok(new ApiResponse("Task deleted"));
    }
}
