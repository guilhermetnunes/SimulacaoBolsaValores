using Moq;
using SimulacaoBolsaValores.DataContext;
using SimulacaoBolsaValores.Model.Entities;
using SimulacaoBolsaValores.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SimulacaoBolsaValores.Testes.Controllers
{
    public class AtivoControllerTeste
    {
        private AtivoController _ativoController;

        private Mock<IDadosRepositorio> _mockDadosRepositorio;

        public AtivoControllerTeste()
        {
            _mockDadosRepositorio = new Mock<IDadosRepositorio>();

            _ativoController = new AtivoController(_mockDadosRepositorio.Object);
        }

        //Utilizado na Action
        public void NovoItemAtivo(AtivoED obj){}
        public void NovosItensAtivos(List<AtivoED> obj) { }

        [Fact]
        public void AdicionarAtivo_DeveAdicionarUmNovoAtivo()
        {
            AtivoED Ativo = new AtivoED { Id = Guid.NewGuid(), Ativo = "PETR123" };

            _mockDadosRepositorio.Setup(x => x.AdicionarAtivo("PETR123")).Returns(Ativo);            

            _ativoController = new AtivoController(_mockDadosRepositorio.Object);

            _ativoController.NovoAtivoAction += NovoItemAtivo;

            var resultado = _ativoController.AdicionarAtivo("PETR123");            
            Assert.NotNull(resultado);
        }

        [Fact]
        public void AdicionarNovaListaAtivos_DeveRetornarListadeAtivos()
        {
            List<AtivoED> listaAtivos = new List<AtivoED>();
            listaAtivos.Add(new AtivoED { Id = Guid.NewGuid(), Ativo = "PETR123" });
            listaAtivos.Add(new AtivoED { Id = Guid.NewGuid(), Ativo = "ABCD456" });

            _mockDadosRepositorio.Setup(x => x.AdicionarNovaListaAtivos(2)).Returns(listaAtivos);

            _ativoController = new AtivoController(_mockDadosRepositorio.Object);

            _ativoController.NovaListaAtivosAction += NovosItensAtivos;

            var resultado = _ativoController.AdicionarNovaListaAtivos(2);
            Assert.True(resultado.Count == 2);
        }

        
            [Fact]
        public void AtualizarAtivos_DeveRetornarListadeAtivosAtualizada()
        {
            List<AtivoED> listaAtivos = new List<AtivoED>();
            listaAtivos.Add(new AtivoED { Id = Guid.NewGuid(), Ativo = "PETR123" });
            listaAtivos.Add(new AtivoED { Id = Guid.NewGuid(), Ativo = "ABCD456" });

            _mockDadosRepositorio.Setup(x => x.AtualizarAtivos()).Returns(listaAtivos);

            _ativoController = new AtivoController(_mockDadosRepositorio.Object);

            _ativoController.NovaListaAtivosAction += NovosItensAtivos;

            var resultado = _ativoController.AtualizarAtivos();
            Assert.True(resultado.Count == 2);
        }


        [Fact]
        public void LimparAtivos_DeveEsvaviarLista()
        {
            _mockDadosRepositorio.Setup(x => x.LimparAtivos()).Returns(true);

            _ativoController = new AtivoController(_mockDadosRepositorio.Object);

            var retorno = _ativoController.LimparAtivos();
            Assert.True(retorno);
        }

    }
}
