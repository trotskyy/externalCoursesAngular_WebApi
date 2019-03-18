namespace AwesomeLists.Services.TaskList
{
    public interface ITaskListMapper
    {
        TaskListDto MapToDto(Data.Entities.TaskList taskList);

        Data.Entities.TaskList MapToEntity(TaskListDto dto);
    }
}
