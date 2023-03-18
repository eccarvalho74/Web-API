using Certificados.Domain.Formatadores;

namespace Certificados.Domain.Dto.Responses
{
    public class Endereco
    {
        public string UF { get; set; }
        public string Cidade { get; set; }
        public string Cep { get; set; }

        private string _rua;

        public string Rua
        {
            get => _rua;
            set
            {
                var tamanhoDoCampoNoTemplate = 25;
                _rua = value;
                TruncateRua = value.Truncate(tamanhoDoCampoNoTemplate);
            }
        }
        public string TruncateRua { get; set; }

        public string Numero { get; set; }

        private string _complemento;
        public string Complemento
        {
            get => _complemento;
            set
            {
                var tamanhoDoCampoNoTemplate = 5;
                _complemento = value;
                TruncateComplemento = value.Truncate(tamanhoDoCampoNoTemplate);
            }
        }
        public string TruncateComplemento { get; set; }

        public string Completo { get => EnderecoCompleto(); }
        public Endereco() { }

        public Endereco(String uF, String cidade, String cep, String rua, String numero, String complemento)
        {
            UF = uF;
            Cidade = cidade;
            Cep = cep.CepFormat();
            Rua = rua;
            Numero = numero;
            Complemento = complemento;
        }

        private string EnderecoCompleto()
        {
            var tamanhoDoCampoNoTemplate = 50;
            return $"{Rua}, {Numero}, {Complemento}".Truncate(tamanhoDoCampoNoTemplate);
        }
    }
}
