using SimulacaoBolsaValores.DataContext;
using SimulacaoBolsaValores.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulacaoBolsaValores.Model.Repositories
{
    public class AtivoRepository
    {
        public List<AtivoEntity> GerarListadeAtivos(int pQtd)
        {
            IContext ativo = new AtivoContext();
            return ativo.GerarListadeAtivos(pQtd);
        }
        public List<AtivoEntity> CleanAtivos()
        {
            IContext ativo = new AtivoContext();
            return ativo.CleanAtivos();
        }
        public List<AtivoEntity> AddAtivos(string pAtivoDigitado, List<AtivoEntity> pLstAtual)
        {
            IContext ativo = new AtivoContext();
            return ativo.AddAtivo(pAtivoDigitado, pLstAtual);
        }
    }
}
