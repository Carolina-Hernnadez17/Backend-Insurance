using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insurance.Application.DTOs.Auth;
using Insurance.Application.Interfaces.Repositories;
using Insurance.Application.Interfaces.Services;

namespace Insurance.Application.UseCases.Auth;

public class LoginUseCase
{
    private readonly IAuthRepository _authRepository;
    private readonly IJwtService _jwtService;

    public LoginUseCase(
        IAuthRepository authRepository,
        IJwtService jwtService)
    {
        _authRepository = authRepository;
        _jwtService = jwtService;
    }

    public async Task<LoginResponseDto> ExecuteAsync(
        LoginRequestDto request)
    {
        var usuario = await _authRepository.LoginAsync(
            request.Usuario,
            request.Password);

        if (usuario == null)
            throw new UnauthorizedAccessException(
                "Usuario o contraseña inválidos.");

        var token = _jwtService.GenerateToken(
            usuario.Id,
            usuario.NombreUsuario,
            usuario.Rol);

        return new LoginResponseDto
        {
            Token = token,
            Usuario = usuario.NombreUsuario,
            Rol = usuario.Rol
        };
    }
}