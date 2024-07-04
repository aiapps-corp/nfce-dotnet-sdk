using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Aiapps.Sdk.Invoices
{
    public class Sefaz
    {
        public DateTime EmitidoEm { get; set; }
        public DateTime? CanceladoEm { get; set; }
        public string Protocolo { get; set; }
        public Environment Ambiente { get; set; } = Environment.Unknown;
        public string Url { get; set; }
        public string Motivo { get; set; }
        public string CodigoStatus { get; set; }
    }


    /// <summary>
    /// Identificação do Ambiente da fatura.
    /// </summary>
    public enum Environment
    {
        [Display(Name = "Desconhecido")]
        Unknown = 0,
        /// <summary>
        /// 1-Produção;
        /// </summary>
        [Display(Name = "1-Produção")]
        Production = 1,

        /// <summary>
        /// 2-Homologação;
        /// </summary>
        [Display(Name = "2-Homologação")]
        Stage = 2,
    }
}
