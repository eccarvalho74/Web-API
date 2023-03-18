using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Certificados.Infra.Core.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _databaseContext;

        public UnitOfWork(ApplicationContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        // Chamada para action assíncrona
        public async Task<T> Execute<T>(Func<Task<T>> action)
        {
            try
            {
                using (var transaction = await _databaseContext.Database
                    .BeginTransactionAsync(IsolationLevel.Unspecified))
                {
                    var result = await action();

                    transaction.Commit();

                    return result;
                }
            }
            catch (Exception ex)
            {
                _databaseContext.Dispose();
                throw ex.GetBaseException();
            }
        }

        // Chamada para action síncrona
        [Obsolete("Considere utilizar métodos assíncronos")]
        public T Execute<T>(Func<T> action)
        {
            try
            {
                using (var transaction = _databaseContext.Database
                    .BeginTransaction(IsolationLevel.Unspecified))
                {
                    var result = action();

                    transaction.Commit();

                    return result;
                }
            }
            catch (Exception ex)
            {
                _databaseContext.Dispose();
                throw ex.GetBaseException();
            }
        }
    }
}
