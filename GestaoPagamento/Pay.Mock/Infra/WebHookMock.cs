using Integration.Pay.Dto;
using Integration.Pay.Interfaces;
using System.Threading.Tasks;

namespace Pay.Mock.Infra
{
    public class WebHookMock : IWebHook
    {
        public async Task<PostMethodResultDto> CallPostMethod(RequesicaoPagamentoDto webHookMethodRequestDto)
        {
            var result = new PostMethodResultDto() { StatusCode = System.Net.HttpStatusCode.OK, ContentResult = "" };
            return await Task.FromResult(result);
        }
    }
}
