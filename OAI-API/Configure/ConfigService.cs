namespace OAI_API.Configure
{
    public class ConfigService : IConfigService
    {
        private readonly IConfigurationRoot _configuration;
        private readonly IWebHostEnvironment _env;

        public ConfigService(IWebHostEnvironment env)
        {
            _env = env;

            // Sets the builder to require the appsettings
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true);

            _configuration = builder.Build();
        }

        public string GetConnectionString()
        {
            var connectionString = _configuration.GetConnectionString("default");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString), $@"default connection string is empty");
            }

            return connectionString;
        }

        public (string address, int port) GetAddressPort()
        {
            var sect = _configuration.GetSection("Addresses").GetSection("default");

            var address = sect.GetValue<string>("address");
            var port = sect.GetValue<int>("port");

            if (string.IsNullOrEmpty(address))
            {
                throw new ArgumentNullException(nameof(address), $@"default address string is empty");
            }

            if (port == 0)
            {
                throw new ArgumentNullException(nameof(port), "default port is zero");
            }

            return (address, port);
        }
    }
}
