using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insurance.Application.DTOs.Vehiculos;
using Insurance.Application.Interfaces.Repositories;
using Insurance.Domain.Entities;

namespace Insurance.Application.UseCases.Vehiculos;

public class CrearVehiculoUseCase
{
    private readonly IVehiculoRepository _repository;

    public CrearVehiculoUseCase(
        IVehiculoRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> ExecuteAsync(
        VehiculoCreateDto dto)
    {
        var vehiculo = new Vehiculo
        {
            ClienteId = dto.ClienteId,
            Placa = dto.Placa,
            Marca = dto.Marca,
            Modelo = dto.Modelo,
            Anio = dto.Anio,
            Activo = true,
            FechaCreacion = DateTime.Now
        };

        return await _repository.CrearAsync(vehiculo);
    }
}
