# Jardineria

![image](https://github.com/Hithel/Jardineria/assets/124798863/6da97679-8091-4ebf-af36-4dc7e23bce3d)

http://localhost:5170/api/Pedido/GetAntesFechaEsperada

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

como necesitamos una informacion especifica de las dos tablas, lo que hacemos es que nos retorne un IEnumerable que devuelva
objetos. creamos una variable result que es la que vamos a retornar donde tiene que esperar para que se resuelva el query.
se elige la tabla pedidos y realizamos un join a la tabla clientes donde ponemos una condicion es que fechaEntrega no puede ser nula
 y debe ser menor que la fecha esperada y luego creamos el objeto con la informacion requerida y la retornamos. 

 ![image](https://github.com/Hithel/Jardineria/assets/124798863/ac0b9ac2-816e-4674-89dc-89a391ec4603)

 http://localhost:5170/api/Cliente/GetClientePagoSinRepresentanteNiOficina

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

como necesitamos una informacion especifica de varias tablas, lo que hacemos es que nos retorne un IEnumerable que devuelva
objetos.  creamos una variable result que es la que vamos a retornar donde tiene que esperar para que se resuelva el query.
se elije la tabla clientes donde le hacemos in join a la tabla empleado donde le pedimos que nosdevuelve esa coleccion,
 hacemos lo mismo con la tabla de oficinas. le ponemos un condicional donde pago no contenga la condicion de que los pagos
 realizados por los clientes no sean iguales al los clientes. osea que nos retornara los clientes que no esten registrados en 
 la tabla pagos, luego creamos el object que vamos a retornar y lo retornamos.


 ![image](https://github.com/Hithel/Jardineria/assets/124798863/bc9bcc4c-75f9-4251-8f3d-dd9d7175816c)

 http://localhost:5170/api/Oficina/GetOficinaNOEmpleados

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

  en este caso vamos a retornar las oficinas que no cumplan la condicion, entonces en vez de object le ponemos la entidad que nos va a
  retornar la informacion. elegimos la tabla Oficinas y le ponemos un condicional que la tabla empleados no contenga en el campo
  codigo oficina al id de las oficinas y que los clientes esten ligados a un empleado y que los pedidos esten ligados al cliente donde
  esos pedidos deben tener un datalle y ese detalle tengan un producto que sea de la gama Frutales y lo retornamos.


  ![image](https://github.com/Hithel/Jardineria/assets/124798863/537d8896-8d66-4218-9c37-5d78f5fa5403)

  http://localhost:5170/api/Producto/Get20masVendidoAgrupado

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

  como necesitamos una informacion especifica, lo que hacemos es que nos retorne un IEnumerable que devuelva
objetos.  creamos una variable result que es la que vamos a retornar donde tiene que esperar para que se resuelva el query.
donde elegimos la tabla DetallePedidos y le decimos que nos agrupe los productos por el codigod del producto y que nos retorne un
objeto con el codigo del producto y el otro campo crea un objeto para sumar la cantidad del producto. y lo retornamos.


![image](https://github.com/Hithel/Jardineria/assets/124798863/c5e84979-f539-4918-8c48-d75b09a44f4f)


http://localhost:5170/api/Producto/GetVentas3000


public async Task<IEnumerable<Object>> GetVentas3000()
{
    var result = await _context.DetallePedidos
    .GroupBy(dp => dp.CodigoProducto)
    .Select(g => new
    {
        CodigoProducto = g.Key,
        TotalUnidadesVendidas = g.Sum(dp => dp.Cantidad),
        TotalFacturado = g.Sum(dp => dp.Cantidad * dp.PrecioUnidad),
        TotalFacturadoConIVA = g.Sum(dp => dp.Cantidad * dp.PrecioUnidad * 1.21m)
    })
    .Where(p => p.TotalFacturado > 3000)
    .ToListAsync();

    return result;
}


como necesitamos una informacion especifica, lo que hacemos es que nos retorne un IEnumerable que devuelva
objetos.  creamos una variable result que es la que vamos a retornar donde tiene que esperar para que se resuelva el query.
elegimos la tabla detalles Pedidos y le decimos que nos agrupe los productos y nos retorne un objeto donde va a tener una informacion
especifica y una serie de operaciones donde sumamos la cantidad de cada producto, cuando se facturo y se le agrega el iva del 21% 
y lo retornamos.


![image](https://github.com/Hithel/Jardineria/assets/124798863/8459f2da-4d53-466c-a6ef-43f26e594e37)

http://localhost:5170/api/DetallePedido/GetProductoMasVendido

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

en este caso debemos retornar el nombre del prodcuto mas vendido entonces le decimos que nos va a retorar una tarea de tipo
string. creamos una variable result que es la que vamos a retornar donde tiene que esperar para que se resuelva el query.
donde elejimos la tabla detalle de pedidos y le pedimos que nos agrupe los productos y nos cree un objeto donde le decimos que
nos sume toda la cantidad de unidades vendidas y nos la ordene en desenso y le haga un join a productos y que agarre el primer
producto y lo busque en el productos y nos traiga el nombre y retornamos el primer producto con mayyor cantidad vendida.


![image](https://github.com/Hithel/Jardineria/assets/124798863/93697447-b041-40a9-a76f-90fd83830a11)

http://localhost:5170/api/Cliente/GetClientePedidos

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

como necesitamos una informacion especifica de varias tablas, lo que hacemos es que nos retorne un IEnumerable que devuelva
objetos.  creamos una variable result que es la que vamos a retornar donde tiene que esperar para que se resuelva el query.
dond elejimos la tabla clientes y le decimos que nos agrupe los pedidos que esten registrado con el codigo del cliente y que nos 
cree un objeto donde tiene informacion especifica y le pedimos que cuente la cantidad de pedidos que a realizado el cliente.
selecionamos y retornamos.


![image](https://github.com/Hithel/Jardineria/assets/124798863/8a4a65e3-b2ae-48ff-81ca-2930e292359c)

http://localhost:5170/api/Cliente/GetCLienteEmpleadoOficina

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

como necesitamos una informacion especifica de varias tablas, lo que hacemos es que nos retorne un IEnumerable que devuelva
objetos.  creamos una variable result que es la que vamos a retornar donde tiene que esperar para que se resuelva el query.
elegimos los empleados y que traiga los registros de clientes y empleados, donde creamos un objeto que tenga la informacion necesaria
y la retorne.


![image](https://github.com/Hithel/Jardineria/assets/124798863/bf224e18-5681-48a8-8007-526c9633d08b)

http://localhost:5170/api/Empleado/GetJefesEmpleados

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

como necesitamos una informacion especifica de varias tablas, lo que hacemos es que nos retorne un IEnumerable que devuelva
objetos.  creamos una variable result que es la que vamos a retornar donde tiene que esperar para que se resuelva el query.
donde elejimos la tabla empleados y hacemos joins con la coleccion que esta almacenando los jefes de los empleados y que nos 
retorne las coleeciones. donde creamos un objeto que tiene informacion requeridad y lo retornamos.

![image](https://github.com/Hithel/Jardineria/assets/124798863/70426200-34d0-4861-a49f-70a3ad0118b0)

http://localhost:5170/api/Producto/GetProductoNoPedidos

public async Task<IEnumerable<Producto>> GetProductoNoPedidos()
{
    var result = await _context.Productos
    .Where(p => !_context.DetallePedidos.Any(dp => dp.CodigoProducto == p.CodigoProducto))
    .ToListAsync();

    return result;
}

en este caso le vamos a pedir que nos retorne toda la informacion de los productos. selecionamos la tabla productos y le decimos
que los detalle pedidos que no tengan el codigo del prodcuto nos lo liste y nos lo retorne.



        






    


        






            
