using EasyNetQ;
using MassTransit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using Test.Client.Data.Entities;
using Test.Client.Interfaces;
using Test.Core.Interfaces;

namespace Test.Client.Services
{
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork _uow;
        private readonly IPublishEndpoint _publishEndpoint;

        public ClientService(IUnitOfWork uow, IPublishEndpoint publishEndpoint)
        {
            this._uow = uow;
            this._publishEndpoint = publishEndpoint;
        }

        public async Task WriteText()
        {
            var text = string.Empty;
            Console.WriteLine("Enter a message. 'Quit' to quit.");
            Console.Write("Воовд текста => ");
            while ((text = Console.ReadLine()) != "Quit")
            {
                var massageRepository = _uow.GetRepository<Message>();
                var mes = new Message { Text = text };
                await massageRepository.InsertAsync(mes);
                await _uow.Commit();

                var connectionString = "host=shrimp.rmq.cloudamqp.com;virtualHost=rxqymogm;username=rxqymogm;password=An71W_Dr8T5yGYO2YZvVE4F3kBkuw9IB";
                using (var bus = RabbitHutch.CreateBus(connectionString))
                {
                    await bus.PubSub.PublishAsync(new Model.Message { Text = text, Id = Guid.NewGuid() });
                    Console.WriteLine("Message published!");
                }
            }
        }
    }



}
