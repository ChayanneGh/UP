using UP.Infrastructure.Context;
using UP.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace UP.Application.Service;

public class RoleSrv(IDbContextFactory<P4rquingContext> contextFactory)
{
    public async Task<bool> Create(Role role)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();

        contexto.Roles.Add(role);
        return await contexto.SaveChangesAsync() > 0;
    }
    public async Task<List<Role>> Read()
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();

        return await contexto.Roles
            .AsNoTracking()
            .ToListAsync();
    }
    public async Task<Role?> ReadOne(int Id)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();

        return await contexto.Roles
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.RolId == Id);
    }
    public async Task<bool> Update(Role role)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();

        contexto.Roles.Update(role);
        return await contexto.SaveChangesAsync() > 0;
    }
    public async Task<bool> Delete(int Id)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();

        var role = await contexto.Roles.FirstOrDefaultAsync(e => e.RolId == Id);

        if (role is null) return false;

        contexto.Roles.Remove(role);
        return await contexto.SaveChangesAsync() > 0;
    }
}
