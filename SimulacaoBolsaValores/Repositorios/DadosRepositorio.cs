using System.Collections.Generic;
using System;
using System.Linq;
using SimulacaoBolsaValores.Model.Entities;
using System.Collections.Concurrent;

namespace SimulacaoBolsaValores.DataContext
{
    public class DadosRepositorio : IDadosRepositorio
    {
        private IRegistrosRepositorio _registrosRepositorio { get; set; }
        private ConcurrentDictionary<Guid, AtivoED> _dicionarioAtivos { get; set; }
        public ConcurrentDictionary<Guid, AtivoED> DicionarioAtivos { get { return _dicionarioAtivos; } set { _dicionarioAtivos = value; } }

        public DadosRepositorio(IRegistrosRepositorio RegistrosRepositorio)
        {            
            _dicionarioAtivos = new ConcurrentDictionary<Guid, AtivoED>();
            _registrosRepositorio = RegistrosRepositorio;
        }        

        public AtivoED AdicionarAtivo(string pAtivoDigitado)
        {
            if (string.IsNullOrEmpty(pAtivoDigitado))
                throw new Exception("Nenhum ativo informado!");

            var ativo = new AtivoED
            {   
                Id = Guid.NewGuid(),
                DataHora = DateTime.Now, 
                Assessor = "-", 
                Conta = "3934072", 
                Ativo = pAtivoDigitado, 
                Tipo = 'C', 
                Qtd = _registrosRepositorio.GerarNumeroInteiroEntre0e100Aleatorio(), 
                QtdAparente = _registrosRepositorio.GerarNumeroInteiroEntre0e100Aleatorio(), 
                QtdDisp = _registrosRepositorio.GerarNumeroInteiroEntre0e100Aleatorio(), 
                QtdCancel = _registrosRepositorio.GerarNumeroInteiroEntre0e100Aleatorio(), 
                QtdExec = _registrosRepositorio.GerarNumeroInteiroEntre0e100Aleatorio(), 
                Valor = _registrosRepositorio.GerarNovoPrecoEntre0e100Aleatorio(),
                ValorDisp = _registrosRepositorio.GerarNovoPrecoEntre0e100Aleatorio(), 
                Objetivo = _registrosRepositorio.GerarNovoPrecoEntre0e100Aleatorio(), 
                ObjDisp = _registrosRepositorio.GerarNovoPrecoEntre0e100Aleatorio(), 
                Reducao = 0 
            };
            
            _dicionarioAtivos.TryAdd(ativo.Id, ativo);

            return ativo;
        }
        public List<AtivoED> AdicionarNovaListaAtivos(int pQtd)
        {
            if (pQtd == 0)
                throw new Exception("Nenhuma quantidade informada!");

            List<AtivoED> lstAtivos = new List<AtivoED>();

            for (int i = 0; i < pQtd; i++)
            {
                string novoCodigo = _registrosRepositorio.GerarCodigoLetrasNumerosAleatorio();
                
                lstAtivos.Add(AdicionarAtivo(novoCodigo));
            }           

            return lstAtivos;
        }
        public List<AtivoED> AtualizarAtivos()
        {
            List<AtivoED> lstAtivos = new List<AtivoED>();

            foreach (AtivoED ativo in _dicionarioAtivos.Values)
            {
                ativo.DataHora = DateTime.Now;
                ativo.Qtd = _registrosRepositorio.GerarNumeroInteiroEntre0e100Aleatorio();
                ativo.QtdAparente = _registrosRepositorio.GerarNumeroInteiroEntre0e100Aleatorio();
                ativo.QtdDisp = _registrosRepositorio.GerarNumeroInteiroEntre0e100Aleatorio();
                ativo.QtdCancel = _registrosRepositorio.GerarNumeroInteiroEntre0e100Aleatorio();
                ativo.QtdExec = _registrosRepositorio.GerarNumeroInteiroEntre0e100Aleatorio();
                ativo.Valor = _registrosRepositorio.GerarNovoPrecoEntre0e100Aleatorio();
                ativo.ValorDisp = _registrosRepositorio.GerarNovoPrecoEntre0e100Aleatorio();
                ativo.Objetivo = _registrosRepositorio.GerarNovoPrecoEntre0e100Aleatorio();
                ativo.ObjDisp = _registrosRepositorio.GerarNovoPrecoEntre0e100Aleatorio();
                lstAtivos.Add(ativo);
            }            

            return lstAtivos;
        }
        public void LimparAtivos()
        {
            _dicionarioAtivos.Clear();
        }
       
    }
}
