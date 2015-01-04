using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExplicandoTransacoes
{
    public enum Status { Pendente = 1, Concluído }

    public class Pedido
    {
        public int IdPedido { get; set; }
        public DateTime DataHora { get; set; }
        public string Cliente { get; set; }
        public decimal VatorTotal { get; set; }
        public Status Status { get; set; }
        public List<ItemPedido> Itens { get; set; }
    }
}
