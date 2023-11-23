

namespace Dominio.Entities;
    public class Producto
    {
        public string CodigoProducto { get; set; }
        public string Nombre { get; set; }

        public string Gama { get; set; }
        public GamaProducto GamaProducto { get; set; }
        
        public string Dimensiones { get; set; }
        public string Proveedor { get; set; }
        public string Descripcion { get; set; }
        public short CantidadStock { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal PrecioProveedor { get; set; }

        public ICollection<DetallePedido> DetallePedidos { get; set; }

    }
