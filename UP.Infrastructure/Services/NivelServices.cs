using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;
using UP.Infrastructure.Models;

namespace UP.Infrastructure.Services;
 public class NivelServices
{
    private readonly string _connectionString;

    public NivelServices(string connectionStrings)
    {
        _connectionString = connectionStrings;
    }

    public async Task<IEnumerable<Nivel>> GetNivelesAsync()
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = "SELECT * FROM Niveles";
        var niveles = await connection.QueryAsync<Nivel>(query);
        return niveles;
    }

    public async Task<Nivel> GetNivelByIdAsync(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = "SELECT * FROM Niveles WHERE Id = @Id";
        var nivel = await connection.QuerySingleOrDefaultAsync<Nivel>(query, new { Id = id });
        return nivel;
    }

}
