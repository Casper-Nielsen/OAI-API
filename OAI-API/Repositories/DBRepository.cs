using OAI_API.Shared;

namespace OAI_API.Repositories
{
    /// <summary>
    /// Base class for data base related repositories 
    /// </summary>
    public abstract class DBRepository
    {
        protected readonly IDatabaseFactory _connectionFactory;

        public DBRepository(IDatabaseFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
    }
}
