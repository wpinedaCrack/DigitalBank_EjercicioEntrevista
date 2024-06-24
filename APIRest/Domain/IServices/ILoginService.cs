using APIRest.Domain.Models;
using System.Threading.Tasks;

namespace APIRest.Domain.IServices
{
    public interface ILoginService
    {
        Task<Usuario> ValidateUser(Usuario usuario);
        Task GuardarLogMonitoreo(LogMonitoreo logMonitoreo);
    }
}
