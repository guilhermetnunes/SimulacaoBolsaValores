using AppExemploMVVM.Services;
using SimulacaoBolsaValores.Model.Entities;
using SimulacaoBolsaValores.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace SimulacaoBolsaValores.ViewModels
{
    public class InicioViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private AtivoRepository _ativoRepository;

        #region Propriedades de Ação
        private string _ativoDigitado; 
        public string AtivoDigitado { get { return _ativoDigitado; } set { _ativoDigitado = value; RaiseChange("AtivoDigitado"); } }

        private int _qtdAtivosDigitadaParaGerarAtomaticamente;
        public int QtdAtivosDigitadaParaGerarAtomaticamente { get { return _qtdAtivosDigitadaParaGerarAtomaticamente; } set { _qtdAtivosDigitadaParaGerarAtomaticamente = value; RaiseChange("AtivoDigitado"); } }

        private int _tempoDigitado;
        public int TempoDigitado { get { return _tempoDigitado; } set { _tempoDigitado = value; RaiseChange("AtivoDigitado"); } }
        #endregion

        #region Propriedades de Tela
        private ObservableCollection<AtivoEntity> _lstAtivos;
        public ObservableCollection<AtivoEntity> lstAtivos { get { return _lstAtivos; } set { _lstAtivos = value; RaiseChange("lstAtivos"); } }
        
        private bool _emExecucao;
        public bool EmExecucao { get { return _emExecucao; } set { _emExecucao = value; RaiseChange("EmExecucao"); } }

        private bool _emStandby;
        public bool EmStandby { get { return _emStandby; } set { _emStandby = value; RaiseChange("EmStandby"); } }

        private bool _gerarAtivosAuto;
        public bool GerarAtivosAuto { get { return _gerarAtivosAuto; } set { _gerarAtivosAuto = value; RaiseChange("GerarAtivosAuto"); } }

        private int _totalQuantidade;
        public int TotalQuantidade { get { return _totalQuantidade; } set { _totalQuantidade = value; RaiseChange("TotalQuantidade"); } }
        
        private int _totalDisponivel;
        public int TotalDisponivel { get { return _totalDisponivel; } set { _totalDisponivel = value; RaiseChange("TotalDisponivel"); } }
        #endregion

        DispatcherTimer timer = new DispatcherTimer();

        #region Commands
        public ICommand AdicionarICommand { get; set; }
        public ICommand AdicionarAutoICommand { get; set; }
        public ICommand IniciarICommand { get; set; }
        public ICommand PararICommand { get; set; }                
        public ICommand LimparICommand { get; set; }
        #endregion

        public InicioViewModel(AtivoRepository ativoRepository)
        {
            this._ativoRepository = ativoRepository;
            this._ativoRepository.NovoAtivo += NovoItemRecebido;

            AdicionarICommand = new CommandBase(AdicionarAtivo);
            AdicionarAutoICommand = new CommandBase(AdicionarAtivoAuto);
            IniciarICommand = new CommandBase(IniciarProcessamento);
            PararICommand = new CommandBase(PararProcessamento);
            LimparICommand = new CommandBase(LimparDados);

            lstAtivos = new ObservableCollection<AtivoEntity>();
            AtivoDigitado = String.Empty;
            TempoDigitado = 1000;
            GerarAtivosAuto = false;
            EmExecucao = false;
            EmStandby = true;

            timer.Tick += Timer_Tick;
            
        }

        private void NovoItemRecebido(AtivoEntity obj)
        {
            lstAtivos.Add(obj);
            AtivoDigitado = "";
            AtualizarTotais();
        }

        void IniciarProcessamento(object obj)
        {
            if ((lstAtivos == null || lstAtivos.Count() == 0) && !GerarAtivosAuto)
                MessageBox.Show("Inclua um ou mais Ativos.", "Simulação da Bolsa de Valores", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else
            {
                EmExecucao = true;
                EmStandby = false;

                timer.Interval = new TimeSpan(0, 0, 0, 0, TempoDigitado);
                timer.Start();
            }
        }
        void PararProcessamento(object obj)
        {
            timer.Stop();

            EmExecucao = false;
            EmStandby = true;
        }
        void LimparDados(object obj)
        {
            if ((lstAtivos == null || lstAtivos.Count() == 0))
                MessageBox.Show("Não há dados para limpar.", "Simulação da Bolsa de Valores", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else
            {                
                lstAtivos.Clear();
                _ativoRepository.LimparAtivos();
                AtualizarTotais();
            }
        }
        void AdicionarAtivo(object obj)
        {
            if (string.IsNullOrEmpty(AtivoDigitado))
                MessageBox.Show("Digite o código do Ativo.", "Simulação da Bolsa de Valores", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else            
                _ativoRepository.AddAtivos(AtivoDigitado);            
        }
        void AdicionarAtivoAuto(object obj)
        {
            if (QtdAtivosDigitadaParaGerarAtomaticamente == 0)
                MessageBox.Show("Digite uma quantidade.", "Simulação da Bolsa de Valores", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else
            {
                var listNovosAtivos = _ativoRepository.GerarListadeAtivos(QtdAtivosDigitadaParaGerarAtomaticamente);

                foreach (var item in listNovosAtivos)
                {
                    lstAtivos.Add(item);
                }

                AtualizarTotais();
            }
        }
        void AtualizarTotais() 
        {
            TotalQuantidade = lstAtivos.Sum(x => x.Qtd);
            TotalDisponivel = lstAtivos.Sum(x => x.QtdDisp);
        }
        private void Timer_Tick(object? sender, EventArgs e)
        {
            var lstUpdates = _ativoRepository.UpdateAtivos();

            foreach (var item in lstUpdates)
            {
                var itemListaAtual = (from a in lstAtivos.Where(x => x.Ativo == item.Ativo) select a).FirstOrDefault();

                lstAtivos.Remove(itemListaAtual);

                lstAtivos.Add(item);
            }

            AtualizarTotais();
        }

        void RaiseChange(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
