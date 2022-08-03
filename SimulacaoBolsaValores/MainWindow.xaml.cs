using SimulacaoBolsaValores.Views;
using System.Diagnostics.CodeAnalysis;
using System.Windows;


namespace SimulacaoBolsaValores
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    [ExcludeFromCodeCoverage]
    public partial class MainWindow : Window
    {
        [ExcludeFromCodeCoverage]
        public MainWindow()
        {
            InitializeComponent();
            mainContent.Content = new Inicio();
        }
    }
}
