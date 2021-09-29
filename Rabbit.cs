using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace SendRabbitMq
{
    public class Rabbit : IDisposable
    {
        IConnectionFactory Factory;
        IConnection Connection;
        IModel Model;
        public Rabbit(string hostname, string user, string password)
        {
            Factory = new ConnectionFactory(){
                HostName = hostname,
                UserName = user,
                Password = password
            };

            Connection = Factory.CreateConnection();
            Model = Connection.CreateModel();
        }

        public void QueueDeclare(string queue){
            Model.QueueDeclare(
                queue: queue,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );
        }

        public void Publish(string routingKey, ReadOnlyMemory<byte> body){
            Model.BasicPublish(
                exchange: "",
                routingKey: routingKey,
                basicProperties: null,
                body: body
            );
        }

        public IModel GetModel(){
            return Model;
        }

        public void Dispose()
        {
            Connection.Dispose();
            Model.Dispose();
        }
    }
}