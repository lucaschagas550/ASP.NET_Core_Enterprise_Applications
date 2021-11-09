using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Services;

namespace NSE.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddHttpClient<IAutenticacaoService, AutenticacaoService>();

            services.AddHttpClient<ICatalogoService, CatalogoService>();

            //Singleton existe um contexto para apresentação toda
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Scoped os dados ficam limitados a cada request 
            services.AddScoped<IUser, AspNetUser>();
        }
    }
}
