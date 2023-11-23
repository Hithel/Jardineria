using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repositorios;
public class PedidoRepository : GenericRepositoryEntity<Pedido>, IPedido
{
    private readonly ApiContext _context;

    public PedidoRepository(ApiContext context) : base(context)
    {
        _context = context;
    }
    public override async Task<IEnumerable<Pedido>> GetAllAsync()
    {
        return await _context.Pedidos
            .ToListAsync();
    }

    public override async Task<(int totalRegistros, IEnumerable<Pedido> registros)> GetAllAsync(int pageIndez, int pageSize, int search)
    {
        var query = _context.Pedidos as IQueryable<Pedido>;

        if (!string.IsNullOrEmpty(search.ToString()))
        {
            query = query.Where(p => p.CodigoPedido.Equals(search));
        }

        query = query.OrderBy(p => p.CodigoPedido);
        var totalRegistros = await query.CountAsync();
        var registros = await query
            .Skip((pageIndez - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }

    public override async Task<Pedido> GetByIdAsync(int id)
    {
        return await _context.Pedidos
        .FirstOrDefaultAsync(p => p.CodigoPedido == id);
    }

        public async Task<IEnumerable<Object>> GetAntesFechaEsperada()
            {
                var result = await
                (
                    from p in _context.Pedidos
                    join c in _context.Clientes on p.CodigoCliente equals c.CodigoCliente
                    where p.FechaEntrega != null &&
                        p.FechaEntrega < p.FechaEsperada
                    select new
                    {
                        p.CodigoPedido,
                        p.CodigoCliente,
                        Cliente = c.CodigoCliente,
                        p.FechaEsperada,
                        p.FechaEntrega
                    }
                ).ToListAsync();
        
                return result;
        
            }


}
