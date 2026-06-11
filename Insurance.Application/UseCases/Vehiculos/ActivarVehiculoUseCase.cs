using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insurance.Application.Interfaces.Repositories;

namespace Insurance.Application.UseCases.Vehiculos;

public class ActivarVehiculoUseCase
{
    private readonly IVehiculoRepository _repository;

    public ActivarVehiculoUseCase(
        IVehiculoRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(int id)
    {
        await _repository.ActivarAsync(id);
    }
}