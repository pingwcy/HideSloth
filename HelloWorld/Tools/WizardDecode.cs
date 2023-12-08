using HideSloth.Crypto;
using HideSloth.Steganography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HideSloth.Tools
{
    public class WizardDecode
    {

        public static bool Decryptor(string pwd, string routeofencrypted, string routeofoutput, Action<string> updateStatus, CancellationToken token)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(routeofencrypted);
            FileInfo[] files = directoryInfo.GetFiles();

            foreach (string file in Directory.GetFiles(routeofencrypted))
            {
                try
                {
                    token.ThrowIfCancellationRequested();

                }
                catch
                {

                    return false;
                }

                FileAES.DecryptFile(file, Path.Combine(routeofoutput, Path.GetFileName(file)), pwd);
                updateStatus?.Invoke($"Decrypted: {file}");
            }
            updateStatus?.Invoke("All files decrypted and saved!");

            return true;
        }



        public static bool IsImageFile(string fileName)
        {
            string extension = Path.GetExtension(fileName).ToLower();
            return extension == ".jpg" || extension == ".png" || extension == ".bmp" || extension == ".jpeg" || extension == ".tiff" || extension == ".gif";
        }
        private static string ReadStringUntilDelimiter(byte[] data, int start, byte delimiter)
        {
            int end = Array.IndexOf(data, delimiter, start);
            if (end == -1) throw new InvalidOperationException("未找到分隔符。");

            return Encoding.UTF8.GetString(data, start, end - start);
        }
        public static string ReadStringUntilDelimiter2(BinaryReader reader, byte delimiter)
        {
            List<byte> byteList = new List<byte>();
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                byte b = reader.ReadByte();
                if (b == delimiter)
                    break;
                byteList.Add(b);
            }
            return Encoding.UTF8.GetString(byteList.ToArray());
        }

        public static void ReadUntilDelimiter(BinaryReader reader, byte delimiter)
        {
            while (reader.BaseStream.Position != reader.BaseStream.Length)
            {
                if (reader.ReadByte() == delimiter)
                    break;
            }
        }

        public static bool StegoExtractLarge(string pwd, string route_loaded, string outputname, Action<string> updateStatus, CancellationToken token)
        {
            //string targetFilePath = Path.Combine(destinationDirectory, originalFileName);
            if (GlobalVariables.rerange_decode)
            {
                Dictionary<int, string> fileParts = new Dictionary<int, string>();


                foreach (string file in Directory.GetFiles(route_loaded).OrderBy(path => Path.GetFileName(path)).ToArray())
                {
                    if (IsImageFile(file))
                    {
                        try
                        {
                            token.ThrowIfCancellationRequested();

                        }
                        catch
                        {

                            return false;
                        }

                        Bitmap unloading = new Bitmap(file);
                        try
                        {
                            string encrypted_result = "";
                            byte[] decrypted_content = new byte[0];



                            if (GlobalVariables.Algor == "LSB")
                            {
                                encrypted_result = LSB_Image.extract(unloading);
                                decrypted_content = AesGcmDecryptor.Decrypt(Convert.FromBase64String(encrypted_result), pwd);

                            }
                            else if (GlobalVariables.Algor == "Linear")
                            {

                                byte[] filecontent = Core_Linear_Image.DecodeFileFromImage(unloading);
                                decrypted_content = AesGcmDecryptor.Decrypt(filecontent, pwd);
                                updateStatus?.Invoke("Readed the sequence information of  File: " + file);
                                int sequenceNumber = BitConverter.ToInt32(decrypted_content, 0);

                                // 查找第一个分隔符的位置
                                int delimiterIndex = Array.IndexOf(decrypted_content, (byte)'|', sizeof(int));
                                if (delimiterIndex == -1) throw new InvalidOperationException("未找到分隔符。");

                                // 读取文件名
                                string originalFileName = ReadStringUntilDelimiter(decrypted_content, delimiterIndex + 1, (byte)'|');

                                fileParts.Add(sequenceNumber, file);

                            }
                        }



                        catch (Exception ex)
                        {
                            updateStatus?.Invoke(ex.Message);

                            return false;
                        }

                    }
                }


                var orderedParts = fileParts.OrderBy(kvp => kvp.Key);
                updateStatus?.Invoke("Successfully reranged the files!");


                foreach (var part in orderedParts)
                {
                    if (IsImageFile(part.Value))
                    {
                        Bitmap unloading = new Bitmap(part.Value);
                        try
                        {
                            string encrypted_result = "";
                            byte[] decrypted_content = new byte[0];
                            if (GlobalVariables.Algor == "LSB")
                            {
                                encrypted_result = LSB_Image.extract(unloading);
                                decrypted_content = AesGcmDecryptor.Decrypt(Convert.FromBase64String(encrypted_result), pwd);

                            }
                            else if (GlobalVariables.Algor == "Linear")
                            {
                                byte[] filecontent = Core_Linear_Image.DecodeFileFromImage(unloading);
                                decrypted_content = AesGcmDecryptor.Decrypt(filecontent, pwd);
                                updateStatus?.Invoke("Readed and Extracted from File: " + part.Value);
                            }
                            using (FileStream fileStream = new FileStream(outputname, FileMode.Append, FileAccess.Write))
                            {


                                int separatorCount = 0;
                                int index = 0;

                                // 遍历byte数组，计数"|"字符
                                for (index = 4; index < decrypted_content.Length; index++)
                                {
                                    if (decrypted_content[index] == (byte)'|')
                                    {
                                        separatorCount++;
                                        if (separatorCount == 2)
                                        {
                                            break; // 找到第二个"|"，停止遍历
                                        }
                                    }
                                }


                                // 计算文件内容的起始位置和长度
                                int contentStart = index + 1;
                                int contentLength = decrypted_content.Length - contentStart;

                                // 创建一个新的byte数组来保存文件内容
                                byte[] fileContent = new byte[contentLength];

                                // 使用Buffer.BlockCopy复制内容
                                Buffer.BlockCopy(decrypted_content, contentStart, fileContent, 0, contentLength);


                                fileStream.Write(fileContent, 0, fileContent.Length);
                            }
                        }
                        catch (Exception ex)
                        {
                            updateStatus?.Invoke(ex.Message);

                            return false;
                        }
                        finally
                        {
                            unloading.Dispose();
                        }

                    }
                }

            }

            else
            {
                foreach (var part in Directory.GetFiles(route_loaded).OrderBy(path => Path.GetFileName(path)).ToArray())
                {
                    if (IsImageFile(part))
                    {
                        Bitmap unloading = new Bitmap(part);
                        try
                        {
                            string encrypted_result = "";
                            byte[] decrypted_content = new byte[0];
                            if (GlobalVariables.Algor == "LSB")
                            {
                                encrypted_result = LSB_Image.extract(unloading);
                                decrypted_content = AesGcmDecryptor.Decrypt(Convert.FromBase64String(encrypted_result), pwd);

                            }
                            else if (GlobalVariables.Algor == "Linear")
                            {
                                byte[] filecontent = Core_Linear_Image.DecodeFileFromImage(unloading);
                                decrypted_content = AesGcmDecryptor.Decrypt(filecontent, pwd);
                                updateStatus?.Invoke("Readed and Extracted from File: " + part);
                            }
                            using (FileStream fileStream = new FileStream(outputname, FileMode.Append, FileAccess.Write))
                            {


                                int separatorCount = 0;
                                int index = 0;

                                // 遍历byte数组，计数"|"字符
                                for (index = 4; index < decrypted_content.Length; index++)
                                {
                                    if (decrypted_content[index] == (byte)'|')
                                    {
                                        separatorCount++;
                                        if (separatorCount == 2)
                                        {
                                            break; // 找到第二个"|"，停止遍历
                                        }
                                    }
                                }


                                // 计算文件内容的起始位置和长度
                                int contentStart = index + 1;
                                int contentLength = decrypted_content.Length - contentStart;

                                // 创建一个新的byte数组来保存文件内容
                                byte[] fileContent = new byte[contentLength];

                                // 使用Buffer.BlockCopy复制内容
                                Buffer.BlockCopy(decrypted_content, contentStart, fileContent, 0, contentLength);


                                fileStream.Write(fileContent, 0, fileContent.Length);
                            }
                        }
                        catch (Exception ex)
                        {
                            updateStatus?.Invoke(ex.Message);

                            return false;
                        }
                        finally
                        {
                            unloading.Dispose();
                        }

                    }
                }


            }





            updateStatus?.Invoke("All extractions are down successully!");

            return true;
        }
    }
}
