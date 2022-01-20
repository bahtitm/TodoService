using Microsoft.EntityFrameworkCore;
using System;
using TodoApiDTO.DAL.EF;
using TodoApiDTO.DAL.Entities;
using TodoApiDTO.DAL.Interfaces;

namespace TodoApiDTO.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private TodoContext db;
        private TodoItemRepository todoItemRepository;

        public EFUnitOfWork(DbContextOptions<TodoContext> options)
        {
            db = new TodoContext(options);
        }
        public IRepository<TodoItem> TodoItems
        {
            get
            {
                if (todoItemRepository == null)
                    todoItemRepository = new TodoItemRepository(db);
                return todoItemRepository;
            }
        }
        public void Save()
        {
            db.SaveChanges();
        }
        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
