using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Services;
using NSE.WebApp.MVC.Services.Handlers;
using System;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using NSE.WebAPI.Core.Usuario;

namespace NSE.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {

        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Valida quando tiver um atributo do tipo CPF
            services.AddSingleton<IValidationAttributeAdapterProvider, CpfValidationAttributeAdapterProvider>();

            //Singleton existe um contexto para apresentação toda
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Scoped os dados ficam limitados a cada request 
            services.AddScoped<IAspNetUser, AspNetUser>();

            #region HttpServices
            //Transient => é chamado uma instancia cada vez
            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient<IAutenticacaoService, AutenticacaoService>()
                    .AddPolicyHandler(PollyExtensions.EsperarTentar())
                    .AddTransientHttpErrorPolicy(
                    p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            //Handler manipula os request, interceptando todos os request que vier de CatalogoService
            services.AddHttpClient<ICatalogoService, CatalogoService>()
                    .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                    .AddPolicyHandler(PollyExtensions.EsperarTentar())
                    .AddTransientHttpErrorPolicy(
                    p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30))); // se tentou chamar uma api 5x e respondeu com erro consecutivamente  não bate mais, ignora, cortando a comunicação, após 5 tentativas

            services.AddHttpClient<IComprasBffService, ComprasBffService>()
                    .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                    .AddPolicyHandler(PollyExtensions.EsperarTentar())
                    .AddTransientHttpErrorPolicy(
                     p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            #endregion
            #region Refit

            ////Exemplo de uso com Refit
            //services.AddHttpClient("Refit", options =>
            //{
            //    options.BaseAddress = new Uri(configuration.GetSection("CatalogoUrl").Value);
            //})
            //    .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
            //    .AddTypedClient(Refit.RestService.For<ICatalogoServiceRefit>);

            #endregion
        }

        #region PollyExtension
        public static class PollyExtensions
        {
            public static AsyncRetryPolicy<HttpResponseMessage> EsperarTentar()
            {
                var retry = HttpPolicyExtensions
                    .HandleTransientHttpError()
                    .WaitAndRetryAsync(new[]
                    {//Tempo de esperar por tentativa, 1 vez 1 segundo...
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10),
                    }, (outcome, timespan, retryCount, context) =>
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine($"Tentando pela {retryCount} vez!");
                        Console.ForegroundColor = ConsoleColor.White;
                    });

                return retry;
            }
        }
        #endregion
    }
}
