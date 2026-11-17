using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using UP.Application.Service;
using UP.Infrastructure.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContextFactory<P4rquingContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AzureConnection")));

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
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapOpenApi();
app.MapScalarApiReference();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
