using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HideSloth.GlobalVariables;

namespace HideSloth.Steganography
{
    public class Linear_Image : ImageAlgorithm
    {
        public static int DecodePixel(Color pixel)
        {
            int red = pixel.R & 3;
            int green = pixel.G & 7;
            int blue = pixel.B & 7;
            int value = blue | green << 3 | red << 6;
            return value;
        }

        private static Point LinearIndexToPoint(int index, int width, int height)
        {
            if (index < 0)
            {
                index *= -1;
            }
            return new Point(index % width, (int)Math.Floor((double)(index / width)));
        }
        public static Color EncodePixel(Color pixel, int value)
        {
            int blueValue = value & 7;
            int greenValue = value >> 3 & 7;
            int redValue = value >> 6 & 3;

            int red = pixel.R & 0xFC | redValue;
            int green = pixel.G & 0xF8 | greenValue;
            int blue = pixel.B & 0xF8 | blueValue;

            return Color.FromArgb(red, green, blue);
        }

        public byte[] Decode(Bitmap img)
        {
            // 锁定位图的整个区域
            Rectangle rect = new Rectangle(0, 0, img.Width, img.Height);
            BitmapData bmpData = img.LockBits(rect, ImageLockMode.ReadOnly, img.PixelFormat);

            // 获取位图数据的首地址
            IntPtr ptr = bmpData.Scan0;

            // 定义数组以保存位图的所有像素数据
            int bytes = Math.Abs(bmpData.Stride) * img.Height;
            byte[] rgbValues = new byte[bytes];

            // 将像素数据复制到数组
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

            int maxLinear = img.Width * img.Height;
            int c = 0;
            string fileLengthStr = "";
            char currentChar;

            char[] binaryCharArray = new char[32]; // 用于存储二进制表示的字符数组
            int count = 0; // 用于计数已读取的字符数



            do
            {
                Point point = LinearIndexToPoint(c, img.Width, img.Height);
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
            int fileLength = Convert.ToInt32(binaryString, 2); // 将二进制字符串转换为整数

            byte[] fileData = new byte[fileLength];

            // 读取并解码文件数据
            for (int i = 0; i < fileLength; i++)
            {
                Point point = LinearIndexToPoint(c+1, img.Width, img.Height);
                fileData[i] = (byte)DecodePixelFromArray(rgbValues, point, img.Width, bmpData.Stride);
                c++;
            }

            // 解锁位图
            img.UnlockBits(bmpData);

            return fileData;
        }

        private static int DecodePixelFromArray(byte[] rgbValues, Point point, int width, int stride)
        {
            int index = point.Y * stride + point.X * 3; // 每个像素4字节

            int blue = rgbValues[index];
            int green = rgbValues[index + 1];
            int red = rgbValues[index + 2];

            int redValue = red & 3;
            int greenValue = green & 7;
            int blueValue = blue & 7;

            int value = blueValue | greenValue << 3 | redValue << 6;
            return value;
        }
        public Bitmap Encode(Bitmap img, byte[] data)
        {
            // 锁定位图的整个区域
            Rectangle rect = new Rectangle(0, 0, img.Width, img.Height);
            BitmapData bmpData = img.LockBits(rect, ImageLockMode.ReadWrite, img.PixelFormat);

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
                    Point point = LinearIndexToPoint(c, img.Width, img.Height);
                    char letter = binaryCharArray[i];
                    int value = Convert.ToInt32(letter);
                    EncodePixelToArray(rgbValues, point, value, img.Width, bmpData.Stride);
                    c++;
                }
                // Write data
                for (int i = 0; i < data.Length; i++)
                {
                    Point point = LinearIndexToPoint(c, img.Width, img.Height);
                    EncodePixelToArray(rgbValues, point, data[i], img.Width, bmpData.Stride);
                    c++;
                }
            }
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

            // 解锁位图
            img.UnlockBits(bmpData);

            return img;
        }

        private static void EncodePixelToArray(byte[] rgbValues, Point point, int value, int width, int stride)
        {
            int index = point.Y * stride + point.X * 3; // 每个像素4字节

            int blueValue = value & 7;
            int greenValue = value >> 3 & 7;
            int redValue = value >> 6 & 3;

            rgbValues[index] = (byte)(rgbValues[index] & 0xF8 | blueValue);
            rgbValues[index + 1] = (byte)(rgbValues[index + 1] & 0xF8 | greenValue);
            rgbValues[index + 2] = (byte)(rgbValues[index + 2] & 0xFC | redValue);
        }

        /*

        public static Bitmap EncodeMsgLinearImage(string text, Bitmap img)
        {
            int maxLinear = img.Width * img.Height;
            int c = 0;
            if (text.Length < maxLinear)
            {
                for (int i = 0; i < text.Length; i++)
                {
                    Point point = LinearIndexToPoint(i, img.Width, img.Height);
                    Color pixel = img.GetPixel(point.X, point.Y);
                    char letter = text[i];
                    int value = Convert.ToInt32(letter);
                    Color n = EncodePixel(pixel, value);
                    img.SetPixel(point.X, point.Y, n);
                    c = i;
                }
                //Null value placed at the end to indicate end of text - or using 255 for better hiding
                c++;
                Point pointEnd = LinearIndexToPoint(c, img.Width, img.Height);
                Color pixelEnd = img.GetPixel(pointEnd.X, pointEnd.Y);
                img.SetPixel(pointEnd.X, pointEnd.Y, EncodePixel(pixelEnd, 0));
            }
            return img;
        }
        public static string DecodeMsgLinearImage(Bitmap img)
        {
            string text = string.Empty;
            int value = 0;
            int i = 0;
            do
            {
                Point point = LinearIndexToPoint(i, img.Width, img.Height);
                Color pixel = img.GetPixel(point.X, point.Y);
                value = DecodePixel(pixel);
                i++;
                if (value != 255)
                    text += Convert.ToChar(value);
            } while (value != 255);
            try
            {
                return text;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        */
    }
}
