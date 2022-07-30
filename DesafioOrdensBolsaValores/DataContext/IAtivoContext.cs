using SimulacaoBolsaValores.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulacaoBolsaValores.DataContext
{
    interface IContext
    {
        List<AtivoEntity> GerarListadeAtivos(int pQtd);
        List<AtivoEntity> CleanAtivos();
        List<AtivoEntity> AddAtivo(string pAtivoDigitadp, List<AtivoEntity> pLstAtual);
    }
}
