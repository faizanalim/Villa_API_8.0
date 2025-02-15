using AutoMapper;
using MagicVilla_VillaAPI.Repository;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Villa_API;
using Villa_API.Data;
using Villa_API.Repository.IRepository;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Serilog
//Log.Logger = new LoggerConfiguration().MinimumLevel.Debug()
//            .WriteTo.File("log/villalogs.txt", rollingInterval: RollingInterval.Month).CreateLogger();
//builder.Host.UseSerilog();

builder.Services.AddDbContext<ApplicationDbContext>(option => {
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
});
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddScoped<IVillaRepository, VillaRepository>();
builder.Services.AddScoped<IVillaNumberRepository, VillaNumberRepository>();
builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddControllers(option => {
  //  option.ReturnHttpNotAcceptable = true;
}).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddSingleton<ILogging, Logging>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
