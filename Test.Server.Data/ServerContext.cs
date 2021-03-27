using Microsoft.EntityFrameworkCore;
using Test.Server.Data.Entities;

namespace Test.Server.Data
{
    public class ServerContext : DbContext
    {
        public ServerContext(DbContextOptions optionsBuilder) : base(optionsBuilder)
        {
            Database.EnsureCreated();
        }

        public DbSet<Message> Message { get; set; }
    }
}
