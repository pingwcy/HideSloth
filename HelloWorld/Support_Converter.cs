using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Cryptography;
using static System.Windows.Forms.DataFormats;
using System.Xml;

namespace HideSloth
{
    class Support_Converter
    {
        public static Image ConvertOthersToPngInMemory(string jpgFilePath)
        {
            using (Image jpgImage = Image.FromFile(jpgFilePath))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    // 将JPG图像保存到内存流中，格式为PNG
                    jpgImage.Save(ms, ImageFormat.Png);

                    ms.Position = 0;

                    return new Bitmap(ms);
                }
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

}
