using Moq;
using SimulacaoBolsaValores.Model.Entities;
using SimulacaoBolsaValores.Services;
using SimulacaoBolsaValores.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SimulacaoBolsaValores.Testes.ViewModel
{
    public class InicioViewModelTeste
    {
        private InicioViewModel _inicioViewModel;

        private Mock<IAtivoService> _mockAtivoService;

        public InicioViewModelTeste()
        {
            _mockAtivoService = new Mock<IAtivoService>();

            _inicioViewModel = new InicioViewModel(_mockAtivoService.Object);
        }

        [Fact]
        public void AtualizarTotais_RetornaSomaDaLista()
        {
            _inicioViewModel.LstAtivos = new System.Collections.ObjectModel.ObservableCollection<Model.Entities.AtivoED>();
            _inicioViewModel.LstAtivos.Add(new AtivoED { Qtd = 10 });
            _inicioViewModel.LstAtivos.Add(new AtivoED { Qtd = 15 });
            _inicioViewModel.AtualizarTotais();

            Assert.Equal(25, _inicioViewModel.TotalQuantidade);
        }
    }
}
