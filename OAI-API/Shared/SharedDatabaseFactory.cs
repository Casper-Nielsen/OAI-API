using MySql.Data.MySqlClient;
using OAI_API.Configure;
using System.Data;

namespace OAI_API.Shared;

public class SharedDatabaseFactory : IDatabaseFactory
{
    private string _connectionString;

    public SharedDatabaseFactory(IConfigService config)
    {
        _connectionString = config.GetConnectionString();
    }

    public async Task<IDbConnection> GetConnection()
    {
        var connection = new MySqlConnection(_connectionString);

        try
        {
            await connection.OpenAsync();

            return connection;
        }
        catch (Exception)
        {
            await connection.DisposeAsync();
            throw;
        }
    }
}