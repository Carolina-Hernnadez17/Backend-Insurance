using System.Text;
using Insurance.Api.Middleware;
using Asp.Versioning;
using Insurance.Application.UseCases.Auth;
using Insurance.Application.UseCases.Clientes;
using Insurance.Application.UseCases.Polizas;
using Insurance.Application.UseCases.Productos;
using Insurance.Application.UseCases.Vehiculos;
using Insurance.Infrastructure.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();

builder.Logging.AddConsole();

builder.Logging.AddDebug();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion =
        new ApiVersion(1, 0);

    options.AssumeDefaultVersionWhenUnspecified =
        true;

    options.ReportApiVersions = true;
});

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Title = "Insurance API",
            Version = "v1"
        });

    options.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            Name = "Authorization",

            Type = SecuritySchemeType.Http,

            Scheme = "bearer",

            BearerFormat = "JWT",

            In = ParameterLocation.Header,

            Description =
                "Ingrese el token JWT."
        });

    options.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference =
                        new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                },
                Array.Empty<string>()
            }
        });
});



builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "ReactPolicy",
        policy =>
        {
            policy
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .WithOrigins(
                    "http://localhost:5173");
        });
});

builder.Services.AddInfrastructure();

#region USE CASES

builder.Services.AddScoped<LoginUseCase>();

builder.Services.AddScoped<CrearClienteUseCase>();
builder.Services.AddScoped<ActualizarClienteUseCase>();
builder.Services.AddScoped<ListarClientesUseCase>();
builder.Services.AddScoped<DesactivarClienteUseCase>();
builder.Services.AddScoped<ActivarClienteUseCase>();

builder.Services.AddScoped<CrearVehiculoUseCase>();
builder.Services.AddScoped<ActualizarVehiculoUseCase>();
builder.Services.AddScoped<DesactivarVehiculoUseCase>();
builder.Services.AddScoped<ActivarVehiculoUseCase>();
builder.Services.AddScoped<ListarVehiculosPorClienteUseCase>();
builder.Services.AddScoped<ListarVehiculosUseCase>();

builder.Services.AddScoped<CrearPolizaUseCase>();
builder.Services.AddScoped<CancelarPolizaUseCase>();
builder.Services.AddScoped<SuspenderPolizaUseCase>();
builder.Services.AddScoped<ListarPolizasUseCase>();
builder.Services.AddScoped<ObtenerPolizaUseCase>();
builder.Services.AddScoped<ListarHistorialPolizaUseCase>();
builder.Services.AddScoped<ReactivarPolizaUseCase>();

builder.Services.AddScoped<ListarProductosUseCase>();

#endregion

#region JWT

var key =
    builder.Configuration["Jwt:Key"];

builder.Services
    .AddAuthentication(
        JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,

                ValidateAudience = true,

                ValidateLifetime = true,

                ValidateIssuerSigningKey = true,

                ValidIssuer =
                    builder.Configuration["Jwt:Issuer"],

                ValidAudience =
                    builder.Configuration["Jwt:Audience"],

                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(key!))
            };
    });



#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("ReactPolicy");

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();