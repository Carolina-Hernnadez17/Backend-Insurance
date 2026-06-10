using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insurance.Application.Interfaces.Repositories;

namespace Insurance.Application.UseCases.Polizas;

public class SuspenderPolizaUseCase
{
    private readonly IPolizaRepository _repository;

    public SuspenderPolizaUseCase(
        IPolizaRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(
        int polizaId,
        int usuarioId,
        string observacion)
    {
        await _repository.SuspenderAsync(
            polizaId,
            usuarioId,
            observacion);
    }
}