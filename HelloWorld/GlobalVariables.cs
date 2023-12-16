using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HideSloth
{
    public static class GlobalVariables
    {
        public static bool check_result;

        public static string mode = "Normal";

        public static bool encode;
        public static bool decode;
        public static bool enableencrypt = true;
        public static bool disablencrypt = false;
        public static bool isfile;
        public static bool isstring;

        public static string? route_container ="";
        public static List<string> route_containers = [];
        public static string? multipal_route;
        public static string? password;
        public static string? route_secret;
        public static string? output_route;
        public static string? outputname;
        public static string? outputnameandroute = "";
        public static string? Algor = "Linear";

        public static int iteration = 100000;
        public static bool CustIter = false;
        public static string Hash = "SHA256";
        public static bool CustHash = false;

        public static byte[] separator = Encoding.UTF8.GetBytes("|");

        public static string defaultname = "";

        public static bool sparse_decode = false;

        public static string outputformat = ".png";//bulk process

        public static bool copymeta = false;//for loaded container
        public static bool keepformat = false;
        public static bool copyotherfilemeta = false;//for other files and directories
        public static int smallstandard = 1;//ignore image capacity
        public static string encalg = "AES";
        public static bool ignoreextracterror = false;

        public static string audioorimage = "image";

        public static string stringinfo = "";
    }
}
