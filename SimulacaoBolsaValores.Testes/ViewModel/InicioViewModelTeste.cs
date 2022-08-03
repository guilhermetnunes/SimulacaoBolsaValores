using Moq;
using SimulacaoBolsaValores.DataContext;
using SimulacaoBolsaValores.Model.Entities;
using SimulacaoBolsaValores.Services;
using SimulacaoBolsaValores.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SimulacaoBolsaValores.Testes.ViewModel
{
    public class InicioViewModelTeste
    {
        private DadosRepositorio _dadosRepositorio;

        private Mock<IRegistrosRepositorio> _mockRegistrosRepositorio;

        private InicioViewModel _inicioViewModel;

        private Mock<IAtivoController> _mockAtivoController;


        public InicioViewModelTeste()
        {
            _mockRegistrosRepositorio = new Mock<IRegistrosRepositorio>();

            _dadosRepositorio = new DadosRepositorio(_mockRegistrosRepositorio.Object);

            _mockAtivoController = new Mock<IAtivoController>();

            _inicioViewModel = new InicioViewModel(_mockAtivoController.Object);
        }

        [Fact (DisplayName = "Deve fazer a soma da coluna Qtd conforme itens da lista.")]
        public void AtualizarTotais_SomaDaQtd()
        {
            _inicioViewModel.LstAtivos.Add(new AtivoED { Qtd = 10 });
            _inicioViewModel.LstAtivos.Add(new AtivoED { Qtd = 15 });
            _inicioViewModel.AtualizarTotais();

            Assert.Equal(25, _inicioViewModel.TotalQuantidade);
        }

        [Fact]
        public void AtualizarTotais_SomaDaQtdDisp()
        {
            _inicioViewModel.LstAtivos.Add(new AtivoED { QtdDisp = 20 });
            _inicioViewModel.LstAtivos.Add(new AtivoED { QtdDisp = 3 });
            _inicioViewModel.AtualizarTotais();

            Assert.Equal(23, _inicioViewModel.TotalDisponivel);
        }

        [Fact]
        public void NovoItemAtivo_AdicionarNovoItemNaLista()
        {
            _inicioViewModel.NovoItemAtivo(new AtivoED { Id = Guid.NewGuid(), Ativo = "PETR123" });
            Assert.Single(_inicioViewModel.LstAtivos);
        }

        [Fact]
        public void NovosItensAtivos_AdicionaNovosItensNaLista()
        {
            List<AtivoED> listaAtivos = new List<AtivoED>();
            listaAtivos.Add(new AtivoED { Id = Guid.NewGuid(), Ativo = "PETR123" });
            listaAtivos.Add(new AtivoED { Id = Guid.NewGuid(), Ativo = "ABCD456" });
            _inicioViewModel.NovosItensAtivos(listaAtivos);
            Assert.Equal(2, _inicioViewModel.LstAtivos.Count);
        }

        [Fact]
        public void Processar_InicializarComListaDeAtivosVazia()
        {
            _inicioViewModel.LstAtivos = new ObservableCollection<AtivoED>();
            var exception = Assert.Throws<Exception>(() => _inicioViewModel.Processar());
            Assert.Equal("Inclua um ou mais Ativos.", exception.Message);
        }

        [Fact]
        public void Processar_InicializarComListaNula()
        {            
            var exception = Assert.Throws<Exception>(() => _inicioViewModel.Processar());
            Assert.Equal("Inclua um ou mais Ativos.", exception.Message);
        }

        [Fact]
        public void Processar_InicializarProcessamentoComAtivosNaLista()
        {
            ObservableCollection<AtivoED> listaAtivos = new ObservableCollection<AtivoED>();
            listaAtivos.Add(new AtivoED { Id = Guid.NewGuid(), Ativo = "PETR123" });
            listaAtivos.Add(new AtivoED { Id = Guid.NewGuid(), Ativo = "ABCD456" });
            _inicioViewModel.LstAtivos = listaAtivos;
            _inicioViewModel.Processar();
            Assert.True(_inicioViewModel.EmExecucao);
        }

        [Fact]
        public void Parar_DeveParar()
        {
            _inicioViewModel.Parar();
            Assert.True(_inicioViewModel.EmStandby);
        }

        [Fact]
        public void Limpar_LimparListaDeAtivos()
        {
            ObservableCollection<AtivoED> listaAtivos = new ObservableCollection<AtivoED>();
            listaAtivos.Add(new AtivoED { Id = Guid.NewGuid(), Ativo = "PETR123" });
            listaAtivos.Add(new AtivoED { Id = Guid.NewGuid(), Ativo = "ABCD456" });
            _inicioViewModel.LstAtivos = listaAtivos;
            _inicioViewModel.Limpar();
            Assert.Empty(listaAtivos);
        }
        
        [Fact]
        public void Adicionar_InicializarComCodigoAtivo()
        {
            Assert.True(false);
            //var lista = new List<string>();

            //string codigoAtivo = "PETR123";
            //_inicioViewModel.AtivoDigitado = codigoAtivo;

            //AtivoED ativo = new AtivoED { Id = Guid.NewGuid(), Ativo = "PETR123" };

            //_mockAtivoController.Setup(x => x.AdicionarAtivo(It.IsAny<string>()))
            //    .Callback<string>(s => lista.Add(s))
            //    .Verifiable();

            //_inicioViewModel.Adicionar();
            //Assert.Single(_inicioViewModel.LstAtivos);
        }

        [Fact]
        public void Adicionar_InicializarSemCodigoAtivo()
        {
            var exception = Assert.Throws<Exception>(() => _inicioViewModel.Adicionar());
            Assert.Equal("Digite o código do Ativo.", exception.Message);
        }

        [Fact]
        public void AdicionarAuto_InicializarComQtdDigitada()
        {
            Assert.True(false);
            //List<AtivoED> listaAtivos = new List<AtivoED>();
            //listaAtivos.Add(new AtivoED { Id = Guid.NewGuid(), Ativo = "PETR123" });
            //listaAtivos.Add(new AtivoED { Id = Guid.NewGuid(), Ativo = "PETR456" });

            //_inicioViewModel.QtdAtivosDigitadaParaGerarAtomaticamente = 2;

            //_mockAtivoController.Setup(x => x.AdicionarNovaListaAtivos(2)).Returns(listaAtivos);

            //var resultado = _inicioViewModel.AdicionarAuto();
            //Assert.Equal(2, resultado.Count);
        }

        [Fact]
        public void Adicionar_InicializarSemQtdDigitada()
        {
            var exception = Assert.Throws<Exception>(() => _inicioViewModel.AdicionarAuto());
            Assert.Equal("Digite uma quantidade.", exception.Message);
        }

        [Fact]
        public void AtualizarRegistros_DeveRetornarDadoDiferete()
        {
            Assert.True(false);
            //Guid Id = Guid.NewGuid();
            //AtivoED ativo = new AtivoED { Id = Id, Ativo = "PETR1234", Qtd = 10 };

            //_dadosRepositorio.DicionarioAtivos = new System.Collections.Concurrent.ConcurrentDictionary<Guid, AtivoED>();            
            //_dadosRepositorio.DicionarioAtivos.TryAdd(Id, ativo);

            //List<AtivoED> listaAtivosAtual = new List<AtivoED>();
            //listaAtivosAtual.Add(ativo);

            //_inicioViewModel.LstAtivos = listaAtivosAtual;

            //List<AtivoED> listaAtivosAtualizada = new List<AtivoED>();
            //listaAtivosAtualizada.Add(new AtivoED { Id = Id, Ativo = "PETR123", Qtd = 20 });

            //_mockAtivoController.Setup(x => x.AtualizarAtivos()).Returns(listaAtivosAtualizada);

            //var resultado = _inicioViewModel.AtualizarRegistros();
            //Assert.NotEqual(listaAtivosAtual[0].Qtd, listaAtivosAtualizada[0].Qtd);
        }


    }
}
