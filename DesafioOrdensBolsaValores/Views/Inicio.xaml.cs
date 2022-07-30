using SimulacaoBolsaValores.ViewModels;
using System.Windows.Controls;

namespace SimulacaoBolsaValores.Views
{
    /// <summary>
    /// Interação lógica para Inicio.xam
    /// </summary>
    public partial class Inicio : Page
    {
        public Inicio()
        {
            InitializeComponent();
            this.DataContext = new InicioViewModel();
        }
    }
}
