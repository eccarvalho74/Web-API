using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Certificados.Domain.Dto.Requests
{
    public class AutenticacaoUsuarioRequest
    {
        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        [RegularExpression(@"^\S*$", ErrorMessage = "Espaços em branco não é permitido")]
        public string Senha { get; set; }


    }
}
