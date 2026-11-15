using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;
using UP.Infrastructure.Models;
using UP.Infrastructure.Interface;

namespace UP.Infrastructure.Repository;
 public class NivelServices
{
    private readonly string _connectionString;

    public NivelServices(string connectionStrings)
    {
        _connectionString = connectionStrings;
    }

 
    public async Task<IEnumerable<Nivele>> GetListAsync()
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = "SELECT * FROM Niveles";
        var niveles = await connection.QueryAsync<Nivele>(query);
        return niveles;
    }

    public async Task<Nivele?> GetIdAsync(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync();
        var query = "SELECT * FROM Niveles WHERE Id = @Id";
        var nivel = await connection.QuerySingleOrDefaultAsync<Nivele>(query, new { Id = id });
        return nivel;
    }

}
