using System.Windows.Forms;
namespace HideSloth
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
# if WINDOWS
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
#elif LINUX
    Console.WriteLine("Running on Linux - No GUI will be started");

#endif
        }
    }
}