using UP.Infrastructure.Context;
using UP.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace UP.Application.Service;

public class SalidaSrv(IDbContextFactory<P4rquingContext> contextFactory)
{
    public async Task<bool> Create(Salida salida)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();

        contexto.Salidas.Add(salida);
        return await contexto.SaveChangesAsync() > 0;
    }
    public async Task<List<Salida>> Read()
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();

        return await contexto.Salidas
            .AsNoTracking()
            .ToListAsync();
    }
    public async Task<Salida?> ReadOne(int Id)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();

        return await contexto.Salidas
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.SalidaId == Id);
    }
    public async Task<bool> Update(int Id, Salida salida)
    {
        if (Id != salida.SalidaId) return false;
        await using var contexto = await contextFactory.CreateDbContextAsync();

        contexto.Salidas.Update(salida);
        return await contexto.SaveChangesAsync() > 0;
    }
    public async Task<bool> Delete(int Id)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();

        var salida = await contexto.Salidas.FirstOrDefaultAsync(e => e.SalidaId == Id);

        if (salida is null) return false;

        contexto.Salidas.Remove(salida);
        return await contexto.SaveChangesAsync() > 0;
    }
}
