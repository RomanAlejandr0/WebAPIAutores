using Microsoft.AspNetCore.Mvc.Filters;

namespace WebAPIAutores.Filtros
{
    public class FiltroDeException : ExceptionFilterAttribute
    {
        private readonly ILogger<FiltroDeException> logger;

        public FiltroDeException(ILogger<FiltroDeException> logger)
        {
            this.logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            logger.LogError(context.Exception, "Ocurrio un error");
            base.OnException(context);
        }
    }
}
