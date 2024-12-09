using Microsoft.EntityFrameworkCore;
using ToDoList.Api.Application.Interfaces.Repositories;
using ToDoList.Api.Infrastructure.Persistence.Context;
using ToDoList.Api.Infrastructure.Persistence.Repositories;

namespace ToDoList.Api.Infrastructure.Persistence
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddDbContext<MainContext>(options =>
            {
                options.UseInMemoryDatabase("DbTest");
            });
            
            services.AddTransient<IToDoListRepository, ToDoListRepository>();
            
            return services;
        }


    }

}
