using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insurance.Application.Interfaces.Repositories;
using Insurance.Domain.Entities;

namespace Insurance.Application.UseCases.Vehiculos;

public class ListarVehiculosUseCase
{
    private readonly IVehiculoRepository _repository;

    public ListarVehiculosUseCase(
        IVehiculoRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Vehiculo>> ExecuteAsync()
    {
        return await _repository.ListarAsync();
    }
}