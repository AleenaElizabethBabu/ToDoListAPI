using System;

namespace TodoListAPI.Models
{
    public class ToDoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? CompletedDate { get; set; }
    }
}
