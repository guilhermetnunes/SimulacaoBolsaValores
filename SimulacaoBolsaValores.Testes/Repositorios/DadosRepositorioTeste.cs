using AutoMapper;
using Moq;
using SimulacaoBolsaValores.DataContext;
using SimulacaoBolsaValores.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SimulacaoBolsaValores.Testes.Services
{
    public class DadosRepositorioTeste
    {
        private DadosRepositorio _dadosRepositorio;

        private Mock<IRegistrosRepositorio> _mockRegistrosRepositorio;

        public DadosRepositorioTeste()
        {
            _mockRegistrosRepositorio = new Mock<IRegistrosRepositorio>();

            _dadosRepositorio = new DadosRepositorio(_mockRegistrosRepositorio.Object);
        }

        [Fact]
        public void LimparAtivos_RetornandoListaVazia()
        {
            _dadosRepositorio.LimparAtivos();            
            Assert.Empty(_dadosRepositorio.DicionarioAtivos);
        }

        [Fact]
        public void AdicionarAtivo_RetornandoAtivoComMesmoNomeInformado()
        {
            var ativo = _dadosRepositorio.AdicionarAtivo("PETR123");
            Assert.Equal("PETR123", ativo.Ativo);
        }

        [Fact]
        public void AdicionarNovaListaAtivos_RetornandoListaComMesmaQtdDeItensInformada()
        {
            _mockRegistrosRepositorio.Setup(x => x.GerarCodigoLetrasNumerosAleatorio()).Returns("AAA1234");
            var qtdItensLista = _dadosRepositorio.AdicionarNovaListaAtivos(5);            
            Assert.Equal(5, qtdItensLista.Count());
        }

        [Fact]
        public void AtualizarAtivos_RetornaListaAtualizadaComGuidDiferente()
        {
            _dadosRepositorio.DicionarioAtivos = new System.Collections.Concurrent.ConcurrentDictionary<Guid, AtivoED>();
            Guid Id = Guid.NewGuid();
            _dadosRepositorio.DicionarioAtivos.TryAdd(Id, new AtivoED { Id = Id, Ativo = "PETR1234", Qtd = 10 });

            var lstAtivos = _dadosRepositorio.AtualizarAtivos();

            Assert.NotEqual(10, lstAtivos[0].Qtd);
        }
    }
}
