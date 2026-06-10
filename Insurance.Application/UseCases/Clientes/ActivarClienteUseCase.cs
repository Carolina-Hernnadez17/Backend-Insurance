using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insurance.Application.Interfaces.Repositories;

namespace Insurance.Application.UseCases.Clientes;

public class ActivarClienteUseCase
{
    private readonly IClienteRepository _repository;

    public ActivarClienteUseCase(
        IClienteRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(
        int clienteId)
    {
        await _repository.ActivarAsync(
            clienteId);
    }
}