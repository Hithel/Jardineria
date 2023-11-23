
using Dominio.Entities;

namespace Dominio.Interfaces;
    public interface IPedido : IGenericRepoEntity<Pedido>
    {
         Task<IEnumerable<Object>> GetAntesFechaEsperada();
    }
