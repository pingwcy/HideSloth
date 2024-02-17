using HideSloth.Steganography;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HideSloth
{
    public static class GlobalVariables
    {
        private static string _mode = "Normal";
        private static bool _enableencrypt = true;
        private static bool _disablencrypt = false;
        private static string _Algor = "PNG/BMP: Linear";
        private static int _iteration = 100000;
        private static bool _CustIter = false;
        private static string? _Hash = "SHA256";
        private static bool _CustHash = false;
        private static readonly byte[] _separator = Encoding.UTF8.GetBytes("|");
        private static string _defaultname = "";
        private static bool _sparse_decode = false;
        private static string _outputformat = ".png";//bulk process
        private static bool _copymeta = false;//for loaded container
        private static bool _keepformat = false;
        private static bool _copyotherfilemeta = false;//for other files and directories
        private static int _smallstandard = 1;//ignore image capacity
        private static string _encalg = "AES";
        private static bool _ignoreextracterror = false;
        private static List<string> _listofsupportimagealg = new List<string> { "PNG/BMP: LSB", "PNG/BMP: Linear", "PNG/BMP: Random LSB" };
        private static bool _rand = false;
        private static string _kdf = "Password Based";
        private static string _pubkey = "";
        private static string _privatekeyenced = "";
        private static bool _waitencmaster = false;
        private static int _rsasize = 2048;
        public interface ImageAlgorithm
        {
            Bitmap Encode(Bitmap img, byte[] data, string pwd);
            byte[] Decode(Bitmap img, string pwd);

            double CheckSize(Image img);
            // 其他方法定义...
        }
        public static class AlgorithmImageFactory
        {
            public static ImageAlgorithm CreateAlgorithm(string name)
            {
                switch (name)
                {
                    case "PNG/BMP: Linear":
                        return new Linear_Image();
                    case "PNG/BMP: LSB":
                        return new LSB_Image();
                    case "PNG/BMP: Random LSB":
                        return new LSBRND_Image();
                    case "SUNIWARD":
                    //return new SUNIWARD_Image();

                    default:
                        throw new ArgumentException("Invalid algorithm name");
                }
            }
        }

        public static ReadOnlyCollection<string> listofsupportimagealg
        {
            get { return _listofsupportimagealg.AsReadOnly(); }
        }
        public static string Mode
        {
            get { return _mode; }
            set { _mode = value; }
        }
        public static bool Enableencrypt
        {
            get { return _enableencrypt; }
            set { _enableencrypt = value; }
        }
        public static bool Disablencrypt
        {
            get { return _disablencrypt; }
            set { _disablencrypt = value; }
        }
        public static string Algor
        {
            get { return _Algor; }
            set { _Algor = value; }
        }
        public static int Iteration
        {
            get { return _iteration; }
            set { _iteration = value; }
        }
        public static bool CustIter
        {
            get { return _CustIter; }
            set { _CustIter = value; }
        }
        public static string Hash
        {
            get { return _Hash ?? ""; }
            set { _Hash = value; }
        }
        public static bool CustHash
        {
            get { return _CustHash; }
            set { _CustHash = value; }
        }
        public static byte[] Separator
        {
            get { return _separator; }
        }
        public static string Defaultname
        {
            get { return _defaultname; }
            set { _defaultname = value; }
        }
        public static bool Sparse_decode
        {
            get { return _sparse_decode; }
            set { _sparse_decode = value; }
        }
        public static string Outputformat
        {
            get { return _outputformat; }
            set { _outputformat = value; }
        }
        public static bool Copymeta
        {
            get { return _copymeta; }
            set { _copymeta = value; }
        }
        public static bool Keepformat
        {
            get { return _keepformat; }
            set { _keepformat = value; }
        }
        public static bool Copyotherfilemeta
        {
            get { return _copyotherfilemeta; }
            set { _copyotherfilemeta = value; }
        }
        public static int Smallstandard
        {
            get { return _smallstandard; }
            set { _smallstandard = value; }
        }
        public static string Encalg
        {
            get { return _encalg; }
            set { _encalg = value; }
        }
        public static bool Ignoreextracterror
        {
            get { return _ignoreextracterror; }
            set { _ignoreextracterror = value; }
        }
        public static string KDF
        {
            get { return _kdf; }
            set { _kdf = value; }
        }
        public static string pubkey
        {
            get { return _pubkey; }
            set { _pubkey = value; }
        }
        public static string privatekeyenced
        {
            get { return _privatekeyenced; }
            set { _privatekeyenced = value; }
        }
        public static bool waitencmaster
        {
            get { return _waitencmaster; }
            set { _waitencmaster = value; }
        }
        public static int rsasize
        {
            get { return _rsasize; }
            set { _rsasize = value; }
        }
    }
}