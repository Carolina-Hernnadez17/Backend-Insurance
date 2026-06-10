using System.Data;
using Dapper;
using Insurance.Application.Interfaces.Repositories;
using Insurance.Domain.Entities;
using Insurance.Infrastructure.Data;

namespace Insurance.Infrastructure.Repositories;

public class HistorialPolizaRepository
    : IHistorialPolizaRepository
{
    private readonly SqlConnectionFactory _factory;

    public HistorialPolizaRepository(
        SqlConnectionFactory factory)
    {
        _factory = factory;
    }

    public async Task<IEnumerable<HistorialPoliza>>
        ListarPorPolizaAsync(
            int polizaId)
    {
        using var connection =
            _factory.CreateConnection();

        return await connection.QueryAsync<HistorialPoliza>(
            "sp_HistorialPoliza_ListarPorPoliza",
            new
            {
                PolizaId = polizaId
            },
            commandType:
            CommandType.StoredProcedure);
    }
}