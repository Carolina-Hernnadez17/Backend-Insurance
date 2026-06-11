using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insurance.Application.Interfaces.Repositories;

namespace Insurance.Application.UseCases.Polizas;

public class ListarPolizasUseCase
{
    private readonly IPolizaRepository _repository;

    public ListarPolizasUseCase(
        IPolizaRepository repository)
    {
        _repository = repository;
    }

    public async Task<object>
        ExecuteAsync()
    {
        return await _repository
            .ListarCompletoAsync();
    }
}