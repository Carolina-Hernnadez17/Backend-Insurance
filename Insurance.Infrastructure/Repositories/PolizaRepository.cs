using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Insurance.Application.Interfaces.Repositories;
using Insurance.Domain.Entities;
using Insurance.Infrastructure.Data;

namespace Insurance.Infrastructure.Repositories;

public class PolizaRepository
    : IPolizaRepository
{
    private readonly SqlConnectionFactory _factory;

    public PolizaRepository(
        SqlConnectionFactory factory)
    {
        _factory = factory;
    }

    public async Task<int> CrearAsync(
        Poliza poliza,
        int vehiculoId,
        int usuarioId)
    {
        using var connection =
            _factory.CreateConnection();

        return await connection
            .ExecuteScalarAsync<int>(
                "sp_Poliza_Crear",
                new
                {
                    poliza.NumeroPoliza,
                    poliza.ClienteId,
                    poliza.ProductoId,
                    VehiculoId = vehiculoId,
                    poliza.FechaInicio,
                    poliza.FechaFin,
                    UsuarioId = usuarioId
                },
                commandType:
                CommandType.StoredProcedure);
    }

    public async Task CancelarAsync(
        int polizaId,
        int usuarioId,
        string observacion)
    {
        using var connection =
            _factory.CreateConnection();

        await connection.ExecuteAsync(
            "sp_Poliza_Cancelar",
            new
            {
                PolizaId = polizaId,
                UsuarioId = usuarioId,
                Observacion = observacion
            },
            commandType:
            CommandType.StoredProcedure);
    }

    public async Task SuspenderAsync(
        int polizaId,
        int usuarioId,
        string observacion)
    {
        using var connection =
            _factory.CreateConnection();

        await connection.ExecuteAsync(
            "sp_Poliza_Suspender",
            new
            {
                PolizaId = polizaId,
                UsuarioId = usuarioId,
                Observacion = observacion
            },
            commandType:
            CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<Poliza>>
        ListarPorClienteAsync(
            int clienteId)
    {
        using var connection =
            _factory.CreateConnection();

        return await connection
            .QueryAsync<Poliza>(
                "sp_Poliza_ListarPorCliente",
                new
                {
                    ClienteId = clienteId
                },
                commandType:
                CommandType.StoredProcedure);
    }

    public async Task ReactivarAsync(
        int polizaId,
        int usuarioId,
        string observacion)
    {
        using var connection =
            _factory.CreateConnection();

        await connection.ExecuteAsync(
            "sp_Poliza_Reactivar",
            new
            {
                PolizaId = polizaId,
                UsuarioId = usuarioId,
                Observacion = observacion
            },
            commandType:
            CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<Poliza>>
        ListarAsync()
    {
        using var connection =
            _factory.CreateConnection();

        return await connection.QueryAsync<Poliza>(
            "sp_Poliza_Listar",
            commandType:
            CommandType.StoredProcedure);
    }

    public async Task<Poliza?>
        ObtenerPorIdAsync(
            int id)
    {
        using var connection =
            _factory.CreateConnection();

        return await connection
            .QueryFirstOrDefaultAsync<Poliza>(
                "sp_Poliza_ObtenerPorId",
                new
                {
                    Id = id
                },
                commandType:
                CommandType.StoredProcedure);
    }

    public async Task<bool>
        VehiculoTienePolizaActivaAsync(
            int vehiculoId)
    {
        using var connection =
            _factory.CreateConnection();

        return await connection
            .ExecuteScalarAsync<bool>(
                "sp_Poliza_VehiculoTieneActiva",
                new
                {
                    VehiculoId = vehiculoId
                },
                commandType:
                CommandType.StoredProcedure);
    }
}