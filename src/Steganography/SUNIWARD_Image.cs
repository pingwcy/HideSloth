using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static HideSloth.GlobalVariables;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
/*
namespace HideSloth.Steganography
{
    public class SUNIWARD_Image : ImageAlgorithm
    {
        public double CheckSize(System.Drawing.Image img)
        {
            return Math.Round(img.Width * img.Height / 1024 * 0.97);
        }

        private static double MAX_PAYLOAD = 0.05;

        private static float[,] DoubletoFloat(double[,] data)
        {
            int rows = data.GetLength(0); // 获取行数
            int cols = data.GetLength(1); // 获取列数

            float[,] floatArray = new float[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    floatArray[i, j] = (float)data[i, j];
                }
            }
            return floatArray;
        }
        {
            {
                int width = bitmap.Width;
                int height = bitmap.Height;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
        {
            }
        }

            return result;
        }
        public Bitmap Encode(Bitmap image, byte[] data, string pwd)
        {
            int n_channels = 3;
            {
                n_channels = 1;
                image = image.Clone(new Rectangle(0, 0, image.Width,   image.Height), PixelFormat.Format24bppRgb);
            }
            int password_seed = 0;

            int m = 1;
            {
                m *= i;
            }
            int capacity = (int)((m * MAX_PAYLOAD) / 8);
            List<byte[]> msg_bits = new List<byte[]>();
            if (n_channels == 1)
            {
                msg_bits.Add(data);
            }
            else
            {
                int l = data.Length / 3;
                msg_bits.Add(data.Take(l).ToArray());
                msg_bits.Add(data.Skip(l).Take(l).ToArray());
                msg_bits.Add(data.Skip(2 * l).ToArray());
            }
            for (int c = 0; c < n_channels; c++)
            {
                (double[,], double[,]) costs = CostFn(I);

            }

            return image;
        }

        public byte[] Decode(Bitmap image, string pwd)
        {

            int n_channels = 3;
            if (image.PixelFormat == PixelFormat.Format8bppIndexed)
            {
                n_channels = 1;
                image = image.Clone(new Rectangle(0, 0, image.Width, image.Height), PixelFormat.Format24bppRgb);
            }

            int password_seed = 0;
            List<byte> ciphertext = new List<byte>();
            for (int c = 0; c < n_channels; c++)
            {
            }
            return ciphertext.ToArray();
        }



        [DllImport("stc.dll")]
        public static extern int stc_hide(int cover_len, int[] cover, float[] costs, int message_len, byte[] message, int[] stego);

        [DllImport("stc.dll")]
        public static extern int stc_unhide(int stego_len, int[] stego, int message_len, byte[] extracted_message);

        public static int[] BytesToBits(byte[] data)
        {
            List<int> array = new List<int>();
            foreach (byte b in data)
            {
                for (int i = 0; i < 8; i++)
                {
                    array.Add((b >> i) & 1);
                }
            }
            return array.ToArray();
        }

        public static int[] HideStc(int[] cover_array, float[] costs_array_m1, float[] costs_array_p1, int[] message_bits, int mx = 255, int mn = 0)
        {
            int[] cover = new int[cover_array.Length];
            for (int i = 0; i < cover_array.Length; i++)
            {
                cover[i] = Convert.ToInt32(cover_array[i]);
            }
            float[] costs = new float[costs_array_m1.Length * 3];
            for (int i = 0; i < costs_array_m1.Length; i++)
            {
                if (cover[i] <= mn)
                {
                    costs[3 * i + 0] = float.PositiveInfinity;
                    costs[3 * i + 1] = 0;
                    costs[3 * i + 2] = costs_array_p1[i];
                }
                else if (cover[i] >= mx)
                {
                    costs[3 * i + 0] = costs_array_m1[i];
                    costs[3 * i + 1] = 0;
                    costs[3 * i + 2] = float.PositiveInfinity;
                }
                else
                {
                    costs[3 * i + 0] = costs_array_m1[i];
                    costs[3 * i + 1] = 0;
                    costs[3 * i + 2] = costs_array_p1[i];
                }
            }
            int m = message_bits.Length;
            byte[] message = new byte[m];
            for (int i = 0; i < m; i++)
            {
                message[i] = Convert.ToByte(message_bits[i]);
            }
            int[] stego = new int[cover_array.Length];
            int result = stc_hide(cover_array.Length, cover, costs, m, message, stego);
            int[] stego_array = (int[])cover_array.Clone();
            for (int i = 0; i < cover_array.Length; i++)
            {
                stego_array[i] = stego[i];
            }
            return stego_array;
        }

        public static int[,] Hide(byte[] message, int[,] cover_matrix, float[,] cost_matrix_m1, float[,] cost_matrix_p1, int password_seed, int mx = 255, int mn = 0)
        {
            Random random = new Random(password_seed);
            int[] message_bits = BytesToBits(message);
            int height = cover_matrix.GetLength(0);
            int width = cover_matrix.GetLength(1);
            int[] cover_array = new int[height * width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    cover_array[i * width + j] = cover_matrix[i, j];
                }
            }
            float[] costs_array_m1 = new float[height * width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    costs_array_m1[i * width + j] = cost_matrix_m1[i, j];
                }
            }
            float[] costs_array_p1 = new float[height * width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    costs_array_p1[i * width + j] = cost_matrix_p1[i, j];
                }
            }
            byte[] data_len = BitConverter.GetBytes(message_bits.Length);
            int[] data_len_bits = BytesToBits(data_len);
            int[] indices = Enumerable.Range(0, cover_array.Length).ToArray();
            indices = indices.OrderBy(x => random.Next()).ToArray();
            cover_array = indices.Select(x => cover_array[x]).ToArray();
            costs_array_m1 = indices.Select(x => costs_array_m1[x]).ToArray();
            costs_array_p1 = indices.Select(x => costs_array_p1[x]).ToArray();
            int[] stego_array_1 = HideStc(cover_array.Take(64).ToArray(), costs_array_m1.Take(64).ToArray(), costs_array_p1.Take(64).ToArray(), data_len_bits, mx, mn);
            int[] stego_array_2 = HideStc(cover_array.Skip(64).ToArray(), costs_array_m1.Skip(64).ToArray(), costs_array_p1.Skip(64).ToArray(), message_bits, mx, mn);
            int[] stego_array = stego_array_1.Concat(stego_array_2).ToArray();
            for (int i = 0; i < indices.Length; i++)
            {
                stego_array[indices[i]] = stego_array[i];
            }
            int[,] stego_matrix = new int[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    stego_matrix[i, j] = stego_array[i * width + j];
                }
            }
            return stego_matrix;
        }

        public static byte[] UnhideStc(int[] stego_array, int message_len)
        {
            int[] stego = new int[stego_array.Length];
            for (int i = 0; i < stego_array.Length; i++)
            {
                stego[i] = Convert.ToInt32(stego_array[i]);
            }
            byte[] extracted_message = new byte[stego_array.Length];
            int s = stc_unhide(stego_array.Length, stego, message_len, extracted_message);
            List<byte> data = new List<byte>();
            int idx = 0;
            int bitidx = 0;
            int bitval = 0;
            for (int i = 0; i < message_len; i++)
            {
                if (bitidx == 8)
                {
                    data.Add(Convert.ToByte(bitval));
                    bitidx = 0;
                    bitval = 0;
                }
                bitval |= extracted_message[i] << bitidx;
                bitidx += 1;
                idx += 1;
            }
            if (bitidx == 8)
            {
                data.Add(Convert.ToByte(bitval));
            }
            return data.ToArray();
        }

        public static byte[] Unhide(int[,] stego_matrix, int password_seed)
        {
            Random random = new Random(password_seed);
            int height = stego_matrix.GetLength(0);
            int width = stego_matrix.GetLength(1);
            int[] stego_array = new int[height * width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    stego_array[i * width + j] = stego_matrix[i, j];
                }
            }
            int[] indices = Enumerable.Range(0, stego_array.Length).ToArray();
            indices = indices.OrderBy(x => random.Next()).ToArray();
            stego_array = indices.Select(x => stego_array[x]).ToArray();
            byte[] data = UnhideStc(stego_array.Take(64).ToArray(), 32);
            int data_len = BitConverter.ToInt32(data, 0);
            data = UnhideStc(stego_array.Skip(64).ToArray(), data_len);
            return data;
        }

        {
            int k = I.GetLength(0);
            int l = I.GetLength(1);
            double[] hpdf = new double[]
            {
            -0.0544158422,  0.3128715909, -0.6756307363,  0.5853546837,
             0.0158291053, -0.2840155430, -0.0004724846,  0.1287474266,
             0.0173693010, -0.0440882539, -0.0139810279,  0.0087460940,
             0.0048703530, -0.0003917404, -0.0006754494, -0.0001174768
            };
            double[] sign = new double[hpdf.Length];
            for (int i = 0; i < hpdf.Length; i++)
            {
                sign[i] = (i % 2 == 0) ? 1 : -1;
            }
            double[] lpdf = new double[hpdf.Length];
            for (int i = 0; i < hpdf.Length; i++)
            {
                lpdf[i] = hpdf[hpdf.Length - 1 - i] * sign[i];
            }
            List<double[,]> F = new List<double[,]>();
            F.Add(OuterProduct(lpdf, hpdf));
            F.Add(OuterProduct(hpdf, lpdf));
            F.Add(OuterProduct(hpdf, hpdf));
            double sgm = 1;
            int padSize = 16;
            double[,] rho = new double[k, l];
            for (int i = 0; i < 3; i++)
            {
                double[,] R0 = Convolve2D(coverPadded, F[i]);
                double[,] X = Convolve2D((Abs(R0)), Rot90(Abs(F[i]), 2));
                if (F[0].GetLength(0) % 2 == 0)
                {
                    X = Roll(X, 1, 0);
                }
                if (F[0].GetLength(1) % 2 == 0)
                {
                    X = Roll(X, 1, 1);
                }
                X = CropArray(X, (X.GetLength(0) - k) / 2, (X.GetLength(1) - l) / 2, k, l);
                rho = AddArrays(rho, X);
            }
            double[,]? rhoM1 = rho.Clone() as double[,];
            double[,]? rhoP1 = rho.Clone() as double[,];
            double INF = double.PositiveInfinity;
            for (int i = 0; i < k; i++)
            {
                for (int j = 0; j < l; j++)
                {
                    if (rhoP1[i, j] > INF)
                    {
                        rhoP1[i, j] = INF;
                    }
                    if (double.IsNaN(rhoP1[i, j]))
                    {
                        rhoP1[i, j] = INF;
                    }
                    {
                        rhoP1[i, j] = INF;
                    }
                    if (rhoM1[i, j] > INF)
                    {
                        rhoM1[i, j] = INF;
                    }
                    if (double.IsNaN(rhoM1[i, j]))
                    {
                        rhoM1[i, j] = INF;
                    }
                    {
                        rhoM1[i, j] = INF;
                    }
                }
            }
            return (rhoM1, rhoP1);
        }

        private static double[,] OuterProduct(double[] a, double[] b)
        {
            int m = a.Length;
            int n = b.Length;
            double[,] result = new double[m, n];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    result[i, j] = a[i] * b[j];
                }
            }
            return result;
        }

        {
            int m = array.GetLength(0);
            int n = array.GetLength(1);
            int newM = m + 2 * padSize;
            int newN = n + 2 * padSize;
            double[,] paddedArray = new double[newM, newN];

            // 填充中心
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    paddedArray[i + padSize, j + padSize] = array[i, j];
                }
            }

            // 填充上下边缘
            for (int i = 0; i < padSize; i++)
            {
                for (int j = 0; j < newN; j++)
                {
                    paddedArray[i, j] = paddedArray[2 * padSize - i, j]; // 上边缘
                    paddedArray[newM - 1 - i, j] = paddedArray[newM - 1 - padSize - i, j]; // 下边缘
                }
            }

            // 填充左右边缘
            for (int i = 0; i < newM; i++)
            {
                for (int j = 0; j < padSize; j++)
                {
                    paddedArray[i, j] = paddedArray[i, 2 * padSize - j]; // 左边缘
                    paddedArray[i, newN - 1 - j] = paddedArray[i, newN - 1 - padSize - j]; // 右边缘
                }
            }

            return paddedArray;
        }

        private static double[,] Convolve2D(double[,] a, double[,] b)
        {
            int m = a.GetLength(0);
            int n = a.GetLength(1);
            int p = b.GetLength(0);
            int q = b.GetLength(1);
            double[,] result = new double[m, n];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    for (int k = 0; k < p; k++)
                    {
                        for (int l = 0; l < q; l++)
                        {
                            int x = i - p / 2 + k;
                            int y = j - q / 2 + l;
                            if (x >= 0 && x < m && y >= 0 && y < n)
                            {
                                result[i, j] += a[x, y] * b[k, l];
                            }
                        }
                    }
                }
            }
            return result;
        }

        private static double[,] Rot90(double[,] array, int k)
        {
            int m = array.GetLength(0);
            int n = array.GetLength(1);
            double[,] result = new double[n, m];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (k == 0)
                    {
                        result[i, j] = array[i, j];
                    }
                    else if (k == 1)
                    {
                        result[j, m - 1 - i] = array[i, j];
                    }
                    else if (k == 2)
                    {
                        result[m - 1 - i, n - 1 - j] = array[i, j];
                    }
                    else if (k == 3)
                    {
                        result[n - 1 - j, i] = array[i, j];
                    }
                }
            }
            return result;
        }

        private static double[,] CropArray(double[,] array, int x, int y, int m, int n)
        {
            double[,] result = new double[m, n];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    result[i, j] = array[x + i, y + j];
                }
            }
            return result;
        }

        private static double[,] AddArrays(double[,] a, double[,] b)
        {
            int m = a.GetLength(0);
            int n = a.GetLength(1);
            double[,] result = new double[m, n];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    result[i, j] = a[i, j] + b[i, j];
                }
            }
            return result;
        }

        private static double[,] Abs(double[,] array)
        {
            int m = array.GetLength(0);
            int n = array.GetLength(1);
            double[,] result = new double[m, n];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    result[i, j] = 1.0 / (Math.Abs(array[i, j]) + 1);
                }
            }
            return result;
        }

        private static double[,] Roll(double[,] array, int shift, int axis)
        {
            int m = array.GetLength(0);
            int n = array.GetLength(1);
            double[,] result = new double[m, n];
            if (axis == 0)
            {
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        result[(i + shift) % m, j] = array[i, j];
                    }
                }
            }
            else if (axis == 1)
            {
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        result[i, (j + shift) % n] = array[i, j];
                    }
                }
            }
            return result;
        }


    }
}
*/