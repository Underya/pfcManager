using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace pfcManager.Model
{
    public class SaltGenerator
    {
        /// <summary>
        /// длина хеша и соли
        /// </summary>
        int size = 40;

        /// <summary>
        /// Количество итерация при работе 
        /// </summary>
        int countIter = 10;

        public SaltGenerator(int Size = 40, int CountIter = 100)
        {
            countIter = CountIter;
            size = Size;
        }

        /// <summary>
        /// Получение по открытому паролю 
        /// </summary>
        /// <param name="OpenPassword"></param>
        /// <param name="Hash"></param>
        /// <param name="Salt"></param>
        public void GetOpenPassword(string OpenPassword, out string Hash, out string Salt)
        {
            byte[] proxyHash = null, proxySalt = null;
            GetOpenPassword(OpenPassword, out proxyHash, out proxySalt);
            Hash = GetString(proxyHash);
            Salt = GetString(proxySalt);
        }

        /// <summary>
        /// Получение по открытому паролю хеша и соли
        /// </summary>
        /// <param name="OpenPassword"></param>
        /// <param name="Hash"></param>
        /// <param name="Salt"></param>
        public void GetOpenPassword(string OpenPassword, out byte[] Hash, out byte[] Salt)
        {
            Salt = GetSalt(OpenPassword);
            Hash = GetHash(OpenPassword, Salt);
        }

        /// <summary>
        /// Получение соли для открытого пароля
        /// </summary>
        /// <param name="OpenPassword"></param>
        /// <returns></returns>
        byte[] GetSalt(string OpenPassword)
        {
            byte[] Salt = null;
            using (Rfc2898DeriveBytes rngCsp = new Rfc2898DeriveBytes(OpenPassword, size, countIter))
            {
                Salt = rngCsp.Salt;
            }

            return Salt;
        }

        /// <summary>
        /// Получение хеша по соли
        /// </summary>
        /// <param name="OpenPassword"></param>
        /// <param name="Salt"></param>
        /// <returns></returns>
        public string GetHash(string OpenPassword, string Salt)
        {
            byte[] proxySalt = GetByte(Salt);
            byte[] proxyHash = GetHash(OpenPassword, proxySalt);
            return GetString(proxyHash);
        }

        /// <summary>
        /// Получение хеша по открытому паролю и соли
        /// </summary>
        /// <param name="OpenPassword"></param>
        /// <param name="Salt"></param>
        /// <returns></returns>
        public byte[] GetHash(string OpenPassword,byte[] Salt)
        {
            byte[] Hash = null;
            using (Rfc2898DeriveBytes rngCsp = new Rfc2898DeriveBytes(OpenPassword, Salt, countIter))
            {
                Hash =  rngCsp.GetBytes(size);
            }

            return Hash;
        }

        /// <summary>
        /// Получение длины хеща и соли
        /// </summary>
        public int GetSize
        {
            get { return size; }
        }

        /// <summary>
        /// Получение из строки набора байт, равных коду символа. Длина равна указаной длине
        /// </summary>
        /// <param name="orign"></param>
        /// <returns></returns>
        byte[] GetByte(string orign)
        {
            byte[] arr = new byte[size];
            for(int i = 0; i < 40; i++)
            {
                arr[i] = (byte)orign[i];
            }

            return arr;
        }

        /// <summary>
        /// Получение строки из байт
        /// </summary>
        /// <param name="orign"></param>
        /// <returns></returns>
        string GetString(byte[] orign)
        {
            string ret = "";
            foreach(byte b in orign)
            {
                ret += (char)b;
            }

            return ret;
        }
    }
}
