using Microsoft.EntityFrameworkCore;
using System;
using Test.Client.Data.Entities;

namespace Test.Client.Data
{
    public class ClientContext : DbContext
    {
        public ClientContext(DbContextOptions optionsBuilder) : base(optionsBuilder)
        {
            Database.EnsureCreated();
        }

        public DbSet<Message> Message { get; set; }
    }
}
