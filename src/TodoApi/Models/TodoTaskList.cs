namespace Todo.Api.Models;

using Newtonsoft.Json;
using System.Text.Json.Serialization;

public class TodoTaskList
{
    public string Id { get; set; }

    public string DisplayName { get; set; }

    public bool IsOwner { get; set; } = false;

    public bool IsShared { get; set; } = false;

    [System.Text.Json.Serialization.JsonConverter(typeof(JsonStringEnumConverter))]
    public WellknownListName WellknownListName { get; set; }

    [JsonProperty("@odata.etag")]
    [JsonPropertyName("@odata.etag")]
    public string? ETag { get; set; }
}