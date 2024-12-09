using Microsoft.EntityFrameworkCore;
using ToDoList.Api.Application.Interfaces.Repositories;
using ToDoList.Api.Domain.Entities;
using ToDoList.Api.Infrastructure.Persistence.Context;

namespace ToDoList.Api.Infrastructure.Persistence.Seeder
{
    public static class ToDoListSeeder
    {
        public static async Task Seed(IToDoListRepository toDoListRepository, MainContext context)
        {
            var tasksCount = await context.Tasks.CountAsync();
            if (tasksCount > 0)
                return;


            var tasks = new List<Tasks>()
            {
                new()
                {
                    Title = "Prueba 1",
                    Description = "Prueba 1",
                },
                 new()
                {
                    Title = "Prueba 2",
                    Description = "Prueba 2",
                },
                  new()
                {
                    Title = "Prueba 3",
                    Description = "Prueba 3",
                }
            };

            foreach (var task in tasks)
            {
                await toDoListRepository.AddAsync(task);
            }
        }
    }
}
