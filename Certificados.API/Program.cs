using Certificados.API.AutoMappers;
using Certificados.API.Controllers.Core;
using Certificados.API.Filters;
using Certificados.Extensions;
using Certificados.Infra;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Certificados.Infra.Core.Initialization;
using Certificados.Services.Core.Initialization;

var builder = WebApplication.CreateBuilder(args);
var versao = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;


builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", false, true)
   // .AddJsonFile($"settings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", false, true)
    .AddJsonFile("Assets/ConfiguracaoEspecificaApolice.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();



// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ApiModelValidationFilter>(1);
    options.Filters.Add<ApiExceptionFilter>(2);
});

//EF
var connectionString = builder.Configuration.GetConnectionString("CertificadosDb") ?? "";
builder.Services.AddDbContext<ApplicationContext>(x =>
        x.UseSqlServer(connectionString,
                       b =>
                       {
                           b.MigrationsAssembly(typeof(ApplicationContext).Assembly.GetName().Name);
                           b.MigrationsHistoryTable("_CertificadosMigrations");
                       }
                       ).UseLazyLoadingProxies()
 );



//AutoMapper
builder.Services.AddAutoMapper(typeof(MapProfile).Assembly);

//Repositories / UnitOfWork / Globalization
builder.Services.AddInfraDependencies();

//Servicos 
builder.Services.AddDomainServicesDependencies();


//JWT
JWTExtensions.JWTConfigure(builder.Services, builder.Configuration);

//builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

SwaggerExtensions.SwaggerGenExtension(builder.Services, builder.Configuration);


builder.Services.AddRouting(options => options.LowercaseUrls = true);


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ResponseTimeMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
);

app.Run();


