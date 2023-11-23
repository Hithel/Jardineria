

using Api.Dtos;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;


public class DetallePedidoController : ApiBaseController
{
    private readonly IUnitOfWork unitofwork;
    private readonly IMapper mapper;

    public DetallePedidoController( IUnitOfWork unitofwork, IMapper mapper)
    {
        this.unitofwork = unitofwork;
        this.mapper = mapper;

    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<DetallePedidoDto>>> Get()
    {
        var entidad = await unitofwork.DetallePedido.GetAllAsync();
        return mapper.Map<List<DetallePedidoDto>>(entidad);
    }


    [HttpGet("{codigoPedido}/{codigoProducto}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DetallePedidoDto>> Get(int CodigoPedido, string CodigoProducto)
    {
        var entidad = await unitofwork.DetallePedido.GetByIdAsync(e => e.CodigoPedido == CodigoPedido && e.CodigoProducto == CodigoProducto);
        if (entidad == null)
        {
            return NotFound();
        }
        return this.mapper.Map<DetallePedidoDto>(entidad);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DetallePedido>> Post(DetallePedidoDto entidadDto)
    {
        var entidad = this.mapper.Map<DetallePedido>(entidadDto);
        this.unitofwork.DetallePedido.Add(entidad);
        await unitofwork.SaveAsync();
        if (entidad == null)
        {
            return BadRequest();
        }

        return CreatedAtAction(nameof(Post), new { codigoPedido = entidadDto.CodigoPedido, codigoProducto = entidadDto.CodigoProducto }, entidadDto);
    }

    [HttpPut("{codigoPedido}/{codigoProducto}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DetallePedidoDto>> Put(int CodigoPedido, string CodigoProducto, [FromBody] DetallePedidoDto entidadDto)
    {
        if (entidadDto == null)
        {
            return NotFound();
        }
        var entidad = this.mapper.Map<DetallePedido>(entidadDto);
        unitofwork.DetallePedido.Update(entidad);
        await unitofwork.SaveAsync();
        return entidadDto;
    }

    [HttpDelete("{codigoPedido}/{codigoProducto}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int CodigoPedido, string CodigoProducto)
    {
        var entidad = await unitofwork.DetallePedido.GetByIdAsync(e => e.CodigoPedido == CodigoPedido && e.CodigoProducto == CodigoProducto);
        if (entidad == null)
        {
            return NotFound();
        }
        unitofwork.DetallePedido.Remove(entidad);
        await unitofwork.SaveAsync();
        return NoContent();
    }

        
        [HttpGet("GetProductoMasVendido")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<string> GetProductoMasVendido()
        {
            return await unitofwork.DetallePedido.GetProductoMasVendido();
        }
}
