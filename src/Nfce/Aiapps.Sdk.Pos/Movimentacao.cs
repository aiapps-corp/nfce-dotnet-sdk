using System;
using System.Collections.Generic;
using System.Text;

namespace Aiapps.Sdk.Pos
{
    public class Movimentacao
    {
        public DateTime Data { get; set; } = DateTime.UtcNow;
        public decimal Valor { get; set; }
        public TipoMovimentacao Tipo { get; set; }
        public Responsavel Responsavel { get; set; } = new Responsavel();
        public Equipamento Equipamento { get; set; } = new Equipamento();
    }
    public enum TipoMovimentacao
    {
        Suprimento = 0,
        Sangria = 1
    }
}
