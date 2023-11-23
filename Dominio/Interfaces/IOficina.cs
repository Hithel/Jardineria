
using Dominio.Entities;

namespace Dominio.Interfaces;
    public interface IOficina : IGenericRepoEntity<Oficina>
    {
        Task<IEnumerable<Oficina>> GetOficinaNOEmpleados();
    }
