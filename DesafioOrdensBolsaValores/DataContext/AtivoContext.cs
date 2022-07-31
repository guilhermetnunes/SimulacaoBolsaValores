using System.Collections.Generic;
using System;
using System.Linq;
using SimulacaoBolsaValores.Model.Entities;
using System.Collections.Concurrent;

namespace SimulacaoBolsaValores.DataContext
{
    public class AtivoContext : IContext
    {
        private ConcurrentDictionary<Guid, AtivoEntity> _concurrentDic { get; set; }

        public AtivoContext()
        {
            _concurrentDic = new ConcurrentDictionary<Guid, AtivoEntity>();
        }

        public List<AtivoEntity> GerarListadeAtivos(int pQtd)
        {
            List<AtivoEntity> lstAtivos = new List<AtivoEntity>();

            for (int i = 0; i < pQtd; i++)
            {
                lstAtivos.Add(AddAtivo(GerarCodigodeAtivoComLetrasENumeros()));
            }

            return lstAtivos;
        }

        public AtivoEntity AddAtivo(string pAtivoDigitado)
        {
            var ativo = new AtivoEntity
            {   
                Id = Guid.NewGuid(),
                DataHora = DateTime.Now, 
                Assessor = "-", 
                Conta = "3934072", 
                Ativo = pAtivoDigitado, 
                Tipo = 'C', 
                Qtd = GerarNumeroInteiroAleatorioEntre0e100(), 
                QtdAparente = GerarNumeroInteiroAleatorioEntre0e100(), 
                QtdDisp = GerarNumeroInteiroAleatorioEntre0e100(), 
                QtdCancel = GerarNumeroInteiroAleatorioEntre0e100(), 
                QtdExec = GerarNumeroInteiroAleatorioEntre0e100(), 
                Valor = GerarPrecoAleatorioEntre0e100(),
                ValorDisp = GerarPrecoAleatorioEntre0e100(), 
                Objetivo = GerarPrecoAleatorioEntre0e100(), 
                ObjDisp = GerarPrecoAleatorioEntre0e100(), 
                Reducao = 0 
            };

            _concurrentDic.TryAdd(ativo.Id, ativo);

            return ativo;
        }

        public void LimparAtivos()
        {
            _concurrentDic.Clear();
        }

        public int GerarNumeroInteiroAleatorioEntre0e100() 
        {
            Random r = new Random();
            int numero = r.Next(0, 100);
            return numero;
        }

        public decimal GerarPrecoAleatorioEntre0e100()
        {
            Random r = new Random();
            double numero = r.Next(0,100);

            double casasDecimais = r.NextDouble();
            numero += casasDecimais;
            
            numero = Math.Round(numero, 2);

            return Convert.ToDecimal(numero);
        }

        public string GerarCodigodeAtivoComLetrasENumeros()
        {
            string codigoAtivo;

            var caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var letras = new char[2];
            Random r = new Random();
            
            for (int i = 0; i < 2; i++)
            {
                letras[i] = caracteres[r.Next(26)];
            }

            codigoAtivo = new String(letras);

            for (int i = 0; i < 3; i++)
            {
                int numero = r.Next(0, 9);
                codigoAtivo += numero.ToString();
            }

            return codigoAtivo;
        }

        public List<AtivoEntity> UpdateAtivos()
        {
            List<AtivoEntity> lstAtivos = new List<AtivoEntity>();

            foreach (AtivoEntity ativo in _concurrentDic.Values)
            {
                ativo.DataHora = DateTime.Now;
                ativo.Qtd = GerarNumeroInteiroAleatorioEntre0e100();
                ativo.QtdAparente = GerarNumeroInteiroAleatorioEntre0e100();
                ativo.QtdDisp = GerarNumeroInteiroAleatorioEntre0e100();
                ativo.QtdCancel = GerarNumeroInteiroAleatorioEntre0e100();
                ativo.QtdExec = GerarNumeroInteiroAleatorioEntre0e100();
                ativo.Valor = GerarPrecoAleatorioEntre0e100();
                ativo.ValorDisp = GerarPrecoAleatorioEntre0e100();
                ativo.Objetivo = GerarPrecoAleatorioEntre0e100();
                ativo.ObjDisp = GerarPrecoAleatorioEntre0e100();
                lstAtivos.Add(ativo);
            }

            return lstAtivos;
        }
    }
}
