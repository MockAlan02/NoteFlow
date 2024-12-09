using Microsoft.EntityFrameworkCore;
using ToDoList.Api.Application.Interfaces.Repositories;
using ToDoList.Api.Domain.Entities;
using ToDoList.Api.Infrastructure.Persistence.Context;

namespace ToDoList.Api.Infrastructure.Persistence.Repositories
{
    public class ToDoListRepository(MainContext context) : IToDoListRepository
    {

        private readonly MainContext _context = context;
        private readonly DbSet<Tasks> _dbset = context.Set<Tasks>();


        public async Task<List<Tasks>> GetAllAsync()
        {
            return await _dbset.AsNoTracking().ToListAsync();
        }


        public async Task<Tasks?> GetByIdAsync(Guid id)
        {
            return await _dbset.FindAsync(id);
        }
        public async Task<Tasks> AddAsync(Tasks tasks)
        {
            await _dbset.AddAsync(tasks);
            await _context.SaveChangesAsync();
            return tasks;
        }

        public async Task<Tasks?> UpdateAsync(Tasks tasks, Guid id)
        {
            var task = await GetByIdAsync(id);
            _dbset.Entry(task!).CurrentValues.SetValues(tasks);
            await _context.SaveChangesAsync();
            return task;
        }


        public async Task DeleteAsync(Guid id)
        {
            var task = await GetByIdAsync(id);
            _dbset.Remove(task!);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Tasks> Queryable()
        {
            return _context.Tasks.AsQueryable();
        }

    }
}
