using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TodoApiDTO.DAL.EF;
using TodoApiDTO.DAL.Entities;
using TodoApiDTO.DAL.Interfaces;

namespace TodoApiDTO.DAL.Repositories
{
    public class TodoItemRepository : IRepository<TodoItem>
    {
        private TodoContext db;

        public TodoItemRepository(TodoContext context)
        {
            db = context;
        }
        public IEnumerable<TodoItem> GetAll()
        {
            return db.TodoItems;
        }
        public TodoItem Get(long id)
        {
            return db.TodoItems.Find(id);
        }
        public void Create(TodoItem todoItem)
        {
            db.TodoItems.Add(todoItem);
        }
        public void Update(TodoItem todoItem)
        {
            db.Entry(todoItem).State = EntityState.Modified;
        }
        public IEnumerable<TodoItem> Find(Func<TodoItem, bool> predicate)
        {
            return db.TodoItems.Where(predicate).ToList();
        }
        public void Delete(long id)
        {
            TodoItem order = db.TodoItems.Find(id);
            if (order != null)
                db.TodoItems.Remove(order);
        }
    }
}
