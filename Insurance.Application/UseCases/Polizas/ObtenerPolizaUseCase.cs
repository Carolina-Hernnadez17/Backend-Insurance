using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insurance.Application.Interfaces.Repositories;

namespace Insurance.Application.UseCases.Polizas;

public class ObtenerPolizaUseCase
{
    private readonly IPolizaRepository _repository;

    public ObtenerPolizaUseCase(
        IPolizaRepository repository)
    {
        _repository = repository;
    }

    public async Task<object?>
        ExecuteAsync(
            int id)
    {
        return await _repository
            .ObtenerPorIdAsync(id);
    }
}