using AutoMapper;
using WebAPIAutores.DTOs;
using WebAPIAutores.Entidades;

namespace WebAPIAutores.Utilidades
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AutorCreacionDTO, Autor>();
            CreateMap<Autor, AutorDTO>();

            CreateMap<LibroCreacionDTO, Libro>()
                .ForMember(libro => libro.AutoresLibros,  opciones => opciones.MapFrom(MapAutoresLibros));
            CreateMap<Libro, LibroDTO>();

            CreateMap<ComentarioCreacionDTO, Comentario>();
            CreateMap<Comentario, ComentarioDTO>();
        }

        private object MapAutoresLibros(LibroCreacionDTO libroCreacionDTO, Libro libro)
        {
            var resultado= new List<AutorLibro>();
            
            if (libroCreacionDTO.AutoresIds == null) { return resultado; }
            
            foreach (var id in libroCreacionDTO.AutoresIds)
            {
                resultado.Add(new AutorLibro() { AutorId = id });
            }

            return resultado;
        }
    }
}
