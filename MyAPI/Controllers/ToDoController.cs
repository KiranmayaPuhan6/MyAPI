using Microsoft.AspNetCore.Mvc;
using MyAPI.Models.Dto;
using MyAPI.Repository;

namespace MyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoRepository _toDoRepository;

        public ToDoController(IToDoRepository toDoRepository)
        {
            _toDoRepository = toDoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetToDoList()
        {
            return Ok(await _toDoRepository.GetAllToDoAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetToDo(int id)
        {
            var result = await _toDoRepository.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateToDo(ToDoDto toDoDto)
        {
            if (!ModelState.IsValid) { return BadRequest(); }
            var result = await _toDoRepository.CreateAsync(toDoDto);
            return Ok(result);
        }
    }
}
