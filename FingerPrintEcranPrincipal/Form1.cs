using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FingerPrintEcranPrincipal
{
    public partial class frm_Acceuil : Form
    {
        public frm_Acceuil()
        {
            InitializeComponent();
        }

        private void frm_Acceuil_Load(object sender, EventArgs e)
        {
            lbl_messageinput.Visible = true;
            lbl_messageinput.Text = "Veuillez renseigner le numero CNI SVP ...";
            radioButton5.Checked = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rd_numPiece_CheckedChanged(object sender, EventArgs e)
        {

        }



        private void radiobutton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton? radioButton = sender as RadioButton;
            if (radioButton != null && radioButton.Checked)
            {
                switch (radioButton.TabIndex)
                {
                    case 0:
                        lbl_messageinput.Visible = true;
                        textBox1.Visible = true;
                        pictureBox2.Visible = false;
                        label2.Visible = false;
                        button1.Visible = true;
                        lbl_messageinput.Text = "Veuillez renseigner le numero CNI SVP ...";
                        break;

                    case 1:
                        lbl_messageinput.Visible = true;
                        textBox1.Visible = true;
                        pictureBox2.Visible = false;
                        label2.Visible = false;
                        button1.Visible = true;
                        lbl_messageinput.Text = "Veuillez renseigner le numero UNIQUE SVP ...";
                        break;

                    case 2:
                        pictureBox2.Visible = true;
                        label2.Visible = true;
                        lbl_messageinput.Visible = false;
                        textBox1.Visible = false;
                        button1.Visible = false;
                        break;

                    default:
                        break;

                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void radiobuttonClient_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton? radioButton = sender as RadioButton;
            if (radioButton != null && radioButton.Checked)
            {
                switch (radioButton.TabIndex)
                {
                    case 0:
                        groupBox1.Visible = false;
                        panelclient.Visible = true;
                        panelporteur.Visible = false;

                        break;
                    case 1:
                        groupBox1.Visible = true;
                        panelclient.Visible = false;
                        panelporteur.Visible = true;
                        radioButton3.Visible = true;
                        radioButton2.Checked = true;
                        label8.Visible = true; textBox6.Visible = true; label9.Visible = false;
                        label10.Visible = false; pictureBox1.Visible = false; label11.Visible = false;
                        button6.Visible = false;
                        break;
                }
            }
        }



        private void RadioButtonCNAMNFCSAISIE(object sender, EventArgs e)
        {
            RadioButton? radioButton = sender as RadioButton;
            if (radioButton != null && radioButton.Checked)
            {
                switch (radioButton.TabIndex)
                {
                    case 0:
                        label8.Visible = true; textBox6.Visible = true; label9.Visible = false;
                        label10.Visible = false; pictureBox1.Visible = false; label11.Visible = false;
                        button6.Visible = false;
                        break;
                    case 1:
                        label8.Visible = false; textBox6.Visible = false; label9.Visible = true;
                        label10.Visible = true; pictureBox1.Visible = false; label11.Visible = false;
                        button6.Visible = false;
                        break;
                    case 2:
                        label8.Visible = false; textBox6.Visible = false; label9.Visible = false;
                        label10.Visible = false; pictureBox1.Visible = true; label11.Visible = true;
                        button6.Visible = true;
                        break;

                }

            }
            //NFC
            if (radioButton3.Checked) { label8.Visible = false; textBox6.Visible = false; }
            //CNAM
            if (radioButton2.Checked) { label8.Visible = true; textBox6.Visible = true; }
            //SAISIE MANNUELLE
            if (radioButton1.Checked) { }
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            groupBox1.Visible = false;
            radioButton5.Checked = true;
        }

        private void panel2_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void panelclient_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void panelporteur_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            PopupInformation pinf = new PopupInformation();
            var test = pinf.ShowDialog();
        }
    }
}
