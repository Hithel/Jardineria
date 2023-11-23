

using Dominio.Entities;

namespace Dominio.Interfaces;
    public interface IDetallePedido : IGenericRepoEntity<DetallePedido>
    {
        Task<string> GetProductoMasVendido();
    }
