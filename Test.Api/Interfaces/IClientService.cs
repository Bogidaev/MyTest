using System.Threading.Tasks;
using Test.Api.ModelDto;
using Test.Model.Responses;

namespace Test.Api.Interfaces
{
    public interface IClientService
    {
        Task SendMessageAsync(MessageDTO messageDTO);

        Task<PrintMessageResponse> PrintMessagesAsync();
    }
}
