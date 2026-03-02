using System.Text.Json.Serialization;

namespace Application.Contracts;

public record Response<T> (
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    T? Result = default,
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    string? Error = null
)
{
    [property: JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public bool Success => string.IsNullOrEmpty(Error);
}

public record Response (
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    string? Result = null,
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    string? Error  = null
)
{
    [property: JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public bool Success => string.IsNullOrEmpty(Error);
}