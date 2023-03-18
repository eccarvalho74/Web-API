using Certificados.API.Controllers.Core;
using Certificados.Services.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Certificados.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificadosController : CustomController
    {
        private readonly ILogger<UsuarioController> logger;
        private readonly ICertificadoService service;


        public CertificadosController(ILogger<UsuarioController> logger, ICertificadoService certificadoService) : base(logger)
        {
            this.logger = logger;
            this.service = certificadoService;
        }


        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFile(IFormFile formFile)
        {
          
            return Result(await service.Upload(formFile));
        }


        [HttpGet()]
        public async Task<IActionResult> GetCertificados()
        {

            return Result(await service.ObterNrCertificados());
        }
    }
}

