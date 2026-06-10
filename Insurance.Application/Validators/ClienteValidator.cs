using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Application.Validators;

public static class ClienteValidator
{
    public static bool DocumentoValido(string documento)
    {
        return !string.IsNullOrWhiteSpace(documento);
    }
}
