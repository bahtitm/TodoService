using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TodoItemTest.Infrastructure
{
    public class TestDbContext : DbContext
    { 
        public TestDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {       

        base.OnModelCreating(modelBuilder);
    }

    public Task InvokeTransactionAsync(Func<Task> action, CancellationToken cancellationToken = default)
    {
        return action();
    }

    public Task<T> InvokeTransactionAsync<T>(Func<Task<T>> action, CancellationToken cancellationToken = default)
    {
        return action();
    }
    
}
}
