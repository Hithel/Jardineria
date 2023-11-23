
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repositorios;
public class DetallePedidoRepository : GenericRepositoryEntity<DetallePedido>, IDetallePedido
{
    private readonly ApiContext _context;

    public DetallePedidoRepository(ApiContext context) : base(context)
    {
        _context = context;
    }
    public override async Task<IEnumerable<DetallePedido>> GetAllAsync()
    {
        return await _context.DetallePedidos
            .ToListAsync();
    }

    public async Task<(int totalRegistros, IEnumerable<DetallePedido> registros)> GetAllAsync(int pageIndez, int pageSize, (int CodigoPedido, string CodigoProducto) search)
    {
        var query = _context.DetallePedidos as IQueryable<DetallePedido>;

        if (!string.IsNullOrEmpty(search.ToString()))
        {
            query = query.Where(p => p.CodigoPedido == search.CodigoPedido && p.CodigoProducto == search.CodigoProducto);
        }


        var totalRegistros = await query.CountAsync();
        var registros = await query
            .OrderBy(p => p.CodigoPedido).ThenBy(p => p.CodigoProducto)
            .Skip((pageIndez - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }

    public async Task<string> GetProductoMasVendido()
        {
            var result = await _context.DetallePedidos
            .GroupBy(dp => dp.CodigoProducto)
            .Select(g => new
            {
                CodigoProducto = g.Key,
                TotalUnidadesVendidas = g.Sum(x => x.Cantidad)
            })
            .OrderByDescending(x => x.TotalUnidadesVendidas)
            .Join(
                _context.Productos,
                dp => dp.CodigoProducto,
                p => p.CodigoProducto,
                (dp, p) => p.Nombre
            )
            .FirstOrDefaultAsync();
    
            return result;
        }    
        
}
