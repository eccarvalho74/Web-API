using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Certificados.Domain.Formatadores
{
    public static class StringHelper
    {
        public static string Truncate(this string value, int maxChars)
        {
            return value.Length <= maxChars ? value : value.Substring(0, maxChars) + "...";
        }

        public static string CnpjFormat(this string value)
        {
            if (!value.All(char.IsDigit)) return value;
            long CNPJ = Convert.ToInt64(value);

            return string.Format(@"{0:00\.000\.000\/0000\-00}", CNPJ);
        }

        public static string CpfFormat(this string value)
        {
            if (!value.All(char.IsDigit)) return value;

            long CPF = Convert.ToInt64(value);
            return string.Format(@"{0:000\.000\.000\-00}", CPF);
        }

        public static string CepFormat(this string value)
        {
            int CEP;
            if (!int.TryParse(value, out CEP)) return value;

            return string.Format(@"{0:00000\-000}", CEP);
        }

        public static string PhoneFormat(this string value)
        {
            int Phone;
            if (!int.TryParse(value, out Phone)) return value;

            var stringLength = value.Length;
            switch (stringLength)
            {
                case 8:
                    return string.Format("{0:0000-0000}", Phone);
                case 9:
                    return string.Format("{0:00000-0000}", Phone);
                case 10:
                    return string.Format("{0:(00) 0000-0000}", Phone);
                case 11:
                    return string.Format("{0:(00) 00000-0000}", Phone);
                default:
                    return value;
            }
        }

        public static string CurrencyFormat(this string value)
        {
            double Money;
            if (!double.TryParse(value, out Money)) return value;

            return string.Format("R$ {0:N}", Money);
        }
    }
}
