using APIRest.Domain.Models;
using System.Threading.Tasks;

namespace APIRest.Domain.IRepositories
{
    public interface ILoginRepository
    {
        Task<Usuario> ValidateUser(Usuario usuario);
        Task GuardarLogMonitoreo(LogMonitoreo logMonitoreo);
    }
}
