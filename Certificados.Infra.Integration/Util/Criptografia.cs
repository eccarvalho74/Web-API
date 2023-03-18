
using Certificados.Infra.Integration.Util;
using System.Security.Cryptography;

namespace Certificados.Infra.Integration.Util
{
    public static class Criptografia
    {
        public static string Criptografar(string textoParaCriptografar)
        {
            var chave = "6FC6D637-4BD3-4FD6-B344-2FD7A20FB5DE";
            var md5 = MD5Criptografia.GerarHashMd5(chave);
            byte[] encrypted;
            using (Aes _Aes = Aes.Create())
            {
                _Aes.Key = System.Text.Encoding.UTF8.GetBytes(md5);
                _Aes.IV = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                encrypted = AESCriptografia.EncryptStringToBytes_Aes(textoParaCriptografar, _Aes.Key, _Aes.IV);

            }
            return Convert.ToBase64String(encrypted);
        }
    }
}
