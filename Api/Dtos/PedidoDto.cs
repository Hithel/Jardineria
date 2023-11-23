

namespace Api.Dtos;

    public class PedidoDto
    {
        public int CodigoPedido { get; set; }
        public DateTime FechaPedido { get; set; }
        public DateTime FechaEsperada { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public string Estado { get; set; }
        public string Comentarios { get; set; }
        public int CodigoCliente { get; set; } 

    }
