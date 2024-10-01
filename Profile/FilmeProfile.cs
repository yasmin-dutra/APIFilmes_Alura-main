using AutoMapper;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles
{
    public class FilmeProfile : Profile   // isso aqui é do AutoMapper, que vai converter um filmeDto em filme(atributo do db)
    {

        // construtor
        public FilmeProfile()     // "ei construtor, define um mapeamento de um CreateFilmeDto p/ um Filme"
        {
            CreateMap<CreateFilmeDto, Filme>();
            CreateMap<CreateFilmeDto, Filme>();
            CreateMap<Filme, UpdateFilmeDto>();
            CreateMap<Filme, ReadFilmeDto>();
        }
    }
}