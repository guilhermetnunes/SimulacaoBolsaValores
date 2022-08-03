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

        private AtivoController _ativoController;

        private Mock<IDadosRepositorio> _mockDadosRepositorio;


        public InicioViewModelTeste()
        {
            _mockRegistrosRepositorio = new Mock<IRegistrosRepositorio>();

            _dadosRepositorio = new DadosRepositorio(_mockRegistrosRepositorio.Object);

            _mockAtivoController = new Mock<IAtivoController>();

            _inicioViewModel = new InicioViewModel(_mockAtivoController.Object);

            _mockDadosRepositorio = new Mock<IDadosRepositorio>();

            _ativoController = new AtivoController(_mockDadosRepositorio.Object);
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
        public void Limpar_ListaDeAtivosNula()
        {
            var exception = Assert.Throws<Exception>(() => _inicioViewModel.Limpar());
            Assert.Equal("Não há dados para limpar.", exception.Message);
        }

        [Fact]
        public void Limpar_ListaDeAtivosVazia()
        {
            _inicioViewModel.LstAtivos = new ObservableCollection<AtivoED>();
            var exception = Assert.Throws<Exception>(() => _inicioViewModel.Limpar());
            Assert.Equal("Não há dados para limpar.", exception.Message);
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
        public void Adicionar_InicializarSemCodigoAtivo()
        {
            var exception = Assert.Throws<Exception>(() => _inicioViewModel.Adicionar());
            Assert.Equal("Digite o código do Ativo.", exception.Message);
        }

        [Fact]
        public void AdicionarAuto_InicializarSemQtdDigitada()
        {
            var exception = Assert.Throws<Exception>(() => _inicioViewModel.AdicionarAuto());
            Assert.Equal("Digite uma quantidade.", exception.Message);
        }

        [Fact]
        public void Adicionar_ComCodigoAtivo()
        {
            AtivoED Ativo = new AtivoED { Id = Guid.NewGuid(), Ativo = "PETR123" };
                        
            _mockDadosRepositorio.Setup(x => x.AdicionarAtivo("PETR123")).Returns(Ativo);

            _ativoController = new AtivoController(_mockDadosRepositorio.Object);

            _mockAtivoController.Setup(x => x.AdicionarAtivo("PETR123")).Returns(Ativo);

            _inicioViewModel = new InicioViewModel(_mockAtivoController.Object);

            _inicioViewModel.AtivoDigitado = "PETR123";
            var resultado = _inicioViewModel.Adicionar();
            Assert.NotNull(resultado);
        }

        [Fact]
        public void AdicionarAuto_ComQtdDigitada()
        {
            List<AtivoED> listaAtivos = new List<AtivoED>();
            listaAtivos.Add(new AtivoED { Id = Guid.NewGuid(), Ativo = "PETR123" });
            listaAtivos.Add(new AtivoED { Id = Guid.NewGuid(), Ativo = "ABCD456" });

            _mockDadosRepositorio.Setup(x => x.AdicionarNovaListaAtivos(2)).Returns(listaAtivos);

            _ativoController = new AtivoController(_mockDadosRepositorio.Object);

            _mockAtivoController.Setup(x => x.AdicionarNovaListaAtivos(2)).Returns(listaAtivos);

            _inicioViewModel = new InicioViewModel(_mockAtivoController.Object);

            _inicioViewModel.QtdAtivosDigitadaParaGerarAtomaticamente = 2;

            var resultado = _inicioViewModel.AdicionarAuto();

            Assert.True(resultado.Count == 2);

        }

        [Fact]
        public void Atualizar_DeveRetornarNovoListadeAtivos()
        {
            List<AtivoED> listaAtivos = new List<AtivoED>();
            listaAtivos.Add(new AtivoED { Id = Guid.NewGuid(), Ativo = "PETR123", Qtd = 10 });
            listaAtivos.Add(new AtivoED { Id = Guid.NewGuid(), Ativo = "ABCD456", Qtd = 56 });

            ObservableCollection<AtivoED> listaAtivosObs = new ObservableCollection<AtivoED>();
            listaAtivosObs.Add(new AtivoED { Id = Guid.NewGuid(), Ativo = "PETR123", Qtd = 15 });
            listaAtivosObs.Add(new AtivoED { Id = Guid.NewGuid(), Ativo = "ABCD456", Qtd = 45 });

            _mockDadosRepositorio.Setup(x => x.AtualizarAtivos()).Returns(listaAtivos);

            _ativoController = new AtivoController(_mockDadosRepositorio.Object);

            _mockAtivoController.Setup(x => x.AtualizarAtivos()).Returns(listaAtivos);

            _inicioViewModel = new InicioViewModel(_mockAtivoController.Object);

            _inicioViewModel.LstAtivos = listaAtivosObs;
            var resultado = _inicioViewModel.Atualizar();

            Assert.Equal(listaAtivosObs[0].Qtd, resultado[0].Qtd);
        }        
    }
}
