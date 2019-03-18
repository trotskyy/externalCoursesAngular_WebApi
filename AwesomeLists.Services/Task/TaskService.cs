using System.Collections.Generic;
using System.Threading.Tasks;
using AwesomeLists.Data.Abstract;
using AwesomeLists.Data.Entities;
using System;

namespace AwesomeLists.Services.Task
{
    public sealed class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TaskService(ITaskRepository taskRepository,
            IUnitOfWork unitOfWork)
        {
            _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<AppTask> AddTaskAsync(AppTask task)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            _taskRepository.AddTask(task);
            await _unitOfWork.SaveAsync();
            return task;
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Id must be >= 0", nameof(id));
            }

            AppTask task = await _taskRepository.FindByIdAsync(id);

            if (task == null)
            {
                throw new ArgumentException("not fount", nameof(id));
            }

            _taskRepository.Delete(task);
            await _unitOfWork.SaveAsync();
        }

        public async Task<AppTask> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Id must be more then zero", nameof(id));
            }

            return await _taskRepository.FindByIdAsync(id);
        }

        public async Task<List<AppTask>> GetByTaskListIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Id must be more then zero", nameof(id));
            }

            return await _taskRepository.GetAllTasksByTaskListIdAsync(id);
        }

        public async System.Threading.Tasks.Task UpdateTaskAsync(int id, AppTask task)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Id must be more then zero", nameof(id));
            }

            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            AppTask entity = await _taskRepository.FindByIdAsync(id);

            if (entity == null)
            {
                throw new ArgumentException("not found", nameof(id));
            }

            task.Id = id;
            _taskRepository.Update(task);

            await _unitOfWork.SaveAsync();
        }
    }
}
