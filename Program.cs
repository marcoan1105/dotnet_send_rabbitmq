using System;
using System.Text;
using System.Threading;
using RabbitMQ.Client;

namespace SendRabbitMq
{
    class Program
    {
        static void Main(string[] args)
        {

            var RBHOST = Environment.GetEnvironmentVariable("RBHOST");
            var RBUSER = Environment.GetEnvironmentVariable("RBUSER");
            var RBPASS = Environment.GetEnvironmentVariable("RBPASS");
            var RBQUEUE = Environment.GetEnvironmentVariable("RBQUEUE");

            RBHOST = RBHOST != null ? RBHOST : "localhost";
            RBUSER = RBUSER != null ? RBUSER : "admin";
            RBPASS = RBPASS != null ? RBPASS : "123456";
            RBQUEUE = RBQUEUE != null ? RBQUEUE : "guid"; 

            using(var rabbit = new Rabbit(
                hostname: RBHOST,
                user: RBUSER,
                password: RBPASS
            )){

                Console.WriteLine(RBQUEUE);
                rabbit.QueueDeclare(RBQUEUE);

                int count = 0;

                while(true){                       

                    count++;

                    Guid message = Guid.NewGuid();

                    var body = Encoding.UTF8.GetBytes($"{message.ToString()} - {count.ToString()}");

                    Console.WriteLine(" [x]  Enviando {0} - {1}", message, count.ToString());

                    rabbit.Publish("guid", body);
                    
                    Thread.Sleep(50);
                }
            }
        }
    }
}
