using SimulacaoBolsaValores.DataContext;
using SimulacaoBolsaValores.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulacaoBolsaValores.Services
{
    public class AtivoController : IAtivoController
    {
        private IDadosRepositorio _dadosRepositorio;

        public Action<AtivoED> NovoAtivoAction { get; set; }
        public Action<List<AtivoED>> NovaListaAtivosAction { get; set; }

        public AtivoController(IDadosRepositorio DadosRepositorio)
        {
            _dadosRepositorio = DadosRepositorio;
        }
        //public AtivoED AdicionarAtivo(string pAtivoDigitado)
        //{
        //    return _dadosRepositorio.AdicionarAtivo(pAtivoDigitado);
        //}
        public void AdicionarAtivo(string pAtivoDigitado)
        {
            NovoAtivoAction.Invoke(_dadosRepositorio.AdicionarAtivo(pAtivoDigitado));
        }
        //public List<AtivoED> AdicionarNovaListaAtivos(int pQtd)
        //{
        //    return _dadosRepositorio.AdicionarNovaListaAtivos(pQtd);
        //}
        public void AdicionarNovaListaAtivos(int pQtd)
        {
            NovaListaAtivosAction.Invoke(_dadosRepositorio.AdicionarNovaListaAtivos(pQtd));
        }
        public List<AtivoED> AtualizarAtivos()
        {
            return _dadosRepositorio.AtualizarAtivos();
        }
        public void LimparAtivos()
        {
            _dadosRepositorio.LimparAtivos();
        }

    }
}
