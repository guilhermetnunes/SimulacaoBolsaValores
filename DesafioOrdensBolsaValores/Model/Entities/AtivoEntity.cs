using SimulacaoBolsaValores.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulacaoBolsaValores.Model.Entities
{
    public class AtivoEntity
    {
        public Guid Id { get; set; }
        public DateTime DataHora { get; set; }
        public string Assessor { get; set; }
        public string Conta { get; set; }
        public string Ativo { get; set; }
        public char Tipo { get; set; }
        public int Qtd { get; set; }
        public int QtdAparente { get; set; }
        public int QtdDisp { get; set; }
        public int QtdCancel { get; set; }
        public int QtdExec { get; set; }
        public decimal Valor { get; set; }
        public decimal ValorDisp { get; set; }
        public decimal Objetivo { get; set; }
        public decimal ObjDisp { get; set; }
        public decimal Reducao { get; set; }        
    }
}
