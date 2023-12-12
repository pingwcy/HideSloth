using HideSloth.Crypto;
using HideSloth.Steganography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Collections;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HideSloth.Tools
{

    public class WizardDecode
    {
        public static List<string> ALLfilePath = new List<string>();

        [DllImport("kernel32.dll")]
        private static extern bool DeviceIoControl(IntPtr hDevice, uint dwIoControlCode,
        IntPtr lpInBuffer, uint nInBufferSize,
        IntPtr lpOutBuffer, uint nOutBufferSize,
        out uint lpBytesReturned, IntPtr lpOverlapped);
        private const uint FSCTL_SET_SPARSE = 0x000900c4;

        public static void GetFilePath(string directory, List<string> filePaths, int depth)
        {
            if (depth == 0)
            {
                return;
            }

            try
            {
                // 获取当前目录下的所有文件
                string[] files = Directory.GetFiles(directory);
                filePaths.AddRange(files);

                // 获取所有子目录
                string[] subDirs = Directory.GetDirectories(directory);
                foreach (string dir in subDirs)
                {
                    // 递归调用
                    GetFilePath(dir, filePaths, depth - 1);
                }
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine("没有权限访问目录: " + directory);
                Console.WriteLine(e.Message);
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine("目录不存在: " + directory);
                Console.WriteLine(e.Message);
            }
        }


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

                FileEnc.DecryptFile(file, Path.Combine(routeofoutput, Path.GetFileName(file)), pwd);
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
        
        public static bool IsToosmall (Bitmap img)
        {
            string size = "";
            if (GlobalVariables.Algor == "Linear")
            {
                size = Math.Round(img.Width * img.Height / 1024 * 0.97).ToString();

            }
            if (GlobalVariables.Algor == "LSB")
            {
                size = Math.Round(img.Width * img.Height * 3 / 8 * 0.89 / 1024 / 1.34).ToString();
            }
            if (Int32.Parse(size) <= GlobalVariables.smallstandard)
            {
                return false;
            }
            return true;
        }

        public static bool StegoExtractLarge(string pwd, string route_loaded, string outputname, int searchdepth, Action<string> updateStatus, CancellationToken token)
        {
            //string targetFilePath = Path.Combine(destinationDirectory, originalFileName);
            ALLfilePath.Clear();
            List<string> targetfloder = [];
            if (Form_DecodeWizard.issub)
            {
                GetFilePath(route_loaded, ALLfilePath, searchdepth);
                targetfloder = ALLfilePath;
            }
            else
            {
                targetfloder = Directory.GetFiles(route_loaded).OrderBy(path => Path.GetFileName(path)).ToArray().ToList();
            }

            if (GlobalVariables.sparse_decode)
            {



                using (FileStream fs = File.Create(outputname))
                {
                    if (!SetSparseFile(fs.SafeFileHandle.DangerousGetHandle()))
                    {

                    }


                    //ALLfilePath.Clear();

                    foreach (string part in targetfloder)
                    {
                        if (IsImageFile(part))
                        {
                            Bitmap unloading = new Bitmap(part);
                            if (IsToosmall(unloading))
                            {
                                try
                                {
                                    string encrypted_result = "";
                                    byte[] decrypted_content = new byte[0];
                                    if (GlobalVariables.Algor == "LSB")
                                    {
                                        encrypted_result = LSB_Image.extract(unloading);
                                        decrypted_content = Aes_ChaCha_Decryptor.Decrypt(Convert.FromBase64String(encrypted_result), pwd);
                                        updateStatus?.Invoke("Readed and Extracted from File: " + part);

                                    }
                                    else if (GlobalVariables.Algor == "Linear")
                                    {
                                        byte[] filecontent = Core_Linear_Image.DecodeFileFromImage(unloading);
                                        decrypted_content = Aes_ChaCha_Decryptor.Decrypt(filecontent, pwd);
                                        updateStatus?.Invoke("Readed and Extracted from File: " + part);
                                    }

                                    long towritepostion = BitConverter.ToInt64(decrypted_content, 0);
                                    long totallength = BitConverter.ToInt64(decrypted_content, 8);
                                    fs.SetLength(totallength); // 设置文件的大小


                                    // 计算文件内容的起始位置和长度
                                    int contentStart = 16;
                                    int contentLength = decrypted_content.Length - contentStart;

                                    // 创建一个新的byte数组来保存文件内容
                                    byte[] fileContent = new byte[contentLength];

                                    // 使用Buffer.BlockCopy复制内容
                                    Buffer.BlockCopy(decrypted_content, contentStart, fileContent, 0, contentLength);
                                    fs.Seek(towritepostion, SeekOrigin.Begin);

                                    // 写入数据
                                    fs.Write(fileContent, 0, fileContent.Length);

                                }


                                catch (Exception ex)
                                {
                                    updateStatus?.Invoke(ex.Message);

                                    if (GlobalVariables.ignoreextracterror == false)
                                    {
                                        return false;
                                    }
                                }
                                finally
                                {
                                    unloading.Dispose();
                                }
                            }
                        }
                    }
                }
            }


            else if(GlobalVariables.sparse_decode == false)
            {
                bool isfirst = true;

                foreach (string part in targetfloder)
                {
                    if (IsImageFile(part))
                    {
                        Bitmap unloading = new Bitmap(part);
                        if (IsToosmall(unloading))
                        {
                            try
                            {
                                string encrypted_result = "";
                                byte[] decrypted_content = new byte[0];
                                if (GlobalVariables.Algor == "LSB")
                                {
                                    encrypted_result = LSB_Image.extract(unloading);
                                    decrypted_content = Aes_ChaCha_Decryptor.Decrypt(Convert.FromBase64String(encrypted_result), pwd);
                                    updateStatus?.Invoke("Readed and Extracted from File: " + part);

                                }
                                else if (GlobalVariables.Algor == "Linear")
                                {
                                    byte[] filecontent = Core_Linear_Image.DecodeFileFromImage(unloading);
                                    decrypted_content = Aes_ChaCha_Decryptor.Decrypt(filecontent, pwd);
                                    updateStatus?.Invoke("Readed and Extracted from File: " + part);
                                }

                                long towritepostion = BitConverter.ToInt64(decrypted_content, 0);
                                long totallength = BitConverter.ToInt64(decrypted_content, 8);


                                // 计算文件内容的起始位置和长度
                                int contentStart = 16;
                                int contentLength = decrypted_content.Length - contentStart;

                                // 创建一个新的byte数组来保存文件内容
                                byte[] fileContent = new byte[contentLength];

                                // 使用Buffer.BlockCopy复制内容
                                Buffer.BlockCopy(decrypted_content, contentStart, fileContent, 0, contentLength);


                                using (FileStream fs = new FileStream(outputname, FileMode.OpenOrCreate, FileAccess.Write))
                                {
                                    fs.Seek(towritepostion, SeekOrigin.Begin);

                                    // 写入数据
                                    fs.Write(fileContent, 0, fileContent.Length);
                                }



                            }
                            catch (Exception ex)
                            {
                                updateStatus?.Invoke(ex.Message);

                                if (GlobalVariables.ignoreextracterror == false)
                                {
                                    return false;
                                }
                            }
                            finally
                            {
                                unloading.Dispose();
                            }
                        }
                    }
                }
            }
            GC.Collect(2, GCCollectionMode.Forced);
            GC.WaitForPendingFinalizers();


            updateStatus?.Invoke("All extractions are down successully!");

            return true;
        }
        private static bool SetSparseFile(IntPtr fileHandle)
        {
            uint bytesReturned = 0;
            return DeviceIoControl(fileHandle, FSCTL_SET_SPARSE, IntPtr.Zero, 0, IntPtr.Zero, 0, out bytesReturned, IntPtr.Zero);
        }

        private static bool WriteZero(string filePath,long fileSize)
        {
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    // 创建一个字节缓冲区（这里为1KB）
                    byte[] buffer = new byte[10485760];

                    // 计算需要写多少次
                    long numWrites = fileSize / buffer.Length;

                    // 写入0值字节
                    for (long i = 0; i < numWrites; i++)
                    {
                        fs.Write(buffer, 0, buffer.Length);
                    }

                    // 处理最后一部分（如果有）
                    int remaining = (int)(fileSize % buffer.Length);
                    if (remaining > 0)
                    {
                        fs.Write(buffer, 0, remaining);
                    }
                    return true;
                }
            }
            catch
            {
                return false;
            }

        }

    }
}

/*
foreach (string file in targetfloder)
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
        if (IsToosmall(unloading))
        {
            try
            {
                string encrypted_result = "";
                byte[] decrypted_content = new byte[0];



                if (GlobalVariables.Algor == "LSB")
                {
                    encrypted_result = LSB_Image.extract(unloading);
                    decrypted_content = Aes_ChaCha_Decryptor.Decrypt(Convert.FromBase64String(encrypted_result), pwd);
                    updateStatus?.Invoke("Readed the sequence information of  File: " + file);
                    int sequenceNumber = BitConverter.ToInt32(decrypted_content, 0);

                    // 查找第一个分隔符的位置
                    int delimiterIndex = Array.IndexOf(decrypted_content, (byte)'|', sizeof(int));
                    if (delimiterIndex == -1) throw new InvalidOperationException("未找到分隔符。");

                    // 读取文件名
                    string originalFileName = ReadStringUntilDelimiter(decrypted_content, delimiterIndex + 1, (byte)'|');

                    fileParts.Add(sequenceNumber, file);

                }
                else if (GlobalVariables.Algor == "Linear")
                {

                    byte[] filecontent = Core_Linear_Image.DecodeFileFromImage(unloading);
                    decrypted_content = Aes_ChaCha_Decryptor.Decrypt(filecontent, pwd);
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
                //throw;
                if (GlobalVariables.ignoreextracterror == false)
                {
                    return false;
                }
            }
        }
        unloading.Dispose();
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
                decrypted_content = Aes_ChaCha_Decryptor.Decrypt(Convert.FromBase64String(encrypted_result), pwd);
                updateStatus?.Invoke("Readed and Extracted from File: " + part.Value);

            }
            else if (GlobalVariables.Algor == "Linear")
            {
                byte[] filecontent = Core_Linear_Image.DecodeFileFromImage(unloading);
                decrypted_content = Aes_ChaCha_Decryptor.Decrypt(filecontent, pwd);
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

            if (GlobalVariables.ignoreextracterror == false)
            {
                return false;
            }
        }
        finally
        {
            unloading.Dispose();
        }

    }
}

}
*/







