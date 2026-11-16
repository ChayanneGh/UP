using UP.Infrastructure.Context;
using UP.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace UP.Application.Service;

public class UsuarioSrv(IDbContextFactory<P4rquingContext> contextFactory)
{
    public async Task<bool> Create(Usuario usuario)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();

        contexto.Usuarios.Add(usuario);
        return await contexto.SaveChangesAsync() > 0;
    }
    public async Task<List<Usuario>> Read()
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();

        return await contexto.Usuarios
            .AsNoTracking()
            .ToListAsync();
    }
    public async Task<Usuario?> ReadOne(int Id)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();

        return await contexto.Usuarios
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.UsuarioId == Id);
    }
    public async Task<bool> Update(Usuario usuario)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();

        contexto.Usuarios.Update(usuario);
        return await contexto.SaveChangesAsync() > 0;
    }
    public async Task<bool> Delete(int Id)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();

        var usuario = await contexto.Usuarios.FirstOrDefaultAsync(e => e.UsuarioId == Id);

        if (usuario is null) return false;

        contexto.Usuarios.Remove(usuario);
        return await contexto.SaveChangesAsync() > 0;
    }
}