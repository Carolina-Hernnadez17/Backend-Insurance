using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Domain.Entities;
    public class Vehiculo
    {
        public int Id { get; set; }

        public int ClienteId { get; set; }

        public string Placa { get; set; } = string.Empty;

        public string Marca { get; set; } = string.Empty;

        public string Modelo { get; set; } = string.Empty;

        public int Anio { get; set; }

        public bool Activo { get; set; }

        public DateTime FechaCreacion { get; set; }
    }

