using Consumer.Drone.AzureFunction.Dto;
using System.Threading.Tasks;

namespace Consumer.Drone.AzureFunction.Services.Interfaces
{
    public interface IAutorizationService
    {
        Task<string> GetToken(AuthenticationRequest authenticationRequest);
    }
}
