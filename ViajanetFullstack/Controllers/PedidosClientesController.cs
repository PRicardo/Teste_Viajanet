using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ViajanetFullstack.Models;
using ViajanetFullstack.RabbitMQ;

namespace ViajanetFullstack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosClientesController : ControllerBase
    {
        private readonly ViajanetContext _context;

        public PedidosClientesController()
        {
            _context = new ViajanetContext();
        }
        
        // PUT: api/PedidosClientes/5
        [HttpPut]
        public async Task<IActionResult> PutPedidoCliente([FromBody] PedidoCliente pedidoCliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            pedidoCliente.PedidoConfirmado = true;

            RabbitManager send = new RabbitManager();
            send.EnviarPedido(pedidoCliente);

            return Ok();
        }

        // POST: api/PedidosClientes
        [HttpPost]
        public async Task<IActionResult> PostPedidoCliente([FromBody] PedidoCliente pedidoCliente)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                pedidoCliente.TotalPedido = (pedidoCliente.QtdAdultos * 500) + (pedidoCliente.QtdCriancas * 250) + (pedidoCliente.QtdBebes * 50);

                if (pedidoCliente.IdaVolta == 2)
                {
                    pedidoCliente.TotalPedido = pedidoCliente.TotalPedido * 2;
                }

                RabbitManager send = new RabbitManager();
                send.EnviarPedido(pedidoCliente);

                return Created("PostPedidoCliente", pedidoCliente);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task GetListaPedidos()
        {
            try
            {
                RabbitManager manager = new RabbitManager();
                PedidoCliente pedido = manager.GetPedidos();

                if (pedido != null)
                {
                    _context.Add(pedido);
                    _context.SaveChanges();

                    //Save JSON file
                }
                else
                {
                    List<PedidoCliente> pedidos = _context.PedidosClientes.ToList();
                    int x = 10;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}