using APIRest.Domain.IRepositories;
using APIRest.Domain.IServices;
using APIRest.Domain.Models;
using System;
using System.Threading.Tasks;

namespace APIRest.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        public LoginService(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        public async Task GuardarLogMonitoreo(LogMonitoreo logMonitoreo)
        {
            logMonitoreo.FechaRegistro = DateTime.Now;
           await _loginRepository.GuardarLogMonitoreo(logMonitoreo);
        }

        public async Task<Usuario> ValidateUser(Usuario usuario)
        {
            return await _loginRepository.ValidateUser(usuario);
        }
    }
}
