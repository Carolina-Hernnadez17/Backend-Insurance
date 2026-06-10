using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Domain.Entities;
    public class PolizaVehiculo
    {
        public int Id { get; set; }

        public int PolizaId { get; set; }

        public int VehiculoId { get; set; }
    }
