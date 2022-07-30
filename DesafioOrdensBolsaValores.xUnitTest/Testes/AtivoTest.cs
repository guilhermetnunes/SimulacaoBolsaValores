namespace SimulacaoBolsaValores.xUnitTest.Testes
{
    public class AtivoTest
    {
        [Fact(DisplayName = "Deve trazer um número interiro entre 1 e 100.")]
        public void DeveGerarNumeroInteiroAleatorioEntre0e100()
        {
            var numero = AtivoContext.GerarNumeroInteiroAleatorioEntre0e100();

            Assert.Equals(0, numero);
        }
    }
}
