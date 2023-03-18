using Certificados.Domain.Core.dto;
using Certificados.Domain.Dto.Requests;
using Certificados.Domain.Dto.Responses;
using Certificados.Services.Core;
using Microsoft.AspNetCore.Http;

namespace Certificados.Services.interfaces
{
    public interface ICertificadoService : IBaseService
    {
        Task<ServiceResult<IEnumerable<ListaCertificadoResponse>>> ListaOpcoes(string cpf, string dt_referencia, string corretora = null);
        Task<ServiceResult<Certificado>> ListaCertificado(CertificadoRequest certificadoRequestDTO);
        Task<ServiceResult<List<Certificado>>> ObterNrCertificados();
        Task<ServiceResult<bool>> Upload(IFormFile fileUpload);
    }
}
