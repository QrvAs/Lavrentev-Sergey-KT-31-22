using LavrentevKT3122lb1.Database;
using LavrentevKT3122lb1.Extensions;
using LavrentevKT3122lb1.Middlewares;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try
{
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();
    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();


    builder.Services.AddDbContext<TeacherDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("TeacherDbConnection")));
   
    builder.Services.AddDbServices();

    builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = null; 
            options.JsonSerializerOptions.WriteIndented = true; 
        });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseMiddleware<ExceptionHandlerMiddleware>();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();

}
catch(Exception ex)
{
    logger.Error(ex, "Stopped program because expection");
}
finally
{
    LogManager.Shutdown();
}