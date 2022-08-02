using SimulacaoBolsaValores.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulacaoBolsaValores.DataContext
{
    public interface IDadosRepositorio
    {
        AtivoED AdicionarAtivo(string pAtivoDigitado);
        List<AtivoED> AdicionarNovaListaAtivos(int pQtd);        
        List<AtivoED> AtualizarAtivos();
        void LimparAtivos();
    }
}
