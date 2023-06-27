namespace Todo.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using TodoApi.Infrastructure;

[Route("me/todo/lists")]
[ApiController]
public class TaskListsController : ControllerBase
{
    private readonly TodoContext context;

    public TaskListsController(TodoContext context)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(context));
    }

    [HttpGet]
    public async Task<IResult> Get()
        => Results.Ok(new ValueResult<TodoTaskList>(await this.context.TaskLists.ToListAsync()));

    [HttpGet("{id}", Name = "GetTaskList")]
    public async Task<IResult> Get(string id)
    {
        var taskList = await this.context.TaskLists.FindAsync(id);

        return taskList == null
            ? Results.NotFound()
            : Results.Ok(taskList);
    }

    [HttpPost]
    public async Task<IResult> Post([FromBody] TodoTaskListCreateRequest value)
    {
        if (string.IsNullOrEmpty(value?.DisplayName))
        {
            return Results.BadRequest();
        }

        var taskList = new TodoTaskList
        {
            Id = Guid.NewGuid().ToString(),
            DisplayName = value.DisplayName,
            IsOwner = true,
        };

        while (true)
        {
            var nameExists = await this.context.TaskLists.AnyAsync(x => x.DisplayName == taskList.DisplayName);
            if (!nameExists)
            {
                break;
            }
            
            taskList.DisplayName = taskList.DisplayName + " (1)";
        }

        _ = this.context.TaskLists.Add(taskList);
        _ = await this.context.SaveChangesAsync();

        return Results.CreatedAtRoute("GetTaskList", new { id = taskList.Id }, taskList);
    }

    [HttpPatch("{id}")]
    public async Task<IResult> Patch(string id, [FromBody] TodoTaskListCreateRequest value)
    {
        if (string.IsNullOrEmpty(value?.DisplayName))
        {
            return Results.BadRequest();
        }

        var taskList = await this.context.TaskLists.FindAsync(id);

        if (taskList == null)
        {
            return Results.NotFound();
        }

        taskList.DisplayName = value.DisplayName;

        _ = await this.context.SaveChangesAsync();

        return Results.Ok(taskList);
    }

    [HttpDelete("{id}")]
    public async Task<IResult> Delete(string id)
    {
        var taskList = await this.context.TaskLists.FindAsync(id);
        if (taskList != null)
        {
            _ = this.context.TaskLists.Remove(taskList);
            _ = await this.context.SaveChangesAsync();
        }

        return Results.NoContent();
    }
}
