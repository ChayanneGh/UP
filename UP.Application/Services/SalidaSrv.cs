using Microsoft.EntityFrameworkCore;
using UP.Application.Interface;
using UP.Infrastructure.Context;
using UP.Infrastructure.Entities;

namespace UP.Application.Services;

public class SalidaSrv(IDbContextFactory<UPContext> contextFactory) : IGenericRepository<Salida>
{
    public async Task<Salida?> GetIdAsync(int id) => await ReadOne(id);

    public async Task<IEnumerable<Salida>> GetListAsync() => await Read();

    public async Task<int> CreateAsync(Salida entity)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Salidas.Add(entity);
        await contexto.SaveChangesAsync();
        return entity.SalidaId;
    }

    public async Task UpdateAsync(Salida entity)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Salidas.Update(entity);
        await contexto.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id) => await Delete(id);

    public async Task<bool> Create(Salida salida)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Salidas.Add(salida);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<List<Salida>> Read()
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Salidas.AsNoTracking().ToListAsync();
    }

    public async Task<Salida?> ReadOne(int Id)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Salidas.AsNoTracking().FirstOrDefaultAsync(e => e.SalidaId == Id);
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
