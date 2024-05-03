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
        public DateTime _datenaissance { get; set; }
        public DateTime dateExpire { get; set; }

        public bool isvalid { get; set; } = false;
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
            items.Add(new KeyValuePair<string, string>("lastCni", "Ancienne CNI"));
            items.Add(new KeyValuePair<string, string>("newCni", "Nouvelle CNI"));
            items.Add(new KeyValuePair<string, string>("passPort", "Passport"));

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
            _datenaissance = Dt_naissance.Value.Date;
            dateExpire = Dt_Exp.Value.Date;
            isvalid = true;
            this.Close();
        }
    }
}
