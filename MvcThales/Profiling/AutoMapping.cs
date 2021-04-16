using AutoMapper;
using MvcThales.Models.Caixa;
using Service.Entidades;

namespace MvcThales.Profiling
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Caixa, CaixaViewModel>()
                .ReverseMap();
        }
    }
}
