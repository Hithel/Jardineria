using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repositorios;
public class ProductoRepository : GenericRepositoryEntity<Producto>, IProducto
{
    private readonly ApiContext _context;

    public ProductoRepository(ApiContext context) : base(context)
    {
        _context = context;
    }
    public override async Task<IEnumerable<Producto>> GetAllAsync()
    {
        return await _context.Productos
            .ToListAsync();
    }

    public override async Task<(int totalRegistros, IEnumerable<Producto> registros)> GetAllAsync(int pageIndez, int pageSize, string search)
    {
        var query = _context.Productos as IQueryable<Producto>;

        if (!string.IsNullOrEmpty(search.ToString()))
        {
            query = query.Where(p => p.CodigoProducto.Equals(search));
        }

        query = query.OrderBy(p => p.CodigoProducto);
        var totalRegistros = await query.CountAsync();
        var registros = await query
            .Skip((pageIndez - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }

    public override async Task<Producto> GetByIdAsync(string id)
    {
        return await _context.Productos
        .FirstOrDefaultAsync(p => p.CodigoProducto == id);
    }

    public async Task<IEnumerable<Object>> Get20masVendidoAgrupado()
    {
        var result = await _context.DetallePedidos
            .GroupBy(dp => dp.CodigoProducto)
            .Select(g => new
            {
                CodigoProducto = g.Key,
                TotalUnidadesVendidas = g.Sum(dp => dp.Cantidad)
            })
            .ToListAsync();

        return result;
    }

    public async Task<IEnumerable<Object>> GetVentas3000()
    {
        var result = await _context.DetallePedidos
        .GroupBy(dp => dp.CodigoProducto)
        .Select(g => new
        {
            CodigoProducto = g.Key,
            TotalUnidadesVendidas = g.Sum(dp => dp.Cantidad),
            TotalFacturado = g.Sum(dp => dp.Cantidad * dp.PrecioUnidad),
            TotalFacturadoConIVA = g.Sum(dp => dp.Cantidad * dp.PrecioUnidad * 1.21m) // Asumiendo un 21% de IVA
        })
        .Where(p => p.TotalFacturado > 3000)
        .ToListAsync();

        return result;
    }


    public async Task<IEnumerable<Producto>> GetProductoNoPedidos()
    {
        var result = await _context.Productos
        .Where(p => !_context.DetallePedidos.Any(dp => dp.CodigoProducto == p.CodigoProducto))
        .ToListAsync();

        return result;
    }

}
