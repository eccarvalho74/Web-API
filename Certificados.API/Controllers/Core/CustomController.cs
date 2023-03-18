using Certificados.Domain.Core.dto;
using Microsoft.AspNetCore.Mvc;

namespace Certificados.API.Controllers.Core
{
    public abstract class CustomController : ControllerBase
    {
        private readonly ILogger _logger;

        protected CustomController(ILogger logger/*, NotificationContext notificationContext*/)
        {
            _logger = logger;
            //_notificationContext = notificationContext;
        }


        /// <summary>
        /// Gera resultado sem resposta em ServiceResult baseado no AJAX Security do OWASP.
        /// <para>OWASP: https://cheatsheetseries.owasp.org/cheatsheets/AJAX_Security_Cheat_Sheet.html</para>
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="serviceResult"></param>
        /// <returns>ObjectResult</returns>
        protected IActionResult Result(ServiceResult serviceResult, int statusCode = StatusCodes.Status200OK)
        {
            if (serviceResult.ContainsError())
            {
          //      _logger.LogCritical("Erro Result {@CodeId} - UId {@UsuarioId}- {@ObjetoErro}", 0, (DadosUsuario.UsuarioId(HttpContext.User) ?? "Não Logado"), JsonConvert.SerializeObject(serviceResult.Errors));

            }

            var objectResult = new ObjectResult(serviceResult);
            objectResult.StatusCode = statusCode;
            return objectResult;
        }
    }
}