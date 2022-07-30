using SimulacaoBolsaValores.xUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SimulacaoBolsaValores.Testes
{
    public class UnitTestes
    {
        [Fact(DisplayName = "Deve trazer um número interiro entre 1 e 100.")]
        public void DeveGerarNumeroInteiroAleatorioEntre0e100()
        {
            var numero = new Ativo().GerarNumeroInteiroAleatorioEntre0e100();

            Assert.Equal(0, numero);
        }
    }
}
