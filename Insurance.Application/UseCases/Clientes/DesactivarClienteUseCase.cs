using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insurance.Application.Interfaces.Repositories;

namespace Insurance.Application.UseCases.Clientes;

public class DesactivarClienteUseCase
{
    private readonly IClienteRepository _repository;

    public DesactivarClienteUseCase(
        IClienteRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(int clienteId)
    {
        var cliente = await _repository.ObtenerPorIdAsync(clienteId);

        if (cliente == null)
            throw new Exception("Cliente no encontrado.");

        await _repository.DesactivarAsync(clienteId);
    }
}
