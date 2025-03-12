using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIAutores.DTOs;
using WebAPIAutores.Entidades;
using WebAPIAutores.Utilidades;

namespace WebAPIAutores.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpleadosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public EmpleadosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }


        //QUE PASA CON LA CACHE


        //[HttpGet]
        //public async Task<ActionResult<List<Empleado>>> Get()
        //{
        //    return await dbContext.Empleados.ToListAsync();
        //}

        [HttpGet]
        public async Task<List<EmpleadoDTO>> Get([FromQuery] PaginacionDTO paginacion)
        {
            var queryable = context.Empleados;
            await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);
            return await queryable
                .OrderBy(x => x.Id)
                .Paginar(paginacion)
                .ProjectTo<EmpleadoDTO>(mapper.ConfigurationProvider).ToListAsync();
            //return await context.Empleados.ProjectTo<EmpleadoDTO>(mapper.ConfigurationProvider).ToListAsync();
        }



        [HttpGet("{id:int}", Name = "ObtenerGenerosPorId")]
        public async Task<ActionResult<EmpleadoDTO>> Get(int id)
        {
            var empleadoDTO = await context.Empleados
                .ProjectTo<EmpleadoDTO>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (empleadoDTO is null)
            {
                return NotFound();
            }


            return empleadoDTO;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmpleadoCreacionDTO empleadoCreacionDTO)
        {
            var empleado = mapper.Map<Empleado>(empleadoCreacionDTO);
            context.Empleados.Add(empleado);
            await context.SaveChangesAsync();
            return CreatedAtRoute("ObtenerGenerosPorId", new { id = empleado.Id }, empleado);

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] EmpleadoCreacionDTO empleadoCreacionDTO)
        {
            var empleadoExiste = await context.Empleados.AnyAsync(x => x.Id == id);

            if (!empleadoExiste)
            {
                return NotFound();
            }

            var empleado = mapper.Map<Empleado>(empleadoCreacionDTO);
            empleado.Id = id;

            context.Empleados.Update(empleado);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var registrosBorrados = await context.Empleados.Where(x => x.Id == id).ExecuteDeleteAsync();

            if (registrosBorrados == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
