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

namespace HideSloth
{
    public class FileNamer
    {
        public static string RenameFileToload(string originalPath)
        {
            // 确保源文件存在
            if (File.Exists(originalPath))
            {
#pragma warning disable CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。
                string directory = Path.GetDirectoryName(originalPath);
#pragma warning restore CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。

                // 获取不带扩展名的文件名
                string filenameWithoutExtension = Path.GetFileNameWithoutExtension(originalPath);

                // 添加新的文件名部分
                string newFilename = filenameWithoutExtension + "_loaded";

                // 获取扩展名
                string extension = Path.GetExtension(originalPath);

                // 组合新路径
#pragma warning disable CS8604 // 引用类型参数可能为 null。
                string newPath = Path.Combine(directory, newFilename + extension);
#pragma warning restore CS8604 // 引用类型参数可能为 null。

                return newPath;
            }
            else
                return originalPath;
        }
        public static string Getpath (string Filepath)
        {
#pragma warning disable CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。
            string directory = Path.GetDirectoryName(Filepath);
#pragma warning restore CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。
#pragma warning disable CS8603 // 可能返回 null 引用。
            return directory;
#pragma warning restore CS8603 // 可能返回 null 引用。
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
            int separatorLength = GlobalVariables.separator.Length;
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
