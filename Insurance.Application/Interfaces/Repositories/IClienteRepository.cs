using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insurance.Domain.Entities;

namespace Insurance.Application.Interfaces.Repositories;

public interface IClienteRepository
{
    Task<int> CrearAsync(Cliente cliente);

    Task ActualizarAsync(Cliente cliente);

    Task<IEnumerable<Cliente>> ListarAsync();

    Task<Cliente?> ObtenerPorIdAsync(int id);
    Task ActivarAsync(int id);

    Task DesactivarAsync(int id);
}
