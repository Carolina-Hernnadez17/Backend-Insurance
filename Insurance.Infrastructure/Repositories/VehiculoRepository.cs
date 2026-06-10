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

public class VehiculoRepository
    : IVehiculoRepository
{
    private readonly SqlConnectionFactory _factory;

    public VehiculoRepository(
        SqlConnectionFactory factory)
    {
        _factory = factory;
    }

    public async Task<int> CrearAsync(
        Vehiculo vehiculo)
    {
        using var connection =
            _factory.CreateConnection();

        return await connection
            .ExecuteScalarAsync<int>(
                "sp_Vehiculo_Crear",
                new
                {
                    vehiculo.ClienteId,
                    vehiculo.Placa,
                    vehiculo.Marca,
                    vehiculo.Modelo,
                    vehiculo.Anio
                },
                commandType:
                CommandType.StoredProcedure);
    }

    public async Task ActualizarAsync(
        Vehiculo vehiculo)
    {
        using var connection =
            _factory.CreateConnection();

        await connection.ExecuteAsync(
            "sp_Vehiculo_Actualizar",
            new
            {
                vehiculo.Id,
                vehiculo.Marca,
                vehiculo.Modelo,
                vehiculo.Anio
            },
            commandType:
            CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<Vehiculo>>
        ListarAsync()
    {
        using var connection =
            _factory.CreateConnection();

        return await connection
            .QueryAsync<Vehiculo>(
                "sp_Vehiculo_Listar",
                commandType:
                CommandType.StoredProcedure);
    }

    public async Task<Vehiculo?>
        ObtenerPorIdAsync(int id)
    {
        using var connection =
            _factory.CreateConnection();

        return await connection
            .QueryFirstOrDefaultAsync<Vehiculo>(
                "sp_Vehiculo_ObtenerPorId",
                new { Id = id },
                commandType:
                CommandType.StoredProcedure);
    }

    public async Task DesactivarAsync(
        int id)
    {
        using var connection =
            _factory.CreateConnection();

        await connection.ExecuteAsync(
            "sp_Vehiculo_Desactivar",
            new
            {
                Id = id
            },
            commandType:
            CommandType.StoredProcedure);
    }
}