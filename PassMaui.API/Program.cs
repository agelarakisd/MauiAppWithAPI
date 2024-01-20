using Microsoft.OpenApi.Models;
using PassMaui.Infrastructure;
using PassMaui.Infrastructure.Configuration;
using PassMaui.Infrastructure.Migrations;

var configuration = Configuration.Create<Program>();
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.UseAllOfToExtendReferenceSchemas();
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pass Maui API", Version = "v1", Contact = new OpenApiContact { Email = "tonphres@gmail.com", Name = "SKASE" } });
});

builder.Services.AddOpenApiDocument();

new InfrastructureCollectionModule().Configure(builder.Services, configuration);

var app = builder.Build();

// Add services to the container.
MigrationHelper.Migrate(typeof(Migration_0001_NewStart).Assembly, configuration.GetConnectionString("PassMauiDb"));
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c=>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pass Maui API");
    });
}

app.UseOpenApi();
//app.UseSwaggerUi3();
    
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();