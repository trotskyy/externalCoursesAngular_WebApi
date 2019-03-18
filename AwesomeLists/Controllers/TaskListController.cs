using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AwesomeLists.Data.Entities;
using AwesomeLists.Services.TaskList;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeLists.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskListController : ControllerBase
    {
        private readonly ITaskListService _taskListService;
        private readonly ITaskListMapper _mapper;

        public TaskListController(ITaskListService taskListService, ITaskListMapper mapper)
        {
            _taskListService = taskListService ?? throw new ArgumentNullException(nameof(taskListService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("summary/{userId}")]
        public async Task<ActionResult<TaskListSummary[]>> GetSummaryAsync([Required]string userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<TaskListSummary> summary = await _taskListService.GetTaskListsSummaryAsync(userId);

            if (summary == null || !summary.Any())
            {
                return NotFound();
            }

            return Ok(summary);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskListDto>> GetByIdAsync([Range(1, int.MaxValue)]int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TaskList taskList = await _taskListService.GetByIdAsync(id);

            if (taskList == null)
            {
                return NotFound();
            }

            TaskListDto dto = _mapper.MapToDto(taskList);

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<TaskList>> CreateAsync([FromBody][Required]TaskListDto taskList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TaskList entity = _mapper.MapToEntity(taskList);
            TaskList addedList =  await _taskListService.AddAsync(entity);
            TaskListDto addedListDto = _mapper.MapToDto(addedList);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = addedListDto.Id }, addedListDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync([Range(1, int.MaxValue)]int id, [FromBody] TaskListUpdateDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _taskListService.UpdateNameAsync(id, updateDto.Name);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync([Range(1, int.MaxValue)]int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            TaskList taskList = await _taskListService.GetByIdAsync(id);

            if (taskList == null)
            {
                return NotFound();
            }

            await _taskListService.DeleteAsync(taskList);

            return NoContent();
        }
    }
}