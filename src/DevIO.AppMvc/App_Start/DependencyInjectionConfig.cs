using System.Reflection;
using System.Web.Mvc;
using AutoMapper;
using DevIO.AppMvc.AutoMapperConfig;
using DevIO.Bussiness.Models.Fornecedores;
using DevIO.Bussiness.Models.Fornecedores.Services;
using DevIO.Bussiness.Models.Produtos;
using DevIO.Bussiness.Models.Produtos.Services;
using DevIO.Bussiness.Notificacoes;
using DevIO.Infra.Data.Repositories;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;

namespace DevIO.AppMvc
{
    public class DependencyInjectionConfig
    {
        public static void RegisterDIContainer()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            InitializeContainer(container);
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.Verify();
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        private static void InitializeContainer(Container container)
        {
            container.Register<DecoratorContext>(Lifestyle.Scoped);
            container.Register<IProdutoRepository, ProdutoRepository>(Lifestyle.Scoped);
            container.Register<IProdutoService, ProdutoService>(Lifestyle.Scoped);
            container.Register<IEnderecoRespository, EnderecoRespository>(Lifestyle.Scoped);
            container.Register<IFornecedorRepository, FornecedorRepository>(Lifestyle.Scoped);
            container.Register<INotificador, Notificador>(Lifestyle.Scoped);

            container.RegisterSingleton(() => AutoMappingConfig.GetAutoMapperConfig().CreateMapper(container.GetInstance));
        }
    }
}