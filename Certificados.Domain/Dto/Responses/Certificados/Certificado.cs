using Certificados.Domain.Formatadores;
using Certificados.Models.Certificados;
using System.Globalization;

namespace Certificados.Domain.Dto.Responses
{
    public class Certificado
    {
        /// <summary>
        /// nr_proposta
        /// </summary>
        public string NumeroCertificado { get; set; }
        /// <summary>
        /// nr_apolice
        /// </summary>
        public string NCertificadoIndividual { get; set; }
        public string NumeroSorte { get; set; }
        public string NumSorteDezembro { get; set; }
        public string NumeroProposta { get; set; }
        public string InicioVigencia { get; set; }
        public string FimVigencia { get; set; }
        public string EmissaoSeguro { get; set; }
        public DateTime DataDocumento { get; set; }
        public string DataAssinatura { get { return DataPorExtenso(DataDocumento); } }



        /** Configuração especifica **/
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
        public Pessoa Segurado { get; set; }

        //Verificar se impacta outros lugares
        public Subestipulante Subestipulante { get; set; }

        private List<Beneficiario> _beneficiarios = new List<Beneficiario>();
        public List<Beneficiario> Beneficiarios
        {
            get { return _beneficiarios; }
            set { _beneficiarios = value; }
        }

        private List<Parcela> _parcela = new List<Parcela>();
        public List<Parcela> Parcelas
        {
            get { return _parcela; }
            set { _parcela = value; }
        }

        public Certificado(
            ConfiguracaoEspecifica configuracaoEspecifica,
            String numeroCertificado,
            String nCertificadoIndividual,
            String numeroSorte,
            String numSorteDezembro,
            String numeroProposta,
            String inicioVigencia,
            String fimVigencia,
            String emissaoSeguro,
            DateTime dataDocumento,
            String premioCobertura1,
            String premioCobertura2,
            String taxaLiquidaMensalCobertura1,
            String taxaLiquidaMensalCobertura2,
            String capitalSeguradoCobertura1,
            String capitalSeguradoCobertura2,
            String premioLiquido,
            String premioBruto,
            String iof,
            String prolabore,
            String premioCobertura3,
            String capitalSeguradoCobertura3,
            Pessoa segurado,
            Subestipulante subestipulante)
        {
            NumeroCertificado = numeroCertificado;
            NCertificadoIndividual = nCertificadoIndividual;
            NumeroSorte = numeroSorte;
            NumSorteDezembro = numSorteDezembro;
            NumeroProposta = numeroProposta;
            InicioVigencia = inicioVigencia;
            FimVigencia = fimVigencia;
            EmissaoSeguro = emissaoSeguro;
            DataDocumento = dataDocumento;
            Segurado = segurado;
            Subestipulante = subestipulante;

            PremioPorCobertura1 = (String.IsNullOrWhiteSpace(configuracaoEspecifica?.PremioPorCobertura1) ? premioCobertura1 : configuracaoEspecifica?.PremioPorCobertura1).CurrencyFormat();
            PremioPorCobertura2 = (String.IsNullOrWhiteSpace(configuracaoEspecifica?.PremioPorCobertura2) ? premioCobertura2 : configuracaoEspecifica?.PremioPorCobertura2).CurrencyFormat();
            PremioPorCobertura3 = (String.IsNullOrWhiteSpace(configuracaoEspecifica?.PremioPorCobertura3) ? premioCobertura3 : configuracaoEspecifica?.PremioPorCobertura3).CurrencyFormat();

            TaxaLiquidaMensalCobertura1 = String.IsNullOrWhiteSpace(configuracaoEspecifica?.TaxaLiquidaMensalCobertura1) ? taxaLiquidaMensalCobertura1 : configuracaoEspecifica?.TaxaLiquidaMensalCobertura1;
            TaxaLiquidaMensalCobertura2 = String.IsNullOrWhiteSpace(configuracaoEspecifica?.TaxaLiquidaMensalCobertura2) ? taxaLiquidaMensalCobertura2 : configuracaoEspecifica?.TaxaLiquidaMensalCobertura2;


            CapitalSeguradoCobertura1 = (String.IsNullOrWhiteSpace(configuracaoEspecifica?.CapitalSeguradoCobertura1) ? capitalSeguradoCobertura1 : configuracaoEspecifica?.CapitalSeguradoCobertura1).CurrencyFormat();
            CapitalSeguradoCobertura2 = (String.IsNullOrWhiteSpace(configuracaoEspecifica?.CapitalSeguradoCobertura2) ? capitalSeguradoCobertura2 : configuracaoEspecifica?.CapitalSeguradoCobertura2).CurrencyFormat();
            CapitalSeguradoCobertura3 = (String.IsNullOrWhiteSpace(configuracaoEspecifica?.CapitalSeguradoCobertura3) ? capitalSeguradoCobertura3 : configuracaoEspecifica?.CapitalSeguradoCobertura3).CurrencyFormat();

            /**
            CapitalSeguradoCobertura1 = capitalSeguradoCobertura1.CurrencyFormat();
            CapitalSeguradoCobertura2 = capitalSeguradoCobertura2.CurrencyFormat();
            **/
            PremioLiquido = (String.IsNullOrWhiteSpace(configuracaoEspecifica?.PremioLiquido) ? premioLiquido : configuracaoEspecifica?.PremioLiquido).CurrencyFormat();
            PremioBruto = (String.IsNullOrWhiteSpace(configuracaoEspecifica?.PremioBruto) ? premioBruto : configuracaoEspecifica?.PremioBruto).CurrencyFormat();
            Iof = (String.IsNullOrWhiteSpace(configuracaoEspecifica?.Iof) ? iof : configuracaoEspecifica?.Iof).CurrencyFormat();
            ProlaboreEstipulante = (String.IsNullOrWhiteSpace(configuracaoEspecifica?.ProlaboreEstipulante) ? prolabore : configuracaoEspecifica?.ProlaboreEstipulante).CurrencyFormat();

            SetDadosConfiguracaoEspecifica(configuracaoEspecifica);
        }

        public Certificado()
        {
            Segurado = new Pessoa();
        }

        private void SetDadosConfiguracaoEspecifica(ConfiguracaoEspecifica configuracaoEspecifica)
        {
            if (configuracaoEspecifica == null) return;
            NumeroApolice = configuracaoEspecifica.NumeroApolice;
            NomeProduto = configuracaoEspecifica.NomeProduto;
            DataEmissaoApolice = configuracaoEspecifica.DataEmissaoApolice;
            DataInicioVigenciaApolice = configuracaoEspecifica.DataInicioVigenciaApolice;
            DataFimVigenciaApolice = configuracaoEspecifica.DataFimVigenciaApolice;
            TaxaLiquidaMensalCobertura3 = configuracaoEspecifica.TaxaLiquidaMensalCobertura3;
            // CapitalSeguradoCobertura3 = configuracaoEspecifica.CapitalSeguradoCobertura3.CurrencyFormat(); ;
            // PremioPorCobertura3 = configuracaoEspecifica.PremioPorCobertura3.CurrencyFormat();
            FormaPagamento = configuracaoEspecifica.FormaPagamento;
        }

        private string DataPorExtenso(DateTime data)
        {
            if (data == null) return "";

            CultureInfo culture = new CultureInfo("pt-BR");
            var mes = data.ToString("MMMM", culture);
            return $"{data.Day} de {mes} de {data.Year}";
        }
    }
}
