using MySqlConnector;
using OAI_API.Configure;
using System.Data;

namespace OAI_API.Shared;

public class SharedDatabaseFactory : IDatabaseFactory
{
    private readonly string _connectionString;
    private MySqlConnection? _connection;

    public SharedDatabaseFactory(IConfigService config)
    {
        _connectionString = config.GetConnectionString();
    }

    public async Task<IDbConnection> GetConnection()
    {
        if (_connection == null)
        {
            _connection = new MySqlConnection(_connectionString);
        }

        if (_connection.State == ConnectionState.Open) 
        {
            return _connection; 
        }

        try
        {
            await _connection.OpenAsync();

            return _connection;
        }
        catch (Exception)
        {
            await _connection.DisposeAsync();
            throw;
        }
    }
}