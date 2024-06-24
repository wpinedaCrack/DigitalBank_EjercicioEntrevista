using APIRest.Domain.IServices;
using APIRest.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRest_Test.Mocks
{
    public class LoginServiceMocksTest : ILoginService
    {
        public Task GuardarLogMonitoreo(LogMonitoreo logMonitoreo)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> ValidateUser(Usuario usuario)
        {
            throw new NotImplementedException();
        }
    }
}
