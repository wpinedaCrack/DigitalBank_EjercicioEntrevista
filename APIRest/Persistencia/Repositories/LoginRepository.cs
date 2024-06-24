using APIRest.Domain.IRepositories;
using APIRest.Domain.Models;
using APIRest.Persistencia.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace APIRest.Persistencia.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly AplicationDbContext _context;
        public LoginRepository(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task GuardarLogMonitoreo(LogMonitoreo logMonitoreo)
        {
            _context.Add(logMonitoreo);
            await _context.SaveChangesAsync();
        }

        public async Task<Usuario> ValidateUser(Usuario usuario)
        {
            var user = await _context.Usuario.Where(x => x.Email == usuario.Email
                                                && x.Passworld == usuario.Passworld).FirstOrDefaultAsync();
            return user;
        }
    }
}
