using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Certificados.Infra.Integration.Util
{
    public class MensageriaConfig
    {  

        public string OneSignalUrlBase { get; set; }
        public string OneSignalAppId { get; set; }
        public int LimitPerCall { get; set; }
        public int MaxDegreeOfParallelism { get; set; }
    }

}
