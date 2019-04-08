using AutoMapper;
using TAS.SA.Dominio;
using TAS.SA.Dominio.Dtos;

namespace TAS.SA.Api.Conversores
{
    public class AnuncioConversor : Profile
    {
        public AnuncioConversor()
        {
            CreateMap<Anuncio, ListarAnuncioDTO>();
            CreateMap<Anuncio, DetalheAnuncioDTO>();
            CreateMap<Anuncio, ListarDeAnunciosFechadosDTO>();
        }
    }
}