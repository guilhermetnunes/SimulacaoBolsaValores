﻿using SimulacaoBolsaValores.DataContext;
using SimulacaoBolsaValores.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulacaoBolsaValores.Services
{
    public class AtivoService : IAtivoService
    {
        private IDadosRepositorio _dadosRepositorio;

        public Action<AtivoED> NovoAtivoAction { get; set; }
        public Action<List<AtivoED>> NovaListaAtivosAction { get; set; }

        public AtivoService(IDadosRepositorio DadosRepositorio)
        {
            _dadosRepositorio = DadosRepositorio;
        }
        public void AdicionarAtivo(string pAtivoDigitado)
        {
            NovoAtivoAction.Invoke(_dadosRepositorio.AdicionarAtivo(pAtivoDigitado));
        }
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
