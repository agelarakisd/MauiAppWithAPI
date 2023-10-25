using PassMaui.Infrastructure;
using PassMaui.Infrastructure.Migrations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
MigrationHelper.Migrate(typeof(Migration_0001_NewStart).Assembly, "DESKTOP-GHM1E7N\\SQLEXPRESS;database=PassMaui;trusted_connection=true"
);

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();