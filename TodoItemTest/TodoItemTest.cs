using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using TodoApiDTO.BAL.DTO;
using TodoApiDTO.BAL.Infrostructure;
using TodoApiDTO.BAL.Interfaces;
using TodoApiDTO.BAL.Services;
using TodoApiDTO.DAL.EF;
using TodoApiDTO.DAL.Interfaces;
using TodoApiDTO.DAL.Repositories;

namespace TodoItemTest
{
    public class Tests
    {
        IUnitOfWork unitOfWork;
        ITodoItemService todoItemService;
        public Tests()
        {
            var options = new DbContextOptionsBuilder<TodoContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            unitOfWork = new EFUnitOfWork(options);
            todoItemService = new TodoItemService(unitOfWork);
        }
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Create_TodoItemCreate_TodoItemExisit()
        {
            var newTodoItem = new TodoItemDTO()
            {
                IsComplete = true,
                Name = "Какое то имя",
            };
            var cretedTodoitemDto = todoItemService.CreateTodoItem(newTodoItem);
            unitOfWork.Save();

            Assert.IsNotNull(unitOfWork.TodoItems.Get(cretedTodoitemDto.Id));
        }
        [SetUp]
        public void SetupGetTodoItem()
        {
            var newTodoItem1 = new TodoItemDTO { Id = 1, IsComplete = true, Name = "Какое то имя", };
            var newTodoItem2 = new TodoItemDTO { Id = 2, IsComplete = true, Name = "Какое то имя", };
            var cretedTodoitemDto1 = todoItemService.CreateTodoItem(newTodoItem1);
            var cretedTodoitemDto2 = todoItemService.CreateTodoItem(newTodoItem2);
            unitOfWork.Save();

        }
        [Test]
        
        public void GetTodoItem_TodoItemWillBeGot_TodoItemNotBeNull()
        {
           todoItemService.GetTodoItem(1).Should().NotBeNull();
           todoItemService.GetTodoItem(2).Should().NotBeNull();
        }
        [SetUp]
        public void SetupGetTodoItemAll()
        {
            var newTodoItem1 = new TodoItemDTO { Id = 1, IsComplete = true, Name = "Какое то имя", };
            var newTodoItem2 = new TodoItemDTO { Id = 2, IsComplete = true, Name = "Какое то имя", };
            var cretedTodoitemDto1 = todoItemService.CreateTodoItem(newTodoItem1);
            var cretedTodoitemDto2 = todoItemService.CreateTodoItem(newTodoItem2);
            unitOfWork.Save();
        }
        [Test]
        public void GetTodoItemAll_TodoItemWillGetedAll_TodoItemNotNull()
        {
            todoItemService.GetTodoItems().Should().NotBeNullOrEmpty();

        }
        [SetUp]
        public void SetupGetTodoItemUpdate()
        {
            var newTodoItem1 = new TodoItemDTO { Id = 1, IsComplete = true, Name = "Какое то имя", };
            var cretedTodoitemDto1 = todoItemService.CreateTodoItem(newTodoItem1);
            unitOfWork.Save();
        }
        [Test]
        public void Update_TodoItemUpdate_TodoItemSholdBeUpdate()
        {
            var updatedTodoItem = new TodoItemDTO { Id = 1, IsComplete = true, Name = "Какое то имя Обновленное", };
            todoItemService.UpdateTodoItem(1, updatedTodoItem);
            todoItemService.GetTodoItem(1).Name.Should().Be("Какое то имя Обновленное");
        }

        [SetUp]
        public void SetupGetTodoItemDelete()
        {
            var newTodoItem1 = new TodoItemDTO { Id = 1, IsComplete = true, Name = "Какое то имя", };
            var cretedTodoitemDto1 = todoItemService.CreateTodoItem(newTodoItem1);
            unitOfWork.Save();
        }
        public void Delete_TodoItemDelete_TodoItemSholdBeDeleted()
        {
            try
            {
                todoItemService.GetTodoItem(1).Should().BeNull();
            }
            catch (ValidationException)
            {

            Assert.Pass();
                
            }
        }
    }
}