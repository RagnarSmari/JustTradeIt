using System;
using System.Text;
using JustTradeIt.Software.API.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;


namespace JustTradeIt.Software.API.Services.Implementations
{
    public class QueueService : IQueueService, IDisposable
    {
        private readonly IConnectionFactory factory;
        private readonly IModel channel;
        private readonly IConnection connection;
        private byte[] ConvertJsonToBytes(object obj) => Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj));
        private readonly string _hostname = Environment.GetEnvironmentVariable("QUEUE_HOST") ?? "localhost"; 


        public QueueService()
        {
            factory = new ConnectionFactory() { HostName = _hostname, UserName = "guest", Password = "guest" };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
        }

        public void Dispose()
        {
            channel.Close();
            connection.Close();
        }

        public void PublishMessage(string routingKey, object body)
        {
            Console.WriteLine(routingKey);
            channel.ExchangeDeclare(exchange: "trade-exchange", type: ExchangeType.Direct, true);

            channel.BasicPublish(exchange: "trade-exchange",
                routingKey: routingKey,
                basicProperties: null,
                body: ConvertJsonToBytes(body));
        }
    }
}