using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulacaoBolsaValores.DataContext
{
    public class RegistrosRepositorio : IRegistrosRepositorio
    {
        public int GerarNumeroInteiroEntre0e100Aleatorio()
        {
            Random r = new Random();
            int numero = r.Next(0, 100);
            return numero;
        }
        public decimal GerarNovoPrecoEntre0e100Aleatorio()
        {
            Random r = new Random();
            double numero = r.Next(0, 100);

            double casasDecimais = r.NextDouble();
            numero += casasDecimais;

            numero = Math.Round(numero, 2);

            return Convert.ToDecimal(numero);
        }
        public string GerarCodigoLetrasNumerosAleatorio()
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
    }
}
