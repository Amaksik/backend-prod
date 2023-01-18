using MongoDB.Driver;
using Repository.DAL.Repositories;
using Repository.DAL.Interfaces;
using CryptoWidget.Settings;
using CryptoWidget.BLL.Interfaces;
using CryptoWidget.BLL.Clients;
using CryptoWidget.BLL.Services;
using Microsoft.Extensions.DependencyInjection;
using CryptoWidget.BLL.ClientsConfiguration;
using Microsoft.Extensions.Configuration;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<MongoDBSettings>(
    builder.Configuration.GetSection("MongoDBSettings")
);
builder.Services.AddSingleton<IMongoDatabase>(options => {
    var settings = builder.Configuration.GetSection("MongoDBSettings").Get<MongoDBSettings>();
    var client = new MongoClient(settings.ConnectionString);
    return client.GetDatabase(settings.DatabaseName);
});
builder.Services.AddSingleton<IRatesLogRepository, RatesLogRepository>();

builder.Services.Configure<CoinLayerSettings>
    (builder.Configuration.GetSection("ApiClients"));

builder.Services.AddScoped<IRateRequest, CoinLayerClient>();

builder.Services.AddScoped<IRatesLogService, RatesLogService>();
builder.Services.AddScoped<IRateService, RateService>();
builder.Services.AddScoped<IRateRequestHandler, ClientWrapper>();
builder.Services.AddTransient<IRateRequest, CoinLayerClient>();

builder.Services.AddTransient<IChartService, ChartService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//enable cors
var allowedMethods = builder.Configuration.GetSection("CORS-Settings:Allow-Methods").Get<string[]>();
var allowedOrigins = builder.Configuration.GetSection("CORS-Settings:Allow-Origins").Get<string[]>();

var app = builder.Build();

app.UseCors(
        options => options
        .WithMethods(allowedMethods)
        .WithOrigins(allowedOrigins)
      );

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
