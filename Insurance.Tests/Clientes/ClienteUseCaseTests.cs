using Insurance.Application.DTOs.Clientes;
using Insurance.Application.UseCases.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Tests.Clientes;

public class ClienteUseCaseTests
{
    private readonly Mock<IClienteRepository>
        _clienteRepository;

    public ClienteUseCaseTests()
    {
        _clienteRepository =
            new Mock<IClienteRepository>();
    }

    [Fact]
    public async Task Debe_Crear_Cliente_Correctamente()
    {
        var useCase =
            new CrearClienteUseCase(
                _clienteRepository.Object);

        var dto =
            new ClienteCreateDto
            {
                Nombres = "Carolina",
                Apellidos = "Zepeda",
                Documento = "12345678",
                Telefono = "77777777",
                Correo = "correo@test.com"
            };

        _clienteRepository
            .Setup(x =>
                x.CrearAsync(
                    It.IsAny<Cliente>()))
            .ReturnsAsync(1);

        var resultado =
            await useCase.ExecuteAsync(dto);

        resultado.Should().Be(1);
    }

    [Fact]
    public async Task Debe_Desactivar_Cliente()
    {
        var useCase =
            new DesactivarClienteUseCase(
                _clienteRepository.Object);

        _clienteRepository
            .Setup(x =>
                x.ObtenerPorIdAsync(1))
            .ReturnsAsync(
                new Cliente
                {
                    Id = 1,
                    Nombres = "Carolina",
                    Apellidos = "Zepeda",
                    Activo = true
                });

        await useCase.ExecuteAsync(1);

        _clienteRepository.Verify(
            x => x.DesactivarAsync(1),
            Times.Once);
    }

    [Fact]
    public async Task Debe_Lanzar_Error_Si_Cliente_No_Existe()
    {
        var useCase =
            new DesactivarClienteUseCase(
                _clienteRepository.Object);

        _clienteRepository
            .Setup(x =>
                x.ObtenerPorIdAsync(1))
            .ReturnsAsync((Cliente?)null);

        Func<Task> action =
            async () =>
                await useCase.ExecuteAsync(1);

        await action.Should()
            .ThrowAsync<Exception>()
            .WithMessage("Cliente no encontrado.");
    }
}