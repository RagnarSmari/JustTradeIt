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

        public QueueService()
        {
            factory = new ConnectionFactory() { HostName = "localhost", UserName = "guest", Password = "guest" };
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
            channel.ExchangeDeclare(exchange: "trade_exchange", type: ExchangeType.Direct);

            channel.BasicPublish(exchange: "trade_exchange",
                routingKey: routingKey,
                basicProperties: null,
                body: ConvertJsonToBytes(body));
        }
    }
}