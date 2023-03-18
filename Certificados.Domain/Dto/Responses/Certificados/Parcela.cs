using System;
using System.Collections.Generic;
using System.Text;

namespace Certificados.Models.Certificados
{
    public class Parcela
    {



        /// <summary>
        /// Número de Parcelas
        /// </summary>
        public string NrParcela { get; set; }

        /// <summary>
        /// Data de pagamento
        /// </summary>
        public string DatadePagamento { get; set; }

        /// <summary>
        /// Capital segurado
        /// </summary>
        public string CapitalSegurado { get; set; }

        /// <summary>
        /// Premio Bruto
        /// </summary>

        public string PremioBruto { get; set; }

        public Parcela(string nrParcela, string dtPagto, string capitalSegurado, string premioBruto)
        {
            NrParcela = nrParcela;
            DatadePagamento = dtPagto;
            CapitalSegurado = capitalSegurado;
            PremioBruto = premioBruto;
        }
    }
}
