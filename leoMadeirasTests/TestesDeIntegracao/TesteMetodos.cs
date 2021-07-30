using Xunit;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using leoMadeirasAPI;
using System.Net.Http;
using System.Threading.Tasks;
using leoMadeirasAPI.Repositories;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using leoMadeirasAPI.Models;
using System.Text.Json;
using leoMadeirasTests.Models;

namespace leoMadeirasTests
{
    public class TestMetodosAPI
    {
        private readonly HttpClient _client;

        public TestMetodosAPI()
        {
            var server = new TestServer(new WebHostBuilder()
                                .UseEnvironment("Development")
                                .UseStartup<Startup>());
            _client = server.CreateClient();
        }

        [Theory]
        [InlineData("andre", "a@jkwm!q1paCm8c")]
        public async Task teste_metodo_gera_token(string username, string password)
        {
            //Arrange
            var user = UserRepository.Get(username, password);
            //Act
            var response = await _client.PostAsJsonAsync("api/geratoken", user);
            //Assert
            Assert.True(response.IsSuccessStatusCode);
        }

        [Theory]
        [InlineData("andre", "a@jkwm!q1paCm8c")]
        public async Task teste_metodo_valida_senha(string username, string password)
        {
            //Arrange
            var user = UserRepository.Get(username, password);
            var response = await _client.PostAsJsonAsync("api/geratoken", user);
            var userDataResponse = response.Content.ReadFromJsonAsync<User>().Result;

            //Act
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userDataResponse.Token);

            var responseSenha = await _client.PostAsJsonAsync("api/valida-senha", user);
            var content = responseSenha.Content.ReadFromJsonAsync<MessageMoq>().Result;

            //Assert
            Assert.True(responseSenha.IsSuccessStatusCode);
            Assert.Equal(true, content.message);
        }

        [Theory]
        [InlineData("andre", "a@jkwm!q1paCm8c")]
        public async Task teste_metodo_gera_senha(string username, string password)
        {
            //Arrange
            var user = UserRepository.Get(username, password);
            var responseToken = await _client.PostAsJsonAsync("api/geratoken", user);
            var userDataResponse = responseToken.Content.ReadFromJsonAsync<User>().Result;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userDataResponse.Token);

            //Act
            var request = new HttpRequestMessage(HttpMethod.Get, "api/gerar-senha");

            var response = await _client.SendAsync(request);

            //Assert
            Assert.True(response.IsSuccessStatusCode);

        }
    }
}