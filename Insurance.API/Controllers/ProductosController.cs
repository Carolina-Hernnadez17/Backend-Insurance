using Asp.Versioning;
using Insurance.Application.UseCases.Productos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.Api.Controllers;

[Authorize]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/productos")]
public class ProductosController : ControllerBase
{
    private readonly ListarProductosUseCase _listar;

    public ProductosController(
        ListarProductosUseCase listar)
    {
        _listar = listar;
    }

    [Authorize(Roles = "Administrador,Ejecutivo,Consulta")]
    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        return Ok(
            await _listar.ExecuteAsync());
    }
}