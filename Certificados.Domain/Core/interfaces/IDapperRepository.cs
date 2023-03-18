using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Certificados.Domain.Core.Interfaces
{
    public interface IDapperRepository   
    {
        Task<List<T>> Execute<T>(string sql) where T : class;
        Task<List<T>> Execute<T>(string sql, object param = null) where T : class;

    }
}
