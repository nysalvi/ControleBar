using System;
using System.Collections.Generic;
using ControleBar.ConsoleApp.ModuloProduto;
using ControleBar.ConsoleApp.Compartilhado;


namespace ControleBar.ConsoleApp.ModuloPedido
{
    public class Pedido : EntidadeBase
    {
        public List<Produto> Produtos { get; set; }

        public Pedido(List<Produto> produtos)
        {
            Produtos = produtos;
        }
        public override string ToString()
        {
            string produtos = "";
            Produtos.ForEach(x =>
            {
                produtos += x.ToString();
            });
            return produtos.ToString() + Environment.NewLine;
        }
    }
}
