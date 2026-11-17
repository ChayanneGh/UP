using Microsoft.EntityFrameworkCore;
using UP.Infrastructure.Context;
using UP.Infrastructure.Entities;

namespace UP.Application.Service;

public class ReservacioneSrv(IDbContextFactory<P4rquingContext> contextFactory)
{
    public async Task<bool> Create(Reservacione reservacion)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Reservaciones.Add(reservacion);
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

    public async Task<bool> Update(int id, Reservacione reservacion)
    {
        if (id != reservacion.ReservacionId) return false;
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Reservaciones.Update(reservacion);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Delete(int id)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        var reservacion = await contexto.Reservaciones.FirstOrDefaultAsync(e => e.ReservacionId == id);
        if (reservacion is null) return false;
        contexto.Reservaciones.Remove(reservacion);
        return await contexto.SaveChangesAsync() > 0;
    }
}
