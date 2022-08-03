using SimulacaoBolsaValores.Model.Entities;
using System;
using System.Collections.Generic;

namespace SimulacaoBolsaValores.Services
{
    public interface IAtivoController
    {
        Action<List<AtivoED>> NovaListaAtivosAction { get; set; }
        Action<AtivoED> NovoAtivoAction { get; set; }

        void AdicionarAtivo(string pAtivoDigitado);
        void AdicionarNovaListaAtivos(int pQtd);
        List<AtivoED> AtualizarAtivos();
        void LimparAtivos();
    }
}