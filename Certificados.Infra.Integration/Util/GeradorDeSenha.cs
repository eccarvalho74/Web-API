using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Certificados.Infra.Integration.Util
{
    public static class GeradorDeSenha
    {
        private static string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        private static int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        public static string ObterSenha()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomNumber(100, 999));
            builder.Append(RandomString(2, false));
            return builder.ToString();
        }

        public static  string GerarSalt(int tamanho)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[tamanho];
            rng.GetBytes(buff);

            return BitConverter.ToString(buff).Replace("-", "").ToLower();
        }

        public static string ObterHash(string texto, string chave)
        {
            UTF8Encoding encoding = new UTF8Encoding();

            Byte[] bytesTexto = encoding.GetBytes(texto);
            Byte[] bytesChave = encoding.GetBytes(chave);

            Byte[] bytesHash;

            using (HMACSHA256 hash = new HMACSHA256(bytesChave))
            {
                bytesHash = hash.ComputeHash(bytesTexto);
            }

            return BitConverter.ToString(bytesHash).Replace("-", "").ToLower();
        }

        public static string EncryptDados(string chave, string texto)
        {
            byte[] encrypted;
            using (Aes _Aes = Aes.Create())
            {
                string chaveMD5 = chave;

                _Aes.Key = System.Text.Encoding.UTF8.GetBytes(MD5Criptografia.GerarHashMd5(chaveMD5));
                _Aes.IV = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                encrypted = AESCriptografia.EncryptStringToBytes_Aes(texto, _Aes.Key, _Aes.IV);

            }
            return Convert.ToBase64String(encrypted);
        }

    }
}
