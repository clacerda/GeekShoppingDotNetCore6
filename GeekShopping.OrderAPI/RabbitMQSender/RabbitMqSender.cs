﻿using GeekShopping.MessageBus;
using GeekShopping.OrderAPI.Messages;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace GeekShopping.OrderApi.RabbitMQSender
{
    public class RabbitMqSender : IRabbitMqSender
    {
        private readonly string _hostName;
        private readonly string _password;
        private readonly string _userName;
        private IConnection _connection;

        public RabbitMqSender()
        {
            _hostName = "localhost";
            _password = "guest";
            _userName = "guest";
        }

        public void SendMessage(BaseMessage message, string queueName)
        {
            if (ConnectionExists())
            {
                using var channel = _connection.CreateModel();
                channel.QueueDeclare(queue: queueName, false, false, false, arguments: null);
                byte[] body = GetsMessageAsByteArray(message);
                channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
            }
        }

        private byte[] GetsMessageAsByteArray(BaseMessage message)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };
            var json = JsonSerializer.Serialize<PaymentVO>((PaymentVO)message, options);
            return Encoding.UTF8.GetBytes(json); 
        }
        private void CreateConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _hostName,
                    UserName = _userName,
                    Password = _password
                };

                _connection = factory.CreateConnection();
            }
            catch (Exception)
            {

                throw;
            }
        }
        private bool ConnectionExists()
        {
            if (_connection != null) return true;
            CreateConnection();
            return _connection != null;
        }

       
    }
}
