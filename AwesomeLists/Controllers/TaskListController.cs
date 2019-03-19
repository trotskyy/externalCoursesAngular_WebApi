﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using AwesomeLists.Data.Entities;
using AwesomeLists.Services.TaskList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeLists.Controllers
{
    [Route("api/task-list")]
    [ApiController]
    [Authorize]
    public class TaskListController : ControllerBase
    {
        private readonly ITaskListService _taskListService;
        private readonly ITaskListMapper _mapper;

        public TaskListController(ITaskListService taskListService, ITaskListMapper mapper)
        {
            _taskListService = taskListService ?? throw new ArgumentNullException(nameof(taskListService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("summary")]
        public async Task<ActionResult<TaskListSummary[]>> GetSummaryAsync()
        {
            string userId = User.FindFirst(JwtRegisteredClaimNames.Sub).Value;

            List<TaskListSummary> summary = await _taskListService.GetTaskListsSummaryAsync(userId);

            if (summary == null || !summary.Any())
            {
                return Ok(Array.Empty<TaskListSummary>());
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
        public async Task<ActionResult<TaskListDto>> CreateAsync([FromBody][Required]TaskListUpdateDto taskListUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string userId = User.FindFirst(JwtRegisteredClaimNames.Sub).Value;

            TaskList addedList =  await _taskListService.AddAsync(new TaskList { Name = taskListUpdateDto.Name, UserId = userId });
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