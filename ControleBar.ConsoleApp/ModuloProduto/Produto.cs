using System;
using ControleBar.ConsoleApp.Compartilhado;


namespace ControleBar.ConsoleApp.ModuloProduto
{
    public class Produto : EntidadeBase
    {
        public decimal Preco { get; set; }
        public string Nome { get; set; }
        public bool Disponivel { get; set; }
        public string Status => Disponivel ? "Disponível" : "Indisponível";

        public Produto(string nome, decimal preco)
        {
            Preco = preco;
            Nome = nome;
            Disponivel = true;
        }
        public void AlterarDisponibilidade()
        {
            Disponivel = !Disponivel;
        }
        public override string ToString()
        {            
            return "Id: " + id + Environment.NewLine +
                "Preço do produto: " + Preco + Environment.NewLine +
                "Nome do produto:" + Nome + Environment.NewLine +
                "Disponibilidade: " + Status + Environment.NewLine;                
        }
    }
}
