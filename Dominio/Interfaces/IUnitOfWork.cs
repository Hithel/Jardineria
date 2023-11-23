

namespace Dominio.Interfaces;
    public interface  IUnitOfWork
    {
        ICliente Cliente { get; }
        IDetallePedido DetallePedido { get; }
        IEmpleado Empleado { get; }
        IGamaProducto GamaProducto { get; }
        IOficina Oficina { get; }
        IPago Pago { get; }
        IPedido Pedido { get; }
        IProducto Producto { get; }
        Task<int> SaveAsync();
    }
