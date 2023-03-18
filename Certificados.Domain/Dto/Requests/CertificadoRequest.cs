using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Certificados.Domain.Dto.Requests
{
    public class CertificadoRequest
    {
        public string CodigoCliente { get; set; }
        public string DataNascimento { get; set; }
        public string TipoParceiro { get; set; }
        public string TipoPlano { get; set; }
        public string ApoliceExt { get; set; }
        public string NumeroCertificado { get; set; }
        public string TipoCertificado { get; set; }
    }
}
