using Certificados.Domain.Core.Interfaces;
using Certificados.Domain.Entities;

namespace Certificados.Domain.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>, IBaseRepository
    {
    }
}
