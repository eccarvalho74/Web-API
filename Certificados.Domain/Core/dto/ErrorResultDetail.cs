using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Certificados.Domain.Core.dto
{
    public class ErrorResultDetail
    {
        public string Code { get; }
        public string Message { get; set; }


        public ErrorResultDetail(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public ErrorResultDetail Format(params object[] valor)
        {
            return new ErrorResultDetail(Code, string.Format(Message, valor));
        }
    }
}
