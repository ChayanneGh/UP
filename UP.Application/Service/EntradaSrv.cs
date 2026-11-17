using UP.Infrastructure.Context;
using UP.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace UP.Application.Service;

public class EntradaSrv(IDbContextFactory<P4rquingContext> contextFactory)
{
    public async Task<bool> Create(Entrada entrada)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();

        contexto.Entradas.Add(entrada);
        return await contexto.SaveChangesAsync() > 0;
    }
    public async Task<List<Entrada>> Read()
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();

        return await contexto.Entradas
            .AsNoTracking()
            .ToListAsync();
    }
    public async Task<Entrada?> ReadOne(int Id)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();

        return await contexto.Entradas
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.EntradaId == Id);
    }
    public async Task<bool> Update(int Id, Entrada entrada)
    {
        if (Id != entrada.EntradaId) return false;

        await using var contexto = await contextFactory.CreateDbContextAsync();

        contexto.Entradas.Update(entrada);
        return await contexto.SaveChangesAsync() > 0;
    }
    public async Task<bool> Delete(int Id)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();

        var entrada = await contexto.Entradas.FirstOrDefaultAsync(e => e.EntradaId == Id);

        if (entrada is null) return false;

        contexto.Entradas.Remove(entrada);
        return await contexto.SaveChangesAsync() > 0;
    }
}
