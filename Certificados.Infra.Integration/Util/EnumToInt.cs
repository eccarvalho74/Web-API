using System;

namespace Certificados.Infra.Integration.Util
{
    public static class EnumToInt
    {

        public static int ToInt<T>(this T tipo) where T : Enum
        {
            return Convert.ToInt32(tipo);
        }
    }
}
