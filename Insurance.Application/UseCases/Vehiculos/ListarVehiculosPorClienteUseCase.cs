using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insurance.Application.Interfaces.Repositories;

namespace Insurance.Application.UseCases.Vehiculos;

public class ListarVehiculosPorClienteUseCase
{
    private readonly IVehiculoRepository _repository;

    public ListarVehiculosPorClienteUseCase(
        IVehiculoRepository repository)
    {
        _repository = repository;
    }

    public async Task<object>
        ExecuteAsync(int clienteId)
    {
        return await _repository
            .ListarPorClienteAsync(
                clienteId);
    }
}