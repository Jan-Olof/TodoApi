using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using TodoApi.Controllers;
using TodoApi.Models;

namespace TodoApiTests
{
    [TestClass]
    public class TodoControllerTests
    {
        private ITodoRepository _todoItems;

        [TestInitialize]
        public void SetUp()
        {
            _todoItems = Substitute.For<ITodoRepository>();
        }

        [TestCleanup]
        public void TearDown()
        {
        }

        [TestMethod]
        public void TestShouldCreateTodoItem()
        {
            // Arrange
            _todoItems.Add(new TodoItem());

            var sut = new TodoController(_todoItems);

            // Act
            var result = sut.Create(new TodoItem());

            // Assert
            var actionResult = (CreatedAtRouteResult)result;

            Assert.AreEqual(201, actionResult.StatusCode);
        }
    }
}