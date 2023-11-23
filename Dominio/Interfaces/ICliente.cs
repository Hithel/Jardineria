

using Dominio.Entities;

namespace Dominio.Interfaces;
public interface ICliente : IGenericRepoEntity<Cliente>
{
    Task<IEnumerable<Object>> GetClientePagoSinRepresentanteNiOficina();
    Task<IEnumerable<Object>> GetClientePedidos();
    Task<IEnumerable<Object>> GetCLienteEmpleadoOficina();

}
