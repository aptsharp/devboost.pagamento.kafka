using System;

namespace Drone.EnviarPedido.Api.Dto
{
    public class PedidoDto
    {
        public int Peso { get; set; }
        public string Bandeira { get; set; }
        public string Numero { get; set; }
        public DateTime Vencimento { get; set; }
        public int CodigoSeguranca { get; set; }
        public decimal Valor { get; set; }
    }
}
