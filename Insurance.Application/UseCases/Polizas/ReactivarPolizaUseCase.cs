using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insurance.Application.Interfaces.Repositories;

namespace Insurance.Application.UseCases.Polizas;

public class ReactivarPolizaUseCase
{
    private readonly IPolizaRepository _repository;

    public ReactivarPolizaUseCase(
        IPolizaRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(
        int polizaId,
        int usuarioId,
        string observacion)
    {
        await _repository.ReactivarAsync(
            polizaId,
            usuarioId,
            observacion);
    }
}