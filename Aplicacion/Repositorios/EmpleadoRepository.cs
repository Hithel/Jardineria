
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repositorios;
    public class EmpleadoRepository : GenericRepositoryEntity<Empleado>, IEmpleado
{
    private readonly ApiContext _context;

    public EmpleadoRepository(ApiContext context) : base(context)
    {
        _context = context;
    }
    public override async Task<IEnumerable<Empleado>> GetAllAsync()
    {
        return await _context.Empleados
            .ToListAsync();
    }

    public override async Task<(int totalRegistros, IEnumerable<Empleado> registros)> GetAllAsync(int pageIndez, int pageSize, int search)
    {
        var query = _context.Empleados as IQueryable<Empleado>;

        if (!string.IsNullOrEmpty(search.ToString()))
        {
            query = query.Where(p => p.CodigoEmpleado.Equals(search));
        }

        query = query.OrderBy(p => p.CodigoEmpleado);
        var totalRegistros = await query.CountAsync();
        var registros = await query
            .Skip((pageIndez - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }

    public override async Task<Empleado> GetByIdAsync(int id)
    {
        return await _context.Empleados
        .FirstOrDefaultAsync(p => p.CodigoEmpleado == id);
    }

    public async Task<IEnumerable<Object>> GetJefesEmpleados()
        {
            var result = await (
            from e in _context.Empleados
            join j in _context.Empleados on e.CodigoJefe equals j.CodigoEmpleado into jefeGroup
            from j in jefeGroup.DefaultIfEmpty()
            join jdj in _context.Empleados on j.CodigoJefe equals jdj.CodigoEmpleado into jefeDelJefeGroup
            from jdj in jefeDelJefeGroup.DefaultIfEmpty()
            select new
            {
                NombreEmpleado = $"{e.Nombre}",
                NombreJefe = j != null ? $"{j.Nombre}" : "Sin jefe",
                NombreJefeDelJefe = jdj != null ? $"{jdj.Nombre}" : "Sin jefe del jefe"
            }
        ).ToListAsync();
    
        return result;
        }

}