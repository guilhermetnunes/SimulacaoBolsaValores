using AppExemploMVVM.Services;
using SimulacaoBolsaValores.DataContext;
using SimulacaoBolsaValores.Model.Entities;
using SimulacaoBolsaValores.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace SimulacaoBolsaValores.ViewModels
{
    public class InicioViewModel : BaseViewModel
    {
        private IAtivoService _ativoService;

        DispatcherTimer _timer;

        #region Propriedades de Ação
        private string _ativoDigitado; 
        public string AtivoDigitado { get => _ativoDigitado; set => SetProperty(ref _ativoDigitado, value); }

        private int _qtdAtivosDigitadaParaGerarAtomaticamente;
        public int QtdAtivosDigitadaParaGerarAtomaticamente { get => _qtdAtivosDigitadaParaGerarAtomaticamente; set => SetProperty(ref _qtdAtivosDigitadaParaGerarAtomaticamente, value); }

        private int _tempoDigitado;
        public int TempoDigitado { get => _tempoDigitado; set => SetProperty(ref _tempoDigitado, value); }
        #endregion

        #region Propriedades de Tela
        private ObservableCollection<AtivoED> _lstAtivos;
        public ObservableCollection<AtivoED> LstAtivos { get => _lstAtivos; set => SetProperty(ref _lstAtivos, value); }

        private bool _emExecucao;
        public bool EmExecucao { get => _emExecucao; set => SetProperty(ref _emExecucao, value); }

        private bool _emStandby;
        public bool EmStandby { get => _emStandby; set => SetProperty(ref _emStandby, value); }

        private bool _gerarAtivosAuto;
        public bool GerarAtivosAuto { get => _gerarAtivosAuto; set => SetProperty(ref _gerarAtivosAuto, value); }

        private int _totalQuantidade;
        public int TotalQuantidade { get => _totalQuantidade; set => SetProperty(ref _totalQuantidade, value); }

        private int _totalDisponivel;
        public int TotalDisponivel { get => _totalDisponivel; set => SetProperty(ref _totalDisponivel, value); }
        #endregion

        #region Commands
        public ICommand AdicionarICommand { get; set; }
        public ICommand AdicionarAutoICommand { get; set; }
        public ICommand IniciarICommand { get; set; }
        public ICommand PararICommand { get; set; }                
        public ICommand LimparICommand { get; set; }
        #endregion

        public InicioViewModel(IAtivoService AtivoService)
        {   
            this._ativoService = AtivoService;            
            this._ativoService.NovoAtivoAction += NovoItemAtivo;
            this._ativoService.NovaListaAtivosAction += NovosItensAtivos;
            this._timer = new DispatcherTimer();

            AdicionarICommand = new CommandBase(AdicionarAtivo);
            AdicionarAutoICommand = new CommandBase(AdicionarAtivoAuto);
            IniciarICommand = new CommandBase(IniciarProcessamento);
            PararICommand = new CommandBase(PararProcessamento);
            LimparICommand = new CommandBase(LimparAtivos);

            LstAtivos = new ObservableCollection<AtivoED>();
            AtivoDigitado = String.Empty;
            TempoDigitado = 1000;
            GerarAtivosAuto = false;
            EmExecucao = false;
            EmStandby = true;

            _timer.Tick += Timer_Tick;
        }
        private void NovoItemAtivo(AtivoED obj)
        {
            LstAtivos.Add(obj);
            AtivoDigitado = "";
            AtualizarTotais();
        }
        private void NovosItensAtivos(List<AtivoED> obj)
        {
            foreach (var item in obj)
            {
                LstAtivos.Add(item);
            }
            AtivoDigitado = "";
            AtualizarTotais();
        }

        public void IniciarProcessamento(object obj)
        {
            try
            {
                if ((LstAtivos == null || LstAtivos.Count() == 0) && !GerarAtivosAuto)
                    throw new Exception("Inclua um ou mais Ativos.");
                else
                {
                    EmExecucao = true;
                    EmStandby = false;

                    _timer.Interval = new TimeSpan(0, 0, 0, 0, TempoDigitado);
                    _timer.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Não foi possível iniciar o processamento.\n",ex.Message), "Simulação da Bolsa de Valores", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        public void PararProcessamento(object obj)
        {
            try
            {
                _timer.Stop();

                EmExecucao = false;
                EmStandby = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Não foi possível parar o processamento.\n", ex.Message), "Simulação da Bolsa de Valores", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        public void LimparAtivos(object obj)
        {
            try
            {
                if ((LstAtivos == null || LstAtivos.Count() == 0))
                    throw new Exception("Não há dados para limpar.");
                else
                {
                    LstAtivos.Clear();
                    _ativoService.LimparAtivos();
                    AtualizarTotais();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Não foi possível limpar.\n", ex.Message), "Simulação da Bolsa de Valores", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        public void AdicionarAtivo(object obj)
        {
            try
            {
                if (string.IsNullOrEmpty(AtivoDigitado))
                    throw new Exception("Digite o código do Ativo.");
                else
                    _ativoService.AdicionarAtivo(AtivoDigitado);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Não foi possível adicionar ativo.\n", ex.Message), "Simulação da Bolsa de Valores", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        public void AdicionarAtivoAuto(object obj)
        {
            try
            {
                if (QtdAtivosDigitadaParaGerarAtomaticamente == 0)
                    throw new Exception("Digite uma quantidade.");
                else
                {
                    _ativoService.AdicionarNovaListaAtivos(QtdAtivosDigitadaParaGerarAtomaticamente);

                    AtualizarTotais();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Não foi possível adicionar ativos.\n", ex.Message), "Simulação da Bolsa de Valores", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        public void AtualizarRegistros()
        {
            try
            {
                var lstAtivosAtualizados = _ativoService.AtualizarAtivos();

                foreach (var item in lstAtivosAtualizados)
                {
                    var itemListaAtual = (from a in LstAtivos.Where(x => x.Ativo == item.Ativo) select a).FirstOrDefault();

                    if(itemListaAtual != null)
                        LstAtivos.Remove(itemListaAtual);

                    LstAtivos.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Não foi possível atualizar.\n", ex.Message), "Simulação da Bolsa de Valores", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        public void AtualizarTotais() 
        {
            TotalQuantidade = LstAtivos.Sum(x => x.Qtd);
            TotalDisponivel = LstAtivos.Sum(x => x.QtdDisp);
        }
        private void Timer_Tick(object? sender, EventArgs e)
        {
            AtualizarRegistros();
            AtualizarTotais();
        }

    }
}
