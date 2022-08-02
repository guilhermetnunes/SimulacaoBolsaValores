namespace SimulacaoBolsaValores.DataContext
{
    public interface IRegistrosRepositorio
    {
        string GerarCodigoLetrasNumerosAleatorio();
        decimal GerarNovoPrecoEntre0e100Aleatorio();
        int GerarNumeroInteiroEntre0e100Aleatorio();
    }
}