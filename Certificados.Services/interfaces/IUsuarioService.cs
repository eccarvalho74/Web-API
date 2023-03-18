using Certificados.Domain.Core.dto;
using Certificados.Domain.Dto.Requests;
using Certificados.Domain.Dto.Responses;
using Certificados.Services.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Certificados.Services.interfaces
{
    public  interface IUsuarioService : IBaseService
    {
        Task<ServiceResult<AutenticacaoUsuarioResponse>> AutenticarUsuario(AutenticacaoUsuarioRequest autenticacaoUsuarioRequest);
        Task<ServiceResult<IEnumerable<UsuarioResponse>>> Get();
   
    }
}
