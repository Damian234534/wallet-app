using WalletApp.Server.Infrastructure;
using Microsoft.EntityFrameworkCore;

using Serilog;
using Microsoft.AspNetCore.Builder;

Log.Logger = new LoggerConfiguration()
    .WriteTo.File("logs/log.txt",
    rollingInterval: RollingInterval.Day,
    rollOnFileSizeLimit: true)
    .CreateLogger();

var configuration = new ConfigurationBuilder()
    .AddEnvironmentVariables()
    .AddCommandLine(args)
    .AddJsonFile("appsettings.json")
    .Build();

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.Bind(configuration);
builder.Services.Configure<ConfigSettings>(builder.Configuration);

builder.Logging.AddSerilog();
builder.Services.AddLogging();

builder.WebHost.UseUrls(urls: builder.Configuration[nameof(ConfigSettings.WEB_HOST_URL)]!);

var app = builder.Build();

app.Run();