using Certificados.Domain.Core;
using Certificados.Domain.Core.dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Certificados.API.Filters
{
     public class ApiModelValidationFilter : ActionFilterAttribute
    {
        private readonly ILogger<ApiModelValidationFilter> logger;


        public ApiModelValidationFilter(ILogger<ApiModelValidationFilter> logger)
        {
            this.logger = logger;
        }

        public override Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            //Intercepta erros no ViewModel de entrada...
            if (!context.ModelState.IsValid)
            {
                var result = new ServiceResult<ObjectResult>();
                context.Result = new BadRequestObjectResult(context.ModelState);
                var errosModel = context.ModelState.ToDictionary(x => x.Key, x => x.Value.Errors);

                foreach (var item in errosModel)
                    result.AddError("MS00", item.Value[0].ErrorMessage);

               // logger.LogCritical("Erro Model [{@CodeId}] - UId {@UsuarioId} - {@ObjetoErro}", context.ActionDescriptor.DisplayName, (DadosUsuario.UsuarioId(context.HttpContext.User) ?? "Não Logado"), JsonConvert.SerializeObject(errosModel));

           
                context.Result = new ObjectResult(result);
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

                return base.OnResultExecutionAsync(context, next);
            }


            
            return base.OnResultExecutionAsync(context, next);
        }
    }
}
