using Microsoft.AspNetCore.Mvc;
using ToDoList.Api.Application.Filters;
using ToDoList.Api.Application.Interfaces.Services;
using ToDoList.Api.Domain.Entities;

namespace ToDoList.Api.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class TasksController(IToDoListService toDoListService) : ControllerBase
    {
        private readonly IToDoListService _toDoListService = toDoListService;


        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var tasks = await _toDoListService.GetAllAsync();
            if (tasks.Count == 0)
                return NoContent();

            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var tasks = await _toDoListService.GetByIdAsync(id);
            if (tasks is null)
                return NoContent();

            return Ok(tasks);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> Filter([FromQuery] TasksFilter filter)
        {
            return Ok(await _toDoListService.ApplyFilter(filter));

        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(Tasks tasks)
        {
            try
            {
                return Ok(await _toDoListService.AddAsync(tasks));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Tasks tasks, Guid id)
        {
            try
            {
                return Ok(await _toDoListService.UpdateAsync(tasks, id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/Complete")]
        public async Task<IActionResult> CompleteTaskAsync(Guid id)
        {
            try
            {
                await _toDoListService.CompleteTask(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                await _toDoListService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
