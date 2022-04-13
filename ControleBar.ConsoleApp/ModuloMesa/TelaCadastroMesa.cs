using System;
using ControleBar.ConsoleApp.Compartilhado;
using System.Collections.Generic;

namespace ControleBar.ConsoleApp.ModuloMesa
{
    public class TelaCadastroMesa : TelaBase, ITelaCadastravel
    {
        private readonly IRepositorio<Mesa> _repositorioMesa;
        private readonly Notificador _notificador;

        public TelaCadastroMesa(IRepositorio<Mesa> repositorioMesa, Notificador notificador)
            : base("Cadastro de Mesa")
        {
            _repositorioMesa = repositorioMesa;
            _notificador = notificador;
        }

        public void Inserir()
        {
            MostrarTitulo("Cadastro de Mesa");

            Mesa novaMesa = ObterMesa();

            _repositorioMesa.Inserir(novaMesa);

            _notificador.ApresentarMensagem("Mesa cadastrada com sucesso!", TipoMensagem.Sucesso);
        }

        public void Editar()
        {
            MostrarTitulo("Editando Mesa");

            bool temRegistrosCadastrados = VisualizarRegistros("Pesquisando");

            if (temRegistrosCadastrados == false)
            {
                _notificador.ApresentarMensagem("Nenhuma mesa cadastrada para editar.", TipoMensagem.Atencao);
                return;
            }

            int numeroMesa = ObterNumeroRegistro();

            Mesa mesaAtualizada = ObterMesa();

            bool conseguiuEditar = _repositorioMesa.Editar(numeroMesa, mesaAtualizada);

            if (!conseguiuEditar)
                _notificador.ApresentarMensagem("Não foi possível editar.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Mesa editada com sucesso!", TipoMensagem.Sucesso);
        }

        public void Excluir()
        {
            MostrarTitulo("Excluindo Mesa");

            bool temFuncionariosRegistrados = VisualizarRegistros("Pesquisando");

            if (temFuncionariosRegistrados == false)
            {
                _notificador.ApresentarMensagem("Nenhuma mesa cadastrada para excluir.", TipoMensagem.Atencao);
                return;
            }

            int numeroGarcom = ObterNumeroRegistro();

            bool conseguiuExcluir = _repositorioMesa.Excluir(numeroGarcom);

            if (!conseguiuExcluir)
                _notificador.ApresentarMensagem("Não foi possível excluir.", TipoMensagem.Erro);
            else
                _notificador.ApresentarMensagem("Mesa excluída com sucesso!", TipoMensagem.Sucesso);
        }

        public bool VisualizarRegistros(string tipoVisualizacao)
        {
            if (tipoVisualizacao == "Tela")
                MostrarTitulo("Visualização de Mesa Cadastradas");

            List<Mesa> mesas = _repositorioMesa.SelecionarTodos();

            if (mesas.Count == 0)
            {
                _notificador.ApresentarMensagem("Nenhuma mesa disponível.", TipoMensagem.Atencao);
                return false;
            }

            foreach (Mesa mesa in mesas)
                Console.WriteLine(mesa.ToString());

            Console.ReadLine();

            return true;
        }

        private Mesa ObterMesa()
        {
            Console.Write("Digite a cor da mesa: ");
            string cor = Console.ReadLine();
            return new Mesa(cor);
        }

        public int ObterNumeroRegistro()
        {
            int numeroRegistro;
            bool numeroRegistroEncontrado;

            do
            {
                Console.Write("Digite o ID da mesa que deseja selecionar: ");
                numeroRegistro = Convert.ToInt32(Console.ReadLine());

                numeroRegistroEncontrado = _repositorioMesa.ExisteRegistro(numeroRegistro);

                if (numeroRegistroEncontrado == false)
                    _notificador.ApresentarMensagem("ID da mesa não foi encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroRegistroEncontrado == false);

            return numeroRegistro;
        }
    }
}
