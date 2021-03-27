using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Test.Client.Data.Entities
{
    public class Message
    {
        [Key]
        public Guid Id { get; set; }
        public string Text { get; set; }
    }
}
