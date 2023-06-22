namespace Todo.Api.Models;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

[JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
public class ValueResult<T>
{
    public IEnumerable<T> Value { get; set; }

    public ValueResult(IEnumerable<T> values) => this.Value = values;
}