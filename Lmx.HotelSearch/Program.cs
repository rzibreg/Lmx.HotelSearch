using Lmx.HotelSearch.API.Middleware;
using MediatR;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using Lmx.HotelSearch.Application.Service;
using Lmx.HotelSearch.Application.Validators;
using Lmx.HotelSearch.Domain.Repository;
using Lmx.HotelSearch.Infrastructure;

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

Log.Information("Lmx.HotelSearch Starting up...");

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
    {
        loggerConfiguration
            .ReadFrom.Configuration(hostingContext.Configuration)
            .Enrich.FromLogContext()
            .WriteTo.Console();
    });

    builder.Services.AddScoped<IHotelService, HotelService>();
    builder.Services.AddSingleton<IHotelRepository, HotelRepository>();
    builder.Services.AddScoped<IHotelValidator, HotelValidator>();

    builder.Services.AddMediatR(AppDomain.CurrentDomain.Load("Lmx.HotelSearch.Application"));
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.Load("Lmx.HotelSearch.Application"));

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Lmx.HotelSearch.API", Version = "v1", Description = "Hotel Search Demo" });
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);

        c.AddSecurityDefinition(ApiKeyAuth.APIKEY, new OpenApiSecurityScheme
        {
            Description = "Api key needed to access the endpoints. ApiKey: 123",
            In = ParameterLocation.Header,
            Name = ApiKeyAuth.APIKEY,
            Type = SecuritySchemeType.ApiKey
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Name = ApiKeyAuth.APIKEY,
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header,
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = ApiKeyAuth.APIKEY
                    }
                },
                new string[] {}
            }
        });
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lmx.HotelSearch.API v1"));
    }

    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseAuthorization();

    app.UseMiddleware<ApiKeyAuth>();
    app.UseMiddleware<ErrorHandler>();

    app.MapControllers();
    app.Run();
}
catch (Exception ex)
{
    if (Log.Logger.GetType().Name == "SilentLogger")
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .CreateLogger();
    }

    Log.Fatal(ex, "Lmx.HotelSearch Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

