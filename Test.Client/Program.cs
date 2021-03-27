using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Test.Client.Data;
using Test.Core.Interfaces;
using Test.Core.Services;
using Microsoft.EntityFrameworkCore;
using Test.Client.Interfaces;
using Test.Client.Services;
using MassTransit;

namespace Test.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var serviceProvider = CreateServiceProvider();
                var clientService = serviceProvider.GetService<IClientService>();
                await clientService.WriteText();
            }
            catch (Exception e)
            {

                throw e;
            }


        }

        public static ServiceProvider CreateServiceProvider()
        {

            //var builder = new HostBuilder()
            //    .ConfigureAppConfiguration((hostingContext, config) =>
            //    {

            //    })
            //    .ConfigureServices((hostContext, services) =>
            //    {
                   
            //    });

            return new ServiceCollection()
                .AddMassTransit(x=> x.UsingRabbitMq((y,z)=> z.Host("amqps://rxqymogm:An71W_Dr8T5yGYO2YZvVE4F3kBkuw9IB@shrimp.rmq.cloudamqp.com/rxqymogm")))
                .AddDbContext<ClientContext>(options=> options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=cliendb;Trusted_Connection=True;"))
                .AddScoped<IUnitOfWork>(x => new UnitOfWork(x.GetService<ClientContext>()))
                .AddTransient<IClientService, ClientService>()
                .BuildServiceProvider();
        }
    }
}
