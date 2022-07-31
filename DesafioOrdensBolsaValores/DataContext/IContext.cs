using SimulacaoBolsaValores.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulacaoBolsaValores.DataContext
{
    public interface IContext
    {
        List<AtivoEntity> GerarListadeAtivos(int pQtd);        
        AtivoEntity AddAtivo(string pAtivoDigitado);
        List<AtivoEntity> UpdateAtivos();
        void LimparAtivos();
    }
}
