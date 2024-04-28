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
    public partial class PopupInformation : Form
    {

        public string Typedepiece { get; set; }
        public string numeroPIECE { get; set; }
        public DateTime datenaissance { get; set; }
        public DateTime dateExpire { get; set; }
        public PopupInformation()
        {
            InitializeComponent();
            ChargeCombo();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void ChargeCombo()
        {
            List<KeyValuePair<string, string>> items = new List<KeyValuePair<string, string>>();
            items.Add(new KeyValuePair<string, string>("P001", "Ancienne CNI"));
            items.Add(new KeyValuePair<string, string>("P002", "Nouvelle CNI"));
            items.Add(new KeyValuePair<string, string>("p003", "Passport"));

            // Attribuez les éléments à la source de données du ComboBox
            comboBox1.DataSource = items;

            // Définissez la propriété DisplayMember pour afficher la valeur de la paire clé/valeur
            comboBox1.DisplayMember = "Value";

            // Définissez la propriété ValueMember pour spécifier la valeur de la paire clé/valeur
            comboBox1.ValueMember = "Key";

            // Sélectionnez un élément par défaut si nécessaire
            comboBox1.SelectedIndex = 0; // Pour sélectionner le premier élément
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Typedepiece = comboBox1.SelectedValue.ToString();
            numeroPIECE = txt_numpiece.Text;
            datenaissance = datenaissance.Date;
            dateExpire = dateExpire.Date;
            this.Close();
            

        }
    }
}
