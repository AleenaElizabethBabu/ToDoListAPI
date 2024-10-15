using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoListAPI.Data;
using TodoListAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class TodoItemsController : ControllerBase
{
    private readonly TodoListContext _context;

    public TodoItemsController(TodoListContext context)
    {
        _context = context;

        if (_context.ToDoItems.Count() == 0)
        {
            // Create a new TodoItem if collection is empty,
            // which means you can't delete all TodoItems.
            _context.ToDoItems.Add(new ToDoItem { Title = "Sample Todo", Description = "Sample Description" });
            _context.SaveChanges();
        }
    }

    // GET: api/TodoItems
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ToDoItem>>> GetToDoItems()
    {
        return await _context.ToDoItems.Where(item => !item.CompletedDate.HasValue).ToListAsync();
    }

    // GET: api/TodoItems/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ToDoItem>> GetToDoItem(int id)
    {
        var toDoItem = await _context.ToDoItems.FindAsync(id);

        if (toDoItem == null)
        {
            return NotFound();
        }

        return toDoItem;
    }

    // POST: api/TodoItems
    [HttpPost]
    public async Task<ActionResult<ToDoItem>> PostToDoItem(ToDoItem toDoItem)
    {
        _context.ToDoItems.Add(toDoItem);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetToDoItem), new { id = toDoItem.Id }, toDoItem);
    }

    // PUT: api/TodoItems/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutToDoItem(int id, ToDoItem toDoItem)
    {
        if (id != toDoItem.Id)
        {
            return BadRequest();
        }

        toDoItem.CompletedDate = DateTime.Now;
        _context.Entry(toDoItem).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ToDoItemExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/TodoItems/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteToDoItem(int id)
    {
        var toDoItem = await _context.ToDoItems.FindAsync(id);
        if (toDoItem == null)
        {
            return NotFound();
        }

        _context.ToDoItems.Remove(toDoItem);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ToDoItemExists(int id)
    {
        return _context.ToDoItems.Any(e => e.Id == id);
    }
}
