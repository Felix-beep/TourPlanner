using log4net.Config;
using TourPlanner.BL;
using TourPlanner.DAL;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

XmlConfigurator.Configure(new FileInfo("log4net.cfg"));

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();
var repo = new MemoryTourRepository();
//var repo = new PsqlTourRepository(
//    new PsqlContext(configuration, "TestDbContext"));

//repo.PopulateWithSampleData();

var mapQuestClient = new MapQuestClient();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<ITourRepository>(repo);
builder.Services.AddSingleton(configuration);
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
