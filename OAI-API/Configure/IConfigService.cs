namespace OAI_API.Configure
{
    public interface IConfigService
    {
        /// <summary>
        /// Gets the connection string to the database
        /// </summary>
        string GetConnectionString();
    }
}
