using AutoMapper;
using EasyNetQ;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Test.Core.Interfaces;
using Test.Core.Services;
using Test.Server.Data;
using Test.Server.Interfaces;
using Test.Server.Services;

namespace Test.Server
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceProvider = CreateServiceProvider();
            var rabbit = serviceProvider.GetService<IRabbit>();
            await rabbit.Registration();
            Console.WriteLine("Listening for messages. Hit <enter> to quit.");
            Console.ReadLine();
        }


        public static ServiceProvider CreateServiceProvider()
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();

            return new ServiceCollection()
                .AddSingleton<IConfiguration>(x => configuration)
                .AddAutoMapper(typeof(Program))
                .AddTransient<IBus>(x => RabbitHutch.CreateBus(configuration["RabbitConnectionStrings"]))
                .AddDbContext<ServerContext>(options => options.UseSqlServer(configuration.GetConnectionString("ServerDatabase")))
                .AddScoped<IUnitOfWork>(x => new UnitOfWork(x.GetService<ServerContext>()))
                .AddTransient<IServerService, ServerService>()
                .AddSingleton<IRabbit, Rabbit>()
                .BuildServiceProvider();
        }
    }
}
