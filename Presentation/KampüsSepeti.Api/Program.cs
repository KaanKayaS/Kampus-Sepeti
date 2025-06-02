using KampüsSepeti.Persistence;
using KampüsSepeti.Application;
using KampüsSepeti.Application.Exceptions;
using KampüsSepeti.Infrastructure;
using Microsoft.AspNetCore.Cors;
using KampüsSepeti.Application.Hubs;
using KampüsSepeti.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using KampüsSepeti.Application.Beheviors;
using MediatR;
using KampüsSepeti.Application.Interfaces.Service;
using KampüsSepeti.Api.Services;
using Newtonsoft.Json.Linq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    // Diğer Swagger konfigürasyonları...

    // Sadece bir kez Bearer security definition ekliyoruz
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Please enter JWT with Bearer into field",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
    });

    // Sadece bir kez security requirement ekliyoruz
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});


builder.Services.AddTransient(typeof(IFileService), typeof(FileService));

builder.Services.AddSignalR();

// cors ayarlarını ekleyelim
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
         builder =>
        {
             builder
                  .WithOrigins("http://localhost:5027")
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials();
        });
});


builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var env = builder.Environment;  // launchsetting jsondaki ASPNETCORE_ENVIRONMENT ortamını alacaktır.

builder.Configuration
    .SetBasePath(env.ContentRootPath)                      // api nin olduğu path'i alacaktır, sunucuda yayınladığımızda path'i farklı olabilir
    .AddJsonFile("appsettings.json", optional: false)     // appsettings'i her türlü görmesini zorunlu kılıyoruz
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);  // fakat bunu prod ve development diye ayırmamış olabiliriz, bu zorunlu değildir

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplicationService(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// wwwroot klasörünün varlığını kontrol et
var wwwrootPath = Path.Combine(app.Environment.ContentRootPath, "wwwroot");
if (!Directory.Exists(wwwrootPath))
{
    Directory.CreateDirectory(wwwrootPath);
}

// CORS middleware'ini en başta kullan
app.UseCors("AllowFrontend");

app.UseHttpsRedirection();

// Statik dosyaları servis et
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

// Authentication ve Authorization sırası önemli
app.UseAuthentication();
app.UseAuthorization();


app.ConfigureExceptionHandlingMiddleware(); // exception middleware tanımlama

app.MapControllers();

app.Run();
