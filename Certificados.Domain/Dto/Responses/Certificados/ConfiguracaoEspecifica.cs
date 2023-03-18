using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Certificados.Domain.Dto.Responses
{
    public class ConfiguracaoEspecifica
    {
        public string NApoliceEtx { get; set; }
        public string NumeroApolice { get; set; }
        public string NomeProduto { get; set; }
        public string DataEmissaoApolice { get; set; }
        public string DataInicioVigenciaApolice { get; set; }
        public string DataFimVigenciaApolice { get; set; }
        public string TaxaLiquidaMensalCobertura1 { get; set; }
        public string TaxaLiquidaMensalCobertura2 { get; set; }
        public string TaxaLiquidaMensalCobertura3 { get; set; }
        public string CapitalSeguradoCobertura1 { get; set; }
        public string CapitalSeguradoCobertura2 { get; set; }
        public string CapitalSeguradoCobertura3 { get; set; }
        public string PremioPorCobertura1 { get; set; }
        public string PremioPorCobertura2 { get; set; }
        public string PremioPorCobertura3 { get; set; }
        public string PremioLiquido { get; set; }
        public string Iof { get; set; }
        public string PremioBruto { get; set; }
        public string ProlaboreEstipulante { get; set; }
        public string FormaPagamento { get; set; }
    }
}
