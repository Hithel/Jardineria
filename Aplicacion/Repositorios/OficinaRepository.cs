using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repositorios;
public class OficinaRepository : GenericRepositoryEntity<Oficina>, IOficina
{
    private readonly ApiContext _context;

    public OficinaRepository(ApiContext context) : base(context)
    {
        _context = context;
    }
    public override async Task<IEnumerable<Oficina>> GetAllAsync()
    {
        return await _context.Oficinas
            .ToListAsync();
    }

    public override async Task<(int totalRegistros, IEnumerable<Oficina> registros)> GetAllAsync(int pageIndez, int pageSize, string search)
    {
        var query = _context.Oficinas as IQueryable<Oficina>;

        if (!string.IsNullOrEmpty(search.ToString()))
        {
            query = query.Where(p => p.CodigoOficina.Equals(search));
        }

        query = query.OrderBy(p => p.CodigoOficina);
        var totalRegistros = await query.CountAsync();
        var registros = await query
            .Skip((pageIndez - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }

    public override async Task<Oficina> GetByIdAsync(string id)
    {
        return await _context.Oficinas
        .FirstOrDefaultAsync(p => p.CodigoOficina == id);
    }

    public async  Task<IEnumerable<Oficina>> GetOficinaNOEmpleados()
        {
            var result = await _context.Oficinas
            .Where(o => !_context.Empleados.Any(e =>
                e.CodigoOficina == o.CodigoOficina && 
                _context.Clientes.Any(c =>
                    c.CodigoEmpleado == e.CodigoEmpleado &&
                    _context.Pedidos.Any(p =>
                        p.CodigoCliente == c.CodigoCliente &&
                        _context.DetallePedidos.Any(dp =>
                            dp.CodigoPedido == p.CodigoPedido &&
                            _context.Productos.Any(pr =>
                                pr.CodigoProducto == dp.CodigoProducto &&
                                pr.Gama == "Frutales"
                            )
                        )
                    )
                )
            ))
            .ToListAsync();
    
        return result;
        }
}
