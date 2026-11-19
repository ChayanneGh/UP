using Microsoft.EntityFrameworkCore;
using UP.Application.Interface;
using UP.Infrastructure.Context;
using UP.Infrastructure.Entities;

namespace UP.Application.Services;

public class TarifaSrv(IDbContextFactory<UPContext> contextFactory) : IGenericRepository<Tarifa>
{
    public async Task<Tarifa?> GetIdAsync(int id) => await ReadOne(id);

    public async Task<IEnumerable<Tarifa>> GetListAsync() => await Read();

    public async Task<int> CreateAsync(Tarifa entity)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Tarifas.Add(entity);
        await contexto.SaveChangesAsync();
        return entity.TarifaId;
    }

    public async Task UpdateAsync(Tarifa entity)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Tarifas.Update(entity);
        await contexto.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id) => await Delete(id);

    public async Task<bool> Create(Tarifa tarifa)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Tarifas.Add(tarifa);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<List<Tarifa>> Read()
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Tarifas.AsNoTracking().ToListAsync();
    }

    public async Task<Tarifa?> ReadOne(int id)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Tarifas.AsNoTracking().FirstOrDefaultAsync(e => e.TarifaId == id);
    }

    public async Task<bool> Update(int id, Tarifa tarifa)
    {
        if (id != tarifa.TarifaId) return false;
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Tarifas.Update(tarifa);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Delete(int id)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        var tarifa = await contexto.Tarifas.FirstOrDefaultAsync(e => e.TarifaId == id);
        if (tarifa is null) return false;
        contexto.Tarifas.Remove(tarifa);
        return await contexto.SaveChangesAsync() > 0;
    }
}
