using AwesomeLists.Data.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace AwesomeLists.Services.Task
{
    public sealed class TaskDto
    {
        public int Id { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        public int TaskListId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public Priority Priority { get; set; }

        public Status Status { get; set; }
    }
}
