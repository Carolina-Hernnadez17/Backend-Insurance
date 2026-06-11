using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insurance.Domain.Entities;

namespace Insurance.Application.Interfaces.Repositories;

public interface IVehiculoRepository
{
    Task<int> CrearAsync(Vehiculo vehiculo);

    Task ActualizarAsync(Vehiculo vehiculo);

    Task<IEnumerable<Vehiculo>> ListarAsync();

    Task ActivarAsync(int id);

    Task<Vehiculo?> ObtenerPorIdAsync(int id);

    Task DesactivarAsync(int id);
}
