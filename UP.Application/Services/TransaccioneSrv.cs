using Microsoft.EntityFrameworkCore;
using UP.Application.Interface;
using UP.Infrastructure.Context;
using UP.Infrastructure.Entities;

namespace UP.Application.Services;

public class TransaccioneSrv(IDbContextFactory<UPContext> contextFactory) : IGenericRepository<Transaccione>
{
    public async Task<Transaccione?> GetIdAsync(int id) => await ReadOne(id);

    public async Task<IEnumerable<Transaccione>> GetListAsync() => await Read();

    public async Task<int> CreateAsync(Transaccione entity)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Transacciones.Add(entity);
        await contexto.SaveChangesAsync();
        return entity.TransaccionId;
    }

    public async Task UpdateAsync(Transaccione entity)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Transacciones.Update(entity);
        await contexto.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id) => await Delete(id);

    public async Task<bool> Create(Transaccione transaccion)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Transacciones.Add(transaccion);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<List<Transaccione>> Read()
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Transacciones.AsNoTracking().ToListAsync();
    }

    public async Task<Transaccione?> ReadOne(int id)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Transacciones.AsNoTracking().FirstOrDefaultAsync(e => e.TransaccionId == id);
    }

    public async Task<bool> Update(int id, Transaccione transaccion)
    {
        if (id != transaccion.TransaccionId) return false;
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Transacciones.Update(transaccion);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Delete(int id)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        var transaccion = await contexto.Transacciones.FirstOrDefaultAsync(e => e.TransaccionId == id);
        if (transaccion is null) return false;
        contexto.Transacciones.Remove(transaccion);
        return await contexto.SaveChangesAsync() > 0;
    }
}
