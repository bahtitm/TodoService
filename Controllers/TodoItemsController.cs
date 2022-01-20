using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApiDTO.BAL.DTO;
using TodoApiDTO.BAL.Interfaces;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoItemService todoItemService;        
        public TodoItemsController(ITodoItemService todoItemService)
        {
            this.todoItemService = todoItemService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            return Ok(todoItemService.GetTodoItems());            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            return todoItemService.GetTodoItem(id);         
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            todoItemService.UpdateTodoItem(id, todoItemDTO);
            return NoContent();         
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            return todoItemService.CreateTodoItem(todoItemDTO);            
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            todoItemService.DeleteTodoItem(id);
            return NoContent();
        }
    }
}
