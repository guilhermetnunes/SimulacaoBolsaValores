using AppExemploMVVM.Services;
using SimulacaoBolsaValores.Model.Entities;
using SimulacaoBolsaValores.Model.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SimulacaoBolsaValores.ViewModels
{
    internal class InicioViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        #region Propriedades de Ação
        public string AtivoDigitado { get; set; }
        public int QtdAtivosDigitadaParaGerarAtomaticamente { get; set; }
        #endregion

        #region Propriedades de Tela
        public List<AtivoEntity> lstAtivos { get; set; }               
        public bool EmExecucao { get; set; }
        public bool EmStandby{ get; set; }
        public bool GerarAtivosAuto { get; set; }
        #endregion

        #region Commands
        public ICommand IniciarICommand { get; set; }
        public ICommand PararICommand { get; set; }        
        public ICommand AdicionarICommand { get; set; }
        public ICommand LimparICommand { get; set; }
        #endregion

        public InicioViewModel()
        {
            IniciarICommand = new CommandBase(IniciarProcessamento);
            PararICommand = new CommandBase(PararProcessamento);
            AdicionarICommand = new CommandBase(AdicionarAtivo);
            LimparICommand = new CommandBase(LimparDados);

            lstAtivos = new List<AtivoEntity>();
            AtivoDigitado = String.Empty;
            GerarAtivosAuto = false;
            EmExecucao = false;
            EmStandby = true;
        }

        void IniciarProcessamento(object obj)
        {
            if ((lstAtivos == null || lstAtivos.Count() == 0) && !GerarAtivosAuto)
                MessageBox.Show("Inclua um Ativo ou marque opção para gerar automaticamente.", "Simulação da Bolsa de Valores", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else
            {
                if(GerarAtivosAuto)
                    lstAtivos = lstAtivos.Union(new AtivoRepository().GerarListadeAtivos(QtdAtivosDigitadaParaGerarAtomaticamente)).ToList();

                EmExecucao = true;
                EmStandby = false;
                RaiseChange("lstAtivos");
                RaiseChange("EmExecucao");
                RaiseChange("EmStandby");
            }
        }
        void PararProcessamento(object obj)
        {
            EmExecucao = false;
            EmStandby = true;
            RaiseChange("EmExecucao");
            RaiseChange("EmStandby");
        }
        void LimparDados(object obj)
        {
            lstAtivos = new AtivoRepository().CleanAtivos();
            RaiseChange("lstAtivos");
        }

        void AdicionarAtivo(object obj)
        {
            if (string.IsNullOrEmpty(AtivoDigitado))
                MessageBox.Show("Digite um Ativo.", "Simulação da Bolsa de Valores", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else
            {
                lstAtivos = new AtivoRepository().AddAtivos(AtivoDigitado, lstAtivos);
                AtivoDigitado = "";
                RaiseChange("lstAtivos");
            }
        }

        void RaiseChange(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
