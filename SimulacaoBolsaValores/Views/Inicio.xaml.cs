using SimulacaoBolsaValores._Services;
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
        private GridViewColumnHeader? lstViewSortCol = null;
        private SortAdorner? lstViewSortAdorner = null;

        [ExcludeFromCodeCoverage]
        public Inicio()
        {
            InitializeComponent();
            this.DataContext = new InicioViewModel(new AtivoController(new DadosRepositorio(new RegistrosRepositorio())));
        }

        [ExcludeFromCodeCoverage]
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

        private void grdAtivos_Sorting(object sender, DataGridSortingEventArgs e)
        {

        }
    }
}
