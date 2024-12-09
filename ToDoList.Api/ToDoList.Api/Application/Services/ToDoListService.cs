using Microsoft.EntityFrameworkCore;
using ToDoList.Api.Application.Filters;
using ToDoList.Api.Application.Interfaces.Repositories;
using ToDoList.Api.Application.Interfaces.Services;
using ToDoList.Api.Domain.Entities;

namespace ToDoList.Api.Application.Services
{
    public class ToDoListService(IToDoListRepository toDoListRepository) : IToDoListService
    {
        private readonly IToDoListRepository _toDoListRepository = toDoListRepository;

        public async Task<List<Tasks>> GetAllAsync()
        {
            return await _toDoListRepository.GetAllAsync();
        }

        public async Task<Tasks?> GetByIdAsync(Guid id)
        {
            return await _toDoListRepository.GetByIdAsync(id);
        }

        public async Task<Tasks> AddAsync(Tasks tasks)
        {
            return await _toDoListRepository.AddAsync(tasks);
        }

        public async Task<Tasks?> UpdateAsync(Tasks tasks, Guid id)
        {
            Tasks? task = await GetByIdAsync(id);
            tasks.IsCompleted = task!.IsCompleted;
            return await _toDoListRepository.UpdateAsync(tasks, id);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _toDoListRepository.DeleteAsync(id);
        }

        public async Task CompleteTask(Guid id)
        {
            var task = await _toDoListRepository.GetByIdAsync(id);
            if (task is null)
                return;

            task.IsCompleted = true;
            await _toDoListRepository.UpdateAsync(task, id);
        }

        public async Task<List<Tasks>> ApplyFilter(TasksFilter filter)
        {
            var query = _toDoListRepository.Queryable();
            if (!string.IsNullOrEmpty(filter.Name))
            {
                query = query.Where(x => x.Title.StartsWith(filter.Name, StringComparison.CurrentCultureIgnoreCase));
            }
            if (filter.Status.HasValue)
            {
                query = query.Where(x => x.IsCompleted == filter.Status);
            }

            return await query!.ToListAsync();
        }
    }
}
