using System.ComponentModel.DataAnnotations;

namespace MyAPI.Models.Dto
{
    public class ToDoDto
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; } = null;

        public bool Completed { get; set; } = false;
    }
}
