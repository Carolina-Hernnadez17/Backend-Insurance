using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insurance.Application.DTOs.Clientes;
using Insurance.Application.Interfaces.Repositories;
using Insurance.Domain.Entities;

namespace Insurance.Application.UseCases.Clientes;

public class CrearClienteUseCase
{
    private readonly IClienteRepository _repository;

    public CrearClienteUseCase(
        IClienteRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> ExecuteAsync(
        ClienteCreateDto dto)
    {
        var cliente = new Cliente
        {
            Nombres = dto.Nombres,
            Apellidos = dto.Apellidos,
            Documento = dto.Documento,
            Telefono = dto.Telefono,
            Correo = dto.Correo,
            Activo = true,
            FechaCreacion = DateTime.Now
        };

        return await _repository.CrearAsync(cliente);
    }
}
