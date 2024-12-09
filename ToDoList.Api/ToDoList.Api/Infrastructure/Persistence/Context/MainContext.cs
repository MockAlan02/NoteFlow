using Microsoft.EntityFrameworkCore;
using ToDoList.Api.Domain.Entities;

namespace ToDoList.Api.Infrastructure.Persistence.Context
{
    public class MainContext(DbContextOptions<MainContext> options) : DbContext(options)
    {

        public DbSet<Tasks> Tasks { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
    }
}
