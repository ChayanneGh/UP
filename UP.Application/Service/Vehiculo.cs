using UP.Infrastructure.Context;
using UP.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace UP.Application.Service;

public class VehiculoSrv(IDbContextFactory<P4rquingContext> contextFactory)
{
    public async Task<bool> Create(Vehiculo vehiculo)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();

        contexto.Vehiculos.Add(vehiculo);
        return await contexto.SaveChangesAsync() > 0;
    }
    public async Task<List<Vehiculo>> Read()
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();

        return await contexto.Vehiculos
            .AsNoTracking()
            .ToListAsync();
    }
    public async Task<Vehiculo?> ReadOne(int Id)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();

        return await contexto.Vehiculos
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.VehiculoId == Id);
    }
    public async Task<bool> Update(Vehiculo vehiculo)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();

        contexto.Vehiculos.Update(vehiculo);
        return await contexto.SaveChangesAsync() > 0;
    }
    public async Task<bool> Delete(int Id)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();

        var vehiculo = await contexto.Vehiculos.FirstOrDefaultAsync(e => e.VehiculoId == Id);

        if (vehiculo is null) return false;

        contexto.Vehiculos.Remove(vehiculo);
        return await contexto.SaveChangesAsync() > 0;
    }
}