using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace TicketFlow.Service.Dapper;

public class DapperContext: IDapperContext
{
    private readonly string _connectionString;

    public DapperContext(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }
    public IDbConnection CreateConnection()
        => new NpgsqlConnection(_connectionString);
}