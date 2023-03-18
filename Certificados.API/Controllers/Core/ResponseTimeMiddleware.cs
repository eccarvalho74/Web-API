using System.Diagnostics;

namespace Certificados.API.Controllers.Core
{
    public class ResponseTimeMiddleware
    {
        //  Custom Headers iniciam com "X-"  
        private const string RESPONSE_HEADER_RESPONSE_TIME = "X-Tempo-Execucao-ms";
        // Handle do proximo Middleware no pipeline  
        private readonly RequestDelegate _next;
        public ResponseTimeMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public Task InvokeAsync(HttpContext context)
        {
            // inicia Timer 
            var watch = new Stopwatch();
            watch.Start();
            context.Response.OnStarting(() =>
            {
                // Para o timer e calcula tempo execução  
                watch.Stop();
                var responseTimeForCompleteRequest = watch.ElapsedMilliseconds;
                // Adiciona o tempo de execução no  headers.   
                context.Response.Headers[RESPONSE_HEADER_RESPONSE_TIME] = responseTimeForCompleteRequest.ToString();
                return Task.CompletedTask;
            });
            // chama o proximo middleware no pipeline   
            return _next(context);
        }
    }
}
