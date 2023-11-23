

using Dominio.Entities;

namespace Dominio.Interfaces;
    public interface IEmpleado : IGenericRepoEntity<Empleado>
    {
        Task<IEnumerable<Object>> GetJefesEmpleados();
    }
