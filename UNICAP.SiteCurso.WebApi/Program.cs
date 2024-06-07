using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.IO;
using System.Reflection;
using System.Text.Json.Serialization;
using UNICAP.SiteCurso.Application.IoC;
using UNICAP.SiteCurso.Application.Jwt;
using UNICAP.SiteCurso.Infrastructure.IoC;

var builder = WebApplication.CreateBuilder(args);

ConfigureCors(builder);
ConfigureMvc(builder);
ConfigureServices(builder);
ConfigureSession(builder);
ConfigureSwagger(builder);
builder.Services.InfrastructureRegister(builder.Configuration);
builder.Services.AppRegister(builder.Configuration);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

var app = builder.Build();

var isDevelopment = builder.Configuration.GetValue<bool>("isDevelopment");

app.UseCors("AllowedWebHosts");

if (app.Environment.IsDevelopment() || isDevelopment)
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint(
        "/swagger/v1/swagger.json",
        "UNICAP.SiteCurso.API v1"));
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuth();
app.UseSession();

app.MapControllers();

app.Run();

#region Cors
void ConfigureCors(WebApplicationBuilder builder)
{
    //builder.Services.AddCors(config => {
    //    config.AddPolicy("AllowedWebHosts", opt => opt.WithOrigins(builder.Configuration.GetSection("AllowedWebHosts").Get<string[]>()).AllowAnyMethod().AllowAnyHeader());
    //});
    builder.Services.AddCors(config => {
        config.AddPolicy("AllowedWebHosts", opt => opt.WithOrigins(builder.Configuration.GetSection("AllowedWebHosts").Get<string[]>()).AllowAnyMethod().AllowAnyHeader());
    });
}
#endregion

#region MVC
void ConfigureMvc(WebApplicationBuilder builder)
{
    builder.Services.AddControllers()
        .AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling =
                Newtonsoft.Json.ReferenceLoopHandling.Ignore)
    .AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
    });
    builder.Services.AddEndpointsApiExplorer();
}
#endregion

#region Services
void ConfigureServices(WebApplicationBuilder builder)
{
    builder.Services.AddEndpointsApiExplorer();
}
#endregion

#region Session
void ConfigureSession(WebApplicationBuilder builder)
{
    builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    builder.Services.AddHttpContextAccessor();

    builder.Services.AddDistributedMemoryCache();
    builder.Services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromMinutes(10);
        options.Cookie.Name = "UserLogged";
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
    });
    builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
    builder.Services.AddAuth(builder.Configuration.GetSection("Jwt").Get<JwtSettings>());
}
#endregion

#region Swagger
void ConfigureSwagger(WebApplicationBuilder builder)
{
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "UNICAP.SiteCurso.API",
            Description = "Site do Curso Sistemas Para Internet",
            Version = "v1",
            License = new OpenApiLicense
            {
                Name = "MIT",
                Url = new Uri("https://opensource.org/licenses/MIT")
            },
            Contact = new OpenApiContact
            {
                Name = "Desenvolvimento",
                Email = "paulobarreto.dev@gmail.com",
                Url = new Uri("https://www.github.com/paulobarretodev/")
            }
        });

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);

        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using Bearer scheme."
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
                new string[] {}
            }
        });

    });
}
#endregion