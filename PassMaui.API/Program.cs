using PassMaui.Infrastructure;
using PassMaui.Infrastructure.Configuration;
using PassMaui.Infrastructure.Migrations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = Configuration.Create<Program>();

var app = builder.Build();

// Add services to the container.
MigrationHelper.Migrate(typeof(Migration_0001_NewStart).Assembly, configuration.GetConnectionString("PassMauiDb"));
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