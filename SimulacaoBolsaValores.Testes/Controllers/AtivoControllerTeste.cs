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

        [Fact]
        public void AtualizarAtivos_DeveRetornarListadeAtivos()
        {
            List<AtivoED> listaAtivos = new List<AtivoED>();
            listaAtivos.Add(new AtivoED { Id = Guid.NewGuid(), Ativo = "PETR123", Qtd = 10 });

            _mockDadosRepositorio.Setup(x => x.AtualizarAtivos()).Returns(listaAtivos);

            var resultado = _ativoController.AtualizarAtivos();            
            Assert.NotNull(resultado);
        }

    }
}
