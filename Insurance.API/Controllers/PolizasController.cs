using Asp.Versioning;
using Insurance.Application.DTOs.Polizas;
using Insurance.Application.UseCases.Polizas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Insurance.Application.DTOs.Poliza;

namespace Insurance.Api.Controllers;

[Authorize]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/polizas")]
public class PolizasController : ControllerBase
{
    private readonly CrearPolizaUseCase _crear;
    private readonly CancelarPolizaUseCase _cancelar;
    private readonly SuspenderPolizaUseCase _suspender;
    private readonly ListarHistorialPolizaUseCase _historial;
    private readonly ReactivarPolizaUseCase _reactivar;
    private readonly ListarPolizasUseCase _listarTodas;
    private readonly ObtenerPolizaUseCase _obtener;

    public PolizasController(
        CrearPolizaUseCase crear,
        CancelarPolizaUseCase cancelar,
        SuspenderPolizaUseCase suspender,
        ReactivarPolizaUseCase reactivar,
        ListarPolizasUseCase listarTodas,
        ObtenerPolizaUseCase obtener,
        ListarHistorialPolizaUseCase historial)
    {
        _crear = crear;
        _cancelar = cancelar;
        _suspender = suspender;
        _reactivar = reactivar;
        _listarTodas = listarTodas;
        _obtener = obtener;
        _historial = historial;
    }

    [Authorize(Roles = "Administrador,Ejecutivo")]
    [HttpPost]
    public async Task<IActionResult> Crear(
        [FromBody] PolizaCreateDto dto)
    {
        var usuarioId =
            int.Parse(
                User.FindFirst(
                        ClaimTypes.NameIdentifier)!
                    .Value);

        var id =
            await _crear.ExecuteAsync(
                dto,
                usuarioId);

        return Ok(new
        {
            Id = id,
            Mensaje = "Póliza creada correctamente"
        });
    }

    [Authorize(Roles = "Administrador")]
    [HttpPut("cancelar")]
    public async Task<IActionResult> Cancelar(
        [FromBody] PolizaCancelarDto dto)
    {
        var usuarioId =
            int.Parse(
                User.FindFirst(
                        ClaimTypes.NameIdentifier)!
                    .Value);

        await _cancelar.ExecuteAsync(
            dto.PolizaId,
            usuarioId,
            dto.Observacion);

        return Ok(new
        {
            Mensaje = "Póliza cancelada correctamente"
        });
    }

    [Authorize(Roles = "Administrador,Ejecutivo,Consulta")]
    [HttpGet("{polizaId}/historial")]
    public async Task<IActionResult>
        Historial(
            int polizaId)
    {
        return Ok(
            await _historial.ExecuteAsync(
                polizaId));
    }

    [Authorize(Roles = "Administrador")]
    [HttpPut("suspender")]
    public async Task<IActionResult> Suspender(
        [FromBody] PolizaSuspenderDto dto)
    {
        var usuarioId =
            int.Parse(
                User.FindFirst(
                        ClaimTypes.NameIdentifier)!
                    .Value);

        await _suspender.ExecuteAsync(
            dto.PolizaId,
            usuarioId,
            dto.Observacion);

        return Ok(new
        {
            Mensaje =
                "Póliza suspendida correctamente"
        });
    }

    [Authorize(Roles = "Administrador")]
    [HttpPut("reactivar")]
    public async Task<IActionResult> Reactivar(
        [FromBody] PolizaReactivarDto dto)
    {
        var usuarioId =
            int.Parse(
                User.FindFirst(
                        ClaimTypes.NameIdentifier)!
                    .Value);

        await _reactivar.ExecuteAsync(
            dto.PolizaId,
            usuarioId,
            dto.Observacion);

        return Ok(new
        {
            Mensaje =
                "Póliza reactivada correctamente"
        });
    }

    [HttpGet]
    public async Task<IActionResult>
        Listar()
    {
        return Ok(
            await _listarTodas.ExecuteAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult>
        Obtener(
            int id)
    {
        return Ok(
            await _obtener.ExecuteAsync(id));
    }
}