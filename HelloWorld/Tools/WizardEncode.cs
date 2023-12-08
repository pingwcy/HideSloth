using HideSloth.Crypto;
using HideSloth.Steganography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace HideSloth.Tools
{
    public class WizardEncode
    {


        public static bool StegoLarge(string pwd, List<int> bufflist, string containers_route, string route_secret, string output_route, List<string> container_list, Action<string> updateStatus, CancellationToken token)
        {
            int cycle = 0;
            using (FileStream fs = new FileStream(route_secret, FileMode.Open, FileAccess.Read))
            using (BinaryReader reader = new BinaryReader(fs))
            {
                byte[] buffer;

                while (true)
                {

                    // 读取指定数量的字节
                    try
                    {
                        token.ThrowIfCancellationRequested();

                    }
                    catch
                    {

                        return false;
                    }
                    buffer = reader.ReadBytes(Convert.ToInt32(bufflist[cycle]));

                    if (buffer.Length == 0)
                    {
                        // 如果没有更多数据，跳出循环
                        break;
                    }
                    if (IsImageFile(Path.Combine(containers_route, container_list[cycle])))
                       {

                        try
                        {
                            //cycle number
                            byte[] intBytes0 = BitConverter.GetBytes(cycle);
                            //if (BitConverter.IsLittleEndian)
                                //Array.Reverse(intBytes0); 确保字节顺序正确
                            byte[] intBytes = intBytes0.Concat(GlobalVariables.separator).ToArray();


                            byte[] stringBytes0 = Encoding.UTF8.GetBytes(Path.GetFileName(route_secret));
                            byte[] stringBytes = stringBytes0.Concat(GlobalVariables.separator).ToArray();


                            byte[] fullbuffer = new byte[intBytes.Length  +stringBytes.Length  + buffer.Length];


                            Buffer.BlockCopy(intBytes, 0, fullbuffer, 0, intBytes.Length);
                            Buffer.BlockCopy(stringBytes, 0, fullbuffer, intBytes.Length, stringBytes.Length);
                            Buffer.BlockCopy(buffer, 0, fullbuffer, intBytes.Length + stringBytes.Length, buffer.Length);



                            byte[] encryptedData = AesGcmEncryptor.Encrypt(fullbuffer, pwd, out byte[] salt, out byte[] nonce, out byte[] tag);
                            Bitmap loaded = (Bitmap)Support_Converter.ConvertOthersToPngInMemory(Path.Combine(containers_route, container_list[cycle]));
                            if (GlobalVariables.Algor == "LSB")
                            {
                                Bitmap result = LSB_Image.embed(Convert.ToBase64String(BytesStringThings.CombineBytes(salt, nonce, tag, encryptedData)), loaded);

                                result.Save(Path.Combine(output_route, container_list[cycle]), System.Drawing.Imaging.ImageFormat.Png);
                                loaded.Dispose();

                                result.Dispose();
                            }
                            else if (GlobalVariables.Algor == "Linear")
                            {
                                Bitmap result = Core_Linear_Image.EncodeFileLinear(loaded, BytesStringThings.CombineBytes(salt, nonce, tag, encryptedData));

                                result.Save(Path.Combine(output_route, container_list[cycle]), System.Drawing.Imaging.ImageFormat.Png);
                                loaded.Dispose();

                                result.Dispose();
                                updateStatus?.Invoke($"Saved: {Path.Combine(output_route, container_list[cycle])}"+",Number: "+(cycle+1).ToString()+".");
                            }
                            
                        }
                        catch (Exception ex)
                        {
                            return false;
                        }
                        
                        //

                        // 如果读取的数据小于缓冲区大小，表示已到达文件末尾
                        if (buffer.Length < bufflist[cycle])
                        {
                            break;
                        }
                        cycle++;

                    }
                }

                return true;
            }
        }

        public static bool Encryptor(string pwd, string routeofsecrets, string routeofoutput, Action<string> updateStatus)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(routeofsecrets);
            FileInfo[] files = directoryInfo.GetFiles();

            foreach (string file in Directory.GetFiles(routeofsecrets))
            {
                FileAES.EncryptFile(file, Path.Combine(routeofoutput, Path.GetFileName(file)), pwd);
                updateStatus?.Invoke($"Encrypted: {file}");
            }
            updateStatus?.Invoke("All files encrypted and saved!");
            
            return true;
        }

        public static List<ImageInfo> CheckCapacity(string folderPath)
        {
            List<ImageInfo> list = new List<ImageInfo>();
            string[] fileEntries = Directory.GetFiles(folderPath).OrderBy(path => Path.GetFileName(path)).ToArray();
            foreach (string fileName in fileEntries)
            {
                if (IsImageFile(fileName))
                {
                    using (Image img = Image.FromFile(fileName))
                    {
                        string dimensions = "";
                        if (GlobalVariables.Algor == "Linear")
                        {
                            dimensions = Math.Round(img.Width * img.Height / 1024 * 0.97).ToString()+" KB";

                        }
                        if (GlobalVariables.Algor == "LSB")
                        {
                            //dimensions = Math.Round(img.Width * img.Height / 1024 * 0.97).ToString() + " KB";
                            MessageBox.Show("No code!!!!");

                        }

                        list.Add(new ImageInfo
                        {
                            FileName = Path.GetFileName(fileName),
                            Dimensions = dimensions
                        });
                    }
                }
            }
            return list;
        }
        public static bool IsImageFile(string fileName)
        {
            string extension = Path.GetExtension(fileName).ToLower();
            return extension == ".jpg" || extension == ".png" || extension == ".bmp" || extension == ".jpeg" || extension == ".tiff" || extension == ".gif";
        }
        public class ImageInfo
        {
            public string? FileName { get; set; }
            public string? Dimensions { get; set; }
        }

    }
}
