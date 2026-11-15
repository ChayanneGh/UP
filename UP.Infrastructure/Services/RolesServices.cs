using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;
using UP.Infrastructure.Models;

namespace UP.Infrastructure.Services;

public class RolesServices
{
    private readonly string _connectionString;
    public RolesServices(string connectionStrings)
    {
        _connectionString = connectionStrings;
    }
    public async Task<IEnumerable<Rol>> GetRolesAsync()
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = "SELECT * FROM Roles";
        var roles = await connection.QueryAsync<Rol>(query);
        return roles;
    }
    public async Task<Rol> GetRolByIdAsync(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = "SELECT * FROM Roles WHERE Id = @Id";
        var rol = await connection.QuerySingleOrDefaultAsync<Rol>(query, new { Id = id });
        return rol;
    }

    public  async Task<int> CreateRolAsync(Rol rol)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = "INSERT INTO Roles (Nombre, Descripcion) VALUES (@Nombre, @Descripcion); SELECT CAST(SCOPE_IDENTITY() as int)";
        var id = await connection.QuerySingleAsync<int>(query, rol);
        return id;
    }

    public async Task<bool> UpdateRolAsync(Rol rol)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = "UPDATE Roles SET Nombre = @Nombre, Descripcion = @Descripcion WHERE Id = @Id";
        var rowsAffected = await connection.ExecuteAsync(query, rol);
        return rowsAffected > 0;
    }   

    public async Task<bool> DeleteRolAsync(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = "DELETE FROM Roles WHERE Id = @Id";
        var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });
        return rowsAffected > 0;
    }
}
