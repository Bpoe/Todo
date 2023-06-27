namespace Todo.Api.Models;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class TodoTaskList
{
    public string Id { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string DisplayName { get; set; }

    public bool IsOwner { get; set; } = false;

    public bool IsShared { get; set; } = false;

    [System.Text.Json.Serialization.JsonConverter(typeof(JsonStringEnumConverter))]
    public WellknownListName WellknownListName { get; set; }

    [JsonPropertyName("@odata.etag")]
    public string? ETag { get; set; }
}