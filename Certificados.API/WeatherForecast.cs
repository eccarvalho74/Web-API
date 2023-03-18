using System.ComponentModel.DataAnnotations;

namespace Certificados.API
{
    public class WeatherForecast
    {
        [Required(ErrorMessage ="Data � obrigatoria")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }


        [Required(ErrorMessage ="Requerido essa baga�a")]
        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }

        [Required(ErrorMessage = "CPF � obrigat�rio")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Senha atual � obrigat�ria")]
        public string SenhaAtual { get; set; }

        [Required(ErrorMessage = "Nova senha � obrigat�ria")]
        public string NovaSenha { get; set; }
    }
}