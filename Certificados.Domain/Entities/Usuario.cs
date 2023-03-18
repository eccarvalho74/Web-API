using Certificados.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Certificados.Domain.Entities
{
    public class Usuario : BaseEntity
    {
        public virtual string Nome { get; set; }

        public virtual string Email { get; set; }

        public virtual string Senha { get; set; }

        public virtual string Salt { get; set; }

    }
}
