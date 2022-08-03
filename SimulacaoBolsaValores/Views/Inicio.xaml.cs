using SimulacaoBolsaValores.DataContext;
using SimulacaoBolsaValores.Services;
using SimulacaoBolsaValores.ViewModels;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace SimulacaoBolsaValores.Views
{
    /// <summary>
    /// Interação lógica para Inicio.xam
    /// </summary>
    public partial class Inicio : Page
    {
        [ExcludeFromCodeCoverage]
        public Inicio()
        {
            InitializeComponent();
            this.DataContext = new InicioViewModel(new AtivoController(new DadosRepositorio(new RegistrosRepositorio())));
        }

    }
}
