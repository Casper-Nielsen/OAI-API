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
    }
}
