using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HideSloth.Tools
{
    public class Bench_Encryption
    {
        public static byte[] Encryptor_Bench(string alg, byte[] dataToEncrypt, byte[] key,out byte[] tag)
        {
            tag = new byte[16];
            byte[] nonce = new byte[12];
            byte[] encryptedData = new byte[dataToEncrypt.Length];

            if (alg == "AES")
            {
                using (var aesGcm = new AesGcm(key, 16))
                {

                    aesGcm.Encrypt(nonce, dataToEncrypt, encryptedData, tag);

                    // 将 salt, nonce, encryptedData, tag 连接成一个 byte 数组并返回
                    return encryptedData;
                }
            }
            if (alg == "ChaCha")
            {
                using (var Cha = new ChaCha20Poly1305(key))
                {

                    Cha.Encrypt(nonce, dataToEncrypt, encryptedData, tag);

                    // 将 salt, nonce, encryptedData, tag 连接成一个 byte 数组并返回
                    return encryptedData;
                }
            }
            return encryptedData;
        }




        public static void Decryptor_Bench(string alg, byte[] combinedData, byte[] tag,byte[] key)
        {
            byte[] nonce = new byte[12];
            byte[] decryptedData = new byte[combinedData.Length];

            if (alg == "AES")
            {
                using (var aesGcm = new AesGcm(key, 16))
                {
                    aesGcm.Decrypt(nonce, combinedData, tag, decryptedData);

                }
            }
            else if (alg == "ChaCha")
            {
                using (var Cha = new ChaCha20Poly1305(key))
                {
                    Cha.Decrypt(nonce, combinedData, tag, decryptedData);

                }
            }

        }

    }
}
    


       
