using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RabbitMQ.Client;
using System.Text;
using ViajanetFullstack.Models;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;

namespace ViajanetFullstack.RabbitMQ
{
    public class RabbitManager
    {
        public void EnviarPedido(PedidoCliente pedido)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "PedidosClientes",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = JsonConvert.SerializeObject(pedido);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "PedidosClientes",
                                     basicProperties: null,
                                     body: body);
            }
        }

        public PedidoCliente GetPedidos()
        {
            PedidoCliente pedido = null;

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                BasicGetResult data;
                using (var cha = connection.CreateModel())
                {
                    data = cha.BasicGet("PedidosClientes", true);
                }

                if (data != null)
                {
                    pedido = JsonConvert.DeserializeObject<PedidoCliente>(Encoding.UTF8.GetString(data.Body));
                }
            }

            return pedido;
        }
    }
}
