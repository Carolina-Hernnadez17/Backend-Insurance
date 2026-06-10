using Insurance.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Application.UseCases.Vehiculos;

public class DesactivarVehiculoUseCase
{
    private readonly IVehiculoRepository _repository;

    public DesactivarVehiculoUseCase(
        IVehiculoRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(
        int id)
    {
        await _repository.DesactivarAsync(id);
    }
}  
