using ToDoList.Api.Infrastructure.Persistence;
using ToDoList.Api.Application;
using ToDoList.Api.Infrastructure.Persistence.Seeder;
using ToDoList.Api.Infrastructure.Persistence.Context;
using ToDoList.Api.Application.Interfaces.Repositories;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddApplication()
    .AddPersistence();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using(var scopped = app.Services.CreateScope())
{
    var context = scopped.ServiceProvider.GetService<MainContext>();
    var repository = scopped.ServiceProvider.GetService<IToDoListRepository>();
    await ToDoListSeeder.Seed(repository, context);
}
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
    //.WithOrigins("https://localhost:44351")); // Allow only this origin can also have multiple origins seperated with comma
    .SetIsOriginAllowed(origin => true));// Allow any origin  

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
