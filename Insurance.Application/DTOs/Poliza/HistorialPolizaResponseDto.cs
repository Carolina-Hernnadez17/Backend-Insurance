using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Application.DTOs.Polizas;

public class HistorialPolizaResponseDto
{
    public int Id { get; set; }

    public int PolizaId { get; set; }

    public string? EstadoAnterior { get; set; }

    public string? EstadoNuevo { get; set; }

    public string? Observacion { get; set; }

    public int? UsuarioId { get; set; }

    public DateTime FechaCambio { get; set; }
}