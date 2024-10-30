using System.Net.Http.Json;
using System.Text;
using Dima.Core.Handlers;
using Dima.Core.Requests.Account;
using Dima.Core.Responses;

namespace Dima.Web.Handlers;

public class AccountHandler(IHttpClientFactory httpClientFactory) : IAccountHandler
{
    private readonly HttpClient _client = httpClientFactory.CreateClient(Configuration.HttpClientName);
    
    public async Task<Response<string>> LoginAsync(LoginRequest request)
    {
        var result = await _client.PostAsJsonAsync("v1/identity/login?useCookies=true", request);
        return GetResponse(result);
    }

    public async Task<Response<string>> RegisterAsync(RegisterRequest request)
    {
        var result = await _client.PostAsJsonAsync("v1/identity/register", request);
        return result.IsSuccessStatusCode
            ? new Response<string>("Cadastro realizado com sucesso!", 201, "Cadastro realizado com sucesso!")
            : new Response<string>(null, 400, "Não foi possível realizar o seu cadastro");
    }

    public async Task LogoutAsync()
    {
        var emptyContent = new StringContent("{}", Encoding.UTF8, "application/json");
        await _client.PostAsJsonAsync("v1/identity/logout", emptyContent);
    }

    private Response<string> GetResponse(HttpResponseMessage result)
    {
        return result.StatusCode switch
        {
            System.Net.HttpStatusCode.OK => new Response<string>("Login realizado com sucesso!", 200, "Login realizado com sucesso!"),
            System.Net.HttpStatusCode.BadRequest => new Response<string>(null, 400, "Não foi possível realizar a operação"),
            System.Net.HttpStatusCode.Unauthorized => new Response<string>(null, 401, "Email ou senha inválidos"),
            System.Net.HttpStatusCode.Forbidden => new Response<string>(null, 403, "Você não tem permissão para acessar este recurso"),
            System.Net.HttpStatusCode.InternalServerError => new Response<string>(null, 500, "Ocorreu um erro inesperado. Entre em contato com nosso suporte"),
            _ => new Response<string>(null, 500, "Ocorreu um erro inesperado. Entre em contato com nosso suporte")
        };
    } 
}