﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Certificados.Domain.Dto.Responses
{
    public  class UsuarioResponse
    {       
            public int Id { get; set; }
            public  string Nome { get; set; }
            public  string Email { get; set; }
            public  string Token { get; set; }
      
    }
}
