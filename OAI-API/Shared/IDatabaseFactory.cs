using System.Data;

namespace OAI_API.Shared
{
    public interface IDatabaseFactory
    {
        Task<IDbConnection> GetConnection();
    }
}
