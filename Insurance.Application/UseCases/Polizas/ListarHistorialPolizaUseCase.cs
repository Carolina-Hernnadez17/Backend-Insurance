using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insurance.Application.DTOs.Polizas;
using Insurance.Application.Interfaces.Repositories;

namespace Insurance.Application.UseCases.Polizas;

public class ListarHistorialPolizaUseCase
{
    private readonly
        IHistorialPolizaRepository _repository;

    public ListarHistorialPolizaUseCase(
        IHistorialPolizaRepository repository)
    {
        _repository = repository;
    }

    public async Task<
            IEnumerable<HistorialPolizaResponseDto>>
        ExecuteAsync(int polizaId)
    {
        var historial =
            await _repository
                .ListarPorPolizaAsync(
                    polizaId);

        return historial.Select(h =>
            new HistorialPolizaResponseDto
            {
                Id = h.Id,
                PolizaId = h.PolizaId,
                EstadoAnterior = h.EstadoAnterior,
                EstadoNuevo = h.EstadoNuevo,
                Observacion = h.Observacion,
                UsuarioId = h.UsuarioId,
                FechaCambio = h.FechaCambio
            });
    }
}