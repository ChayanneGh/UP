using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using System.Text.Json.Serialization;
using UP.Application.Services;
using UP.Infrastructure.Context;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("AzureConnection");

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTodo", policy =>
    {
        policy.AllowAnyOrigin()  
              .AllowAnyMethod()   
              .AllowAnyHeader();  
    });
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddOpenApi();

builder.Services.AddDbContextFactory<UPContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<EntradaSrv>();
builder.Services.AddScoped<SalidaSrv>();
builder.Services.AddScoped<EstacionamientoSrv>();
builder.Services.AddScoped<NiveleSrv>();
builder.Services.AddScoped<ReservacioneSrv>();
builder.Services.AddScoped<RoleSrv>();
builder.Services.AddScoped<TarifaSrv>();
builder.Services.AddScoped<TiposVehiculoSrv>();
builder.Services.AddScoped<TransaccioneSrv>();
builder.Services.AddScoped<UsuarioSrv>();
builder.Services.AddScoped<VehiculoSrv>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseCors("PermitirTodo");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
