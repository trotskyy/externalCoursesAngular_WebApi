using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AwesomeLists.Data.Entities;
using AwesomeLists.Services.Task;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeLists.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly ITaskMapper _mapper;

        public TaskController(ITaskService taskService, ITaskMapper mapper)
        {
            _taskService = taskService ?? throw new ArgumentNullException(nameof(taskService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("list-id")]
        public async Task<ActionResult<TaskDto[]>> GetByListIdAsync([FromQuery]int taskListId, CancellationToken token)
        {
            if (taskListId <= 0)
            {
                return BadRequest();
            }

            List<AppTask> tasks = await _taskService.GetByTaskListIdAsync(taskListId, token);

            if (tasks == null || !tasks.Any())
            {
                return Ok(Array.Empty<AppTask>());
            }

            TaskDto[] taskDtos = tasks.Select(task => _mapper.MapToDto(task)).ToArray();
            return Ok(taskDtos);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDto>> GetByIdAsync(int id, CancellationToken token)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            AppTask task = await _taskService.GetByIdAsync(id, token);

            if (task == null)
            {
                return NotFound();
            }

            TaskDto taskDto = _mapper.MapToDto(task);
            return Ok(taskDto);
        }

        [HttpPost()]
        public async Task<ActionResult<TaskDto>> CreateAsync([FromBody][Required]TaskDto taskDto, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var task = _mapper.MapToEntity(taskDto);
            var created = await _taskService.AddTaskAsync(task, token);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = created.Id }, _mapper.MapToDto(created));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync([Range(1, int.MaxValue)]int id, [FromBody]TaskDto taskDto, CancellationToken token)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AppTask task = _mapper.MapToEntity(taskDto);
            await _taskService.UpdateTaskAsync(id, task, token);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync([Range(1, int.MaxValue)]int id, CancellationToken token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _taskService.DeleteAsync(id, token);

            return NoContent();
        }
    }
}