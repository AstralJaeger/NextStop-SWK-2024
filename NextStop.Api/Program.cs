using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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

builder.Services.AddScoped<IRoutingService, RoutingService>();

builder.Services.AddControllers(options => options.ReturnHttpNotAcceptable = true)
    .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            // JSON-Daten sind nicht case-sensitiv, z. B. "Name" und "name" werden gleich behandelt.
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            // JSON-Objekte verwenden CamelCase (z. B. `orderDate` statt `OrderDate`).
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            // Serialisiert Enums als Strings (z. B. `Rating.A` wird zu "A").
        })
    .AddXmlDataContractSerializerFormatters();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var allowSpecificOriginPolicy = "_AllowAllOrigins";
builder.Services.AddCors(
    b => b.AddPolicy(allowSpecificOriginPolicy,
        policy =>
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
    )
);

builder.Services.AddAuthentication(opts =>
    {
        opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(opts =>
    {
        opts.RequireHttpsMetadata = false;
        opts.Authority = "http://localhost:8090/realms/nextstop";
        opts.Audience = "account";
        opts.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "http://localhost:8090/realms/nextstop",
            ValidateAudience = true,
            ValidateLifetime = true
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = ""
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

var app = builder.Build();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors(allowSpecificOriginPolicy);
app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await app.RunAsync();