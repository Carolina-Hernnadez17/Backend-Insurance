using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insurance.Application.DTOs.Polizas;
using Insurance.Domain.Entities;

namespace Insurance.Application.Interfaces.Repositories;

public interface IPolizaRepository
{
    Task<int> CrearAsync(
        Poliza poliza,
        int vehiculoId,
        int usuarioId);

    Task CancelarAsync(
        int polizaId,
        int usuarioId,
        string observacion);

    Task SuspenderAsync(
        int polizaId,
        int usuarioId,
        string observacion);

    Task<IEnumerable<Poliza>>
        ListarPorClienteAsync(
            int clienteId);

    Task<IEnumerable<PolizaListDto>>
        ListarCompletoAsync();

    Task ReactivarAsync(
        int polizaId,
        int usuarioId,
        string observacion);

    Task<IEnumerable<Poliza>> ListarAsync();

    Task<Poliza?> ObtenerPorIdAsync(int id);

    Task<bool>
        VehiculoTienePolizaActivaAsync(
            int vehiculoId);
}