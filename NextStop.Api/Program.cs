using System.Text.Json;
using System.Text.Json.Serialization;
using NextStop.Common;
using NextStop.Dal.Ado;
using NextStop.Dal.Interface;
using NextStop.Service;
using NextStop.Service.Interfaces;
using NextStop.Service.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

IConfiguration configuration = ConfigurationUtil.GetConfiguration();
builder.Services.AddSingleton<IConnectionFactory>(sp => DefaultConnectionFactory.FromConfiguration(configuration, "NextStopDbConnection", "ProviderName"));

builder.Services.AddScoped<IHolidayService, HolidayService>();
builder.Services.AddScoped<IHolidayDao, HolidayDao>();

builder.Services.AddScoped<IRouteService, RouteService>();
builder.Services.AddScoped<IRouteDao, RouteDao>();

builder.Services.AddScoped<IRouteStopPointService, RouteStopPointService>();
builder.Services.AddScoped<IRouteStopPointDao, RouteStopPointDao>();

builder.Services.AddScoped<IStopPointService, StopPointService>();
builder.Services.AddScoped<IStopPointDao, StopPointDao>();

builder.Services.AddScoped<ITripCheckInService, TripCheckInService>();
builder.Services.AddScoped<ITripCheckinDao, TripCheckinDao>();

builder.Services.AddScoped<ITripService, TripService>();
builder.Services.AddScoped<ITripDao, TripDao>();


builder.Services.AddControllers(options => options.ReturnHttpNotAcceptable = true).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    // JSON-Daten sind nicht case-sensitiv, z. B. "Name" und "name" werden gleich behandelt.
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    // JSON-Objekte verwenden CamelCase (z. B. `orderDate` statt `OrderDate`).
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    // Serialisiert Enums als Strings (z. B. `Rating.A` wird zu "A").
}).AddXmlDataContractSerializerFormatters();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Code aus OrderManagement
// builder.Services.AddRouting(options => options.LowercaseUrls = true);
// // Konfiguriert, dass alle URLs klein geschrieben werden, z. B. `/api/orders` statt `/Api/Orders`.
//
// builder.Services.AddOpenApiDocument(settings => settings.Title = "Order Management API");
// // Fügt OpenAPI/Swagger-Dokumentation hinzu und legt den Titel der API-Dokumentation fest.

builder.Services.AddCors(
    builder => builder.AddDefaultPolicy(policy =>
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
    )
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseOpenApi(); ???
}

app.UseHttpsRedirection();

//app.UseCors(); ???

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();