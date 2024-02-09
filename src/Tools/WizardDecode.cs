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
using static HideSloth.GlobalVariables;

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

        public static bool IsToosmall(Bitmap img)
        {
            string size = "";
            var stegoAlg = AlgorithmImageFactory.CreateAlgorithm(GlobalVariables.Algor);
            size = stegoAlg.CheckSize(img).ToString();
            if (Int32.Parse(size) <= GlobalVariables.Smallstandard)
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


            foreach (string part in targetfloder)
            {
                if (IsImageFile(part))
                {
                    Bitmap unloading = new Bitmap(part);
                    if (IsToosmall(unloading))
                    {
                        try
                        {
                            token.ThrowIfCancellationRequested();
                            var stegoAlg = AlgorithmImageFactory.CreateAlgorithm(GlobalVariables.Algor);
                            byte[] decrypted_content = stegoAlg.Decode(unloading,pwd);

                            if (GlobalVariables.Enableencrypt)
                            {
                                decrypted_content = Aes_ChaCha_Decryptor.Decrypt(decrypted_content, pwd);
                            }
                            updateStatus?.Invoke("Readed and Extracted from File: " + part);


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
                                if (GlobalVariables.Sparse_decode)
                                {
                                    if (!SetSparseFile(fs.SafeFileHandle.DangerousGetHandle()))
                                    {

                                    }
                                    fs.SetLength(totallength); // 设置文件的大小
                                }
                                fs.Seek(towritepostion, SeekOrigin.Begin);

                                // 写入数据
                                fs.Write(fileContent, 0, fileContent.Length);
                            }

                        }
                        catch (Exception ex)
                        {
                            updateStatus?.Invoke(ex.Message);

                            if (GlobalVariables.Ignoreextracterror == false)
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

    }
}

