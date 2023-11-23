
namespace Dominio.Entities;
    public class GamaProducto 
    {
        public string Gama { get; set; }
        public string DescripcionTexto { get; set; }
        public string DescripcionHtml { get; set; }
        public string Imagen { get; set; }

        public ICollection<Producto> Productos { get; set; }
    }
