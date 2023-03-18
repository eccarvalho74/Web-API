using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Certificados.Domain.Core.dto
{
    public class ServiceResult<T> : ServiceResult
    {
        public T Data { get; set; }
        public ServiceResult() : this(default) { }
        public ServiceResult(T result) => Data = result;
    }

    public class ServiceResult
    {
        public string Message { get; set; }
        public bool IsSuccess { get { return !(Errors?.Count > 0); } }
        public List<ErrorResultDetail> Errors { get; set; } = new List<ErrorResultDetail>();


        public void AddError(string codErro, string mensagem)
        {
            ErrorResultDetail erro = new ErrorResultDetail(codErro, mensagem);
            Errors.Add(erro);
        }

        public void AddError(ErrorResultDetail errorResultDetail)
        {
            Errors.Add(errorResultDetail);
        }

        public bool ContainsError()
        {
            return Errors.Any();
        }
    }
}
