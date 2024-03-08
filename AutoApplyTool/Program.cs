using AutoApplyTool.Ads;
using Google.Ads.GoogleAds.Config;
using Google.Ads.GoogleAds.Lib;

namespace AutoApplyTool
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
            ApplicationConfiguration.Initialize();
            Application.Run(new AutoApplyForm());

            Console.ReadKey();
        }
    }
}