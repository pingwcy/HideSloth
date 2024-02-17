using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace HideSloth.Crypto
{
    public class Aes_ChaCha_Encryptor
    {
        private const int KeySize = 32; // 256 bits
        private const int NonceSize = 12; // AES-GCM 常用的随机数大小
        private const int TagSize = 16; // AES-GCM 认证标签的大小


        public static byte[] Encrypt(byte[] dataToEncrypt, string password, out byte[] salt, out byte[] nonce, out byte[] tag)
        {
            int SaltSize = 16; // 128 bits
            if (GlobalVariables.waitencmaster)
            {
                salt = new byte[SaltSize];
                nonce = new byte[NonceSize];

                RandomNumberGenerator.Create().GetBytes(salt);
                RandomNumberGenerator.Create().GetBytes(nonce);
                // 使用PBKDF2生成密钥
                using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, Support_Converter.StringToHashAlgorithmName("SHA256")))
                {
                    var key_ = pbkdf2.GetBytes(KeySize);
                    tag = new byte[TagSize];
                    byte[] encryptedkey = new byte[dataToEncrypt.Length];

                    // 进行AES-GCM加密
                    using (var aesGcm = new AesGcm(key_, TagSize))
                    {
                        aesGcm.Encrypt(nonce, dataToEncrypt, encryptedkey, tag);

                        // 将 salt, nonce, encryptedData, tag 连接成一个 byte 数组并返回
                        return encryptedkey;
                    }
                }
            }
            else if (GlobalVariables.KDF == "Password Based")
            {
                salt = new byte[SaltSize];
                nonce = new byte[NonceSize];

                RandomNumberGenerator.Create().GetBytes(salt);
                RandomNumberGenerator.Create().GetBytes(nonce);

                // 使用PBKDF2生成密钥
                using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, GlobalVariables.Iteration, Support_Converter.StringToHashAlgorithmName(GlobalVariables.Hash)))
                {
                    var key2 = pbkdf2.GetBytes(KeySize);
                    tag = new byte[TagSize];
                    byte[] encryptedData2 = new byte[dataToEncrypt.Length];

                    // 进行AES-GCM加密
                    if (GlobalVariables.Encalg == "AES")
                    {
                        using (var aesGcm = new AesGcm(key2, TagSize))
                        {
                            aesGcm.Encrypt(nonce, dataToEncrypt, encryptedData2, tag);

                            // 将 salt, nonce, encryptedData, tag 连接成一个 byte 数组并返回
                            return encryptedData2;
                        }
                    }
                    else if (GlobalVariables.Encalg == "ChaCha")
                    {
                        using (var ChaCha = new ChaCha20Poly1305(key2))
                        {
                            ChaCha.Encrypt(nonce, dataToEncrypt, encryptedData2, tag);

                            // 将 salt, nonce, encryptedData, tag 连接成一个 byte 数组并返回
                            return encryptedData2;
                        }

                    }
                    return encryptedData2;
                }

            }
            else
            {
                // 生成盐
                nonce = new byte[NonceSize];

                RandomNumberGenerator.Create().GetBytes(nonce);
                byte[] key = new byte[KeySize];
                // 使用PBKDF2生成密钥
                RandomNumberGenerator.Create().GetBytes(key);
                //加密主密钥
                RSA rSA = RSA.Create();
                rSA.ImportSubjectPublicKeyInfo(Convert.FromBase64String(GlobalVariables.pubkey), out _);
                rSA.KeySize = GlobalVariables.rsasize;
                salt = rSA.Encrypt(key, RSAEncryptionPadding.Pkcs1);

                tag = new byte[TagSize];
                byte[] encryptedData = new byte[dataToEncrypt.Length];

                // 进行AES-GCM加密
                if (GlobalVariables.Encalg == "AES")
                {
                    using (var aesGcm = new AesGcm(key, TagSize))
                    {
                        aesGcm.Encrypt(nonce, dataToEncrypt, encryptedData, tag);

                        // 将 salt, nonce, encryptedData, tag 连接成一个 byte 数组并返回
                        return encryptedData;
                    }
                }
                else if (GlobalVariables.Encalg == "ChaCha")
                {
                    using (var ChaCha = new ChaCha20Poly1305(key))
                    {
                        ChaCha.Encrypt(nonce, dataToEncrypt, encryptedData, tag);

                        // 将 salt, nonce, encryptedData, tag 连接成一个 byte 数组并返回
                        return encryptedData;
                    }

                }
                return encryptedData;
            }

        }
    }

    public class Aes_ChaCha_Decryptor
    {
        private const int KeySize = 32; // 256 bits
        private const int NonceSize = 12; // AES-GCM 常用的随机数大小
        private const int TagSize = 16; // AES-GCM 认证标签的大小

        public static byte[] Decrypt(byte[] combinedData, string password)
        {
            int SaltSize = 16; // 128 bits
            if (GlobalVariables.KDF != "Password Based")
            {
                SaltSize = GlobalVariables.rsasize / 8;
            }

            // 提取盐、随机数、认证标签
            byte[] salt = new byte[SaltSize];
            int Iterations = GlobalVariables.Iteration; // PBKDF2 iterations
            string HashAlg = GlobalVariables.Hash;

            Buffer.BlockCopy(combinedData, 0, salt, 0, SaltSize);

            byte[] key = new byte[KeySize];
            // 从 combinedData 中截取 encryptedData
            if (GlobalVariables.KDF == "Password Based")
            {
                // 使用PBKDF2生成密钥
                var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, Support_Converter.StringToHashAlgorithmName(HashAlg));
                key = pbkdf2.GetBytes(KeySize); // 256 bits
            }
            else
            {
                byte[] privatekey = getPrivateKey(Convert.FromBase64String(GlobalVariables.privatekeyenced), password);
                RSA rSA = RSA.Create();
                rSA.KeySize = GlobalVariables.rsasize;
                rSA.ImportSubjectPublicKeyInfo(Convert.FromBase64String(GlobalVariables.pubkey), out _);
                rSA.ImportPkcs8PrivateKey(privatekey, out _);
                key = rSA.Decrypt(salt, RSAEncryptionPadding.Pkcs1);
            }
            byte[] nonce = new byte[NonceSize];
            byte[] tag = new byte[TagSize];

            Buffer.BlockCopy(combinedData, SaltSize, nonce, 0, NonceSize);
            Buffer.BlockCopy(combinedData, SaltSize + NonceSize, tag, 0, TagSize);
            // 计算加密数据的长度
            int offset = SaltSize + NonceSize + TagSize;
            int encryptedDataLength = combinedData.Length - offset;
            byte[] encryptedData = new byte[encryptedDataLength];
            byte[] decryptedData = new byte[encryptedDataLength];
            Buffer.BlockCopy(combinedData, offset, encryptedData, 0, encryptedDataLength);

            // 进行AES-GCM解密
            if (GlobalVariables.Encalg == "AES")
            {
                using (var aesGcm = new AesGcm(key, TagSize))
                {
                    aesGcm.Decrypt(nonce, encryptedData, tag, decryptedData);

                    return decryptedData;
                }
            }
            else if (GlobalVariables.Encalg == "ChaCha")
            {
                using (var ChaCha = new ChaCha20Poly1305(key))
                {
                    ChaCha.Decrypt(nonce, encryptedData, tag, decryptedData);

                    return decryptedData;
                }

            }
            return decryptedData;
        }
        public static byte[] getPrivateKey(byte[] combinedData, string password)
        {
            // 提取盐、随机数、认证标签
            int SaltSize = 16; // 128 bits
            byte[] salt = new byte[SaltSize];
            byte[] nonce = new byte[NonceSize];
            byte[] tag = new byte[TagSize];

            Buffer.BlockCopy(combinedData, 0, salt, 0, SaltSize);
            Buffer.BlockCopy(combinedData, SaltSize, nonce, 0, NonceSize);
            Buffer.BlockCopy(combinedData, SaltSize + NonceSize, tag, 0, TagSize);

            // 计算加密数据的长度
            int offset = SaltSize + NonceSize + TagSize;
            int encryptedDataLength = combinedData.Length - offset;
            byte[] encryptedData = new byte[encryptedDataLength];

            // 从 combinedData 中截取 encryptedData
            Buffer.BlockCopy(combinedData, offset, encryptedData, 0, encryptedDataLength);

            // 使用PBKDF2生成密钥
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, Support_Converter.StringToHashAlgorithmName("SHA256")))
            {
                var key = pbkdf2.GetBytes(KeySize); // 256 bits
                byte[] decryptedData = new byte[encryptedDataLength];

                // 进行AES-GCM解密
                using (var aesGcm = new AesGcm(key, TagSize))
                {
                    aesGcm.Decrypt(nonce, encryptedData, tag, decryptedData);
                    return decryptedData;
                }

            }

        }
    }
}

