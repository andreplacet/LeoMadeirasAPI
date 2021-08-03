using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using leoMadeirasAPI.Models;
using leoMadeirasAPI.Services;
using leoMadeirasAPI.RegexTools;
using leoMadeirasAPI.Tools;
using System;
using leoMadeirasAPI.interfaces;

namespace leoMadeirasAPI.Controllers
{
    [ApiController]
    [Route("api")]
    public class HomeController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly ITokenService _tokenservice;
        private readonly IPassword _pwdvalidate;

        public HomeController(IRepository repository, ITokenService tokenservice, IPassword pwdvalidate)
        {
            _repository = repository;
            _tokenservice = tokenservice;
            _pwdvalidate = pwdvalidate;
        }

        [HttpGet]
        [Route("home")]
        [AllowAnonymous]
        public async Task<ActionResult> Home()
        {
            return Ok(new { message = "Desafio LeoMadeirasAPI" });
        }

        [HttpPost]
        [Route("geratoken")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> GeraToken([FromBody] User model)
        {
            var user = _repository.GetUser(model).Result;

            if (user == null)
            {
                return Ok(new { message = "Usuario invalido" });
            }

            user.Token = _tokenservice.GenerateToken(user);
            return new { Id = user.Id, Username = user.Username, Password = user.Password, Token = user.Token, ExpireDate = user.ExpireTokenDate };
        }

        [HttpPost]
        [Route("valida-senha")]
        [Authorize]
        public async Task<ActionResult<dynamic>> ValidarSenha([FromBody] Senha password)
        {
            var result = _pwdvalidate.ValidarSenha(password.Password);

            if (!result)
            {
                return Ok(new { message = false });
            }
            return Ok(new { message = true });
        }

        [HttpGet]
        [Route("gerar-senha")]
        [Authorize]
        public async Task<IActionResult> GerarSenha()
        {
            try
            {
                return Ok(new { senha = GeradorDeSenha.GerarSenha() });
            }
            catch (Exception erro)
            {
                return Ok(new { error = erro });
            }

        }
    }

}