using OAI_API;
using OAI_API.Configure;
using OAI_API.Manager;
using OAI_API.Repositories;
using OAI_API.Services;
using OAI_API.Shared;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCustomCors();

builder.Services.AddSingleton<IConfigService>(new ConfigService(builder.Environment));
builder.Services.AddSingleton<IDatabaseFactory, SharedDatabaseFactory>();
builder.Services.AddTransient<IAnswerRepository, AnswerTCPRepository>();
builder.Services.AddTransient<IAnswerService, AnswerService>();
builder.Services.AddTransient<IAnwserManager, AnswerManager>();

builder.Services.AddTransient<ILocationRepository, LocationDBRepository>();
builder.Services.AddTransient<ILocationService, LocationService>();

builder.Configuration.AddJsonFile("appsettings.json", false, true);

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCustomCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();