using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Application.DTOs.Polizas;

public class PolizaListDto
{
    public int Id { get; set; }

    public string NumeroPoliza { get; set; } = "";

    public string Cliente { get; set; } = "";

    public string Vehiculo { get; set; } = "";

    public string Producto { get; set; } = "";

    public string Estado { get; set; } = "";

    public DateTime FechaInicio { get; set; }

    public DateTime FechaFin { get; set; }
}