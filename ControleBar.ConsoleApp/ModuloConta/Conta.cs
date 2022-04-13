using System;
using System.Collections.Generic;
using ControleBar.ConsoleApp.Compartilhado;
using ControleBar.ConsoleApp.ModuloMesa;
using ControleBar.ConsoleApp.ModuloPedido;
using ControleBar.ConsoleApp.ModuloGarcom;

namespace ControleBar.ConsoleApp.ModuloConta
{
    public class Conta : EntidadeBase
    {
        public Mesa Mesa { get; set; }
        public List<Pedido> Pedidos{ get; set; }
        public Garcom Garcom { get; set; }
        public decimal TotalPedidos { get; set; }
        public bool EmAberto { get; set; }
        public DateTime Dia { get; set; }
        public int GorjetaPorcentagem { get; set; }
        public Conta(Mesa mesa, Garcom garcom, DateTime dia)
        {
            Mesa = mesa;
            Garcom = garcom;
            Dia = dia;

            Pedidos = new List<Pedido>();
            TotalPedidos = 0;
            GorjetaPorcentagem = 0;
            EmAberto = true;
        }
        public override string ToString()
        {            
            return "Id: " + id + Environment.NewLine +
                "Número da mesa/cor: " + Mesa.id + Mesa.Cor + Environment.NewLine +
                "Garçom Atendente: " + Garcom.Nome + Environment.NewLine +
                "Total dos pedidos: R$" + TotalPedidos + Environment.NewLine
                + "Dia: " + Dia;
        }
    }
}
