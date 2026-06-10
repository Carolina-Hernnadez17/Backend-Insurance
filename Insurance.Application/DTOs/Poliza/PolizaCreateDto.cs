using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Application.DTOs.Polizas;

public class PolizaCreateDto
{
    public int ClienteId { get; set; }

    public int ProductoId { get; set; }

    public int VehiculoId { get; set; }

    public DateTime FechaInicio { get; set; }

    public DateTime FechaFin { get; set; }
}