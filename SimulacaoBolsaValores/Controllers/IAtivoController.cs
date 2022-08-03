using SimulacaoBolsaValores.Model.Entities;
using System;
using System.Collections.Generic;

namespace SimulacaoBolsaValores.Services
{
    public interface IAtivoController
    {
        Action<List<AtivoED>> NovaListaAtivosAction { get; set; }
        Action<AtivoED> NovoAtivoAction { get; set; }

        AtivoED AdicionarAtivo(string pAtivoDigitado);
        List<AtivoED> AdicionarNovaListaAtivos(int pQtd);
        List<AtivoED> AtualizarAtivos();
        bool LimparAtivos();
    }
}