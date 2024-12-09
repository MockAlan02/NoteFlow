using ToDoList.Api.Domain.Entities;

namespace ToDoList.Api.Application.Interfaces.Repositories
{
    public interface IToDoListRepository
    {
        Task<Tasks> AddAsync(Tasks tasks);
        Task DeleteAsync(Guid id);
        Task<List<Tasks>> GetAllAsync();
        Task<Tasks?> GetByIdAsync(Guid id);
        Task<Tasks?> UpdateAsync(Tasks tasks, Guid id);
        IQueryable<Tasks> Queryable();
    }
}