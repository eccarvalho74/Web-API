using System.ComponentModel.DataAnnotations;

namespace Certificados.API
{
    public class WeatherForecast
    {
        [Required(ErrorMessage ="Data é obrigatoria")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }


        [Required(ErrorMessage ="Requerido essa bagaça")]
        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }

        [Required(ErrorMessage = "CPF é obrigatório")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Senha atual é obrigatória")]
        public string SenhaAtual { get; set; }

        [Required(ErrorMessage = "Nova senha é obrigatória")]
        public string NovaSenha { get; set; }
    }
}