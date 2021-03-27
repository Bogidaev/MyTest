using System;
using Test.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Test.Api.Data
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
