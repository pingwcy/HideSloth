using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HideSloth.Steganography;
using System.Diagnostics;
using HideSloth.Crypto;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HideSloth.Tools
{
    public class Benchmark
    {
        public static List<List<double>> KDFBench(string alg, int iter, string sha)
        {
            List<double> time1 = new List<double>();
            List<double> time2 = new List<double>();
            List<List<double>> time = new List<List<double>>();
            if (alg == "PBKDF2")
            {
                using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
                {
                    byte[] salt = new byte[16];
                    RandomNumberGenerator.Create().GetBytes(salt);
                    string pwd = "123456789abcdefghijklmnopqrstuvwxyz.-*/+! ";
                    Stopwatch stopwatch1 = Stopwatch.StartNew();
                    using (var pbkdf2 = new Rfc2898DeriveBytes(pwd, salt, iter, Support_Converter.StringToHashAlgorithmName(sha)))
                    {
                        var key = pbkdf2.GetBytes(32);
                    }
                    stopwatch1.Stop();
                    time1.Add(stopwatch1.Elapsed.TotalSeconds);

                }
            }
            time.Add(time1);
            return time;
        }
        public static List<List<double>> EncAlgBench(string alg, int size, int count)
        {
            List<double> time1 = new List<double>();
            List<double> time2 = new List<double>();
            List<double> time3 = new List<double>();
            List<double> time4 = new List<double>();

            List<List<double>> time = new List<List<double>>();
            //byte[] testdata;

            //Start the test
            if (alg == "AES256-GCM" || alg == "All")
            {
                using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
                {
                    var data = new byte[size];
                    var key = new byte[32];
                    rng.GetBytes(key);

                    rng.GetBytes(data);

                    for (int i = 0; i < count; i++)
                    {
                        Stopwatch stopwatch1 = Stopwatch.StartNew();

                        data = Bench_Encryption.Encryptor_Bench("AES",data, key,out byte[] tag);
                        stopwatch1.Stop();
                        time1.Add(stopwatch1.Elapsed.TotalSeconds);

                        Stopwatch stopwatch2 = Stopwatch.StartNew();

                        Bench_Encryption.Decryptor_Bench("AES", data, tag,key);
                        stopwatch2.Stop();
                        time2.Add(stopwatch2.Elapsed.TotalSeconds);

                    }
#pragma warning disable CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。
                    //testdata = null;
#pragma warning restore CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。
                    data = null;
                    GC.Collect();
                }
            }

            if (alg == "ChaCha20-Poly1305" || alg == "All")
            {
                using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
                {
                    var data = new byte[size];
                    var key = new byte[32];
                    rng.GetBytes(key);

                    rng.GetBytes(data);

                    for (int i = 0; i < count; i++)
                    {
                        Stopwatch stopwatch3 = Stopwatch.StartNew();

                        data = Bench_Encryption.Encryptor_Bench("ChaCha",data, key, out byte[] tag);
                        stopwatch3.Stop();
                        time3.Add(stopwatch3.Elapsed.TotalSeconds);

                        Stopwatch stopwatch4 = Stopwatch.StartNew();

                        Bench_Encryption.Decryptor_Bench("ChaCha", data, tag, key);
                        stopwatch4.Stop();
                        time4.Add(stopwatch4.Elapsed.TotalSeconds);

                    }
#pragma warning disable CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。
                    //testdata = null;
#pragma warning restore CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。
                    data = null;
                    GC.Collect();
                }
            }



            if (alg == "AES256-GCM" || alg == "All")
            {
                time.Add(time1);
                time.Add(time2);
            }
            if (alg == "ChaCha20-Poly1305" || alg == "All")
            {
                time.Add(time3);
                time.Add(time4);
            }

            return time;
        }








        public static List<List<double>> AlgBench(string alg, int size, int count)
        {
            List<double> time1 = new List<double>();
            List<double> time2 = new List<double>();
            List<List<double>> time = new List<List<double>>();

            double figuresize = 0;
            if (alg == "PNG/BMP: LSB" || alg == "PNG/BMP: ALL")
            {
                figuresize = size * 1.34 * 1.1 * 8 / 3;

            }
            else if (alg == "PNG/BMP: Linear")
            {
                figuresize = size * 1.03;
            }
            int lengthwith = (int)Math.Ceiling(Math.Sqrt(figuresize));
            //MessageBox.Show(lengthwith.ToString());
            Bitmap virtualBitmap = new Bitmap(lengthwith, lengthwith);

            //Start the test
            if (alg == "PNG/BMP: LSB" || alg == "PNG/BMP: ALL")
            {
                using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
                {
                    for (int i = 0; i < count; i++)
                    {
                        var data = new byte[size];
                        rng.GetBytes(data);
                        Stopwatch stopwatch1 = Stopwatch.StartNew();
                        LSB_Image.embed(Convert.ToBase64String(data), virtualBitmap);
                        stopwatch1.Stop();
                        time1.Add(stopwatch1.Elapsed.TotalSeconds);

                    }
                }


            }
            if (alg == "PNG/BMP: Linear" || alg == "PNG/BMP: ALL")
            {
                using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
                {
                    for (int i = 0; i < count; i++)
                    {
                        var data = new byte[size];
                        rng.GetBytes(data);
                        Stopwatch stopwatch2 = Stopwatch.StartNew();
                        Core_Linear_Image.EncodeFileLinear(virtualBitmap, data);
                        stopwatch2.Stop();
                        time2.Add(stopwatch2.Elapsed.TotalSeconds);

                    }
                }

            }

            virtualBitmap.Dispose();
            if (alg == "PNG/BMP: ALL")
            {
                time.Add(time1);
                time.Add(time2);
            }
            else if (alg == "PNG/BMP: LSB")
            {
                time.Add(time1);
            }
            else if (alg == "PNG/BMP: Linear")
            {
                time.Add(time2);
            }
            return time;
        }
    }
}