﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExplicandoTransacoes
{
    public class Produto
    {
        public int IdProduto { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public int Estoque { get; set; }
        public List<ItemPedido> Itens { get; set; }
    }
}
