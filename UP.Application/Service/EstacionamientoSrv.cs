using Microsoft.EntityFrameworkCore;
using UP.Infrastructure.Context;
using UP.Infrastructure.Entities;

namespace UP.Application.Service;

public class EstacionamientoSrv(IDbContextFactory<P4rquingContext> contextFactory)
{
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
