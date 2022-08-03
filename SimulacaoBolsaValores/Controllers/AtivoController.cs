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
        
        public AtivoED Ativo { get; set; }
        public List<AtivoED> LstAtivos { get; set; }

        public bool LimpezaFeita { get; set; }

        public Action<AtivoED> NovoAtivoAction { get; set; }
        public Action<List<AtivoED>> NovaListaAtivosAction { get; set; }

        public AtivoController(IDadosRepositorio DadosRepositorio)
        {
            _dadosRepositorio = DadosRepositorio;
        }

        public AtivoED AdicionarAtivo(string pAtivoDigitado)
        {
            Ativo = _dadosRepositorio.AdicionarAtivo(pAtivoDigitado);

            NovoAtivoAction.Invoke(Ativo);

            return Ativo;
        }

        public List<AtivoED> AdicionarNovaListaAtivos(int pQtd)
        {
            LstAtivos = _dadosRepositorio.AdicionarNovaListaAtivos(pQtd);

            NovaListaAtivosAction.Invoke(LstAtivos);

            return LstAtivos;
        }
        public List<AtivoED> AtualizarAtivos()
        {
            LstAtivos = _dadosRepositorio.AtualizarAtivos();

            return LstAtivos;
        }
        public bool LimparAtivos()
        {
            LimpezaFeita = _dadosRepositorio.LimparAtivos();

            return LimpezaFeita;
        }
    }
}
