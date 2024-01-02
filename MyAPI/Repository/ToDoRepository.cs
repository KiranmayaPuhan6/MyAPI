using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyAPI.Data;
using MyAPI.Models.Domain;
using MyAPI.Models.Dto;

namespace MyAPI.Repository
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ToDoRepository(AppDbContext appDbContext, IMapper mapper)
        {
            _context = appDbContext;
            _mapper = mapper;
        }
        public async Task<ToDoDto> CreateAsync(ToDoDto toDoDto)
        {
            var result = _mapper.Map<ToDo>(toDoDto);
            await _context.Todos.AddAsync(result);
            await _context.SaveChangesAsync();
            return toDoDto;
        }

        public async Task<IEnumerable<ToDoDto>> GetAllToDoAsync()
        {
            var reult = await _context.Todos.ToListAsync();
            var resultDto=_mapper.Map<List<ToDoDto>>(reult);
            return resultDto;
        }

        public async Task<ToDoDto> GetByIdAsync(int id)
        {
            var reult = await _context.Todos.FirstOrDefaultAsync(x=>x.Id==id);
            var resultDto = _mapper.Map<ToDoDto>(reult);
            return resultDto;
        }
    }
}
