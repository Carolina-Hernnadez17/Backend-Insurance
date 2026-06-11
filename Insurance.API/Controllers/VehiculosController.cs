using Asp.Versioning;
using Insurance.Application.DTOs.Vehiculos;
using Insurance.Application.UseCases.Vehiculos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.Api.Controllers;

[Authorize]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/vehiculos")]
public class VehiculosController : ControllerBase
{
    private readonly CrearVehiculoUseCase _crear;
    private readonly ActualizarVehiculoUseCase _actualizar;
    private readonly ListarVehiculosUseCase _listar;
    private readonly ListarVehiculosPorClienteUseCase _listarPorCliente;
    private readonly ActivarVehiculoUseCase _activar;
    private readonly DesactivarVehiculoUseCase _desactivar;

    public VehiculosController(
        CrearVehiculoUseCase crear,
        ActualizarVehiculoUseCase actualizar,
        ListarVehiculosPorClienteUseCase listarPorCliente,
        ListarVehiculosUseCase listar,
        DesactivarVehiculoUseCase desactivar,
        ActivarVehiculoUseCase activar)
    {
        _crear = crear;
        _listarPorCliente = listarPorCliente;
        _actualizar = actualizar;
        _listar = listar;
        _desactivar = desactivar;
        _activar = activar;
    }

    [Authorize(Roles = "Administrador,Ejecutivo")]
    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        return Ok(
            await _listar.ExecuteAsync());
    }

    [Authorize(
        Roles = "Administrador,Ejecutivo")]
    [HttpPost]
    public async Task<IActionResult> Crear(
        [FromBody] VehiculoCreateDto dto)
    {
        var id =
            await _crear.ExecuteAsync(dto);

        return Ok(new
        {
            Id = id,
            Mensaje = "Vehículo creado correctamente"
        });
    }

    [Authorize(
        Roles = "Administrador,Ejecutivo")]
    [HttpPut]
    public async Task<IActionResult> Actualizar(
        [FromBody] VehiculoUpdateDto dto)
    {
        await _actualizar.ExecuteAsync(dto);

        return Ok(new
        {
            Mensaje = "Vehículo actualizado correctamente"
        });
    }

    [Authorize(Roles = "Administrador")]
    [HttpPut("activar/{id}")]
    public async Task<IActionResult>
        Activar(int id)
    {
        await _activar.ExecuteAsync(id);

        return Ok(new
        {
            Mensaje =
                "Vehículo activado correctamente"
        });
    }

    [HttpGet("cliente/{clienteId}")]
    public async Task<IActionResult>
        ListarPorCliente(int clienteId)
    {
        return Ok(
            await _listarPorCliente.ExecuteAsync(
                clienteId));
    }

    [Authorize(
        Roles = "Administrador")]
    [HttpDelete("{id}")]
    public async Task<IActionResult>
        Desactivar(int id)
    {
        await _desactivar.ExecuteAsync(id);

        return Ok(new
        {
            Mensaje =
                "Vehículo desactivado correctamente"
        });
    }
}