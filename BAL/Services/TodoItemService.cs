using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using TodoApiDTO.BAL.DTO;
using TodoApiDTO.BAL.Infrostructure;
using TodoApiDTO.BAL.Interfaces;
using TodoApiDTO.DAL.Entities;
using TodoApiDTO.DAL.Interfaces;

namespace TodoApiDTO.BAL.Services
{
    public class TodoItemService : ITodoItemService
    {
        IUnitOfWork Database { get; set; }
        public TodoItemService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public IEnumerable<TodoItemDTO> GetTodoItems()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TodoItem, TodoItemDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<TodoItem>, List<TodoItemDTO>>(Database.TodoItems.GetAll());
        }
        public TodoItemDTO GetTodoItem(long? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id todoitem", "");
            var todoItem = Database.TodoItems.Get(id.Value);
            if (todoItem == null)
                throw new ValidationException("todoitem не найден", "");

            return new TodoItemDTO
            {
                IsComplete = todoItem.IsComplete,
                Id = todoItem.Id,
                Name = todoItem.Name
            };
        }

        public void UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            if (id != todoItemDTO.Id)
            {
                throw new ValidationException("id не совпадает", "");
            }
            var todoItem = Database.TodoItems.Get(id);
            if (todoItem == null)
            {
                throw new ValidationException("TodoItem не найден", "");
            }

            todoItem.Name = todoItemDTO.Name;
            todoItem.IsComplete = todoItemDTO.IsComplete;
            try
            {
                Database.Save();
            }
            catch (DbUpdateConcurrencyException) when (!TodoItemExists(id))
            {
                throw new NotImplementedException();
            }
        }

        public TodoItemDTO CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            var todoItem = new TodoItem
            {
                IsComplete = todoItemDTO.IsComplete,
                Name = todoItemDTO.Name
            };
            Database.TodoItems.Create(todoItem);
            Database.Save();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TodoItem, TodoItemDTO>()).CreateMapper();
            return mapper.Map<TodoItemDTO>(todoItem);
        }
        public void DeleteTodoItem(long id)
        {
            var todoItem = Database.TodoItems.Get(id);
            if (todoItem == null)
            {
                throw new ValidationException("TodoItem не найден", "");
            }
            Database.TodoItems.Delete(id);
            Database.Save();
        }
        private bool TodoItemExists(long id)
        {
            if (Database.TodoItems.Get(id) is TodoItem)
            {
                return true;
            };
            return false;
        }
        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
