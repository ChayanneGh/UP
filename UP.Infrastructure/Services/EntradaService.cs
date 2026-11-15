using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Dapper;
using UP.Infrastructure.Models;


namespace UP.Application.Services;

public class EntradaService
{
    private readonly string _connectionString;
    public EntradaService(string connectionStrings)
    {
        _connectionString = connectionStrings;
    }
public async Task<IEnumerable<Entrada>> GetEntradasAsync()
{
    using var connection = new SqlConnection(_connectionString);
    await connection.OpenAsync();
    var query = "SELECT * FROM Entradas";
    var entradas = await connection.QueryAsync<Entrada>(query);
    return entradas;
}

    public async Task<Entrada> GetEntradaByIdAsync(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = "SELECT * FROM Entradas WHERE Id = @Id";
        var entrada = await connection.QuerySingleOrDefaultAsync<Entrada>(query, new { Id = id });
        return entrada;
    }

    public async Task<int> CreateEntradaAsync(Entrada entrada)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = "INSERT INTO Entradas (Nombre, Descripcion, Fecha) VALUES (@Nombre, @Descripcion, @Fecha); SELECT CAST(SCOPE_IDENTITY() as int)";
        var id = await connection.QuerySingleAsync<int>(query, entrada);
        return id;
    }

    public async Task<bool> UpdateEntradaAsync(Entrada entrada)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = "UPDATE Entradas SET Nombre = @Nombre, Descripcion = @Descripcion, Fecha = @Fecha WHERE Id = @Id";
        var rowsAffected = await connection.ExecuteAsync(query, entrada);
        return rowsAffected > 0;
    }

    public async Task<bool> DeleteEntradaAsync(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = "DELETE FROM Entradas WHERE Id = @Id";
        var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });
        return rowsAffected > 0;
    }



}   


