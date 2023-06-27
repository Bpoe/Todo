namespace Todo.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using TodoApi.Infrastructure;
using TodoApi.Models;

[Route("me/todo/lists/{list}/tasks")]
[ApiController]
public class TasksController : ControllerBase
{
    private readonly TodoContext context;

    public TasksController(TodoContext context)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(context));
    }

    [HttpGet]
    public async Task<IResult> Get(string list)
        => Results.Ok(new ValueResult<TodoTask>(await this.context.Tasks.Where(t => string.Equals(t.List, list)).ToListAsync()));

    [HttpGet("{id}", Name = "Get")]
    public async Task<IResult> Get(string list, string id)
    {
        var taskList = await this.context.Tasks.FindAsync(list, id);

        return taskList == null
            ? Results.NotFound()
            : Results.Ok(taskList);
    }

    [HttpPost]
    public async Task<IResult> Post(string list, [FromBody] TodoTask value)
    {
        value.List = list;
        value.Id = Guid.NewGuid().ToString();

        _ = this.context.Tasks.Add(value);
        _ = await this.context.SaveChangesAsync();

        return Results.CreatedAtRoute("Get", new { list = value.List, id = value.Id }, value);
    }

    [HttpPatch("{id}")]
    public async Task<IResult> Patch(string list, string id, [FromBody] TodoTask value)
    {
        var existingTask = await this.context.Tasks.FindAsync(list, id);

        if (existingTask == null)
        {
            return Results.NotFound();
        }

        value.ApplyTo(existingTask);
        this.context.Entry(existingTask).State = EntityState.Modified;
        _ = await this.context.SaveChangesAsync();

        return Results.Ok(existingTask);
    }

    [HttpDelete("{id}")]
    public async Task<IResult> Delete(string list, string id)
    {
        var taskList = await this.context.Tasks.FindAsync(list, id);
        if (taskList != null)
        {
            _ = this.context.Tasks.Remove(taskList);
            _ = await this.context.SaveChangesAsync();
        }

        return Results.NoContent();
    }

    private static TodoTask Create(TodoTask task, string list)
    {
        task.List = list;
        task.Id = Guid.NewGuid().ToString();

        return task;
    }
}
