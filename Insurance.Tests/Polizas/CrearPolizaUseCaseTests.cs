using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Tests.Polizas;

public class CrearPolizaUseCaseTests
{
    private readonly Mock<IPolizaRepository>
        _polizaRepository;

    private readonly Mock<IClienteRepository>
        _clienteRepository;

    private readonly CrearPolizaUseCase
        _useCase;

    public CrearPolizaUseCaseTests()
    {
        _polizaRepository =
            new Mock<IPolizaRepository>();

        _clienteRepository =
            new Mock<IClienteRepository>();

        _useCase =
            new CrearPolizaUseCase(
                _polizaRepository.Object,
                _clienteRepository.Object);
    }

    [Fact]
    public async Task Debe_Lanzar_Error_Si_FechaFin_Es_Menor()
    {
        var dto = new PolizaCreateDto
        {
            ClienteId = 1,
            ProductoId = 1,
            VehiculoId = 1,
            FechaInicio = DateTime.Now,
            FechaFin = DateTime.Now.AddDays(-1)
        };

        Func<Task> action =
            async () =>
                await _useCase.ExecuteAsync(dto, 1);

        await action.Should()
            .ThrowAsync<Exception>()
            .WithMessage(
                "La fecha fin debe ser mayor que la fecha inicio.");
    }

    [Fact]
    public async Task Debe_Lanzar_Error_Si_Vehiculo_Tiene_Poliza()
    {
        var dto = new PolizaCreateDto
        {
            ClienteId = 1,
            ProductoId = 1,
            VehiculoId = 1,
            FechaInicio = DateTime.Now,
            FechaFin = DateTime.Now.AddMonths(1)
        };

        _polizaRepository
            .Setup(x =>
                x.VehiculoTienePolizaActivaAsync(
                    dto.VehiculoId))
            .ReturnsAsync(true);

        Func<Task> action =
            async () =>
                await _useCase.ExecuteAsync(dto, 1);

        await action.Should()
            .ThrowAsync<Exception>()
            .WithMessage(
                "El vehículo ya posee una póliza activa.");
    }

    [Fact]
    public async Task Debe_Lanzar_Error_Si_Cliente_No_Existe()
    {
        var dto = new PolizaCreateDto
        {
            ClienteId = 1,
            ProductoId = 1,
            VehiculoId = 1,
            FechaInicio = DateTime.Now,
            FechaFin = DateTime.Now.AddMonths(1)
        };

        _polizaRepository
            .Setup(x =>
                x.VehiculoTienePolizaActivaAsync(
                    dto.VehiculoId))
            .ReturnsAsync(false);

        _clienteRepository
            .Setup(x =>
                x.ObtenerPorIdAsync(
                    dto.ClienteId))
            .ReturnsAsync((Cliente?)null);

        Func<Task> action =
            async () =>
                await _useCase.ExecuteAsync(dto, 1);

        await action.Should()
            .ThrowAsync<Exception>()
            .WithMessage(
                "Cliente no existe");
    }

    [Fact]
    public async Task Debe_Lanzar_Error_Si_Cliente_Inactivo()
    {
        var dto = new PolizaCreateDto
        {
            ClienteId = 1,
            ProductoId = 1,
            VehiculoId = 1,
            FechaInicio = DateTime.Now,
            FechaFin = DateTime.Now.AddMonths(1)
        };

        _polizaRepository
            .Setup(x =>
                x.VehiculoTienePolizaActivaAsync(
                    dto.VehiculoId))
            .ReturnsAsync(false);

        _clienteRepository
            .Setup(x =>
                x.ObtenerPorIdAsync(
                    dto.ClienteId))
            .ReturnsAsync(
                new Cliente
                {
                    Id = 1,
                    Activo = false
                });

        Func<Task> action =
            async () =>
                await _useCase.ExecuteAsync(dto, 1);

        await action.Should()
            .ThrowAsync<Exception>()
            .WithMessage(
                "No se puede emitir póliza para un cliente inactivo");
    }

    [Fact]
    public async Task Debe_Crear_Poliza_Correctamente()
    {
        var dto = new PolizaCreateDto
        {
            ClienteId = 1,
            ProductoId = 1,
            VehiculoId = 1,
            FechaInicio = DateTime.Now,
            FechaFin = DateTime.Now.AddMonths(1)
        };

        _polizaRepository
            .Setup(x =>
                x.VehiculoTienePolizaActivaAsync(
                    dto.VehiculoId))
            .ReturnsAsync(false);

        _clienteRepository
            .Setup(x =>
                x.ObtenerPorIdAsync(
                    dto.ClienteId))
            .ReturnsAsync(
                new Cliente
                {
                    Id = 1,
                    Activo = true
                });

        _polizaRepository
            .Setup(x =>
                x.CrearAsync(
                    It.IsAny<Poliza>(),
                    It.IsAny<int>(),
                    It.IsAny<int>()))
            .ReturnsAsync(1);

        var resultado =
            await _useCase.ExecuteAsync(dto, 1);

        resultado.Should().Be(1);
    }
}