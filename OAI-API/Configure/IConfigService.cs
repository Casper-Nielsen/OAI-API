namespace OAI_API.Configure
{
    public interface IConfigService
    {
        /// <summary>
        /// Gets the connection string to the database
        /// </summary>
        string GetConnectionString();
        /// <summary>
        /// Gets the addess and port for the AI
        /// </summary>
        /// <returns>the address and the port</returns>
        (string address, int port) GetAddressPort();
        /// <summary>
        /// Gets the salt
        /// </summary>
        string GetSalts(string type);
    }
}
