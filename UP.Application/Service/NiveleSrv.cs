using Microsoft.EntityFrameworkCore;
using UP.Infrastructure.Context;
using UP.Infrastructure.Entities;

namespace UP.Application.Service;

public class NiveleSrv(IDbContextFactory<P4rquingContext> contextFactory)
{
    public async Task<bool> Create(Nivele nivel)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Niveles.Add(nivel);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<List<Nivele>> Read()
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Niveles.AsNoTracking().ToListAsync();
    }

    public async Task<Nivele?> ReadOne(int id)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Niveles.AsNoTracking().FirstOrDefaultAsync(e => e.NivelId == id);
    }

    public async Task<bool> Update(int id, Nivele nivel)
    {
        if (id != nivel.NivelId) return false;
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Niveles.Update(nivel);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Delete(int id)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        var nivel = await contexto.Niveles.FirstOrDefaultAsync(e => e.NivelId == id);
        if (nivel is null) return false;
        contexto.Niveles.Remove(nivel);
        return await contexto.SaveChangesAsync() > 0;
    }
}
