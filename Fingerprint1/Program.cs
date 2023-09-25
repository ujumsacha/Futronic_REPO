using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

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
            createfile();
            Application.Run(new Acceuil());
        }
        public static IServiceContainer ConfigureServices()
        {
            IServiceContainer services = new ServiceContainer();
            services.AddService(typeof(Acceuil), new Acceuil());
            //services.AddService(typeof(FormSearch), new FormSearch());


            return services;
        }

        public static void createfile()
        {
            try
            {
                string lechemin = Path.Combine(Directory.GetCurrentDirectory(), "ConfigFolder");
                if (!Directory.Exists(lechemin))
                {
                    Directory.CreateDirectory(lechemin);
                }

                string lechemin1 = Path.Combine(Directory.GetCurrentDirectory(), "tempfolder");
                if (!Directory.Exists(lechemin1))
                {
                    Directory.CreateDirectory(lechemin1);
                }


                string file = Path.Combine(lechemin, "param.json");

                if (!File.Exists(file))
                {
                    using (StreamWriter fs = File.CreateText(file))
                    {
                        Param pr = new Param();
                        pr.publicrepertory = "";
                        pr.DatabaseString = "";
                        fs.Write(JsonConvert.SerializeObject(pr));
                    }
                }
                
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
    }
}
