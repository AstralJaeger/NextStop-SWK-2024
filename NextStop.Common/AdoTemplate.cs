using System.Data;
using System.Data.Common;

namespace NextStop.Common;

/// <summary>
/// Defines a delegate that describes a method for mapping a database row (<see cref="IDataRecord"/>) to an object of type T.
/// </summary>
/// <typeparam name="T">The type of object to which the row is mapped.</typeparam>
/// <param name="row">The database row to be mapped.</param>
/// <returns>An object of type T containing the mapped data of the row.</returns>
public delegate T RowMapper< out T>(IDataRecord row);

public class AdoTemplate(IConnectionFactory connectionFactory)
{
    /// <summary>
    /// Adds the provided parameters to a <see cref="DbCommand"/>.
    /// </summary>
    /// <param name="command">The <see cref="DbCommand"/> to which the parameters should be added.</param>
    /// <param name="parameters">An array of <see cref="QueryParameter"/> objects containing the names and values of the parameters.</param>
    private static void AddParameters(DbCommand command, QueryParameter[] parameters)
    {
        foreach (var p in parameters)
        {
            DbParameter dbParam = command.CreateParameter();  // Creates a new DbParameter object
            dbParam.ParameterName = p.Name;                   // Sets the parameter name
            dbParam.Value = p.Value;                          // Sets the parameter value
            command.Parameters.Add(dbParam);                  // Adds the parameter to the DbCommand
        }
    }

    /// <summary>
    /// Executes a SQL query asynchronously and maps the result set to a list of objects of type T using a <see cref="RowMapper{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of objects to which the database rows are mapped.</typeparam>
    /// <param name="sql">The SQL command to be executed.</param>
    /// <param name="rowMapper">A delegate that specifies how each row of the result is mapped to an object of type T.</param>
    /// <param name="parameters">An optional list of <see cref="QueryParameter"/> objects used as SQL parameters.</param>
    public async Task<IEnumerable<T>> QueryAsync<T>(string sql, RowMapper<T> rowMapper,
        params QueryParameter[] parameters)
    {
        // Creates and opens a new database connection asynchronously
        await using DbConnection connection = await connectionFactory.CreateConnectionAsync();

        // Creates a new DbCommand and configures the SQL command
        await using DbCommand command = connection.CreateCommand();
        command.CommandText = sql;
        AddParameters(command, parameters); // Adds the SQL parameters

        // Executes the query asynchronously and creates a DbDataReader to read the result set
        await using DbDataReader reader = await command.ExecuteReaderAsync();
        var items = new List<T>();

        // Iterates through each row of the result set and applies the RowMapper to map the row to an object of type T
        while (await reader.ReadAsync())
        {
            items.Add(rowMapper(reader));  // Adds the mapped object to the result list
        }

        // Returns the list of mapped objects
        return items;
    }

    /// <summary>
    /// Executes a SQL query that returns only a single row or <c>null</c> if no row exists.
    /// </summary>
    /// <typeparam name="T">The type of object to which the row is mapped.</typeparam>
    /// <param name="sql">The SQL command to be executed.</param>
    /// <param name="rowMapper">A delegate that specifies how the result row is mapped to an object of type T.</param>
    /// <param name="parameters">An optional list of <see cref="QueryParameter"/> objects used as SQL parameters.</param>
    /// <returns>A <see cref="Task{T}"/> with a single object of type T or <c>null</c> if no row exists.</returns>
    public async Task<T?> QuerySingleAsync<T>(string sql, RowMapper<T> rowMapper,
        params QueryParameter[] parameters)
    {
        // Calls QueryAsync and returns the single row or null
        return (await QueryAsync(sql, rowMapper, parameters)).SingleOrDefault();
    }

    /// <summary>
    /// Executes a SQL data manipulation query asynchronously, such as INSERT, UPDATE, or DELETE,
    /// and returns the number of rows affected.
    /// </summary>
    /// <param name="sql">The SQL command to be executed.</param>
    /// <param name="parameters">An optional list of <see cref="QueryParameter"/> objects used as SQL parameters.</param>
    /// <returns>A <see cref="Task{int}"/> indicating the number of rows affected by the query.</returns>
    public async Task<int> ExecuteAsync(string sql, params QueryParameter[] parameters)
    {
        // Creates and opens the database connection asynchronously
        await using DbConnection connection = await connectionFactory.CreateConnectionAsync();

        // Creates a DbCommand and configures the SQL command
        await using DbCommand command = connection.CreateCommand();
        command.CommandText = sql;
        AddParameters(command, parameters); // Adds the specified parameters

        // Executes the command asynchronously and returns the number of rows affected
        return await command.ExecuteNonQueryAsync();
    }
}
