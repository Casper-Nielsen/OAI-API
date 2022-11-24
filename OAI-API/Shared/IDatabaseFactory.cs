using System.Data;

namespace OAI_API.Shared
{
    public interface IDatabaseFactory
    {
        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <returns></returns>
        Task<IDbConnection> GetConnection();
    }
}
