using MyAPI.Models.Dto;

namespace MyAPI.Repository
{
    public interface IToDoRepository
    {
        Task<IEnumerable<ToDoDto>> GetAllToDoAsync();

        Task<ToDoDto> GetByIdAsync(int id);

        Task<ToDoDto> CreateAsync(ToDoDto toDoDto);
    }
}
