using SimulacaoBolsaValores.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SimulacaoBolsaValores.Testes.Services
{
    public class RegistrosRepositorioTeste
    {
        private RegistrosRepositorio _registrosRepositorio;

        public RegistrosRepositorioTeste()
        {
            _registrosRepositorio = new RegistrosRepositorio();
        }

        [Fact]
        public void GerarCodigoLetrasNumerosAleatorio_DeveConterLetrasENumeros()
        {
            var codigo = _registrosRepositorio.GerarCodigoLetrasNumerosAleatorio();

            bool codigoValido = codigo.Any(char.IsLetter);
            codigoValido = codigo.Any(char.IsNumber);

            Assert.True(codigoValido);
        }

        [Fact]
        public void GerarNumeroInteiroEntre0e100Aleatorio_DeveConterUmNumeroEntre0e100()
        {
            var numero = _registrosRepositorio.GerarNumeroInteiroEntre0e100Aleatorio();

            bool numeroValido = numero >= 0 && numero <= 100;

            Assert.True(numeroValido);
        }

        [Fact]
        public void GerarNovoPrecoEntre0e100Aleatorio_DeveConterUmValorDecimalEntre0e100()
        {
            var numero = _registrosRepositorio.GerarNovoPrecoEntre0e100Aleatorio();

            bool numeroValido = numero >= 0 && numero <= 100;

            Assert.True(numeroValido);
        }

        [Fact]
        public void BuscarCor_DeveTrazerCorVermelha()
        {
            var charColor = _registrosRepositorio.BuscarCor(1);            

            Assert.Equal('R',charColor);
        }

        [Fact]
        public void BuscarCor_DeveTrazerCorAmarela()
        {
            var charColor = _registrosRepositorio.BuscarCor(50);

            Assert.Equal('Y', charColor);
        }

        [Fact]
        public void BuscarCor_DeveTrazerCorAzul()
        {
            var charColor = _registrosRepositorio.BuscarCor(80);

            Assert.Equal('B', charColor);
        }

    }
}
