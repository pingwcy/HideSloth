using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HideSloth.Tools
{
    public class BenchAES
    {
        public static byte[] Encryptor_Bench(byte[] dataToEncrypt, byte[] key,out byte[] tag)
        {
            using (var aesGcm = new AesGcm(key,16))
            {
                tag = new byte[16];
                byte[] nonce = new byte[12];
                byte[] encryptedData = new byte[dataToEncrypt.Length];

                aesGcm.Encrypt(nonce, dataToEncrypt, encryptedData, tag);

                // 将 salt, nonce, encryptedData, tag 连接成一个 byte 数组并返回
                return encryptedData;
            }

        }




        public static void Decrypt(byte[] combinedData, byte[] tag,byte[] key)
        {
            byte[] nonce = new byte[12];

            using (var aesGcm = new AesGcm(key,16))
            {
                byte[] decryptedData = new byte[combinedData.Length];
                aesGcm.Decrypt(nonce, combinedData, tag, decryptedData);

            }
        }

    }
}
    


       
