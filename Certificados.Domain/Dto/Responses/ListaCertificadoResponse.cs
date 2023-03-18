namespace Certificados.Domain.Dto.Responses
{
    public class ListaCertificadoResponse
    {/// <summary>
     /// Número do certificadoTratamento de erro
     /// </summary>
        public string NumCertificado { get; set; }
        /// <summary>
        /// Nome do segurado
        /// </summary>
        public string NomeSegurado { get; set; }
        /// <summary>
        /// Data de nascimetno do segurado
        /// </summary>
        public string DTNascimentoSegurado { get; set; }
        /// <summary>
        /// CPF ou CNPJ do segurado
        /// </summary>
        public string NrCnpjCPFSegurado { get; set; }
        /// <summary>
        /// Data de inicio de vigencia
        /// </summary>
        public string DtIniVigencia { get; set; }
        /// <summary>
        /// Data fim de vigencia
        /// </summary>
        public string DtFimVigencia { get; set; }
        /// <summary>
        /// Tipo de plano
        /// </summary>
        public string TipoPlano { get; set; }
        /// <summary>
        /// FLAG de indicação se pode imprimir ou nao
        /// </summary>
        public string Flag { get; set; }
        /// <summary>
        /// Código do cliente
        /// </summary>
        public string CdCliente { get; set; }
        /// <summary>
        /// Número Apólice Externo
        /// </summary>
        public string NrApoliceExt { get; set; }

     

    }
}
