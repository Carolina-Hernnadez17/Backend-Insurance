using System.Data;
using Dapper;
using Insurance.Application.Interfaces.Repositories;
using Insurance.Domain.Entities;
using Insurance.Infrastructure.Data;

namespace Insurance.Infrastructure.Repositories;

public class ClienteRepository
    : IClienteRepository
{
    private readonly SqlConnectionFactory _factory;

    public ClienteRepository(
        SqlConnectionFactory factory)
    {
        _factory = factory;
    }

    public async Task ActivarAsync(
        int id)
    {
        using var connection =
            _factory.CreateConnection();

        await connection.ExecuteAsync(
            "sp_Cliente_Activar",
            new { Id = id },
            commandType:
            CommandType.StoredProcedure);
    }

    public async Task<int> CrearAsync(
        Cliente cliente)
    {
        using var connection =
            _factory.CreateConnection();

        return await connection
            .ExecuteScalarAsync<int>(
                "sp_Cliente_Crear",
                new
                {
                    cliente.Nombres,
                    cliente.Apellidos,
                    cliente.Documento,
                    cliente.Telefono,
                    cliente.Correo
                },
                commandType:
                CommandType.StoredProcedure);
    }

    public async Task ActualizarAsync(
        Cliente cliente)
    {
        using var connection =
            _factory.CreateConnection();

        await connection.ExecuteAsync(
            "sp_Cliente_Actualizar",
            new
            {
                cliente.Id,
                cliente.Nombres,
                cliente.Apellidos,
                cliente.Telefono,
                cliente.Correo
            },
            commandType:
            CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<Cliente>>
        ListarAsync()
    {
        using var connection =
            _factory.CreateConnection();

        return await connection
            .QueryAsync<Cliente>(
                "sp_Cliente_Listar",
                commandType:
                CommandType.StoredProcedure);
    }

    public async Task<Cliente?> ObtenerPorIdAsync(int id)
    {
        using var connection =
            _factory.CreateConnection();

        return await connection.QueryFirstOrDefaultAsync<Cliente>(
            "sp_Cliente_ObtenerPorId",
            new
            {
                Id = id
            },
            commandType:
            CommandType.StoredProcedure);
    }

    public async Task DesactivarAsync(
        int id)
    {
        using var connection =
            _factory.CreateConnection();

        await connection.ExecuteAsync(
            "sp_Cliente_Desactivar",
            new { Id = id },
            commandType:
            CommandType.StoredProcedure);
    }
}