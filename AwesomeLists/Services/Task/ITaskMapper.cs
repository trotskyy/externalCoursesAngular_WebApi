using AwesomeLists.Data.Entities;

namespace AwesomeLists.Services.Task
{
    public interface ITaskMapper
    {
        TaskDto MapToDto(AppTask task);

        AppTask MapToEntity(TaskDto dto);
    }
}
