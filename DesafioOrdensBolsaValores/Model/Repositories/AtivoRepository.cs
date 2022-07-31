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
        private IContext _context;
        public Action<AtivoEntity> NovoAtivo { get; set; }

        public AtivoRepository(IContext Context) 
        { 
            this._context = Context;
        }

        public List<AtivoEntity> GerarListadeAtivos(int pQtd)
        {            
            return _context.GerarListadeAtivos(pQtd);
        }

        public void AddAtivos(string pAtivoDigitado)
        {
            NovoAtivo.Invoke(_context.AddAtivo(pAtivoDigitado));
        }

        public List<AtivoEntity> UpdateAtivos()
        {
            return _context.UpdateAtivos();
        }

        public void LimparAtivos()
        {
            _context.LimparAtivos();
        }

    }
}
