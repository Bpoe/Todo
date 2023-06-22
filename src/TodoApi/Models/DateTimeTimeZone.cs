namespace Todo.Api.Models;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

[JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
public class DateTimeTimeZone
{
    public DateTime DateTime { get; set; }

    public string TimeZone { get; set; } = "UTC";
}
