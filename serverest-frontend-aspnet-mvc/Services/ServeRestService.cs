using Microsoft.AspNetCore.Identity.Data;
using NuGet.Protocol.Plugins;
using serverest_frontend_aspnet_mvc.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace serverest_frontend_aspnet_mvc.Services
{
    public class ServeRestService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ServeRestService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://serverest.dev/");
            _httpContextAccessor = httpContextAccessor;
        }

        // ROTA LOGIN
        public async Task<LoginDtoResponse> LoginAsync(string email, string password)
        {
            var loginRequest = new LoginDtoRequest
            {
                Email = email,
                Password = password
            };

            var response = await _httpClient.PostAsJsonAsync("login", loginRequest);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<LoginDtoResponse>();
            }
            else
            {
                return null;
            }
        }

        public bool AuthorizationToken()
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("AuthToken");
            if (!string.IsNullOrEmpty(token))
            {
                return true;
            }
            return false;
        }

        public void SetAuthorizationToken()
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("AuthToken");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Add("Authorization", token);
            }
        }

        // ROTA USUARIOS

        public async Task<Usuario> GetUserByEmailAsync(string email)
        {
            return (await _httpClient.GetFromJsonAsync<UsuarioResponse>($"usuarios?email={email}")).Usuarios.FirstOrDefault();
        }

        public async Task<List<Usuario>> GetAllUsuarios()
        {
            return (await _httpClient.GetFromJsonAsync<UsuarioResponse>("usuarios")).Usuarios;
        }

       
        // ROTAS PRODUTOS
        public async Task<List<Produto>>GetAllProdutos()
        {
            return (await _httpClient.GetFromJsonAsync<ProdutoResponse>("produtos")).Produtos;
        }

        public async Task<Produto> GetProdutoById(string id)
        {
            return (await _httpClient.GetFromJsonAsync<Produto>($"produtos/{id}"));
        }

        public async Task<string> CreateProduto(ProdutoCreate produto)
        {
            SetAuthorizationToken();
            var response = await _httpClient.PostAsJsonAsync($"produtos", produto);
            var responseMessage = await response.Content.ReadFromJsonAsync<ResponseMessage>();
            return responseMessage?.Message ?? "Erro ao atualizar o produto.";
        }

        public async Task<string> UpdateProdutoById(string id, ProdutoCreate produto)
        {
            SetAuthorizationToken();
            var response = await _httpClient.PutAsJsonAsync($"produtos/{id}", produto);
            var responseMessage = await response.Content.ReadFromJsonAsync<ResponseMessage>();
            return responseMessage?.Message ?? "Erro ao atualizar o produto.";
        }

        public async Task<string> DeleteProdutoById(string id)
        {
            SetAuthorizationToken();
            var response = await _httpClient.DeleteAsync($"produtos/{id}");
            var responseMessage = await response.Content.ReadFromJsonAsync<ResponseMessage>();
            return responseMessage?.Message ?? "Erro ao atualizar o produto.";
        }
    }
}
