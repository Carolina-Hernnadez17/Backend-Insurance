using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Application.DTOs.Clientes;

public class ClienteResponseDto
{
    public int Id { get; set; }

    public string NombreCompleto { get; set; } = string.Empty;

    public string Documento { get; set; } = string.Empty;

    public bool Activo { get; set; }
}
