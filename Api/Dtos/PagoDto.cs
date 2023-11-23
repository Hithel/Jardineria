
namespace Api.Dtos;

    public class PagoDto
    {
        public int CodigoCLiente { get; set; }
        public string FormaPago { get; set; }
        public string IdTransacion { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal Total { get; set; }
    }
