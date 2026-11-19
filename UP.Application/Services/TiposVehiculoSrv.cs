using Microsoft.EntityFrameworkCore;
using UP.Application.Interface;
using UP.Infrastructure.Context;
using UP.Infrastructure.Entities;

namespace UP.Application.Services;

public class TiposVehiculoSrv(IDbContextFactory<UPContext> contextFactory) : IGenericRepository<TiposVehiculo>
{
    public async Task<TiposVehiculo?> GetIdAsync(int id) => await ReadOne(id);

    public async Task<IEnumerable<TiposVehiculo>> GetListAsync() => await Read();

    public async Task<int> CreateAsync(TiposVehiculo entity)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.TiposVehiculos.Add(entity);
        await contexto.SaveChangesAsync();
        return entity.TipoVehiculoId;
    }

    public async Task UpdateAsync(TiposVehiculo entity)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.TiposVehiculos.Update(entity);
        await contexto.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id) => await Delete(id);

    public async Task<bool> Create(TiposVehiculo tiposVehiculo)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.TiposVehiculos.Add(tiposVehiculo);
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

    public async Task<bool> Update(int id, TiposVehiculo tiposVehiculo)
    {
        if (id != tiposVehiculo.TipoVehiculoId) return false;
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.TiposVehiculos.Update(tiposVehiculo);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Delete(int id)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        var tiposVehiculo = await contexto.TiposVehiculos.FirstOrDefaultAsync(e => e.TipoVehiculoId == id);
        if (tiposVehiculo is null) return false;
        contexto.TiposVehiculos.Remove(tiposVehiculo);
        return await contexto.SaveChangesAsync() > 0;
    }
}
