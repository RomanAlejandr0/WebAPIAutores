using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIAutores.Entidades;

namespace WebAPIAutores.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpleadosController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public EmpleadosController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //[HttpGet]
        //public async Task<ActionResult<List<Empleado>>> Get()
        //{
        //    return await dbContext.Empleados.ToListAsync();
        //}

        [HttpGet]
        public List<Empleado> Get()
        {
            return new List<Empleado>()
            {
                new Empleado() { Id = 1, Nombre = "Juan" },
                new Empleado() { Id = 2, Nombre = "Pedro" },
            };
        }



        [HttpGet("{id:int}")]
        public async Task<ActionResult<List<Empleado>>> Get(int id)
        {
            throw new NotImplementedException();
        }

    }
}
