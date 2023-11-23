

namespace Dominio.Entities;
    public class Pago
    {
        public int CodigoCLiente { get; set; }
        public Cliente Cliente { get; set; }
        public string FormaPago { get; set; }
        public string IdTransacion { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal Total { get; set; }

    }
