using AutoMapper;
using Certificados.Domain.Dto.Responses;
using Certificados.Domain.Entities;

namespace Certificados.API.AutoMappers
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            // CreateMap<TipoResposta, TipoRespostaResponse>().ReverseMap();
            // CreateMap<TipoResposta, ExportarDadosTipoRespostaResponse>().ReverseMap();


            CreateMap<Usuario, UsuarioResponse>();
        }
    }
}
