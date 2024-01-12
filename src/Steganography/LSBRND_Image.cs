using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HideSloth.GlobalVariables;
using System.Security.Cryptography;

namespace HideSloth.Steganography
{
    public class LSBRND_Image : ImageAlgorithm
    {
        public double CheckSize(Image img)
        {
            return Math.Round(img.Width * img.Height / 1024 * 0.97);
        }

        public static List<Point> GenerateRandomCoordinates(int width, int height, int seed, int length, bool extractlarge)
        {
            var random = new Random(seed);
            var totalPoints = width * height;
            var points = new List<Point>();

            if (length < totalPoints / 2 && !extractlarge) // 当length较小时，使用随机生成方法
            {
                var existingPoints = new HashSet<Point>();
                while (points.Count < length)
                {
                    int x = random.Next(width);
                    int y = random.Next(height);
                    var point = new Point(x, y);

                    if (existingPoints.Add(point))
                    {
                        points.Add(point);
                    }
                }
            }
            else // 当length较大时，使用全部生成然后选取的方法
            {
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        points.Add(new Point(x, y));
                    }
                }

                // 随机打乱点的顺序
                var shuffledPoints = points.OrderBy(p => random.Next()).ToList();

                // 选取前length个点
                return shuffledPoints.Take(length).ToList();
            }

            return points;
        }
        public Bitmap Encode(Bitmap img, byte[] data, string pwd)
        {
            int seed = GetDeterministicHashCode(pwd);

            // 锁定位图的整个区域
            Rectangle rect = new Rectangle(0, 0, img.Width, img.Height);
            BitmapData bmpData = img.LockBits(rect, ImageLockMode.ReadWrite, img.PixelFormat);
            List<Point> points = GenerateRandomCoordinates(img.Width, img.Height, seed, data.Length + 44, false);

            // 获取位图数据的首地址
            IntPtr ptr = bmpData.Scan0;

            // 定义数组以保存位图的所有像素数据
            int bytes = Math.Abs(bmpData.Stride) * img.Height;
            byte[] rgbValues = new byte[bytes];

            // 将像素数据复制到数组
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

            int c = 0;
            int maxLinear = img.Width * img.Height;

            if (data.Length < maxLinear)
            {
                string binaryString = Convert.ToString(data.Length, 2).PadLeft(32, '0'); // 转换为二进制字符串，左侧填充零以达到 32 位

                char[] binaryCharArray = binaryString.ToCharArray(); // 将二进制字符串转换为字符数组


                for (int i = 0; i < binaryCharArray.Length; i++)
                {
                    Point point = points[c];
                    char letter = binaryCharArray[i];
                    int value = Convert.ToInt32(letter);
                    EncodePixelToArray(rgbValues, point, value, img.Width, bmpData.Stride);
                    c++;
                }
                // Write data
                for (int i = 0; i < data.Length; i++)
                {
                    Point point = points[c];
                    EncodePixelToArray(rgbValues, point, data[i], img.Width, bmpData.Stride);
                    c++;
                }
            }
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

            // 解锁位图
            img.UnlockBits(bmpData);

            return img;
        }
        public byte[] Decode(Bitmap img, string pwd)
        {
            int seed = GetDeterministicHashCode(pwd);
            // 锁定位图的整个区域
            Rectangle rect = new Rectangle(0, 0, img.Width, img.Height);
            BitmapData bmpData = img.LockBits(rect, ImageLockMode.ReadOnly, img.PixelFormat);
            // 获取位图数据的首地址
            IntPtr ptr = bmpData.Scan0;
            var points = new List<Point>();

            // 定义数组以保存位图的所有像素数据
            int bytes = Math.Abs(bmpData.Stride) * img.Height;
            byte[] rgbValues = new byte[bytes];

            // 将像素数据复制到数组
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);
            int maxLinear = img.Width * img.Height;
            int c = 31;
            int fileLength = 0;
            try
            {
                points = GenerateRandomCoordinates(img.Width, img.Height, seed, 44, false);
                string binaryString = DecodeLength(points, rgbValues, img, bmpData, maxLinear);
                fileLength = Convert.ToInt32(binaryString, 2); // 将二进制字符串转换为整数

            }
            catch
            {
                points = GenerateRandomCoordinates(img.Width, img.Height, seed, 44, true);
                string binaryString = DecodeLength(points, rgbValues, img, bmpData, maxLinear);
                fileLength = Convert.ToInt32(binaryString, 2); // 将二进制字符串转换为整数
            }
            byte[] fileData = new byte[fileLength];
            points = GenerateRandomCoordinates(img.Width, img.Height, seed, 44 + fileLength, false);

            // 读取并解码文件数据
            for (int i = 0; i < fileLength; i++)
            {
                Point point = points[c + 1];
                fileData[i] = (byte)DecodePixelFromArray(rgbValues, point, img.Width, bmpData.Stride);
                c++;
            }

            // 解锁位图
            img.UnlockBits(bmpData);


            return fileData;
        }
        private static void EncodePixelToArray(byte[] rgbValues, Point point, int value, int width, int stride)
        {
            int index = point.Y * stride + point.X * 3; // 每个像素3字节

            int blueValue = value & 7;
            int greenValue = value >> 3 & 7;
            int redValue = value >> 6 & 3;

            rgbValues[index] = (byte)(rgbValues[index] & 0xF8 | blueValue);
            rgbValues[index + 1] = (byte)(rgbValues[index + 1] & 0xF8 | greenValue);
            rgbValues[index + 2] = (byte)(rgbValues[index + 2] & 0xFC | redValue);
        }
        private static int DecodePixelFromArray(byte[] rgbValues, Point point, int width, int stride)
        {
            int index = point.Y * stride + point.X * 3; // 每个像素3字节

            int blue = rgbValues[index];
            int green = rgbValues[index + 1];
            int red = rgbValues[index + 2];

            int redValue = red & 3;
            int greenValue = green & 7;
            int blueValue = blue & 7;

            int value = blueValue | greenValue << 3 | redValue << 6;
            return value;
        }
        private static string DecodeLength(List<Point> points, byte[] rgbValues, Bitmap img, BitmapData bmpData, int maxLinear)
        {
            string fileLengthStr = "";
            char currentChar;
            char[] binaryCharArray = new char[32]; // 用于存储二进制表示的字符数组
            int count = 0; // 用于计数已读取的字符数
            int c = 0;
            do
            {
                Point point = points[c];
                int decodedValue = DecodePixelFromArray(rgbValues, point, img.Width, bmpData.Stride);
                currentChar = (char)decodedValue;
                binaryCharArray[count] = currentChar;
                count++;
                if (count == 32) // 检查是否已读取了32个字符
                {
                    break; // 如果已读取32个字符，跳出循环
                }

                fileLengthStr += currentChar;
                c++;
            } while (c < maxLinear); // 确保循环不超过图像像素总数
            string binaryString = new string(binaryCharArray); // 将字符数组转换为字符串
            return binaryString;
        }
        private static int GetDeterministicHashCode(string str)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(str);
            byte[] hashBytes = MD5.HashData(inputBytes);

            // 将前4个字节转换为一个 32 位整数
            return BitConverter.ToInt32(hashBytes, 0);
        }

    }
}
