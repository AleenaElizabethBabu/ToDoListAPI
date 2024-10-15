using Microsoft.EntityFrameworkCore;
using TodoListAPI.Models;

namespace TodoListAPI.Data
{
    public class TodoListContext : DbContext
    {
        public TodoListContext(DbContextOptions<TodoListContext> options) : base(options) { }

        public DbSet<ToDoItem> ToDoItems { get; set; }
    }
}
