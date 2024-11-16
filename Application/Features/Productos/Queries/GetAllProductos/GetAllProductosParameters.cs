using Application.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Productos.Queries.GetAllProductos
{
    public class GetAllProductosParameters : RequestParameter
    {
        public string? Parametros { get; set; }
    }
}
