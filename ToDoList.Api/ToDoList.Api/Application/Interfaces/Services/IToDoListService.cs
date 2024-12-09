using ToDoList.Api.Application.Filters;
using ToDoList.Api.Domain.Entities;

namespace ToDoList.Api.Application.Interfaces.Services
{
    public interface IToDoListService
    {
        Task CompleteTask(Guid id);
        Task DeleteAsync(Guid id);
        Task<List<Tasks>> GetAllAsync();
        Task<Tasks> AddAsync(Tasks tasks);
        Task<Tasks?> GetByIdAsync(Guid id);
        Task<Tasks?> UpdateAsync(Tasks tasks, Guid id);
        Task<List<Tasks>> ApplyFilter(TasksFilter filter);
    }
}