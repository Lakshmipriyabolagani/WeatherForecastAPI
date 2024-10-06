using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using WeatherForecastAPI.ApiServices;
using WeatherForecastAPI.ConfigurationClasses;



var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration["WeatherAPI"];

builder.Services.Configure<WeatherApiConfigDetails>(
    builder.Configuration.GetSection("WeatherApiConfigDetails"));

builder.Services.Configure<MongoDBclass>(
    builder.Configuration.GetSection("MongoDBclass"));

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddHttpClient<WeatherAPIServices>();

builder.Services.AddScoped<MongoDBServices>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
