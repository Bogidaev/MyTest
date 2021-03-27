using System;
using System.ComponentModel.DataAnnotations;

namespace Test.Api.Data.Entities
{
    public class Message
    {
        [Key]
        public Guid Id { get; set; }
        public string Text { get; set; }
    }
}
