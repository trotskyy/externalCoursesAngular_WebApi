using System.Collections.Generic;
using System.Threading.Tasks;
using AwesomeLists.Data.Abstract;
using AwesomeLists.Data.Entities;
using System;
using System.Threading;

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

        public async Task<AppTask> AddTaskAsync(AppTask task, CancellationToken token)
        {
            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            _taskRepository.AddTask(task);
            await _unitOfWork.SaveAsync(token);
            return task;
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id, CancellationToken token)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Id must be >= 0", nameof(id));
            }

            AppTask task = await _taskRepository.FindByIdAsync(id, token);

            if (task == null)
            {
                throw new ArgumentException("not fount", nameof(id));
            }

            _taskRepository.Delete(task);
            await _unitOfWork.SaveAsync(token);
        }

        public async Task<AppTask> GetByIdAsync(int id, CancellationToken token)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Id must be more then zero", nameof(id));
            }

            return await _taskRepository.FindByIdAsync(id, token);
        }

        public async Task<List<AppTask>> GetByTaskListIdAsync(int id, CancellationToken token)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Id must be more then zero", nameof(id));
            }

            return await _taskRepository.GetAllTasksByTaskListIdAsync(id, token);
        }

        public async System.Threading.Tasks.Task UpdateTaskAsync(int id, AppTask task, CancellationToken token)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Id must be more then zero", nameof(id));
            }

            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            AppTask entity = await _taskRepository.FindByIdAsync(id, token);

            if (entity == null)
            {
                throw new ArgumentException("not found", nameof(id));
            }

            entity.Date = task.Date;
            entity.Description = task.Description;
            entity.Name = task.Name;
            entity.Priority = task.Priority;
            entity.Status = task.Status;

            await _unitOfWork.SaveAsync(token);
        }
    }
}
