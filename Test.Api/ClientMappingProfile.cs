using AutoMapper;
using Test.Api.ModelDto;

namespace Test.Api
{
    public class ClientMappingProfile : Profile
    {
        public ClientMappingProfile()
        {
            this.CreateMap<Model.Message, Data.Entities.Message>();
            this.CreateMap<Data.Entities.Message, Model.Message>();
            this.CreateMap<MessageDTO, Data.Entities.Message>();
            this.CreateMap<MessageDTO, Model.Message>();
        }
    }
}
