using Microsoft.Extensions.Logging;

namespace WebAPIAutores.Middlewares
{
    public static class LoguearRespuestaHTTPMiddlewareExtensions
    {
        public static IApplicationBuilder UseLoguearRespuestaHTTP(this IApplicationBuilder app)
        {
            return app.UseMiddleware<LoguearRespuestaHTTPMiddleware>();
        }
    }


    public class LoguearRespuestaHTTPMiddleware
    {
        private readonly RequestDelegate siguiente;
        private readonly ILogger<LoguearRespuestaHTTPMiddleware> logger;

        public LoguearRespuestaHTTPMiddleware(RequestDelegate siguiente, 
            ILogger<LoguearRespuestaHTTPMiddleware> logger)
        {
            this.siguiente = siguiente;
            this.logger = logger;
        }

        //Invoke o InvokeAsync
        public async Task InvokeAsync(HttpContext contexto)
        {
            // Solo procesar rutas que empiecen con /api
            if (contexto.Request.Path.StartsWithSegments("/api"))
            {
                using (var ms = new MemoryStream())
                {
                    var cuerpoOriginal = contexto.Response.Body;
                    contexto.Response.Body = ms;

                    await siguiente(contexto);

                    ms.Seek(0, SeekOrigin.Begin);
                    string respuesta = new StreamReader(ms).ReadToEnd();
                    ms.Seek(0, SeekOrigin.Begin);

                    // Solo registrar si es una respuesta JSON
                    if (contexto.Response.ContentType?.Contains("application/json") == true)
                    {
                        logger.LogInformation($"Endpoint: {contexto.Request.Path}, Respuesta: {respuesta}");
                    }

                    await ms.CopyToAsync(cuerpoOriginal);

                    contexto.Response.Body = cuerpoOriginal;
                }
            }
            else
            {
                await siguiente(contexto);
            }
        }
    }
}
