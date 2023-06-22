namespace Todo.Api.Models;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

[JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
public class TodoTaskListCreateRequest
{
    public string DisplayName { get; set; }
}
