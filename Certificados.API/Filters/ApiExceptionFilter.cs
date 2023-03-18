using Certificados.Domain.Core;
using Certificados.Domain.Core.dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace Certificados.API.Filters
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        public object ExceptionToResult { get; private set; }
        private readonly ILogger<ApiExceptionFilter> logger;

        public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
        {
            this.logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            var result = new ServiceResult<bool>(false);
            var ObjetoErro = JsonConvert.SerializeObject(context.Exception);
            var chamador = context.ActionDescriptor.DisplayName;
            var message = string.Empty;
            var statusHttp = StatusCodes.Status400BadRequest;
         
             result.AddError("EX00", chamador + ": " + context.Exception.GetBaseException().Message);
            // logger.LogCritical(context.Exception, "API Erro: - {@chamador}  Handler genérico  {@UsuarioId} - {@ObjetoErro}", chamador, userId, ObjetoErro);
          
            context.Result = new ObjectResult(result);
            context.HttpContext.Response.StatusCode = statusHttp;
        }
    }
}
