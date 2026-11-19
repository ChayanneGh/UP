using Microsoft.EntityFrameworkCore;
using UP.Application.Interface;
using UP.Infrastructure.Context;
using UP.Infrastructure.Entities;

namespace UP.Application.Services;

public class UsuarioSrv(IDbContextFactory<UPContext> contextFactory) : IGenericRepository<Usuario>
{
    public async Task<Usuario?> GetIdAsync(int id) => await ReadOne(id);

    public async Task<IEnumerable<Usuario>> GetListAsync() => await Read();

    public async Task<int> CreateAsync(Usuario entity)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Usuarios.Add(entity);
        await contexto.SaveChangesAsync();
        return entity.UsuarioId;
    }

    public async Task UpdateAsync(Usuario entity)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Usuarios.Update(entity);
        await contexto.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id) => await Delete(id);

    public async Task<bool> Create(Usuario usuario)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Usuarios.Add(usuario);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<List<Usuario>> Read()
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Usuarios.AsNoTracking().ToListAsync();
    }

    public async Task<Usuario?> ReadOne(int id)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Usuarios.AsNoTracking().FirstOrDefaultAsync(e => e.UsuarioId == id);
    }

    public async Task<bool> Update(int id, Usuario usuario)
    {
        if (id != usuario.UsuarioId) return false;
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Usuarios.Update(usuario);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Delete(int id)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        var usuario = await contexto.Usuarios.FirstOrDefaultAsync(e => e.UsuarioId == id);
        if (usuario is null) return false;
        contexto.Usuarios.Remove(usuario);
        return await contexto.SaveChangesAsync() > 0;
    }
}
