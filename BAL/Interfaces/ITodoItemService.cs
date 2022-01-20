using System.Collections.Generic;
using TodoApiDTO.BAL.DTO;

namespace TodoApiDTO.BAL.Interfaces
{
    public interface ITodoItemService
    {
        TodoItemDTO GetTodoItem(long? id);
        IEnumerable<TodoItemDTO> GetTodoItems();
        void UpdateTodoItem(long id, TodoItemDTO todoItemDTO);
        TodoItemDTO CreateTodoItem(TodoItemDTO todoItemDTO);
        void DeleteTodoItem(long id);
        void Dispose();
    }
}
