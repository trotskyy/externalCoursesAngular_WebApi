using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AwesomeLists.Data.Abstract;
using AwesomeLists.Data.Entities;

namespace AwesomeLists.Services.TaskList
{
    public sealed class TaskListService : ITaskListService
    {
        private readonly ITaskListRepository _taskListRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TaskListService(ITaskListRepository taskListRepository,
            IUnitOfWork unitOfWork)
        {
            _taskListRepository = taskListRepository ?? throw new ArgumentNullException(nameof(taskListRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Data.Entities.TaskList> AddAsync(Data.Entities.TaskList taskList)
        {
            if (taskList == null)
            {
                throw new ArgumentNullException(nameof(taskList));
            }

            _taskListRepository.Add(taskList);
            await _unitOfWork.SaveAsync();

            return taskList;
        }

        public async System.Threading.Tasks.Task DeleteAsync(Data.Entities.TaskList taskList)
        {
            if (taskList == null)
            {
                throw new ArgumentNullException(nameof(taskList));
            }

            _taskListRepository.Delete(taskList);

            await _unitOfWork.SaveAsync();
        }

        public async Task<Data.Entities.TaskList> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("must be more then zero", nameof(id));
            }

            return await _taskListRepository.GetByIdAsync(id);
        }

        public async Task<List<TaskListSummary>> GetTaskListsSummaryAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentException("is null or white space", nameof(userId));
            }

            return await _taskListRepository.GetTaskListsSummaryAsync(userId);
        }

        public async System.Threading.Tasks.Task UpdateNameAsync(int id, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("is null or white space", nameof(name));
            }

            if (id <= 0)
            {
                throw new ArgumentException("must be more then zero", nameof(id));
            }

            Data.Entities.TaskList taskList = await _taskListRepository.GetByIdAsync(id);
            taskList.Name = name;

            await _unitOfWork.SaveAsync();
        }
    }
}
