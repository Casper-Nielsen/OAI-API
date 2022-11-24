namespace OAI_API
{
    internal static class ConfigureCORS
    {
        private const string CorsPolicyName = "AllowAllOrigins";

        /// <summary>
        /// Sets up Cors to allow anyconnections
        /// </summary>
        public static void AddCustomCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicyName, builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                    builder.SetPreflightMaxAge(TimeSpan.FromMinutes(60));
                });
            });
        }

        /// <summary>
        /// Sets it to use the custom cors
        /// </summary>
        public static void UseCustomCors(this IApplicationBuilder app)
        {
            app.UseCors(CorsPolicyName);
        }
    }
}
