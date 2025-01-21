using Ardalis.Specification;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Specifications
{
    public class ClientesSpecification : Specification<Cliente>
    {
        public ClientesSpecification(string? parametros) 
        {
            if (!string.IsNullOrEmpty(parametros))
            {
                Query.Where(x =>
                    EF.Functions.Like(x.Nombres, $"%{parametros}") ||
                    EF.Functions.Like(x.Apellidos, $"%{parametros}%") ||
                    EF.Functions.Like(x.TipoDocumento, $"%{parametros}%") ||
                    EF.Functions.Like(x.NumeroDocumento, $"%{parametros}%") ||
                    EF.Functions.Like(x.Ciudad, $"%{parametros}%")
                );
            }
        }
    }
}
