using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Domain.Entities;

    public class Poliza
    {
        public int Id { get; set; }

        public string NumeroPoliza { get; set; } = string.Empty;

        public int ClienteId { get; set; }

        public int ProductoId { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }

        public string Estado { get; set; } = string.Empty;

        public DateTime FechaCreacion { get; set; }
    }

