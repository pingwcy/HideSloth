using HideSloth.Crypto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HideSloth
{
    public class MainCLI
    {
        public static void RunCli(string[] args)
        {
            var parameters = new Dictionary<string, string>();

            for (int i = 0; i < args.Length; i++)
            {
                // 确保参数以 "-" 开头
                if (args[i].ToLower().StartsWith("-h") || args[i].ToLower().StartsWith("-help"))
                {
                    Console.WriteLine("Help Information of HideSloth");
                    Console.ReadKey();
                    return;
                }
                if (args[i].StartsWith("-"))
                {
                    string key = args[i];
                    string value = null;

                    // 检查是否存在值，并且值不是以 "-" 开头的另一个参数
                    if (i + 1 < args.Length && !args[i + 1].StartsWith("-"))
                    {
                        value = args[i + 1];
                        i++; // 跳过值
                    }

                    parameters[key] = value;
                }
            }
            parameters.TryGetValue("-p", out string? pwd);
            pwd ??= "";

            if (parameters.ContainsKey("-en") && ((parameters.ContainsKey("-sf")! && parameters.ContainsKey("-o")) || parameters.ContainsKey("-sm")))
            {
                if (parameters.ContainsKey("-c") && parameters.ContainsKey("-sm"))
                {

                }

                else if (parameters.ContainsKey("-c") && parameters.ContainsKey("-sf"))
                {

                }


                else if (parameters.ContainsKey("-sm"))
                {
                    string? content = parameters["-sm"];
                    content ??= "";
                    byte[] plain_bin = Convert.FromBase64String(BytesStringThings.StringtoBase64(content));
                    byte[] encryptedData = Aes_ChaCha_Encryptor.Encrypt(plain_bin, pwd, out byte[] salt, out byte[] nonce, out byte[] tag);
                    string outstring = (Convert.ToBase64String(BytesStringThings.CombineBytes(salt, nonce, tag, encryptedData)));
                    Console.WriteLine(outstring);
                }
                else if (parameters["-sf"]!=null && parameters["-o"]!=null)
                {
                    Console.WriteLine("Encrypting...");
                    FileEnc.EncryptFile(parameters["-sf"], parameters["-o"], pwd);
                    Console.WriteLine("Encrypting Done!");

                }

            }
            else if (parameters.ContainsKey("-de") && ((parameters.ContainsKey("-sf")! && parameters.ContainsKey("-o")) || parameters.ContainsKey("-sm")))
            {
                if (parameters.ContainsKey("-c") && parameters.ContainsKey("-sm"))
                {

                }

                else if (parameters.ContainsKey("-c") && parameters.ContainsKey("-sf"))
                {

                }

                else if (parameters.ContainsKey("-sm"))
                {
                    string? content = parameters["-sm"];
                    content ??= "";
                    string outstring = Encoding.UTF8.GetString(Aes_ChaCha_Decryptor.Decrypt(Convert.FromBase64String(content), pwd));
                    Console.WriteLine(outstring);
                }
                else if (parameters["-sf"] != null && parameters["-o"] != null)
                {
                    Console.WriteLine("Decrypting...");
                    FileEnc.DecryptFile(parameters["-sf"], parameters["-o"], pwd);
                    Console.WriteLine("Decrypting Done!");

                }
            }

            // 输出处理结果
            foreach (var param in parameters)
            {
                Console.WriteLine($"Parameter: {param.Key}, Value: {param.Value ?? "null"}");
            }
            Console.ReadLine();

            // 处理 args 中的命令行参数

        }

    }
}
