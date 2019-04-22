using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ViajanetFullstack.Models
{
    public class PedidoCliente
    {
        public int PedidoClienteId { get; set; }

        public string Ip { get; set; }

        public string Browser { get; set; }

        public string Pagina { get; set; }

        public int IdaVolta { get; set; }

        public string Origem { get; set; }

        public string Destino { get; set; }

        public string DataIda { get; set; }

        public string DataVolta { get; set; }

        public int QtdAdultos { get; set; }

        public int QtdCriancas { get; set; }

        public int QtdBebes { get; set; }

        public double TotalPedido { get; set; }

        public bool PedidoConfirmado { get; set; }
    }
}
