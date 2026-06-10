using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insurance.Application.Interfaces.Repositories;
using Insurance.Domain.Entities;

namespace Insurance.Application.UseCases.Productos;

public class ListarProductosUseCase
{
    private readonly IProductoRepository _repository;

    public ListarProductosUseCase(
        IProductoRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Producto>> ExecuteAsync()
    {
        return await _repository.ListarAsync();
    }
}
