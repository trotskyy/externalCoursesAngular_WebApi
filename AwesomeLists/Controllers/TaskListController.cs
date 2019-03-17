﻿using System;
using System.Linq;
using System.Threading.Tasks;
using AwesomeLists.Services;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeLists.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TaskListController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<TaskList[]>> GetAllAsync()
        {
            var taskLists = Data.TaskLists;
            return Ok(taskLists);
        }

        [HttpGet("summary")]
        public async Task<ActionResult<TaskListSummary[]>> GetSummaryAsync()
        {
            var taskLists = Data.TaskLists.Select(list =>
            {
                return new TaskListSummary()
                {
                    TaskListId = list.TaskListId,
                    Name = list.Name,
                    Total = list.Tasks.Count,
                    ToDoTotal = list.Tasks.Where(t => t.Status == Status.ToDo).ToArray().Length,
                    InProgressTotal = list.Tasks.Where(t => t.Status == Status.InProgress).ToArray().Length,
                    DoneTotal = list.Tasks.Where(t => t.Status == Status.Done).ToArray().Length,
                };
            });
            return Ok(taskLists);
        }

        [HttpGet("{taskListId}")]
        public async Task<ActionResult<TaskList>> GetByIdAsync(string taskListId)
        {
            try
            {
                var taskList = Data.TaskLists.First(list => list.TaskListId == taskListId);
                return Ok(taskList);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult<TaskList>> CreateAsync([FromBody]TaskList taskList)
        {
            taskList.TaskListId = new Guid().ToString();
            Data.TaskLists.Add(taskList);
            return Ok(taskList);
        }

        [HttpPut("{taskListId}")]
        public async Task<ActionResult<TaskList>> UpdateAsync(string taskListId, [FromBody] TaskList taskList)
        {
            var existingLists = Data.TaskLists.Where(list => list.TaskListId == taskListId).ToArray();
            if (existingLists.Length != 1)
            {
                return NotFound();
            }

            taskList.TaskListId = taskListId;
            Data.TaskLists.Remove(existingLists[0]);
            Data.TaskLists.Add(taskList);
            
            return Ok(taskList);
        }

        [HttpDelete("{taskListId}")]
        public async Task<ActionResult> DeleteAsync(string taskListId)
        {
            var existingLists = Data.TaskLists.Where(list => list.TaskListId == taskListId).ToArray();
            if (existingLists.Length != 1)
            {
                return NotFound();
            }

            Data.TaskLists.Remove(existingLists[0]);

            return Ok();
        }
    }
}