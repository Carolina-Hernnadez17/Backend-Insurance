using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insurance.Domain.Entities;

namespace Insurance.Application.Interfaces.Repositories;

public interface IProductoRepository
{
    Task<IEnumerable<Producto>> ListarAsync();
}