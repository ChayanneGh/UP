using Microsoft.EntityFrameworkCore;
using UP.Application.Interface;
using UP.Infrastructure.Context;
using UP.Infrastructure.Entities;

namespace UP.Application.Services;

public class EntradaSrv(IDbContextFactory<UPContext> contextFactory) : IGenericRepository<Entrada>
{
    // ==========================================
    // IGenericRepository Implementation
    // ==========================================
    public async Task<Entrada?> GetIdAsync(int id)
    {
        return await ReadOne(id);
    }

    public async Task<IEnumerable<Entrada>> GetListAsync()
    {
        return await Read();
    }

    public async Task<int> CreateAsync(Entrada entity)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Entradas.Add(entity);
        await contexto.SaveChangesAsync();
        return entity.EntradaId;
    }

    public async Task UpdateAsync(Entrada entity)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Entradas.Update(entity);
        await contexto.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        await Delete(id);
    }

    // ==========================================
    // Legacy / Direct Methods (Used by Controllers)
    // ==========================================
    public async Task<bool> Create(Entrada entrada)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Entradas.Add(entrada);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<List<Entrada>> Read()
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Entradas.AsNoTracking().ToListAsync();
    }

    public async Task<Entrada?> ReadOne(int Id)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Entradas.AsNoTracking().FirstOrDefaultAsync(e => e.EntradaId == Id);
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
