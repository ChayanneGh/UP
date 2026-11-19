using Microsoft.EntityFrameworkCore;
using UP.Application.Interface;
using UP.Infrastructure.Context;
using UP.Infrastructure.Entities;

namespace UP.Application.Services;

public class EstacionamientoSrv(IDbContextFactory<UPContext> contextFactory) : IGenericRepository<Estacionamiento>
{
    public async Task<Estacionamiento?> GetIdAsync(int id) => await ReadOne(id);

    public async Task<IEnumerable<Estacionamiento>> GetListAsync() => await Read();

    public async Task<int> CreateAsync(Estacionamiento entity)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Estacionamientos.Add(entity);
        await contexto.SaveChangesAsync();
        return entity.EstacionamientoId;
    }

    public async Task UpdateAsync(Estacionamiento entity)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Estacionamientos.Update(entity);
        await contexto.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id) => await Delete(id);

    public async Task<bool> Create(Estacionamiento estacionamiento)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Estacionamientos.Add(estacionamiento);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<List<Estacionamiento>> Read()
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Estacionamientos.AsNoTracking().ToListAsync();
    }

    public async Task<Estacionamiento?> ReadOne(int id)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Estacionamientos.AsNoTracking().FirstOrDefaultAsync(e => e.EstacionamientoId == id);
    }

    public async Task<bool> Update(int id, Estacionamiento estacionamiento)
    {
        if (id != estacionamiento.EstacionamientoId) return false;
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Estacionamientos.Update(estacionamiento);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Delete(int id)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        var estacionamiento = await contexto.Estacionamientos.FirstOrDefaultAsync(e => e.EstacionamientoId == id);
        if (estacionamiento is null) return false;
        contexto.Estacionamientos.Remove(estacionamiento);
        return await contexto.SaveChangesAsync() > 0;
    }
}
