﻿using AwesomeLists.Services.Task;
using System.ComponentModel.DataAnnotations;

namespace AwesomeLists.Services.TaskList
{
    public sealed class TaskListDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(1, int.MaxValue)]
        public string UserId { get; set; }

        public TaskDto[] Tasks { get; set; }
    }
}
