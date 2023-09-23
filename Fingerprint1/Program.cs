using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fingerprint1
{
    static class Program
    {
        private static IServiceProvider serviceProvider { get; set; }
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        public static IServiceContainer ConfigureServices()
        {
            IServiceContainer services = new ServiceContainer();
            services.AddService(typeof(Form1), new Form1());


            return services;
        }
    }
}
