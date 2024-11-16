using Ardalis.Specification;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Specifications
{
    public class PagedClientesSpecification : Specification<Cliente>
    {
        public PagedClientesSpecification(int pageSize, int pageNumber, string parametros)
        {
            Query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            if (!string.IsNullOrEmpty(parametros)) {
                //Es como un LIKE, compara
                //Query.Search(x => x.Nombres, "%" + parametros + "%");

                //Otra forma para ampliar la búsqueda
                Query.Where( x=>
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
