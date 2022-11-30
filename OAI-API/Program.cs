using OAI_API;
using OAI_API.Configure;
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

builder.Services.AddTransient<ILocationRepository, LocationDBRepository>();
builder.Services.AddTransient<IAnswerRepository, AnswerDBRepository>();
builder.Services.AddTransient<IAIRepository, AnswerTCPRepository>();
builder.Services.AddTransient<IQuestionRepository, QuestionDBRepository>();
builder.Services.AddTransient<ILunchRepository, LunchDBRepository>();

builder.Services.AddTransient<IAnswerService, AnswerService>();
builder.Services.AddTransient<IQuestionService, QuestionService>();

builder.Configuration.AddJsonFile("appsettings.json", false, true);

var app = builder.Build();

IdExtension.SetSalt(app.Services.GetService<IConfigService>()?.GetSalts("Question") ?? "text");

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