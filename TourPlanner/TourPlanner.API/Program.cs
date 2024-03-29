using log4net.Config;
using TourPlanner.BL;
using TourPlanner.DAL;
using static Org.BouncyCastle.Math.EC.ECCurve;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

XmlConfigurator.Configure(new FileInfo("log4net.cfg"));

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var repo = new PsqlTourRepository(
    new PsqlContext(configuration, "TestDbContext"));
repo.PopulateWithSampleData();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton(configuration);
builder.Services.AddSingleton<ITourRepository>(repo);
builder.Services.AddSingleton<IImageCache, FileSystemImageCache>();
builder.Services.AddSingleton<IRouteClient, MapQuestClient>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
