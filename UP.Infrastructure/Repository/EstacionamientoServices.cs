using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;
using UP.Infrastructure.Models;
using UP.Infrastructure.Interface;  

namespace UP.Infrastructure.Repository;

public class EstacionamientoService
{
    private readonly string _connectionString;
    public EstacionamientoService(string connectionString) => _connectionString = connectionString;

    public async Task<IEnumerable<Estacionamiento>> GetListAsync()
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = "SELECT * FROM Estacionamientos";
        var estacionamientos = await connection.QueryAsync<Estacionamiento>(query);
        return estacionamientos;
    }

    public async Task<Estacionamiento?> GetIdAsync(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = "SELECT * FROM Estacionamientos WHERE Id = @Id";
        var estacionamiento = await connection.QuerySingleOrDefaultAsync<Estacionamiento>(query, new { Id = id });
        return estacionamiento;
    }       

    public async Task<int> CreateAsync(Estacionamiento entity)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = "INSERT INTO Estacionamientos (Nombre, Ubicacion, Capacidad) VALUES (@Nombre, @Ubicacion, @Capacidad); SELECT CAST(SCOPE_IDENTITY() as int)";
        var id = await connection.QuerySingleAsync<int>(query, entity);
        return id;
    }

    public async Task<bool> UpdateAsync(Estacionamiento entity)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = "UPDATE Estacionamientos SET Nombre = @Nombre, Ubicacion = @Ubicacion, Capacidad = @Capacidad WHERE Id = @Id";
        var rowsAffected = await connection.ExecuteAsync(query, entity);
        return rowsAffected > 0;
    }

}
