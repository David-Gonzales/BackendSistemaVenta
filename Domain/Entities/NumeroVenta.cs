using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class NumeroVenta
    {
        public int IdNumeroVenta { get; set; }
        public int UltimoNumero { get; set; }
        public DateTime? FechaRegistro { get; set; }
    }   
}
