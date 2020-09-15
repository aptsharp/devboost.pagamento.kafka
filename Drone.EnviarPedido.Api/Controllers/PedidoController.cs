using Drone.EnviarPedido.Api.Services.Interfaces;
using Drone.EnviarPedido.Api.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Drone.EnviarPedido.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        readonly IKafkaProducer _kafkaProducer;

        public PedidoController(IKafkaProducer kafkaProducer)
        {
            _kafkaProducer = kafkaProducer;
        }

        [HttpPost("realizarpedido")]
        public async Task<ActionResult> RealizarPedido([FromBody] PedidoDto pedido)
        {
            try
            {
                var result = await _kafkaProducer.RealizarPedido(pedido);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
