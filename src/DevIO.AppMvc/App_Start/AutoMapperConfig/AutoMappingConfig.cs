using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using DevIO.AppMvc.ViewModels;
using DevIO.Bussiness.Models.Fornecedores;
using DevIO.Bussiness.Models.Produtos;

namespace DevIO.AppMvc.AutoMapperConfig
{
    public class AutoMappingConfig
    {
        public static MapperConfiguration GetAutoMapperConfig()
        {
            var profiles = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(x => typeof(Profile).IsAssignableFrom(x));

            return new MapperConfiguration(cfg =>
            {
                foreach (var profile in profiles)
                    cfg.AddProfile(Activator.CreateInstance(profile) as Profile);
            });
        }
    }

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Produto, ProdutoViewModel>().ReverseMap();
            CreateMap<Endereco, EnderecoViewModel>().ReverseMap();
            CreateMap<Fornecedor, FornecedorViewModel>().ReverseMap();
        }
    }
}