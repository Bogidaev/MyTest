using System.Threading.Tasks;
using Test.Model.Requests;
using Test.Model.Responses;

namespace Test.Server.Interfaces
{
    public interface IServerService
    {
        Task HandleTextMessage(Model.Message textMessage);

        Task<PrintMessageResponse> HandlePrintMessages(PrintMessageRequest Request);
    }
}
