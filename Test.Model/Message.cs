using EasyNetQ;
using System;

namespace Test.Model
{
    [Queue("Server", ExchangeName = "Server")]
    public class Message
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public string Ip { get; set; }

        public DateTime Date { get; set; }
    }
}
