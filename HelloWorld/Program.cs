using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace HideSloth
{

    internal static partial class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        ///     
        [LibraryImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool AllocConsole();

        [STAThread]

        static void Main(string[] args)
        {

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
#if WINDOWS
            if (args.Length > 0)
            {
                // 运行 CLI 模式
                AllocConsole();
                MainCLI.RunCli(args);
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm(SimpleEventAggregator.Instance)); 
                /*
                ApplicationConfiguration.Initialize();
                Application.Run(new MainForm());
                */
            }
#elif LINUX
    Console.WriteLine("Running on Linux - No GUI will be started");

#endif
        }
    }
}