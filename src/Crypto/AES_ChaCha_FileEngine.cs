using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Security.Cryptography;
using HideSloth;

namespace HideSloth.Crypto
{
    public class FileEnc
    {
        private const int BlockSize = 10 * 1024 * 1024; // 10 MB
        private const int NonceSize = 12;
        private const int TagSize = 16;
        private const int KeySize = 32; // 256 bits

        public static void EncryptFile(string inputFilePath, string outputFilePath, string password)
        {
            int Iterations = GlobalVariables.Iteration;
            string HashAlg = GlobalVariables.Hash;
            int SaltSize = 16;
            if (GlobalVariables.KDF != "Password Based")
            {
                SaltSize = GlobalVariables.rsasize/8;
            }
            // 生成 salt
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] key = new byte[KeySize];
            // 使用 PBKDF2 从密码生成密钥
            if (GlobalVariables.KDF == "Password Based")
            {
                using var keyDerivationFunction = new Rfc2898DeriveBytes(password, salt, Iterations, (Support_Converter.StringToHashAlgorithmName(HashAlg)));
                key = keyDerivationFunction.GetBytes(KeySize);
            }
            else
            {
                RandomNumberGenerator.Create().GetBytes(key);
                //加密主密钥
                RSA rSA = RSA.Create();
                rSA.ImportSubjectPublicKeyInfo(Convert.FromBase64String(GlobalVariables.pubkey), out _);
                rSA.KeySize = GlobalVariables.rsasize;
                salt = rSA.Encrypt(key, RSAEncryptionPadding.Pkcs1);

            }
            AesGcm? Enc_Obj = null; // 使用可空类型，因为 AesGcm 是一个引用类型
            ChaCha20Poly1305? Enc_obj2 = null;
            if (GlobalVariables.Encalg == "AES")
            {
                Enc_Obj = new AesGcm(key, TagSize);
            }
            else if (GlobalVariables.Encalg == "ChaCha")
            {
                Enc_obj2 = new ChaCha20Poly1305(key);
            }
            using var inputFileStream = new FileStream(inputFilePath, FileMode.Open);
            using var outputFileStream = new FileStream(outputFilePath, FileMode.Create);


            byte[] buffer = new byte[BlockSize];
            int bytesRead;
            while ((bytesRead = inputFileStream.Read(buffer, 0, BlockSize)) > 0)
            {
                // 将 salt 写入输出文件
                outputFileStream.Write(salt, 0, SaltSize);

                // 为每个块生成新的随机 nonce
                byte[] nonce = RandomNumberGenerator.GetBytes(NonceSize);
                byte[] encryptedData = new byte[bytesRead];
                byte[] tag = new byte[TagSize];

                // 加密数据块
                if (GlobalVariables.Encalg == "AES" && Enc_Obj != null)
                {
                    Enc_Obj.Encrypt(nonce, buffer.AsSpan(0, bytesRead), encryptedData, tag);
                }
                else if (GlobalVariables.Encalg == "ChaCha" && Enc_obj2 != null)
                {
                    Enc_obj2.Encrypt(nonce, buffer.AsSpan(0, bytesRead), encryptedData, tag);

                }
                // 将 nonce、加密数据和 tag 写入输出文件
                outputFileStream.Write(nonce, 0, NonceSize);
                outputFileStream.Write(tag, 0, TagSize);
                outputFileStream.Write(encryptedData, 0, encryptedData.Length);
            }
        }
        public static void DecryptFile(string inputFilePath, string outputFilePath, string password)
        {
            int Iterations = GlobalVariables.Iteration;
            string HashAlg = GlobalVariables.Hash;

            using var inputFileStream = new FileStream(inputFilePath, FileMode.Open);
            using var outputFileStream = new FileStream(outputFilePath, FileMode.Create);


            while (inputFileStream.Position < inputFileStream.Length)
            {
                int SaltSize = 16;
                if (GlobalVariables.KDF != "Password Based")
                {
                    SaltSize = GlobalVariables.rsasize / 8;
                }

                // 从输入文件读取 salt
                byte[] salt = new byte[SaltSize];
                inputFileStream.Read(salt, 0, SaltSize);
                byte[] key = new byte[KeySize];
                // 使用 PBKDF2 从密码生成密钥
                if (GlobalVariables.KDF == "Password Based")
                {
                    using var keyDerivationFunction = new Rfc2898DeriveBytes(password, salt, Iterations, (Support_Converter.StringToHashAlgorithmName(HashAlg)));
                    key = keyDerivationFunction.GetBytes(KeySize);
                }
                else
                {
                    byte[] pri_key = Aes_ChaCha_Decryptor.getPrivateKey(Convert.FromBase64String(GlobalVariables.privatekeyenced), password);
                    RSA rSA = RSA.Create();
                    rSA.KeySize = GlobalVariables.rsasize;
                    rSA.ImportSubjectPublicKeyInfo(Convert.FromBase64String(GlobalVariables.pubkey), out _);
                    rSA.ImportPkcs8PrivateKey(pri_key, out _);
                    key = rSA.Decrypt(salt, RSAEncryptionPadding.Pkcs1);
                }
                AesGcm? Dec_Obj = null; // 使用可空类型，因为 AesGcm 是一个引用类型
                ChaCha20Poly1305? Dec_obj2 = null;
                if (GlobalVariables.Encalg == "AES")
                {
                    Dec_Obj = new AesGcm(key, TagSize);
                }
                else if (GlobalVariables.Encalg == "ChaCha")
                {
                    Dec_obj2 = new ChaCha20Poly1305(key);
                }

                byte[] buffer = new byte[BlockSize + TagSize];
                byte[] nonce = new byte[NonceSize];

                // 读取 nonce
                inputFileStream.Read(nonce, 0, NonceSize);

                int bytesRead = inputFileStream.Read(buffer, 0, buffer.Length);
                int tagSize = TagSize;
                int encryptedDataSize = bytesRead - tagSize;

                // 分离出加密数据和 tag
                byte[] encryptedData = new byte[encryptedDataSize];
                byte[] tag = new byte[tagSize];
                Array.Copy(buffer, 0, tag, 0, tagSize);
                Array.Copy(buffer, tagSize, encryptedData, 0, encryptedDataSize);

                byte[] decryptedData = new byte[encryptedDataSize];

                // 解密数据块
                if (GlobalVariables.Encalg == "AES" && Dec_Obj != null)
                {
                    Dec_Obj.Decrypt(nonce, encryptedData, tag, decryptedData);
                }
                else if (GlobalVariables.Encalg == "ChaCha" && Dec_obj2 != null)
                {
                    Dec_obj2.Decrypt(nonce, encryptedData, tag, decryptedData);
                }
                // 写入解密数据到输出文件
                outputFileStream.Write(decryptedData, 0, decryptedData.Length);
            }
        }

    }
}
