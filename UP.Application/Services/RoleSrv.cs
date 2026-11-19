using Microsoft.EntityFrameworkCore;
using UP.Application.Interface;
using UP.Infrastructure.Context;
using UP.Infrastructure.Entities;

namespace UP.Application.Services;

public class RoleSrv(IDbContextFactory<UPContext> contextFactory) : IGenericRepository<Role>
{
    public async Task<Role?> GetIdAsync(int id) => await ReadOne(id);

    public async Task<IEnumerable<Role>> GetListAsync() => await Read();

    public async Task<int> CreateAsync(Role entity)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Roles.Add(entity);
        await contexto.SaveChangesAsync();
        return entity.RolId;
    }

    public async Task UpdateAsync(Role entity)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Roles.Update(entity);
        await contexto.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id) => await Delete(id);

    public async Task<bool> Create(Role role)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Roles.Add(role);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<List<Role>> Read()
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Roles.AsNoTracking().ToListAsync();
    }

    public async Task<Role?> ReadOne(int id)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        return await contexto.Roles.AsNoTracking().FirstOrDefaultAsync(e => e.RolId == id);
    }

    public async Task<bool> Update(int id, Role role)
    {
        if (id != role.RolId) return false;
        await using var contexto = await contextFactory.CreateDbContextAsync();
        contexto.Roles.Update(role);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Delete(int id)
    {
        await using var contexto = await contextFactory.CreateDbContextAsync();
        var role = await contexto.Roles.FirstOrDefaultAsync(e => e.RolId == id);
        if (role is null) return false;
        contexto.Roles.Remove(role);
        return await contexto.SaveChangesAsync() > 0;
    }
}
