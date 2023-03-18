using System;
using System.Threading.Tasks;

namespace Certificados.Infra.Core.UoW
{
    public interface IUnitOfWork
    {
        //Interface assíncrona
        Task<T> Execute<T>(Func<Task<T>> action);
        //Interface síncrona
        T Execute<T>(Func<T> action);
    }
}
