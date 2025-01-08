using Ardalis.Specification;
using Domain.Entities;

public class VentasPorClienteSpecification : Specification<Venta>
{
    public VentasPorClienteSpecification(int clienteId)
    {
        // Filtrar ventas por el cliente
        Query.Where(v => v.IdCliente == clienteId);
    }
}