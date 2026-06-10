using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insurance.Application.Interfaces.Repositories;
using Insurance.Domain.Entities;

namespace Insurance.Application.UseCases.Clientes;

public class ListarClientesUseCase
{
    private readonly IClienteRepository _repository;

    public ListarClientesUseCase(
        IClienteRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Cliente>> ExecuteAsync()
    {
        return await _repository.ListarAsync();
    }
}