using System;
using System.Collections.Generic;
using System.Text;

namespace Certificados.Domain.Core.DTO
{
    public class PagingParametersBase
    {
        public int PaginaCorrente { get; set; } = 1;
        public int RegistroPorPagina { get; set; } = 50;
        public bool TotalizarRegs { get; set; } = false;
    }
}
