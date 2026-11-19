using Microsoft.EntityFrameworkCore;
using UP.Infrastructure.Context;
using UP.Infrastructure.Entities;

namespace UP.Application.Service;

public class TiposVehiculoSrv(IDbContextFactory<P4rquingContext> contextFactory)
{
    public async Task<bool> Create(TiposVehiculo tipoVehiculo)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.TiposVehiculos.Add(tipoVehiculo);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<List<TiposVehiculo>> Read()
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.TiposVehiculos.AsNoTracking().ToListAsync();
    }

    public async Task<TiposVehiculo?> ReadOne(int id)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.TiposVehiculos.AsNoTracking().FirstOrDefaultAsync(e => e.TipoVehiculoId == id);
    }

    public async Task<bool> Update(int id, TiposVehiculo tipoVehiculo)
    {
        if (id != tipoVehiculo.TipoVehiculoId) return false;
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.TiposVehiculos.Update(tipoVehiculo);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Delete(int id)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        var tipoVehiculo = await contexto.TiposVehiculos.FirstOrDefaultAsync(e => e.TipoVehiculoId == id);
        if (tipoVehiculo is null) return false;
        contexto.TiposVehiculos.Remove(tipoVehiculo);
        return await contexto.SaveChangesAsync() > 0;
    }
}
