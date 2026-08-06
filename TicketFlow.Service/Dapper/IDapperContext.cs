using System.Data;

namespace TicketFlow.Service.Dapper;

public interface IDapperContext
{
    IDbConnection CreateConnection();
}