using Microsoft.EntityFrameworkCore;
using UP.Application.Interface;
using UP.Infrastructure.Context;
using UP.Infrastructure.Entities;

namespace UP.Application.Services;

public class NiveleSrv(IDbContextFactory<UPContext> contextFactory) : IGenericRepository<Nivele>
{
    public async Task<Nivele?> GetIdAsync(int id) => await ReadOne(id);

    public async Task<IEnumerable<Nivele>> GetListAsync() => await Read();

    public async Task<int> CreateAsync(Nivele entity)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Niveles.Add(entity);
        await contexto.SaveChangesAsync();
        return entity.NivelId;
    }

    public async Task UpdateAsync(Nivele entity)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Niveles.Update(entity);
        await contexto.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id) => await Delete(id);

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
