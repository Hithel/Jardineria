using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repositorios;
public class PagoRepository : GenericRepositoryEntity<Pago>, IPago
{
    private readonly ApiContext _context;

    public PagoRepository(ApiContext context) : base(context)
    {
        _context = context;
    }
    public override async Task<IEnumerable<Pago>> GetAllAsync()
    {
        return await _context.Pagos
            .ToListAsync();
    }

    public override async Task<(int totalRegistros, IEnumerable<Pago> registros)> GetAllAsync(int pageIndez, int pageSize, string search)
    {
        var query = _context.Pagos as IQueryable<Pago>;

        if (!string.IsNullOrEmpty(search.ToString()))
        {
            query = query.Where(p => p.IdTransacion.Equals(search));
        }

        query = query.OrderBy(p => p.IdTransacion);
        var totalRegistros = await query.CountAsync();
        var registros = await query
            .Skip((pageIndez - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }

    public override async Task<Pago> GetByIdAsync(string id)
    {
        return await _context.Pagos
        .FirstOrDefaultAsync(p => p.IdTransacion == id);
    }



}
