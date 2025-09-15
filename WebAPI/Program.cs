using Application;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Contexts;
using Shared;
using WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Servicios a matricular
builder.Services.AgregarCapaAplicacion();
builder.Services.AgregarSharedInfrastructure(builder.Configuration);
builder.Services.AgregarPersistenceInfrastructure(builder.Configuration);
builder.Services.AgregarVersionamientoAPIExtension();

builder.Services.AddCors(options =>
{
    options.AddPolicy("NuevaPolitica", app =>
    {
        app.WithOrigins("https://grupo-horeb.vercel.app")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//Agregamos Mddleware
app.UseErrorHandlingMiddleware();

app.UseCors("NuevaPolitica");

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}
app.Run();
