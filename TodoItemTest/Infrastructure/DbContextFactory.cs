using Microsoft.EntityFrameworkCore;
using System;

namespace TodoItemTest.Infrastructure
{
    public class DbContextFactory
    {
        public static TestDbContext Create()
        {
            var options = new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new TestDbContext(options);

            context.Database.EnsureCreated();
            context.SaveChanges();

            return context;
        }

        public static void Destroy(TestDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}
