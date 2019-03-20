using AwesomeLists.Services.Task;
using System;
using System.ComponentModel.DataAnnotations;

namespace AwesomeLists.Services.TaskList
{
    public sealed class TaskListDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public TaskDto[] Tasks { get; set; } = Array.Empty<TaskDto>();
    }
}
