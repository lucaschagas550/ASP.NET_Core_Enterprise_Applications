﻿using NSE.WebApp.MVC.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Services
{
    public class AutenticacaoSerice : IAutenticacaoService
    {
        private readonly HttpClient _httpCliente;

        public AutenticacaoSerice(HttpClient httpClient)
        {
            _httpCliente = httpClient;
        }

        public async Task<string> Login(UsuarioLogin usuarioLogin)
        {
            //Transforma o conteudo em json
            var loginContent = new StringContent(
                JsonSerializer.Serialize(usuarioLogin),
                Encoding.UTF8, 
                "application/json");

            var response = await _httpCliente.PostAsync("https://localhost:44354/api/identidade/autenticar", loginContent);

            var teste = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<string>(await response.Content.ReadAsStringAsync());
        }

        public async Task<string> Registro(UsuarioRegistro usuarioRegistro)
        {
            //Transforma o conteudo em json
            var registroContent = new StringContent(
                JsonSerializer.Serialize(usuarioRegistro),
                Encoding.UTF8,
                "application/json");

            var response = await _httpCliente.PostAsync("https://localhost:44354/api/identidade/nova-conta", registroContent);

            return JsonSerializer.Deserialize<string>(await response.Content.ReadAsStringAsync());
        }
    }
}
