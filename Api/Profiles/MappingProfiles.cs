

using Api.Dtos;
using AutoMapper;
using Dominio.Entities;

namespace Api.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {

        CreateMap<Cliente,ClienteDto>().ReverseMap();
        CreateMap<DetallePedido,DetallePedidoDto>().ReverseMap();
        CreateMap<Empleado,EmpleadoDto>().ReverseMap();
        CreateMap<GamaProducto,GamaProductoDto>().ReverseMap();
        CreateMap<Oficina,OficinaDto>().ReverseMap();
        CreateMap<Pago,PagoDto>().ReverseMap();
        CreateMap<Pedido,PedidoDto>().ReverseMap();
        CreateMap<Producto,ProductoDto>().ReverseMap();

    }
}