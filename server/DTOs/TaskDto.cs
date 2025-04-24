using System.ComponentModel.DataAnnotations;

using server.Models;
namespace server.DTOs;

public class TaskDto
{
    [Required]
    public string Title { get; set; }

    public string? Description { get; set; }

    [Required]
    [EnumDataType(typeof(Priority), ErrorMessage = "Priority must be High, Medium, or Low.")]
    public string Priority { get; set; }

    public string[]? Tags { get; set; }

    public bool? IsCompleted {get; set; } = false;

    public DateTime DueDate { get; set; }
}
