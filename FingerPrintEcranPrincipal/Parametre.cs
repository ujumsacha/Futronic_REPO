using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FingerPrintEcranPrincipal
{
    public partial class Parametre : Form
    {
        public bool is_refresh { get; set; } = false;
        public Parametre()
        {
            InitializeComponent();
            checkBox1.Checked=  Outils.ReadContenue().Item2;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                bool res = (checkBox1.Checked) ? true : false;
                Outils.ModifierContenueParam(res);
                is_refresh = true;
                this.Close();
                
                MessageBox.Show("Mise a jour Effectué avec Succes , l'application se fermera");
                Application.Exit();
            }
            catch (Exception ex)
            {

                MessageBox.Show("erreur systeme Veuillez contacter l'administrateur");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
