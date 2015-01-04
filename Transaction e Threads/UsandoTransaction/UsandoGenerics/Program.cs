using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UsandoGenerics
{
    class Program
    {
        static void Main(string[] args)
        {
            Categoria cat = new Categoria();
            cat.IdCategoria = 1;
            cat.Descricao = "Categoria 1";
            BDUtil<Categoria> db = new BDUtil<Categoria>();
            db.Adicionar(cat);
        }
    }
}
