using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Application.DTOs.Polizas;

public class PolizaCancelarDto
{
    public int PolizaId { get; set; }

    public string Observacion { get; set; }
        = string.Empty;
}