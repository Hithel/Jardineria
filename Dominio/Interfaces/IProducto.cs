using Dominio.Entities;

namespace Dominio.Interfaces;
    public interface IProducto : IGenericRepoEntity<Producto>
    {
        Task<IEnumerable<Object>> Get20masVendidoAgrupado();
        Task<IEnumerable<Object>> GetVentas3000();
        Task<IEnumerable<Producto>> GetProductoNoPedidos();
    }
