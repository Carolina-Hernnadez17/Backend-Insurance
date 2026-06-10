using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Application.DTOs.Poliza;

public class PolizaReactivarDto
{
    public int PolizaId { get; set; }

    public string Observacion { get; set; } =
        string.Empty;
}