﻿namespace OAI_API.Configure
{
    public interface IConfigService
    {
        /// <summary>
        /// Gets the connection string to the database
        /// </summary>
        string GetConnectionString();
        (string address, int port) GetAddressPort();
        /// <summary>
        /// Gets the salt
        /// </summary>
        string GetSalts(string type);
    }
}
