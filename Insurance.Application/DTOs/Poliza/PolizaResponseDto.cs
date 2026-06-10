using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Application.DTOs.Polizas;

public class PolizaResponseDto
{
    public int Id { get; set; }

    public string NumeroPoliza { get; set; } = string.Empty;

    public string Cliente { get; set; } = string.Empty;

    public string Producto { get; set; } = string.Empty;

    public string Estado { get; set; } = string.Empty;
}
