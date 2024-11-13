namespace NextStop.Common;

/// <summary>
/// Represents a query parameter used in SQL commands.
/// </summary>
public class QueryParameter(string name, object? value)
{
    /// <summary>
    /// Gets the name of the parameter as it is used in the SQL query (e.g., "@name").
    /// </summary>
    public string Name { get; } = name;

    /// <summary>
    /// Gets the value of the parameter to be inserted into the query.
    /// </summary>
    public object? Value { get; } = value;
}