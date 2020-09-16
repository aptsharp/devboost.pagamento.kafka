using Confluent.Kafka;
using Integration.Pay.Dto;
using System.Threading.Tasks;

namespace Integration.Pay.Interfaces
{
    public interface IEnviaPagamentoKafka
    {
        Task<DeliveryResult<Null, string>> SendPay(RequesicaoPagamentoDto requisicaoPagamentoDto);
    }
}
