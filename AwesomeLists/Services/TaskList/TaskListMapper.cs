using System;
using System.Linq;
using AwesomeLists.Services.Task;

namespace AwesomeLists.Services.TaskList
{
    public sealed class TaskListMapper : ITaskListMapper
    {
        private readonly ITaskMapper _taskMapper;

        public TaskListMapper(ITaskMapper taskMapper)
        {
            _taskMapper = taskMapper ?? throw new ArgumentNullException(nameof(taskMapper));
        }

        public TaskListDto MapToDto(Data.Entities.TaskList taskList)
        {
            return new TaskListDto
            {
                Id = taskList.Id,
                Name = taskList.Name,
                UserId = taskList.UserId,
                Tasks = taskList.Tasks != null
                    ? taskList.Tasks.Select(task => _taskMapper.MapToDto(task)).ToArray()
                    : null
            };
        }

        public Data.Entities.TaskList MapToEntity(TaskListDto dto)
        {
            return new Data.Entities.TaskList
            {
                Id = dto.Id,
                Name = dto.Name,
                UserId = dto.UserId,
                Tasks = dto.Tasks != null
                    ? dto.Tasks.Select(task => _taskMapper.MapToEntity(task)).ToArray()
                    : null
            };
        }
    }
}
