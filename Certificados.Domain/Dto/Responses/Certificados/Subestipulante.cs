using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Certificados.Domain.Dto.Responses
{
    public class Subestipulante
    {
        public string RazaoSub { get; set; }
        public string CnpjSub { get; set; }
        public string Telefone_sub { get; set; }
        public string EnderecoSub { get; set; }
        public string NumeroSub { get; set; }
        public string ComplementoSub { get; set; }
        public string CepSub { get; set; }
        public string CidadeSub { get; set; }
        public string UfSub { get; set; }

        public Subestipulante(String razaoSub, String cnpjSub, String telefoneSub, String enderecoSub, String numeroSub, String complementoSub, String cepSub, String cidadeSub, String ufSub)
        {
            RazaoSub = razaoSub;
            CnpjSub = cnpjSub;
            Telefone_sub = telefoneSub;
            EnderecoSub = enderecoSub;
            NumeroSub = numeroSub;
            ComplementoSub = complementoSub;
            CepSub = cepSub;
            CidadeSub = cidadeSub;
            UfSub = ufSub;
        }
    }
}
