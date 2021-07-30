using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using leoMadeirasAPI.Models;
using leoMadeirasAPI.Services;
using leoMadeirasAPI.Repositories;
using leoMadeirasAPI.RegexTools;
using leoMadeirasAPI.Tools;
using System;

namespace leoMadeirasAPI.Controllers
{
    [ApiController]
    [Route("api")]
    public class HomeController : ControllerBase
    {
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
            var user = UserRepository.Get(model.Username, model.Password);

            if (user == null)
            {
                return NotFound(new { message = "Usuario invalido" });
            }

            user.Token = TokenService.GenerateToken(user);
            return new { Id = user.Id, Username = user.Username, Password = user.Password, Token = user.Token, ExpireDate = user.ExpireTokenDate };
        }

        [HttpPost]
        [Route("valida-senha")]
        [Authorize]
        public async Task<ActionResult<dynamic>> ValidarSenha([FromBody] User model)
        {
            var validaSenha = new RegexValidator();
            var result = validaSenha.ValidarSenha(model.Password);

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