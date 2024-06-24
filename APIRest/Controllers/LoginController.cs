using APIRest.Domain.IServices;
using APIRest.Domain.Models;
using APIRest.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace APIRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IConfiguration _config;

        public LoginController(IConfiguration config)
        {
            _config = config;
        }
        public LoginController(ILoginService loginService, IConfiguration config)
        {
            _loginService = loginService;
            _config = config;
        }

        [Route("Autenticar")]
        [HttpPost]
        public async Task<IActionResult> Autenticar([FromBody] Usuario usuario)
        {
            try
            {
                usuario.Passworld = Encriptar.EncriptarPassword(usuario.Passworld);

                var user = await _loginService.ValidateUser(usuario);
                if (user == null)
                {
                    return BadRequest(new { message = "Usuario o contraseña invalidos" });
                }

                string tokenString = JwtConfigurator.GetToken(user, _config);

                return Ok(new { token = tokenString, userId = user.Id }); //usuario = user.NombreUsuario
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("GuardarLog")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GuardarLog([FromBody] LogMonitoreo logMonitoreo)
        {
            try
            {
                await _loginService.GuardarLogMonitoreo(logMonitoreo);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
