using SimulacaoBolsaValores._Services;
using SimulacaoBolsaValores.DataContext;
using SimulacaoBolsaValores.Services;
using SimulacaoBolsaValores.ViewModels;
using System.ComponentModel;
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
        private GridViewColumnHeader? lstViewSortCol = null;
        private SortAdorner? lstViewSortAdorner = null;
        
        public Inicio()
        {
            InitializeComponent();
            this.DataContext = new InicioViewModel(new AtivoService(new DadosRepositorio(new RegistrosRepositorio())));
        }

        private void GridColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = (sender as GridViewColumnHeader);

            string sortBy = column.Tag.ToString();

            if (lstViewSortCol != null)
            {
                AdornerLayer.GetAdornerLayer(lstViewSortCol).Remove(lstViewSortAdorner);
                grdAtivos.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDir = ListSortDirection.Ascending;
            if (lstViewSortCol == column && lstViewSortAdorner.Direction == newDir)
                newDir = ListSortDirection.Descending;

            lstViewSortCol = column;
            lstViewSortAdorner = new SortAdorner(lstViewSortCol, newDir);
            AdornerLayer.GetAdornerLayer(lstViewSortCol).Add(lstViewSortAdorner);
            grdAtivos.Items.SortDescriptions.Add(new SortDescription(sortBy, newDir));

        }
    }
}
