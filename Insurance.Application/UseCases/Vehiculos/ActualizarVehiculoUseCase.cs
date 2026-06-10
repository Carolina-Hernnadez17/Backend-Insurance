using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insurance.Application.DTOs.Vehiculos;
using Insurance.Application.Interfaces.Repositories;

namespace Insurance.Application.UseCases.Vehiculos;

public class ActualizarVehiculoUseCase
{
    private readonly IVehiculoRepository _repository;

    public ActualizarVehiculoUseCase(
        IVehiculoRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(
        VehiculoUpdateDto dto)
    {
        var vehiculo = await _repository.ObtenerPorIdAsync(dto.Id);

        if (vehiculo == null)
            throw new Exception("Vehículo no encontrado.");

        vehiculo.Marca = dto.Marca;
        vehiculo.Modelo = dto.Modelo;
        vehiculo.Anio = dto.Anio;

        await _repository.ActualizarAsync(vehiculo);
    }
}