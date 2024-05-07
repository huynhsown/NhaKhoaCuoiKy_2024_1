using NhaKhoaCuoiKy.Views;
using NhaKhoaCuoiKy.Views.Appointment;
using NhaKhoaCuoiKy.Views.LogIn;
using NhaKhoaCuoiKy.FaceRecognize;
using NhaKhoaCuoiKy.ShiftSchedule;

namespace NhaKhoaCuoiKy
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
            Application.Run(new LoginForm());
        }
    }
}