using ControleBar.ConsoleApp.ModuloGarcom;
using ControleBar.ConsoleApp.ModuloConta;
using ControleBar.ConsoleApp.ModuloMesa;
using ControleBar.ConsoleApp.ModuloProduto;
using System;

namespace ControleBar.ConsoleApp.Compartilhado
{
    public class TelaMenuPrincipal
    {
        private readonly IRepositorio<Garcom> repositorioGarcom;
        private readonly TelaCadastroGarcom telaCadastroGarcom;

        private readonly IRepositorio<Conta> repositorioConta;
        private readonly TelaCadastroConta telaCadastroConta;

        private readonly IRepositorio<Mesa> repositorioMesa;
        private readonly TelaCadastroMesa telaCadastroMesa;

        private readonly IRepositorio<Produto> repositorioProduto;
        private readonly TelaCadastroProduto telaCadastroProduto;

        public TelaMenuPrincipal(Notificador notificador)
        {
            repositorioGarcom = new RepositorioGarcom();
            telaCadastroGarcom = new TelaCadastroGarcom(repositorioGarcom, notificador);

            repositorioConta = new RepositorioConta();
            telaCadastroConta = new TelaCadastroConta(repositorioConta, notificador);

            repositorioMesa = new RepositorioMesa();
            telaCadastroMesa = new TelaCadastroMesa(repositorioMesa, notificador);

            repositorioProduto = new RepositorioProduto();
            telaCadastroProduto = new TelaCadastroProduto(repositorioProduto, notificador);

            PopularAplicacao();
        }

        public string MostrarOpcoes()
        {
            Console.Clear();

            Console.WriteLine("Controle de Mesas de Bar 1.0");

            Console.WriteLine();

            Console.WriteLine("Digite 1 para Gerenciar Garçons");
            Console.WriteLine("Digite 2 para Gerenciar Contas");
            Console.WriteLine("Digite 3 para Gerenciar Mesas");
            Console.WriteLine("Digite 4 para Gerenciar Produtos");
            Console.WriteLine("Digite s para sair");

            string opcaoSelecionada = Console.ReadLine();

            return opcaoSelecionada;
        }

        public TelaBase ObterTela()
        {
            string opcao = MostrarOpcoes();

            TelaBase tela = null;

            if (opcao == "1")
                tela = telaCadastroGarcom;

            else if (opcao == "2")
                tela = telaCadastroConta;

            else if (opcao == "3")
                tela = telaCadastroMesa;

            else if (opcao == "4")
                tela = telaCadastroProduto;

            else if (opcao == "5")
                tela = null;

            return tela;
        }

        private void PopularAplicacao()
        {
            var garcom = new Garcom("Julinho", "230.232.519-98");
            repositorioGarcom.Inserir(garcom);

            Mesa mesa = new Mesa("Vermelho", 7);
            Mesa mesa1 = new Mesa("Verde", 15);
            Mesa mesa2 = new Mesa("Amarelo", 9);

            repositorioMesa.Inserir(mesa);
            repositorioMesa.Inserir(mesa1);
            repositorioMesa.Inserir(mesa2);

            Produto cachaca = new Produto("Cachaça 51", 5.99m);
            Produto coca = new Produto("Coca", 9.99m);

            repositorioProduto.Inserir(cachaca);
            repositorioProduto.Inserir(coca);
        }
    }
}
