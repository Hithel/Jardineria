using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repositorios;
public class ClienteRepository : GenericRepositoryEntity<Cliente>, ICliente
{
    private readonly ApiContext _context;

    public ClienteRepository(ApiContext context) : base(context)
    {
        _context = context;
    }
    public override async Task<IEnumerable<Cliente>> GetAllAsync()
    {
        return await _context.Clientes
            .ToListAsync();
    }

    public override async Task<(int totalRegistros, IEnumerable<Cliente> registros)> GetAllAsync(int pageIndez, int pageSize, int search)
    {
        var query = _context.Clientes as IQueryable<Cliente>;

        if (!string.IsNullOrEmpty(search.ToString()))
        {
            query = query.Where(p => p.CodigoCliente.Equals(search));
        }

        query = query.OrderBy(p => p.CodigoCliente);
        var totalRegistros = await query.CountAsync();
        var registros = await query
            .Skip((pageIndez - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }

    public override async Task<Cliente> GetByIdAsync(int id)
    {
        return await _context.Clientes
        .FirstOrDefaultAsync(p => p.CodigoCliente == id);
    }

    public async Task<IEnumerable<Object>> GetClientePagoSinRepresentanteNiOficina()
    {
        var result = await
        (
            from c in _context.Clientes
            join e in _context.Empleados on c.CodigoEmpleado equals e.CodigoEmpleado into empGroup
            from e in empGroup.DefaultIfEmpty()
            join o in _context.Oficinas on e.CodigoOficina equals o.CodigoOficina into oficinaGroup
            from o in oficinaGroup.DefaultIfEmpty()
            where !_context.Pagos.Any(p => p.CodigoCLiente == c.CodigoCliente)
            select new
            {
                NombreCliente = c.NombreCliente,
                NombreRepresentante = e != null ? $"{e.Nombre}" : "Sin representante",
                CiudadOficinaRepresentante = o != null ? o.Cuidad : "Sin representante"
            }
        ).ToListAsync();

        return result;
    }

    public async Task<IEnumerable<Object>> GetClientePedidos()
    {
        var result = await _context.Clientes
        .GroupJoin(
            _context.Pedidos,
            cliente => cliente.CodigoCliente,
            pedido => pedido.CodigoCliente,
            (cliente, pedidos) => new
            {
                NombreCliente = cliente.NombreCliente,
                CantidadPedidos = pedidos.Count()
            }
        )
        .Select(cliente => new
        {
            NombreCliente = cliente.NombreCliente,
            CantidadPedidos = cliente.CantidadPedidos > 0 ? cliente.CantidadPedidos : 0
        })
        .ToListAsync();

        return result;
    }

    public async Task<IEnumerable<Object>> GetCLienteEmpleadoOficina() 
        {
            var result = await _context.Clientes
            .Join(_context.Empleados,
                cliente => cliente.CodigoEmpleado,
                empleado => empleado.CodigoEmpleado,
                (cliente, empleado) => new
                {
                    cliente.NombreCliente,
                    NombreRepresentante = empleado.Nombre,
                    ApellidoRepresentante = empleado.Apellido1,
                    CiudadOficina = empleado.Oficina.Cuidad
                })
            .ToListAsync();
    
        return result;
        }

}
