using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormCreation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        private void button1_Click(object sender, EventArgs e)
        {

            //// Chemin du fichier batch
            //string cheminFichierBatch = "\"C:\\Users\\sacha.ogou\\source\\repos\\Futronic_REPO\\FingerPrintEcranPrincipal\\bin\\Debug\\net5.0-windows\\APP NFC\\scan.jar\"";

            //// Arguments à passer au fichier batch
            //string arguments = " \" newCni,CI003054046,19931112,20320128\"";




            //// Création du processus
            //Process process = new Process();
            //process.StartInfo.FileName = cheminFichierBatch;
            //process.StartInfo.Arguments = arguments;

            //// Optionnel: Redirection de la sortie standard et d'erreur
            //process.StartInfo.RedirectStandardOutput = true;
            //process.StartInfo.RedirectStandardError = true;
            //process.StartInfo.UseShellExecute = false;

            //// Démarrage du processus
            ////Process.Start(cheminFichierBatch);
            //process.Start();

            //// Attend que le processus se termine
            ////process.WaitForExit();

            //// Récupération de la sortie standard et d'erreur (optionnel)
            //string output = process.StandardOutput.ReadToEnd();
            //string error = process.StandardError.ReadToEnd();
            WriteContenueFichierBat();

            string command = @"C:\Users\sacha.ogou\source\repos\Futronic_REPO\FingerPrintEcranPrincipal\bin\Debug\net5.0-windows\APP NFC\NFC.bat";
            //string args = "MyParam1 MyParam2";

            Process process = new Process();
            process.StartInfo.FileName = command;
            //process.StartInfo.Arguments = args;
            process.Start();

            


                //string command = @"java -jar ""C:\Users\sacha.ogou\source\repos\Futronic_REPO\FingerPrintEcranPrincipal\bin\Debug\net5.0-windows\APP NFC\scan.jar"" ""newCni,CI003054046,19931112,20320128""";

                //ProcessStartInfo procStartInfo = new ProcessStartInfo("cmd", "/c " + command);

                //procStartInfo.RedirectStandardOutput = true;
                //procStartInfo.UseShellExecute = false;
                //procStartInfo.CreateNoWindow = true;

                //using (Process process = new Process())
                //{
                //    process.StartInfo = procStartInfo;
                //    process.Start();

                //    // Add this: wait until process does its work
                //    process.WaitForExit();

                //    // and only then read the result
                //    string result = process.StandardOutput.ReadToEnd();
                //    Console.WriteLine(result);
                //}





            }

        public static (bool, bool) WriteContenueFichierBat()
        {
            string urifile = @"C:\Users\sacha.ogou\source\repos\Futronic_REPO\FingerPrintEcranPrincipal\bin\Debug\net5.0-windows\APP NFC\NFC.bat";
            try
            {
                using (StreamWriter writer = new StreamWriter(urifile))
                {
                    var test = @"@echo off
                                java -jar ""C:\Users\sacha.ogou\source\repos\Futronic_REPO\FingerPrintEcranPrincipal\bin\Debug\net5.0-windows\APP NFC\scan.jar"" ""newCni,CI003054046,Test,20320128""

                                pause";
                    writer.Write(test);
                }
                return (true,true);
            }
            catch (Exception ex)
            {

                throw new Exception("erreur Systeme");
            }
        }

            private void button1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
