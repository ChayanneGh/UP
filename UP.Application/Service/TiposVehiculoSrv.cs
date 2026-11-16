using UP.Infrastructure.Context;
using UP.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace UP.Application.Service;

public class TiposVehiculoSrv(IDbContextFactory<P4rquingContext> contextFactory)
{
    public async Task<bool> Create(TiposVehiculo tiposVehiculo)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();

        contexto.TiposVehiculos.Add(tiposVehiculo);
        return await contexto.SaveChangesAsync() > 0;
    }
    public async Task<List<TiposVehiculo>> Read()
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();

        return await contexto.TiposVehiculos
            .AsNoTracking()
            .ToListAsync();
    }
    public async Task<TiposVehiculo?> ReadOne(int Id)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();

        return await contexto.TiposVehiculos
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.TipoVehiculoId == Id);
    }
    public async Task<bool> Update(TiposVehiculo tiposVehiculo)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();

        contexto.TiposVehiculos.Update(tiposVehiculo);
        return await contexto.SaveChangesAsync() > 0;
    }
    public async Task<bool> Delete(int Id)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();

        var tiposVehiculo = await contexto.TiposVehiculos.FirstOrDefaultAsync(e => e.TipoVehiculoId == Id);

        if (tiposVehiculo is null) return false;

        contexto.TiposVehiculos.Remove(tiposVehiculo);
        return await contexto.SaveChangesAsync() > 0;
    }
}