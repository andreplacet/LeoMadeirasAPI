using leoMadeirasAPI.interfaces;
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
        private readonly IRepository _repository;
        public TestesUnidadeAPI()
        {
            _regex = new RegexValidator();
        }
        [Fact]
        public void test_gerar_token()
        {
            //Arrange
            var user = _repository.GetUser("andre", "a@jkwm!q1paCm8c");

            //Act
            user.Token = TokenService.GenerateToken(user);

            //Assert
            Assert.False(string.IsNullOrEmpty(user.Token));
        }

        [Fact]
        public void teste_validar_senha_valida()
        {
            //Arrange
            var user = _repository.GetUser("andre", "a@jkwm!q1paCm8c");

            //Act
            var senhaValida = _regex.ValidarSenha(user.Password);

            //Assert
            Assert.True(senhaValida);
        }

        [Fact]
        public void teste_gerar_senha()
        {
            //Arrange
            var user = _repository.GetUser("andre", "a@jkwm!q1paCm8c");

            //Act
            var senhaGerada = GeradorDeSenha.GerarSenha();

            //Assert
            Assert.True(_regex.ValidarSenha(senhaGerada));
        }
    }
}