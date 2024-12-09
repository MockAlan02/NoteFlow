using ToDoList.Api.Application.Interfaces.Services;
using ToDoList.Api.Application.Services;

namespace ToDoList.Api.Application
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            #region addServices
            services.AddTransient<IToDoListService, ToDoListService>();
            #endregion
            
            return services;
        }
    }
}
