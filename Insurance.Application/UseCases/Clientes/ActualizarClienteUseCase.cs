using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insurance.Application.DTOs.Clientes;
using Insurance.Application.Interfaces.Repositories;

namespace Insurance.Application.UseCases.Clientes;

public class ActualizarClienteUseCase
{
    private readonly IClienteRepository _repository;

    public ActualizarClienteUseCase(
        IClienteRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(
        ClienteUpdateDto dto)
    {
        var cliente = await _repository.ObtenerPorIdAsync(dto.Id);

        if (cliente == null)
            throw new Exception("Cliente no encontrado.");

        cliente.Nombres = dto.Nombres;
        cliente.Apellidos = dto.Apellidos;
        cliente.Telefono = dto.Telefono;
        cliente.Correo = dto.Correo;

        await _repository.ActualizarAsync(cliente);
    }
}
