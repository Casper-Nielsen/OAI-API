namespace OAI_API
{
    internal static class ConfigureCORS
    {
        private const string CorsPolicyName = "AllowAllOrigins";

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

        public static void UseCustomCors(this IApplicationBuilder app)
        {
            app.UseCors(CorsPolicyName);
        }
    }
}
