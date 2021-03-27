using System;
using EasyNetQ;

namespace Test.Server.Model.Subscribe
{
    [Queue("Server", ExchangeName = "Server")]
    public class Message
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public string Ip { get; set; }
    }
}
