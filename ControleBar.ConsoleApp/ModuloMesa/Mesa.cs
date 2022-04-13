using System;
using ControleBar.ConsoleApp.Compartilhado;

namespace ControleBar.ConsoleApp.ModuloMesa
{
    public class Mesa : EntidadeBase
    {
        public string Cor { get; set; }
        public decimal TotalConta { get; set; }
        public Mesa(string cor)
        {
            Cor = cor;
        }
        public Mesa(string cor, int id)
        {
            base.id = id;
            Cor = cor;
        }
        public override string ToString()
        {
            return "Id: " + id + Environment.NewLine +
                   "Cor: " + Cor + Environment.NewLine;
        }
    }
}
