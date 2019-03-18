using System.ComponentModel.DataAnnotations;

namespace AwesomeLists.Services.TaskList
{
    public sealed class TaskListUpdateDto
    {
        [Required]
        public string Name { get; set; }
    }
}
