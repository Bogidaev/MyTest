using System;
using System.ComponentModel.DataAnnotations;

namespace Test.Server.Data.Entities
{
    public class Message
    {
        [Key]
        public Guid Id { get; set; }

        public string Text { get; set; } 

        public string Ip { get; set; }

        public DateTime Date { get; set; }
    }
}
