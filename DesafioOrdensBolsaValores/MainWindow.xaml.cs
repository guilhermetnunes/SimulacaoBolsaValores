using SimulacaoBolsaValores.Views;
using System.Windows;


namespace SimulacaoBolsaValores
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            mainContent.Content = new Inicio();
        }
    }
}
