using Asp.Versioning;
using Insurance.Application.DTOs.Clientes;
using Insurance.Application.UseCases.Clientes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.Api.Controllers;

[Authorize]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/clientes")]
public class ClientesController : ControllerBase
{
    private readonly CrearClienteUseCase _crear;
    private readonly ActualizarClienteUseCase _actualizar;
    private readonly ListarClientesUseCase _listar;
    private readonly ActivarClienteUseCase _activar;
    private readonly DesactivarClienteUseCase _desactivar;

    public ClientesController(
        CrearClienteUseCase crear,
        ActualizarClienteUseCase actualizar,
        ListarClientesUseCase listar,
        ActivarClienteUseCase activar,
        DesactivarClienteUseCase desactivar)
    {
        _crear = crear;
        _actualizar = actualizar;
        _listar = listar;
        _activar = activar;
        _desactivar = desactivar;
    }

    [Authorize(Roles = "Administrador,Ejecutivo")]
    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        return Ok(
            await _listar.ExecuteAsync());
    }

    [Authorize(Roles = "Administrador,Ejecutivo")]
    [HttpPost]
    public async Task<IActionResult> Crear(
        [FromBody] ClienteCreateDto dto)
    {
        var id =
            await _crear.ExecuteAsync(dto);

        return Ok(new
        {
            Id = id,
            Mensaje = "Cliente creado correctamente"
        });
    }

    [Authorize(Roles = "Administrador,Ejecutivo")]
    [HttpPut]
    public async Task<IActionResult> Actualizar(
        [FromBody] ClienteUpdateDto dto)
    {
        await _actualizar.ExecuteAsync(dto);

        return Ok(new
        {
            Mensaje = "Cliente actualizado correctamente"
        });
    }

    [Authorize(Roles = "Administrador")]
    [HttpPut("activar/{id}")]
    public async Task<IActionResult> Activar(
        int id)
    {
        await _activar.ExecuteAsync(id);

        return Ok(new
        {
            Mensaje =
                "Cliente activado correctamente"
        });
    }

    [Authorize(Roles = "Administrador")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Desactivar(
        int id)
    {
        await _desactivar.ExecuteAsync(id);

        return Ok(new
        {
            Mensaje = "Cliente desactivado correctamente"
        });
    }
}