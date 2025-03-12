using Microsoft.EntityFrameworkCore;
using WebAPIAutores.DTOs;

namespace WebAPIAutores.Utilidades
{
    public static class HttpContextExtensions
    {
        public async static Task InsertarParametrosPaginacionEnCabecera<T>(this HttpContext httpContext, 
            IQueryable<T> queryable)
        {
            if (httpContext is null) { throw new ArgumentNullException(nameof(httpContext)); }
           
            double cantidad = await queryable.CountAsync();
            httpContext.Response.Headers.Add("cantidad-total-registros", cantidad.ToString());



            //double cantidadPaginas = Math.Ceiling(cantidad / paginacionDTO.RecordsPorPagina);
            //httpContext.Response.Headers.Add("cantidadTotalPaginas", cantidadPaginas.ToString());
        }
    }
}
