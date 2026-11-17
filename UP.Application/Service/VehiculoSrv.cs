using Microsoft.EntityFrameworkCore;
using UP.Infrastructure.Context;
using UP.Infrastructure.Entities;

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
        return await contexto.Vehiculos.AsNoTracking().ToListAsync();
    }

    public async Task<Vehiculo?> ReadOne(int id)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Vehiculos.AsNoTracking().FirstOrDefaultAsync(e => e.VehiculoId == id);
    }

    public async Task<bool> Update(int id, Vehiculo vehiculo)
    {
        if (id != vehiculo.VehiculoId) return false;
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Vehiculos.Update(vehiculo);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Delete(int id)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        var vehiculo = await contexto.Vehiculos.FirstOrDefaultAsync(e => e.VehiculoId == id);
        if (vehiculo is null) return false;
        contexto.Vehiculos.Remove(vehiculo);
        return await contexto.SaveChangesAsync() > 0;
    }
}
