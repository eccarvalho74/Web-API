using Certificados.API.Controllers.Core;
using Certificados.Domain.Dto.Requests;
using Certificados.Services.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Certificados.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : CustomController
    {
        private readonly ILogger<UsuarioController> logger;
        private readonly IUsuarioService service;
      

        public UsuarioController(ILogger<UsuarioController> logger, IUsuarioService usuarioService) : base(logger)
        {
            this.logger = logger;
            this.service = usuarioService;
        }


        [HttpPost]
        [Route("autenticacao")]
        public async Task<IActionResult> Autenticar([FromBody] AutenticacaoUsuarioRequest request)
        {
            return Result(await service.AutenticarUsuario(request));
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> Get()
        {
            return Result(await this.service.Get());
        }

    }
}
