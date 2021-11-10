using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Services;
using NSE.WebApp.MVC.Services.Handlers;

namespace NSE.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {

        public static void RegisterServices(this IServiceCollection services)
        {
            //Transient => é chamado uma instancia cada vez
            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient<IAutenticacaoService, AutenticacaoService>();

            //Handler manipula os request, interceptando todos os request que vier de CatalogoService
            services.AddHttpClient<ICatalogoService, CatalogoService>()
                    .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

            //Singleton existe um contexto para apresentação toda
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Scoped os dados ficam limitados a cada request 
            services.AddScoped<IUser, AspNetUser>();
        }
    }
}
