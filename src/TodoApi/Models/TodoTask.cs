namespace Todo.Api.Models;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class TodoTask
{
    [JsonIgnore]
    public string List { get; set; }

    public string Id { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string Title { get; set; }

    [JsonPropertyName("@odata.etag")]
    public string ETag { get; set; }

    public string Importance { get; set; }

    public bool IsReminderOn { get; set; } = false;

    public string Status { get; set; }

    public DateTimeOffset CreatedDateTime { get; set; }

    public DateTimeOffset DueDateTime { get; set; }

    public DateTimeOffset LastModifiedDateTime { get; set; }

    public bool HasAttachments { get; set; } = false;

    public IList<string>? Categories { get; set; } = new List<string>();

    public DateTimeTimeZone? CompletedDateTime { get; set; }

    public DateTimeTimeZone? BodyLastModifiedDateTime { get; set; }

    public PatternedRecurrence? Recurrence { get; set; }

    public DateTimeTimeZone? ReminderDateTime { get; set; }

    public DateTimeTimeZone? StartDateTime { get; set; }

    public ItemBody? Body { get; set; }
}
