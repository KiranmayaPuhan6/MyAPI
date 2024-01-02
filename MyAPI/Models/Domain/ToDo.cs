namespace MyAPI.Models.Domain
{
    public class ToDo
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; } = null;

        public bool Completed { get; set; } = false;
    }
}
