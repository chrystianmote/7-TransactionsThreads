using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UsandoTransaction
{
    class Pedido
    {
        public int IdProduto { get; set; }
        public DateTime DataHora { get; set; }
        public string Cliente { get; set; }
        public decimal ValorTotal { get; set; }
        public string Status { get; set; }
        public List<ItemPedido> Itens { get; set; }
    }
}
