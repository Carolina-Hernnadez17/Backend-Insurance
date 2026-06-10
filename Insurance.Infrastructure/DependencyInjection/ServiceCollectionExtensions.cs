using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insurance.Application.Interfaces.Repositories;
using Insurance.Application.Interfaces.Services;
using Insurance.Infrastructure.Data;
using Insurance.Infrastructure.Repositories;
using Insurance.Infrastructure.Security;
using Microsoft.Extensions.DependencyInjection;

namespace Insurance.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection
        AddInfrastructure(
            this IServiceCollection services)
    {
        services.AddScoped<
            SqlConnectionFactory>();

        services.AddScoped<
            IAuthRepository,
            AuthRepository>();

        services.AddScoped<
            IClienteRepository,
            ClienteRepository>();

        services.AddScoped<
            IVehiculoRepository,
            VehiculoRepository>();

        services.AddScoped<
            IProductoRepository,
            ProductoRepository>();

        services.AddScoped<
            IPolizaRepository,
            PolizaRepository>();

        services.AddScoped<
            IHistorialPolizaRepository,
            HistorialPolizaRepository>();

        services.AddScoped<
            IJwtService,
            JwtService>();

        services.AddScoped<
            IHistorialPolizaRepository,
            HistorialPolizaRepository>();


        return services;
    }
}