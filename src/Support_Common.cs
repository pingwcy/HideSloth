using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection.Metadata;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Security.Cryptography.Xml;
using System.Security.Policy;
using System.Drawing.Imaging;
using System.Security.Cryptography;

namespace HideSloth
{
    public class Support_Checker
    {
        public static bool IsSuitableForBeingContainer(string Candidate_Path)
        {
            return false;
        }
    }
    class Support_Converter
    {
        public static Bitmap ConvertOthersToPngInMemory(string jpgFilePath)
        {
            using (Image jpgImage = Image.FromFile(jpgFilePath))
            {
                // 创建一个与原始图像尺寸相同的空白Bitmap对象
                Bitmap pngBitmap = new Bitmap(jpgImage.Width, jpgImage.Height);

                // 使用Graphics对象将JPG图像绘制到Bitmap中
                using (Graphics g = Graphics.FromImage(pngBitmap))
                {
                    g.DrawImage(jpgImage, new Rectangle(0, 0, jpgImage.Width, jpgImage.Height));
                }

                // 此时pngBitmap是一个包含原始JPG图像的PNG格式的Bitmap
                return pngBitmap;
            }
        }
        public static HashAlgorithmName StringToHashAlgorithmName(string hashAlgorithm)
        {
            switch (hashAlgorithm.ToUpper())
            {
                case "SHA256":
                    return HashAlgorithmName.SHA256;
                case "SHA384":
                    return HashAlgorithmName.SHA384;
                case "SHA512":
                    return HashAlgorithmName.SHA512;
                default:
                    throw new ArgumentException("Invalid Input");
            }
        }
        public static ImageFormat SaveFormatImage(string formatstring)
        {
            ImageFormat format;

            switch (formatstring)
            {
                case ".bmp":
                    format = ImageFormat.Bmp;
                    return format;
                case ".png":
                    format = ImageFormat.Png;
                    return format;
                default:
                    throw new InvalidOperationException("Unsupported format");
            }

        }
    }



    public class BytesStringThings
    {
        public static string ReadFileToStringwithName (string FilePath)
        {
            byte[] fileBytes = File.ReadAllBytes(FilePath);
            byte[] fileNameBytes = Encoding.UTF8.GetBytes(Path.GetFileName(FilePath));
            byte[] separator = Encoding.UTF8.GetBytes("|"); // 选择一个不太可能出现在文件名或内容中的字符

            int combinedLength = fileNameBytes.Length + separator.Length + fileBytes.Length;

            byte[] combined = new byte[combinedLength];

            Buffer.BlockCopy(fileNameBytes, 0, combined, 0, fileNameBytes.Length);
            Buffer.BlockCopy(separator, 0, combined, fileNameBytes.Length, separator.Length);
            Buffer.BlockCopy(fileBytes, 0, combined, fileNameBytes.Length + separator.Length, fileBytes.Length);

            string base64String = Convert.ToBase64String(combined);
            return base64String;
        }

        public static byte[] ReadFileToByteswithName (string FilePath)//临时调整
        {
            byte[] fileBytes = File.ReadAllBytes(FilePath);
            byte[] fileNameBytes = Encoding.UTF8.GetBytes(Path.GetFileName(FilePath));
            byte[] separator = Encoding.UTF8.GetBytes("|"); // 选择一个不太可能出现在文件名或内容中的字符

            int combinedLength = fileNameBytes.Length + separator.Length + fileBytes.Length;

            byte[] combined = new byte[combinedLength];

            Buffer.BlockCopy(fileNameBytes, 0, combined, 0, fileNameBytes.Length);
            Buffer.BlockCopy(separator, 0, combined, fileNameBytes.Length, separator.Length);
            Buffer.BlockCopy(fileBytes, 0, combined, fileNameBytes.Length + separator.Length, fileBytes.Length);

            return combined;
        }

        public static string ExtractFileName(byte[] combined, int separatorIndex)
        {
            byte[] fileNameBytes = new byte[separatorIndex];
            Buffer.BlockCopy(combined, 0, fileNameBytes, 0, separatorIndex);
            return Encoding.UTF8.GetString(fileNameBytes);
        }
        public static byte[] ExtractFileContent(byte[] combined, int separatorIndex)
        {
            int separatorLength = GlobalVariables.Separator.Length;
            int fileContentStartIndex = separatorIndex + separatorLength;
            int fileContentLength = combined.Length - fileContentStartIndex;
            byte[] fileContent = new byte[fileContentLength];
            Buffer.BlockCopy(combined, fileContentStartIndex, fileContent, 0, fileContentLength);
            return fileContent;
        }


        public static int FindSeparatorIndex(byte[] source, byte[] separatorByte)
        {
            // 实现一个高效的分隔符查找算法
            // 例如，你可以使用Array.IndexOf，但对于大文件可能需要更高效的算法
            for (int i = 0; i <= source.Length - separatorByte.Length; i++)
            {
                bool found = true;
                for (int j = 0; j < separatorByte.Length; j++)
                {
                    if (source[i + j] != separatorByte[j])
                    {
                        found = false;
                        break;
                    }
                }
                if (found)
                {
                    return i;
                }
            }
            return -1; // 分隔符未找到
        }


        public static void StringWritetoFile(string FilePath, string content)
        {
            try
            {
                // 将Base64字符串解码为二进制数据
                byte[] bytes = Convert.FromBase64String(content);
                File.WriteAllBytes(FilePath, bytes);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error");
                // 在这里处理异常，例如无效的Base64字符串
                // 这里返回null或抛出自定义异常都是可行的
                
            }

        }
        public static void BytesWritetoFile(string FilePath, byte[] content)
        {
            try
            {
                // 将Base64字符串解码为二进制数据
                File.WriteAllBytes(FilePath, content);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                // 在这里处理异常，例如无效的Base64字符串
                // 这里返回null或抛出自定义异常都是可行的

            }
        }

        public static byte[] CombineBytes(byte[] salt, byte[] nonce, byte[] tag, byte[] encryptedData)
        {
            int totalSize = salt.Length + nonce.Length + tag.Length + encryptedData.Length;

            // 创建一个新的字节数组来存储所有数据
            byte[] combinedData = new byte[totalSize];
            // 使用Buffer.BlockCopy合并数组
            int offset = 0;
            Buffer.BlockCopy(salt, 0, combinedData, offset, salt.Length);
            offset += salt.Length;
            Buffer.BlockCopy(nonce, 0, combinedData, offset, nonce.Length);
            offset += nonce.Length;
            Buffer.BlockCopy(tag, 0, combinedData, offset, tag.Length);
            offset += tag.Length;

            Buffer.BlockCopy(encryptedData, 0, combinedData, offset, encryptedData.Length);
            return combinedData;
        }


        public static string StringtoBase64 (string target)
        {
            byte[] binaryData = Encoding.UTF8.GetBytes(target);
            return Convert.ToBase64String(binaryData);
        }

        public static string Base64toString (string target)
        {
            byte[] bindata = Convert.FromBase64String(target);
            return Encoding.UTF8.GetString(bindata);
        }
    }

}
