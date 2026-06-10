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

public class ProductoRepository
    : IProductoRepository
{
    private readonly SqlConnectionFactory _factory;

    public ProductoRepository(
        SqlConnectionFactory factory)
    {
        _factory = factory;
    }

    public async Task<IEnumerable<Producto>>
        ListarAsync()
    {
        using var connection =
            _factory.CreateConnection();

        return await connection
            .QueryAsync<Producto>(
                "sp_Producto_Listar",
                commandType:
                CommandType.StoredProcedure);
    }
}