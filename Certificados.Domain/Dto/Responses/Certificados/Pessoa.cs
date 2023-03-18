using Certificados.Domain.Formatadores;

namespace Certificados.Domain.Dto.Responses
{
    public class Pessoa
    {
        private string _nome;

        public string Nome
        {
            get { return _nome; }
            set
            {
                var tamanhoDoCampoNoTemplate = 17;
                _nome = value;
                TruncateNome = value.Truncate(tamanhoDoCampoNoTemplate);
            }
        }
        public string TruncateNome { get; set; }
        public string DataNascimento { get; set; }
        public string Cpf { get; set; }

        private string _cpfOrgaoEmisso;

        public string CpfOrgaoEmissor
        {
            get { return _cpfOrgaoEmisso; }
            set
            {
                var tamanhoDoCampoNoTemplate = 5;
                _cpfOrgaoEmisso = value;
                TruncateCpfOrgaoEmissor = value.Truncate(tamanhoDoCampoNoTemplate);
            }
        }

        public string TruncateCpfOrgaoEmissor { get; set; }
        public string Nacionalidade { get; set; }
        public string Telefone { get; set; }
        public Endereco Endereco { get; set; }

        public Pessoa()
        {
            Endereco = new Endereco();
        }

        public Pessoa(String nome, string dataNascimento, String cpf, String cpfOrgaoEmissor, String nacionalidade, String telefone, Endereco endereco)
        {
            Nome = nome;
            DataNascimento = dataNascimento;
            Cpf = cpf.CpfFormat();
            CpfOrgaoEmissor = cpfOrgaoEmissor;
            Nacionalidade = nacionalidade;
            Telefone = telefone.PhoneFormat();
            Endereco = endereco;
        }
    }
}
