using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Dapper;
using UP.Infrastructure.Models;
using UP.Infrastructure.Interface;
     

namespace UP.Infrastructure.Repository;

public class EntradaService : IGenericRepository<Entrada>
{
    private readonly string _connectionFactory;
    public EntradaService(ConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<IEnumerable<Entrada>> GetListAsync()
{
    using var connection = new SqlConnection(_connectionFactory);
    await connection.OpenAsync();
    var query = "SELECT * FROM Entradas";
    var entradas = await connection.QueryAsync<Entrada>(query);
    return entradas;
}

   public async Task<Entrada?> GetIdAsync(int id) 
    {
        using var connection = new SqlConnection(_connectionFactory);
        await connection.OpenAsync();
        var query = "SELECT * FROM Entradas WHERE Id = @Id";
        var entrada = await connection.QuerySingleOrDefaultAsync<Entrada>(query, new { Id = id });
        return entrada;
    }

    public async Task<int> CreateAsync(Entrada entity)
    {
        using var connection = new SqlConnection(_connectionFactory);
        await connection.OpenAsync();
        var query = "INSERT INTO Entradas (Nombre, Descripcion, Fecha) VALUES (@Nombre, @Descripcion, @Fecha); SELECT CAST(SCOPE_IDENTITY() as int)";
        var id = await connection.QuerySingleAsync<int>(query, entity);
        return id;
    }

    public async Task<bool> UpdateAsync(Entrada entity)
    {
        using var connection = new SqlConnection(_connectionFactory);
        await connection.OpenAsync();
        var query = "UPDATE Entradas SET Nombre = @Nombre, Descripcion = @Descripcion, Fecha = @Fecha WHERE Id = @Id";
        var rowsAffected = await connection.ExecuteAsync(query, entity );
        return rowsAffected > 0;
    }

     public async Task<bool> DeleteAsync(int id)
    {
        using var connection = new SqlConnection(_connectionFactory);
        await connection.OpenAsync();
        var query = "DELETE FROM Entradas WHERE Id = @Id";
        var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });
        return rowsAffected > 0;
    }

}


