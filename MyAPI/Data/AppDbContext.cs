using Microsoft.EntityFrameworkCore;
using MyAPI.Models.Domain;

namespace MyAPI.Data
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "ToDoDataBase");
        }

        public DbSet<ToDo> Todos { get; set; }
    }
}
