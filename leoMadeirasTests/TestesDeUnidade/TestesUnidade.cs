using leoMadeirasAPI.interfaces;
using leoMadeirasAPI.Models;
using leoMadeirasAPI.RegexTools;
using leoMadeirasAPI.Repositories;
using leoMadeirasAPI.Services;
using leoMadeirasAPI.Tools;
using Xunit;

namespace leoMadeirasTests.TestesDeUnidade
{
    public class TestesUnidadeAPI
    {
        private readonly RegexValidator _regex;
        private readonly ITokenService _tokenservice;
        public TestesUnidadeAPI()
        {
            _regex = new RegexValidator();
            _tokenservice = new TokenService();
        }
        [Fact]
        public void test_gerar_token()
        {
            //Arrange
            var user = new User("andre", "a@jkwm!q1paCm8c");

            //Act
            user.Token = _tokenservice.GenerateToken(user);

            //Assert
            Assert.False(string.IsNullOrEmpty(user.Token));
        }

        [Fact]
        public void teste_validar_senha_valida()
        {
            //Arrange
            var user = new User("andre", "a@jkwm!q1paCm8c");
            //Act
            var senhaValida = _regex.ValidarSenha(user.Password);

            //Assert
            Assert.True(senhaValida);
        }

        [Fact]
        public void teste_gerar_senha()
        {
            //Act
            var senhaGerada = GeradorDeSenha.GerarSenha();

            //Assert
            Assert.True(_regex.ValidarSenha(senhaGerada));
        }
    }
}