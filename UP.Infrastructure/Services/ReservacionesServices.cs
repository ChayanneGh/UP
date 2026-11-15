using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;
using UP.Infrastructure.Models;

namespace UP.Infrastructure.Services;

internal class ReservacionesServices
{
    private readonly string _connectionString;
    public ReservacionesServices(string connectionStrings)
    {
        _connectionString = connectionStrings;
    }
    public async Task<IEnumerable<Reservacion>> GetReservacionesAsync()
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = "SELECT * FROM Reservaciones";
        var reservaciones = await connection.QueryAsync<Reservacion>(query);
        return reservaciones;
    }
    public async Task<Reservacion> GetReservacionByIdAsync(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = "SELECT * FROM Reservaciones WHERE Id = @Id";
        var reservacion = await connection.QuerySingleOrDefaultAsync<Reservacion>(query, new { Id = id });
        return reservacion;
    }
    public async Task<int> CreateReservacionAsync(Reservacion reservacion)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = "INSERT INTO Reservaciones (IdUsuario, IdEstacionamiento, IdNivel, FechaInicio, FechaFin) VALUES (@IdUsuario, @IdEstacionamiento, @IdNivel, @FechaInicio, @FechaFin); SELECT CAST(SCOPE_IDENTITY() as int)";
        var id = await connection.QuerySingleAsync<int>(query, reservacion);
        return id;
    }

    public async Task<bool> UpdateReservacionAsync(Reservacion reservacion)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = "UPDATE Reservaciones SET IdUsuario = @IdUsuario, IdEstacionamiento = @IdEstacionamiento, IdNivel = @IdNivel, FechaInicio = @FechaInicio, FechaFin = @FechaFin WHERE Id = @Id";
        var rowsAffected = await connection.ExecuteAsync(query, reservacion);
        return rowsAffected > 0;
    }

    public async Task<bool> DeleteReservacionAsync(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = "DELETE FROM Reservaciones WHERE Id = @Id";
        var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });
        return rowsAffected > 0;
    }

}
