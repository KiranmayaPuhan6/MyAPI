using Microsoft.AspNetCore.Mvc;
using Moq;
using MyAPI.Controllers;
using MyAPI.Models.Domain;
using MyAPI.Models.Dto;
using MyAPI.Repository;
using NUnit.Framework;
using System.Xml.Linq;

namespace MyAPI.Test.ControllerTest
{
    [TestFixture]
    public class ToDoControllerTest
    {
        private Mock<IToDoRepository> _repository;
        private Tuple<List<ToDo>, List<ToDoDto>, ToDoDto> mockData;
        private ToDoController _controller;

        [SetUp]
        public void Setup()
        {
            mockData = SourceList();
            _repository = new Mock<IToDoRepository>();

        }

        private Tuple<List<ToDo>, List<ToDoDto>, ToDoDto> SourceList()
        {
            var todoList = new List<ToDo>
            {
                new ToDo { Id = 1, Name = "Todo", Description = "Todo Method", Completed = true } ,
                new ToDo { Id = 2, Name = "Todo2", Description = "Todo Method2", Completed = true }
            };
            var todoDtoList = new List<ToDoDto>
            {
                new ToDoDto { Id = 1, Name = "TodoDto", Description = "TodoDto Method", Completed = true } ,
                new ToDoDto { Id = 2, Name = "TodoDto2", Description = "TodoDto Method2", Completed = true }
            };
            var todo = new ToDoDto { Id = 1, Name = string.Empty, Description = "TodoDto Method", Completed = true };

            var result = Tuple.Create(todoList, todoDtoList, todo);
            return result;

        }

        [Test]
        public async Task GetToDoList_WithData_ReturnsOkResult()
        {
            _repository.Setup(x => x.GetAllToDoAsync()).ReturnsAsync(mockData.Item2);

            _controller = new ToDoController(_repository.Object);
            var result = await _controller.GetToDoList();

            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public async Task GetToDo_ValidId_ReturnsOkResult()
        {
            _repository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(mockData.Item2.FirstOrDefault());

            _controller = new ToDoController(_repository.Object);
            var result = await _controller.GetToDo(1);

            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public async Task GetToDo_InValidId_ReturnsNotFoundResult()
        {
            _repository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(() => null);

            _controller = new ToDoController(_repository.Object);
            var result = await _controller.GetToDo(1);

            Assert.IsInstanceOf<NotFoundResult>(result);
            var notFoundResult = result as NotFoundResult;
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }

        [Test]
        public async Task CreateToDo_ValidModelState_ReturnsOkResult()
        {
            _repository.Setup(x => x.CreateAsync(mockData.Item2.FirstOrDefault())).ReturnsAsync(mockData.Item2.FirstOrDefault());

            _controller = new ToDoController(_repository.Object);
            var result = await _controller.CreateToDo(mockData.Item2.FirstOrDefault());

            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public async Task CreateToDo_InValidModelState_ReturnsBadRequest()
        {
            _repository.Setup(x => x.CreateAsync(mockData.Item3));
            _controller = new ToDoController(_repository.Object);

            _controller.ModelState.AddModelError("Field", "Error message");
            var result = await _controller.CreateToDo(mockData.Item3);

            Assert.IsInstanceOf<BadRequestResult>(result);
            var badRequestResult = result as BadRequestResult;
            Assert.AreEqual(400, badRequestResult.StatusCode);
        }
    }
}
