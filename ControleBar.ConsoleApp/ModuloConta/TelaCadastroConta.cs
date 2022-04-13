using System;
using System.Collections.Generic;
using ControleBar.ConsoleApp.Compartilhado;

using ControleBar.ConsoleApp.ModuloGarcom;
using ControleBar.ConsoleApp.ModuloConta;
using ControleBar.ConsoleApp.ModuloMesa;
using ControleBar.ConsoleApp.ModuloProduto;

namespace ControleBar.ConsoleApp.ModuloConta
{
    internal class TelaCadastroConta : TelaBase
    {
        private readonly IRepositorio<Conta> repositorioConta;
        private readonly Notificador _notificador;

        private readonly IRepositorio<Garcom> repositorioGarcom;
        private readonly TelaCadastroGarcom telaCadastroGarcom;

        private readonly IRepositorio<Mesa> repositorioMesa;
        private readonly TelaCadastroMesa telaCadastroMesa;

        public TelaCadastroConta(IRepositorio<Conta> repositorioConta, IRepositorio<Garcom> repositorioGarcom, TelaCadastroGarcom telaCadastroGarcom,
            IRepositorio<Mesa> repositorioMesa, TelaCadastroMesa telaCadastroMesa, Notificador notificador) 
            : base("Cadastro de Contas")
        {
            this.repositorioConta = repositorioConta;
            this.telaCadastroGarcom = telaCadastroGarcom;
            this.repositorioGarcom = repositorioGarcom;

            this.telaCadastroMesa = telaCadastroMesa;
            this.repositorioMesa = repositorioMesa;
            _notificador = notificador;
        }

        public void AbrirConta()
        {
            MostrarTitulo("Cadastro de Conta");

            Conta novaConta = ObterConta();

            if (novaConta == null)
            {
                _notificador.ApresentarMensagem("Mesa ou garçom inexistentes", TipoMensagem.Erro);
                return;
            }
            repositorioConta.Inserir(novaConta);
            _notificador.ApresentarMensagem("Conta cadastrada com sucesso!", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Contas Cadastradas");

            List<Conta> contas = repositorioConta.SelecionarTodos();

            if (contas.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhuma conta disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Conta conta in contas)
                Console.WriteLine(conta.ToString());

            Console.ReadLine();

            return true;
        }

        public bool VisualizarContasAbertas(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Contas Abertas");

            List<Conta> contas = repositorioConta.Filtrar(x => x.EmAberto);

            if (contas.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhuma conta disponível.", TipoMensagem.Atencao);
                return false;
            }
            foreach (Conta conta in contas)
                Console.WriteLine(conta.ToString());

            Console.ReadLine();

            return true;
        }

        internal void AdicionarPedidos()
        {
            throw new NotImplementedException();
        }

        private Conta ObterConta()
        {
            Console.Write("Digite o id da mesa: ");
            int _mesa = Convert.ToInt32(Console.ReadLine());
            Mesa mesa = repositorioMesa.SelecionarRegistro(_mesa);
            if (mesa == null)
                return null;
            Console.Write("Digite o id do Garçom: ");
            int _garcom = Convert.ToInt32(Console.ReadLine());
            Garcom garcom = repositorioGarcom.SelecionarRegistro(_garcom);
            if (garcom == null)
                return null;
            Console.Write("Digite o dia: ");
            DateTime dia = DateTime.Parse(Console.ReadLine());
            return new Conta(mesa, garcom, dia);
        }

        public bool VisualizarTotalGorjetas(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização do Total de Gorjetas em um determinado Dia para um Determinado Garçom");

            Console.WriteLine("Digite o dia: ");
            DateTime dia = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Lista de Garçons ---------------------");
            if (!telaCadastroGarcom.VisualizarRegistros("Tela"))
                return;

            Console.WriteLine("Digite a id do garçom desejado: ");
            int idGarcom = Convert.ToInt32(Console.ReadLine());
            Garcom garcom = repositorioGarcom.SelecionarRegistro();
            List<Conta> contas = repositorioConta.Filtrar(x => !x.EmAberto);

            if (contas.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhuma conta disponível.", TipoMensagem.Atencao);
                return false;
            }
            decimal soma = 0;

            contas.ForEach(x =>
            {
                soma += x.TotalPedidos * 1 / x.GorjetaPorcentagem;
            });
            Console.WriteLine("O total do Lucro Faturado em {0} é de: {1}", dia, soma);

            Console.ReadLine();

            return true;
        }

        public bool VisualizarTotalFaturadoDia(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização do Lucro em um determinado Dia");

            Console.WriteLine("Digite o dia: ");
            DateTime dia = DateTime.Parse(Console.ReadLine());

            List<Conta> contas = repositorioConta.Filtrar(x => !x.EmAberto && x.Dia == dia);

            if (contas.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhuma conta disponível.", TipoMensagem.Atencao);
                return false;
            }
            decimal soma = 0;

            contas.ForEach(x =>
            {
                soma += x.TotalPedidos;
            });            
            Console.WriteLine("O total do Lucro Faturado em {0} é de: {1}", dia, soma);

            Console.ReadLine();

            return true;

        }

        public override string MostrarOpcoes()
        {
            MostrarTitulo(Titulo);

            Console.WriteLine("Digite 1 para Abrir uma Conta");
            Console.WriteLine("Digite 2 para Adicionar Pedidos");
            Console.WriteLine("Digite 3 para Visualizar Contas Abertas");
            Console.WriteLine("Digite 4 para Visualizar Total Faturado Dia");
            Console.WriteLine("Digite 5 para Visualizar Total de Gorjetas");

            Console.WriteLine("Digite s para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }

        public int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID da conta que deseja selecionar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = repositorioConta.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    _notificador.ApresentarMensagem("ID da conta não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }
        /*public void Editar()
        {
            MostrarTitulo("Editando Conta");

            bool temRegistrosCadastrados = VisualizarRegistros("Pesquisando");

            if (temRegistrosCadastrados == false)
            {
                _notificador.ApresentarMensagem("Nenhuma conta cadastrada para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroConta = ObterNumeroRegistro();

            Conta contaAtualizada = ObterConta();

            bool conseguiuEditar = _repositorioConta.Editar(numeroConta, contaAtualizada);

            if (!conseguiuEditar)
                _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Conta editada com sucesso!", TipoMensagem.Sucesso);
        }*/

        /*public void Excluir()
        {
            MostrarTitulo("Excluíndo Conta");

            bool temRegistrosCadastrados = VisualizarRegistros("Pesquisando");

            if (temRegistrosCadastrados == false)
            {
                _notificador.ApresentarMensagem("Nenhuma conta cadastrada para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroCliente = ObterNumeroRegistro();

            bool conseguiuExcluir = _repositorioConta.Excluir(numeroCliente);

            if (!conseguiuExcluir)
                _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Conta excluída com sucesso!", TipoMensagem.Sucesso);
        }*/
    }
}
