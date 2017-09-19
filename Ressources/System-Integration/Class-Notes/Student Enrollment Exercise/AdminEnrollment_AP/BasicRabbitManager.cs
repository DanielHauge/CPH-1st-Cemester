using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Threading;

namespace AdminEnrollment_AP
{
    class BasicRabbitManager
    {
        public const string Host_name = "46.101.160.252";

        
        private static string GetMessage(string[] args)
        {
            return ((args.Length > 0) ? string.Join(" ", args) : "Hello World!");
        }
        public void BasicSendMessage(string Que_name, byte[] body)
        {
            var factory = new ConnectionFactory() { HostName = Host_name, UserName = "admin", Password = "password" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                //Declares a que (Declares the mailbox)
                channel.QueueDeclare(queue: Que_name,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);


                string message = Encoding.UTF8.GetString(body);

                // channel.BasicPublish(Exchange, RoutingKey(What Que, Properties, message in bytes)
                channel.BasicPublish(exchange: "",   // If it needs to be default "" or fanout (pub-sub) or other type, ""
                                     routingKey: "BasicMessageQue",  // What Adress is the mailbox located (or city for topics)
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();



        }
        public void BasicReceiveMessage(string Que_name)
        {


            var factory = new ConnectionFactory() { HostName = Host_name, UserName = "admin", Password = "password" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                //Like before = Declare the que to know where to pick up messages.
                channel.QueueDeclare(queue: Que_name, durable: false, exclusive: false, autoDelete: false, arguments: null);


                // Creates consumer
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Received {0}", message);
                };
                // Consumes messages once. Only once. This is not a worker (Which listens to messages)
                channel.BasicConsume(queue: Que_name,
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();


            }

        }
        public void WorkerSendMessage(string Que_name, byte[] body)
        {
            var factory = new ConnectionFactory() { HostName = Host_name, UserName = "admin", Password = "password" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                //Declares a que
                channel.QueueDeclare(queue: Que_name, durable: true, exclusive: false, autoDelete: false, arguments: null);


                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;


                //Publish Message
                channel.BasicPublish(exchange: "", routingKey: Que_name, basicProperties: null, body: body);
                Console.WriteLine(" [x] Sent {0}", Encoding.UTF8.GetString(body));


            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
        public string WorkerReceiveMessage(string Que_name)
        {
            string y = "No message yet";
            var factory = new ConnectionFactory() { HostName = Host_name, UserName = "admin", Password = "password" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                //Like before = Declare the que to know where to pick up messages.
                channel.QueueDeclare(queue: Que_name, durable: true, exclusive: false, autoDelete: false, arguments: null);
                channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

                // Creates consumer
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var results = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Received {0}", results);

                    y = results;

                    Console.WriteLine(" [x] Done");

                    //Acknowlegding the message
                    channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                };
                // Consumes messages once. Only once. This is not a worker (Which listens to messages)
                channel.BasicConsume(queue: Que_name, autoAck: false, consumer: consumer);

                
            }
            return y;
        }
        public void PupSubSend(string ExchangeName, byte[] body)
        {
            ///This example is a logging system send.
            var factory = new ConnectionFactory() { HostName = Host_name, UserName = "admin", Password = "password" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                //Declares a exchange instead of a specific que. (This way, more receivers can listen in instead of consuming on one single que.)
                channel.ExchangeDeclare(exchange: ExchangeName, type: "fanout");


                channel.BasicPublish(exchange: ExchangeName,
                                     routingKey: "",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", body);
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();



        }
        public void PubSubRecieve(string ExchangeName)
        {
            var factory = new ConnectionFactory() { HostName = Host_name, UserName = "admin", Password = "password" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                //Like before = Declare the que to know where to pick up messages.
                channel.ExchangeDeclare(exchange: ExchangeName, type: "fanout");

                var queueName = channel.QueueDeclare().QueueName;
                channel.QueueBind(queue: queueName,
                                  exchange: ExchangeName,
                                  routingKey: "");

                Console.WriteLine(" [*] Waiting for logs.");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] {0}", message);
                };
                channel.BasicConsume(queue: queueName,
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}

