using Microsoft.EntityFrameworkCore;
using UP.Application.Interface;
using UP.Infrastructure.Context;
using UP.Infrastructure.Entities;

namespace UP.Application.Services;

public class ReservacioneSrv(IDbContextFactory<UPContext> contextFactory) : IGenericRepository<Reservacione>
{
    public async Task<Reservacione?> GetIdAsync(int id) => await ReadOne(id);

    public async Task<IEnumerable<Reservacione>> GetListAsync() => await Read();

    public async Task<int> CreateAsync(Reservacione entity)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Reservaciones.Add(entity);
        await contexto.SaveChangesAsync();
        return entity.ReservacionId;
    }

    public async Task UpdateAsync(Reservacione entity)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Reservaciones.Update(entity);
        await contexto.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id) => await Delete(id);

    public async Task<bool> Create(Reservacione reservacione)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Reservaciones.Add(reservacione);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<List<Reservacione>> Read()
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Reservaciones.AsNoTracking().ToListAsync();
    }

    public async Task<Reservacione?> ReadOne(int id)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Reservaciones.AsNoTracking().FirstOrDefaultAsync(e => e.ReservacionId == id);
    }

    public async Task<bool> Update(int id, Reservacione reservacione)
    {
        if (id != reservacione.ReservacionId) return false;
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Reservaciones.Update(reservacione);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Delete(int id)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        var reservacione = await contexto.Reservaciones.FirstOrDefaultAsync(e => e.ReservacionId == id);
        if (reservacione is null) return false;
        contexto.Reservaciones.Remove(reservacione);
        return await contexto.SaveChangesAsync() > 0;
    }
}
