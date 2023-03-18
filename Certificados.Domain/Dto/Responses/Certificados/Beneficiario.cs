using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Certificados.Domain.Dto.Responses
{
    public class Beneficiario
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Parentesco { get; set; }
        public string Participacao { get; set; }

        public Beneficiario() { }
        public Beneficiario(string nome, string cpf, string parentesco, string participacao)
        {
            Nome = nome;
            Cpf = cpf;
            Parentesco = parentesco;
            Participacao = participacao;
        }
    }
}
