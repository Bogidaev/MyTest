using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test.Server
{
    public class ServerMappingProfile: Profile
    {
        public ServerMappingProfile()
        {
            this.CreateMap<Model.Message, Data.Entities.Message>().ForMember(x => x.Date, y => y.MapFrom(y=> DateTime.Now));
            this.CreateMap<Data.Entities.Message, Model.Message>();
        }
    }
}
