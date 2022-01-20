using System;
using TodoApiDTO.DAL.Entities;

namespace TodoApiDTO.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TodoItem> TodoItems { get; }

        void Save();
    }
}
