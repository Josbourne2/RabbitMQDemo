using System;

namespace RabbitMQDemo.Entities
{
    public class RabbitMQConfiguration
    {
        public string Hostname { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string[] QueueNames { get; set; }
    }
}