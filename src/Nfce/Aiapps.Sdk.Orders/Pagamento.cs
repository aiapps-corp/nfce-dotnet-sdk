﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Aiapps.Sdk.Orders
{
    public class Pagamento
    {
        public DateTime? DataVencimento { get; set; }
        /// <summary>
        /// Forma de pagamento:
        /// 01=Dinheiro 
        /// 02=Cheque 
        /// 03=Cartão de Crédito 
        /// 04=Cartão de Débito 
        /// 05=Crédito Loja 
        /// 10=Vale Alimentação 
        /// 11=Vale Refeição 
        /// 12=Vale Presente 
        /// 13=Vale Combustível 
        /// 15=Boleto Bancário
        /// 16=Depósito Bancário
        /// 17=Pagamento Instantâneo(PIX) – Dinâmico
        /// 18=Transferência bancária, Carteira Digital
        /// 19=Programa de fidelidade, Cashback, Crédito Virtual
        /// 20=Pagamento Instantâneo(PIX) – Estático
        /// 90=Sem pagamento
        /// 99=Outros
        /// </summary>
        public string Tipo { get; set; }

        /// <summary>
        /// Descricação da forma de pagamento.
        /// Quando for informado 99-Outros deve ser informado a descrição
        /// </summary>
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public Cartao Cartao { get; set; }

        /// <summary>
        /// Código de referência do pagamento
        /// </summary>
        public string Referencia { get; set; }

        /// <summary>
        /// Informar o CNPJ do estabelecimento beneficiário do pagamento
        /// </summary>
        public string CnpjBeneficiarioPagamento { get; set; }


    }

}
