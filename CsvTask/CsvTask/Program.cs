using CsvTask.Data;
using CsvTask.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var mongoDbSettings = builder.Configuration.GetSection(nameof(MongoDbSetting));
var mongoDbConfig = mongoDbSettings.Get<MongoDbSetting>();
builder.Services.Configure<MongoDbSetting>(mongoDbSettings);
builder.Services.AddSingleton<IMongoDbSetting>(sp =>
              sp.GetRequiredService<IOptions<MongoDbSetting>>().Value);
builder.Services.AddScoped<IMongoClient, MongoClient>(sp =>
{
    return new MongoClient(mongoDbConfig.ConnectionString);
});

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IDataSeedService, DataSeedService>();

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using var scope = app.Services.CreateScope();
var seedingService = scope.ServiceProvider.GetRequiredService<IDataSeedService>();
await seedingService.Seed();
scope.Dispose();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
