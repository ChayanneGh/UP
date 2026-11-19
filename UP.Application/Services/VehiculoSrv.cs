using Microsoft.EntityFrameworkCore;
using UP.Application.Interface;
using UP.Infrastructure.Context;
using UP.Infrastructure.Entities;

namespace UP.Application.Services;

public class VehiculoSrv(IDbContextFactory<UPContext> contextFactory) : IGenericRepository<Vehiculo>
{
    public async Task<Vehiculo?> GetIdAsync(int id) => await ReadOne(id);

    public async Task<IEnumerable<Vehiculo>> GetListAsync() => await Read();

    public async Task<int> CreateAsync(Vehiculo entity)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Vehiculos.Add(entity);
        await contexto.SaveChangesAsync();
        return entity.VehiculoId;
    }

    public async Task UpdateAsync(Vehiculo entity)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Vehiculos.Update(entity);
        await contexto.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id) => await Delete(id);

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
