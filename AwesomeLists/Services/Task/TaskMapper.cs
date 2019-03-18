using AwesomeLists.Data.Entities;

namespace AwesomeLists.Services.Task
{
    public sealed class TaskMapper : ITaskMapper
    {
        public TaskDto MapToDto(AppTask task)
        {
            return new TaskDto
            {
                Id = task.Id,
                Name = task.Name,
                Priority = task.Priority,
                Status = task.Status,
                TaskListId = task.TaskListId,
                Date = task.Date,
                Description = task.Description
            };
        }

        public AppTask MapToEntity(TaskDto dto)
        {
            return new AppTask
            {
                Id = dto.Id,
                Name = dto.Name,
                Priority = dto.Priority,
                Status = dto.Status,
                TaskListId = dto.TaskListId,
                Date = dto.Date,
                Description = dto.Description
            };
        }
    }
}
