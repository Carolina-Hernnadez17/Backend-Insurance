using Insurance.Application.DTOs.Auth;
using Insurance.Application.Interfaces.Services;
using Insurance.Application.UseCases.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Tests.Auth;

public class LoginUseCaseTests
{
    private readonly Mock<IAuthRepository>
        _authRepository;

    private readonly Mock<IJwtService>
        _jwtService;

    private readonly LoginUseCase
        _useCase;

    public LoginUseCaseTests()
    {
        _authRepository =
            new Mock<IAuthRepository>();

        _jwtService =
            new Mock<IJwtService>();

        _useCase =
            new LoginUseCase(
                _authRepository.Object,
                _jwtService.Object);
    }

    [Fact]
    public async Task Debe_Loguear_Correctamente()
    {
        var request =
            new LoginRequestDto
            {
                Usuario = "admin",
                Password = "Admin123"
            };

        _authRepository
            .Setup(x =>
                x.LoginAsync(
                    request.Usuario,
                    request.Password))
            .ReturnsAsync(
                new Usuario
                {
                    Id = 1,
                    NombreUsuario = "admin",
                    Rol = "Administrador",
                    RolId = 1
                });

        _jwtService
            .Setup(x =>
                x.GenerateToken(
                    It.IsAny<int>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()))
            .Returns("TOKEN");

        var resultado =
            await _useCase.ExecuteAsync(request);

        resultado.Should().NotBeNull();

        resultado.Token.Should().Be("TOKEN");
    }

    [Fact]
    public async Task Debe_Lanzar_Error_Cuando_Login_Es_Invalido()
    {
        var request =
            new LoginRequestDto
            {
                Usuario = "admin",
                Password = "mala"
            };

        _authRepository
            .Setup(x =>
                x.LoginAsync(
                    request.Usuario,
                    request.Password))
            .ReturnsAsync((Usuario?)null);

        Func<Task> action =
            async () =>
                await _useCase.ExecuteAsync(request);

        await action.Should()
            .ThrowAsync<UnauthorizedAccessException>();
    }
}