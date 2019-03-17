using System;
using System.Linq;
using System.Threading.Tasks;
using AwesomeLists.Services;
using Microsoft.AspNetCore.Mvc;
using Task = AwesomeLists.Services.Task;

namespace AwesomeLists.Controllers
{
    [ApiController]
    public class TaskController : ControllerBase
    {
        [HttpGet("/taskList/{taskListId}/task")]
        public async Task<ActionResult<Task[]>> GetAllAsync(string taskListId)
        {
            try
            {
                var taskList = Data.TaskLists.First(list => list.TaskListId == taskListId);
                return Ok(taskList.Tasks);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }


        [HttpGet("/taskList/{taskListId}/task/{taskId}")]
        public async Task<ActionResult<Task>> GetByIdAsync(string taskListId, string taskId)
        {
            try
            {
                var taskList = Data.TaskLists.First(list => list.TaskListId == taskListId);
                var task = taskList.Tasks.First(t => t.TaskId == taskId);
                return Ok(task);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [HttpPost("/taskList/{taskListId}/task")]
        public async Task<ActionResult<Task>> CreateAsync(string taskListId, [FromBody]Task task)
        {
            try
            {
                var taskList = Data.TaskLists.First(list => list.TaskListId == taskListId);
                task.TaskId = new Guid().ToString();
                task.TaskListId = taskList.TaskListId;

                taskList.Tasks.Add(task);

                return Ok(task);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [HttpPut("/taskList/{taskListId}/task/{taskId}")]
        public async Task<ActionResult<TaskList>> UpdateAsync(string taskListId, string taskId, [FromBody]Task task)
        {
            try
            {
                var existingLists = Data.TaskLists.Where(list => list.TaskListId == taskListId).ToArray();
                var existingTasks = existingLists[0].Tasks.Where(t => t.TaskId == taskId).ToArray();

                if (existingTasks.Length != 1)
                {
                    return NotFound();
                }

                task.TaskId = new Guid().ToString();
                task.TaskListId = existingLists[0].TaskListId;

                existingLists[0].Tasks.Remove(existingTasks[0]);
                existingLists[0].Tasks.Add(task);

                return Ok(task);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [HttpDelete("/taskList/{taskListId}/task/{taskId}")]
        public async Task<ActionResult> DeleteAsync(string taskListId, string taskId)
        {

            try
            {
                var existingLists = Data.TaskLists.Where(list => list.TaskListId == taskListId).ToArray();
                var existingTasks = existingLists[0].Tasks.Where(t => t.TaskId == taskId).ToArray();

                if (existingTasks.Length != 1)
                {
                    return NotFound();
                }

                existingLists[0].Tasks.Remove(existingTasks[0]);

                return Ok();
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }
    }
}