using Asp.Versioning;
using Insurance.Application.DTOs.Auth;
using Insurance.Application.UseCases.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.Api.Controllers;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/auth")]
public class AuthController : ControllerBase
{
    private readonly LoginUseCase _loginUseCase;

    public AuthController(
        LoginUseCase loginUseCase)
    {
        _loginUseCase = loginUseCase;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequestDto dto)
    {
        var response =
            await _loginUseCase.ExecuteAsync(dto);

        if (response == null)
            return Unauthorized(
                "Usuario o contraseña incorrectos.");

        return Ok(response);
    }
}