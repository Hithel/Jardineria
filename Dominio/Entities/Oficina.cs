

namespace Dominio.Entities;
    public class Oficina
    {
        public string CodigoOficina { get; set; }
        public string Cuidad { get; set; }
        public string Pais { get; set; }
        public string Region { get; set; }
        public string CodigoPostal { get; set; }
        public string Telefono { get; set; }
        public string LineaDireccion1 { get; set; }
        public string LineaDireccion2 { get; set; }

        public ICollection<Empleado> Empleados { get; set; } 
    }
