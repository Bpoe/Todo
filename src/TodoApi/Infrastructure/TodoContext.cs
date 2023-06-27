namespace TodoApi.Infrastructure;

using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Todo.Api.Models;

public class TodoContext : DbContext
{
    public TodoContext(DbContextOptions<TodoContext> options)
    : base(options)
    {
    }

    public DbSet<TodoTaskList> TaskLists { get; set; } = null!;

    public DbSet<TodoTask> Tasks { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<TodoTask>()
            .HasKey(t => new { t.List, t.Id });

        modelBuilder
            .Entity<TodoTask>()
            .Property(e => e.Recurrence)
            .HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<PatternedRecurrence>(v));

        modelBuilder
            .Entity<TodoTask>()
            .Property(e => e.Categories)
            .HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<List<string>>(v));

        modelBuilder.Entity<TodoTask>().OwnsOne(p => p.CompletedDateTime);
        modelBuilder.Entity<TodoTask>().OwnsOne(p => p.ReminderDateTime);
        modelBuilder.Entity<TodoTask>().OwnsOne(p => p.BodyLastModifiedDateTime);
        modelBuilder.Entity<TodoTask>().OwnsOne(p => p.StartDateTime);
        modelBuilder.Entity<TodoTask>().OwnsOne(p => p.Body);

        base.OnModelCreating(modelBuilder);
    }
}