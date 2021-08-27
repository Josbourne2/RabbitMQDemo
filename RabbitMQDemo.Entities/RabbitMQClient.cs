using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace RabbitMQDemo.Entities
{
    public class RabbitMQClient : IMQClient
    {
        private readonly string _hostname;
        private readonly string _password;
        private readonly string[] _queueNames;
        private readonly string _username;
        private IConnection _connection;

        public RabbitMQClient(IOptions<RabbitMQConfiguration> rabbitMqOptions)
        {
            _hostname = rabbitMqOptions.Value.Hostname;
            _username = rabbitMqOptions.Value.UserName;
            _password = rabbitMqOptions.Value.Password;
            _queueNames = rabbitMqOptions.Value.QueueNames;
            CreateConnection();
        }

        public void SendMessage(string message)
        {
            if (ConnectionExists())
            {
                using (var channel = _connection.CreateModel())
                {

                    var body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish(exchange: "logs", routingKey: "IDon'tKnowRoutingKey", basicProperties: null, body: body, mandatory: true);
                }
            }
        }

        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _hostname,
                    UserName = _username,
                    Password = _password
                };
                _connection = factory.CreateConnection();
                using (var channel = _connection.CreateModel())
                {
                    channel.ExchangeDeclare("logs", ExchangeType.Fanout);
                    foreach (var queueName in _queueNames)
                    {
                        channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
                        channel.QueueBind(queue: queueName, exchange: "logs", routingKey: "");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not create connection: {ex.Message}");
            }
        }

        private bool ConnectionExists()
        {
            if (_connection != null)
            {
                return true;
            }

            CreateConnection();

            return _connection != null;
        }
    }
}