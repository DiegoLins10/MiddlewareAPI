using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RoomService.WebAPI
{
    public class PasswordCheckerMiddleware
    {
        private readonly RequestDelegate _next;

        public PasswordCheckerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Verifica se a solicitação contém o cabeçalho "passwordKey" com o valor esperado
            if (context.Request.Headers.TryGetValue("passwordKey", out var passwordKeyValues) && passwordKeyValues == "passwordKey123456789")
            {
                // Se o cabeçalho e o valor são válidos, passa para o próximo middleware na pipeline
                await _next(context);
            }
            else
            {
                // Caso contrário, retorna uma resposta 403 Forbidden
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Acesso negado.");
            }
        }
    }
}