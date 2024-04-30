using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
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

            // Chemin du fichier batch
            string cheminFichierBatch = "\"C:\\Users\\sacha.ogou\\source\\repos\\Futronic_REPO\\FingerPrintEcranPrincipal\\bin\\Debug\\net5.0-windows\\APP NFC\\scan.jar\"";

            // Arguments à passer au fichier batch
            string arguments = " \"newCni,CI003054046,19931112,20320128\"";

            // Création du processus
            Process process = new Process();
            process.StartInfo.FileName = cheminFichierBatch;
            process.StartInfo.Arguments = arguments;

            // Optionnel: Redirection de la sortie standard et d'erreur
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.UseShellExecute = false;

            // Démarrage du processus
            //Process.Start(cheminFichierBatch);
            process.Start();

            // Attend que le processus se termine
            //process.WaitForExit();

            // Récupération de la sortie standard et d'erreur (optionnel)
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();
        }
    }
}
