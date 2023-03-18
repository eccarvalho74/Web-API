using AutoMapper;
using Certificados.Domain.Entities;
using Certificados.Domain.Interfaces;
using Certificados.Infra.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Certificados.Infra.Repositories
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(ApplicationContext context, IMapper mapper) : base(context, mapper)
        {
            
        }
    }
}
