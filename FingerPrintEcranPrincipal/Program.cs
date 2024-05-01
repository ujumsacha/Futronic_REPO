using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FingerPrintEcranPrincipal
{
    internal static class Program
    {
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
            createfileParametre();
            Application.Run(new frm_Acceuil());
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
                        pr.baseUriApi = "";
                        pr.NFCappLaunch = "";
                        fs.Write(JsonConvert.SerializeObject(pr));
                    }
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Erreur de lancement de l'application veuillez verifier le fichier de configuration");
            }

        }
        public static void createfileParametre()
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


                string file = Path.Combine(lechemin, "AdminSystem.json");

                if (!File.Exists(file))
                {
                    using (StreamWriter fs = File.CreateText(file))
                    {
                        AdminsystemeParam pr = new AdminsystemeParam();
                        pr.created_at = DateTime.Now;
                        pr.modify_at = DateTime.Now;
                        pr.Use_empreinte = false;
                        fs.Write(JsonConvert.SerializeObject(pr));
                    }
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show("Erreur de lancement de l'application veuillez verifier le fichier de configuration");
            }

        }
    }
}
