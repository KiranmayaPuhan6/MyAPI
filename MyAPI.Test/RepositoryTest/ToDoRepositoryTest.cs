using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using MyAPI.Data;
using MyAPI.Models.Domain;
using MyAPI.Models.Dto;
using MyAPI.Repository;
using NUnit.Framework;

namespace MyAPI.Tests
{
    [TestFixture]
    public class ToDoRepositoryTests
    {
        private AppDbContext _context;
        private Mock<IMapper> _mockMapper;
        private IToDoRepository _repository;
        private Tuple<List<ToDo>, List<ToDoDto>> mockData;

        [SetUp]
        public void Setup()
        {
            mockData = SourceList();
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext();

            _mockMapper = new Mock<IMapper>();
            _repository = new ToDoRepository(_context, _mockMapper.Object);
        }
        private Tuple<List<ToDo>, List<ToDoDto>> SourceList()
        {
            var todoList = new List<ToDo>
            {
                new ToDo { Id = new Random().Next(), Name = "Todo", Description = "Todo Method", Completed = true } ,
                new ToDo { Id = new Random().Next(), Name = "Todo2", Description = "Todo Method2", Completed = true }
            };
            var todoDtoList = new List<ToDoDto>
            {
                new ToDoDto { Id = new Random().Next(), Name = "TodoDto", Description = "TodoDto Method", Completed = true } ,
                new ToDoDto { Id = new Random().Next(), Name = "TodoDto2", Description = "TodoDto Method2", Completed = true }
            };

            var result = Tuple.Create(todoList, todoDtoList);
            return result;

        }

        [Test]
        public async Task CreateAsync_ValidInput_ReturnsToDoDto()
        {
            _mockMapper.Setup(m => m.Map<ToDo>(mockData.Item2.FirstOrDefault())).Returns(mockData.Item1.FirstOrDefault());

            var result = await _repository.CreateAsync(mockData.Item2.FirstOrDefault());

            Assert.AreEqual(mockData.Item2.FirstOrDefault(), result);

            _context.Database.EnsureDeleted();
            _context.SaveChanges();
        }

        [Test]
        public async Task GetAllToDoAsync_WithData_ReturnsListOfToDoDtos()
        {
            _context.Todos.AddRange(mockData.Item1.ToList());
            _context.SaveChanges();
            _mockMapper.Setup(m => m.Map<List<ToDoDto>>(mockData.Item1)).Returns(mockData.Item2);

            var result = await _repository.GetAllToDoAsync();

            CollectionAssert.AreEqual(mockData.Item2, result);

            _context.Database.EnsureDeleted();
            _context.SaveChanges();
        }

        [Test]
        public async Task GetByIdAsync_ValidId_ReturnsToDoDto()
        {
            _context.Todos.AddRange(mockData.Item1.ToList());
            _context.SaveChanges();
            _mockMapper.Setup(m => m.Map<ToDoDto>(mockData.Item1.FirstOrDefault())).Returns(mockData.Item2.FirstOrDefault());

            var result = await _repository.GetByIdAsync(mockData.Item1.FirstOrDefault().Id);

            Assert.AreEqual(mockData.Item2.FirstOrDefault(), result);

            _context.Database.EnsureDeleted();
            _context.SaveChanges();
        }
    }
}
