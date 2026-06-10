using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insurance.Application.DTOs.Polizas;
using Insurance.Application.Interfaces.Repositories;
using Insurance.Domain.Constants;
using Insurance.Domain.Entities;

namespace Insurance.Application.UseCases.Polizas;

public class CrearPolizaUseCase
{
    private readonly IPolizaRepository _repository;
    private readonly IClienteRepository _clienteRepository;

    public CrearPolizaUseCase(
        IPolizaRepository repository,
        IClienteRepository clienteRepository)
    {
        _repository = repository;
        _clienteRepository = clienteRepository;
    }

    public async Task<int> ExecuteAsync(
        PolizaCreateDto dto,
        int usuarioId)
    {
        if (dto.FechaFin <= dto.FechaInicio)
        {
            throw new Exception(
                "La fecha fin debe ser mayor que la fecha inicio.");
        }

        var tienePoliza =
            await _repository
                .VehiculoTienePolizaActivaAsync(
                    dto.VehiculoId);

        if (tienePoliza)
        {
            throw new Exception(
                "El vehículo ya posee una póliza activa.");
        }

        var poliza = new Poliza
        {
            NumeroPoliza =
                $"POL-{DateTime.Now:yyyyMMddHHmmss}",

            ClienteId = dto.ClienteId,

            ProductoId = dto.ProductoId,

            FechaInicio = dto.FechaInicio,

            FechaFin = dto.FechaFin,

            Estado = Estados.Activa,

            FechaCreacion = DateTime.Now
        };

        var cliente =
            await _clienteRepository
                .ObtenerPorIdAsync(dto.ClienteId);

        if (cliente == null)
        {
            throw new Exception(
                "Cliente no existe");
        }

        if (!cliente.Activo)
        {
            throw new Exception(
                "No se puede emitir póliza para un cliente inactivo");
        }

        return await _repository.CrearAsync(
            poliza,
            dto.VehiculoId,
            usuarioId); ;
    }
}