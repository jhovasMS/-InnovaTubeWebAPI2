using InnovaTubeWebAPI.Datos;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//var origenesPermitidos = builder.Configuration.GetValue<string>("OrigenesPermitidos")!.Split(",");
var permitirOrigenesEspecificos = "permitirOrigenesEspecificos";

builder.Services.AddCors(opciones =>
{
    opciones.AddPolicy(name: permitirOrigenesEspecificos, politica =>
    {
        politica.WithOrigins("https://innovatube-webapp-production.up.railway.app").AllowAnyHeader().AllowAnyMethod();
    });
});

//var connectionStrings = builder.Configuration.GetValue<string>("ConnectionStrings");
builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
    opciones.UseNpgsql("name=DefaultConnection"));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseCors(permitirOrigenesEspecificos);

app.UseAuthorization();

app.MapControllers();

app.Run();
