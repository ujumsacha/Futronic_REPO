using FingerPrintEcranPrincipal.Reponses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FingerPrintEcranPrincipal
{
    public partial class frm_Acceuil : Form
    {
        private string URIBD = Outils.recup().baseUriApi.ToString();
        private string LanceAPP = Outils.recup().NFCappLaunch.ToString();
        public frm_Acceuil()
        {
            InitializeComponent();
        }

        private void frm_Acceuil_Load(object sender, EventArgs e)
        {
            lbl_messageinput.Visible = true;
            lbl_messageinput.Text = "Veuillez renseigner le numero CNI SVP ...";
            radioButton5.Checked = true;


            panelconsentement.Visible = false;
            panelEnrollement.Visible = false;
            panelVerif.Visible = true;
            panelEmpreinte.Visible = false;
            panelSignaletique.Visible = false;
            panelResultatRecherche.Visible = false;

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
        private async void button6_Click(object sender, EventArgs e)
        {

            //**************************************************************************
            //var rett = await LanceAppliRecupData();
            //**************************************************************************

            //*******************************************AJOUTéééééééééééééé***********************
            panelconsentement.Visible = false;
            panelEnrollement.Visible = false;
            panelVerif.Visible = false;
            panelEmpreinte.Visible = false;
            panelSignaletique.Visible = true;
            panelResultatRecherche.Visible = false;
            textBox7.Text = "ANKON";
            textBox8.Text = "OGOU SACH MAXIME";
            //*******************************************AJOUTéééééééééééééé***********************


            //PopupInformation popup = new PopupInformation();
            //if (popup.ShowDialog() == DialogResult.OK)
            //{

            //    // Récupérez les informations saisies par l'utilisateur
            //    string numpiece = popup.numeroPIECE;
            //    string typePiece = popup.Typedepiece;
            //    DateTime datenaissance = popup.datenaissance;
            //    DateTime dateExpire = popup.dateExpire;

            //    // Utilisez les informations récupérées comme vous le souhaitez
            //    MessageBox.Show($"Nom d'utilisateur saisi : {typePiece} avec TypePiece {typePiece}");

            //}
            //if (DialogResult == DialogResult.None)
            //{
            //    MessageBox.Show($"Vous avez annuler l'operation");
            //}
        }




        //**********************************VERIFICATION DES RETOUR AFIN DE CALL LE PANEL ADEQUAT***********************************************
        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Define the URL where you want to send the POST request
                    string url = URIBD + "/GetOne";
                    string jsonContent = null;
                    ParamVerif parmV = new ParamVerif
                    {
                        r_value = textBox1.Text
                    };
                    if (rd_numPiece.Checked)
                    {
                        parmV.r_Key = "CNI";
                    }
                    if (rd_numUnique.Checked)
                    {
                        parmV.r_Key = "UNIQUE";
                    }

                    jsonContent = JsonConvert.SerializeObject(parmV);
                    // Define your JSON content to be sent in the request body

                    HttpContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(URIBD, content);

                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();

                    GeneraleResponse GN = new GeneraleResponse();
                    GN = JsonConvert.DeserializeObject<GeneraleResponse>(responseBody);
                    if (GN.data == "[  ]")
                    {
                        bool bres = false;
                        DemandeMessage Dm = new DemandeMessage();

                        Dm.ShowDialog();
                        if (Dm.ret)
                        {
                            panelconsentement.Visible = false;
                            panelEnrollement.Visible = true;
                            panelVerif.Visible = false;
                            panelEmpreinte.Visible = false;
                            panelSignaletique.Visible = false;
                            panelResultatRecherche.Visible = false;
                            label1.Text = "ENROLLEMENT";
                        }
                        else
                        {
                            textBox1.Text = String.Empty;
                        }
                    }
                    else
                    {
                        panelconsentement.Visible = false;
                        panelEnrollement.Visible = false;
                        panelVerif.Visible = false;
                        panelEmpreinte.Visible = false;
                        panelSignaletique.Visible = false;
                        panelResultatRecherche.Visible = true;
                        label1.Text = "INFORMATION PERSONNELLES";
                        //**********************************Appeler la method pour renseigner les differents valeur retrouver sur l'interface*****************

                        //**********************************Appeler la method pour renseigner les differents valeur retrouver sur l'interface*****************

                    }

                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        //**********************************VERIFICATION DES RETOUR AFIN DE CALL LE PANEL ADEQUAT***********************************************



        private void pictureThumb_Click(object sender, EventArgs e)
        {
            labelMsgemp.Text = "Veuillez poser votre pouce sur le Capteur ...";
            labelMsgemp.Visible = true;
            pB_empr.Visible = true;
        }
        private void pictureIndex_Click(object sender, EventArgs e)
        {
            labelMsgemp.Text = "Veuillez poser index pouce sur le Capteur ...";
            labelMsgemp.Visible = true;
            pB_empr.Visible = true;
        }
        private void pictureMiddle_Click(object sender, EventArgs e)
        {
            labelMsgemp.Text = "Veuillez poser votre Majeur sur le Capteur ...";
            labelMsgemp.Visible = true;
            pB_empr.Visible = true;
        }
        private void pictureRing_Click(object sender, EventArgs e)
        {
            labelMsgemp.Text = "Veuillez poser votre Annulaire sur le Capteur ...";
            labelMsgemp.Visible = true;
            pB_empr.Visible = true;
        }
        private void pictureLittle_Click(object sender, EventArgs e)
        {
            labelMsgemp.Text = "Veuillez poser votre Auriculaire sur le Capteur ...";
            labelMsgemp.Visible = true;
            pB_empr.Visible = true;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            panelconsentement.Visible = false;
            panelEnrollement.Visible = false;
            panelVerif.Visible = true;
            panelEmpreinte.Visible = false;
            panelSignaletique.Visible = false;
            panelResultatRecherche.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panelconsentement.Visible = false;
            panelEnrollement.Visible = false;
            panelVerif.Visible = true;
            panelEmpreinte.Visible = false;
            panelSignaletique.Visible = false;
            panelResultatRecherche.Visible = false;
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            //**********************************Si Je suis client BNI*********************************
            if (radioButton5.Checked)
            {
                AvertissementPopup Ap = new AvertissementPopup("EN ATTENTE D'INTEGRATION DE L'API BNI...");
                Ap.ShowDialog();
            }
            //**********************************Si Je suis client BNI*********************************

            //**********************************Si je ne suis pas client******************************
            if (radioButton4.Checked)
            {
                if (radioButton1.Checked)
                {
                    button6.Visible = false;
                    //********************************************PAR SAISIE MANUELLE *************************************************************
                    panelconsentement.Visible = false;
                    panelEnrollement.Visible = false;
                    panelVerif.Visible = false;
                    panelEmpreinte.Visible = false;
                    panelSignaletique.Visible = true;
                    panelResultatRecherche.Visible = false;
                    label1.Text = "SAISIE DES INFORMATION SIGNALETIQUE";
                    //********************************************PAR SAISIE MANUELLE *************************************************************
                }
                else if (radioButton2.Checked)
                {
                    button6.Visible = false;
                    //********************************************Appeler API CNAM *************************************************************
                    AvertissementPopup Ap = new AvertissementPopup("EN ATTENTE DU COMPTE CNAM DE LA BNI...");
                    Ap.ShowDialog();
                    //********************************************Appeler API CNAM *************************************************************
                }
                else
                {
                    button6.Visible = false;
                    //********************************************PAR NFC **************************************************************
                    //var rett = await LanceAppliRecupData();

                    //********************************************PAR NFC **************************************************************
                }
            }
            //**********************************Si je ne suis pas client******************************

        }


        public async Task<(bool, string)> LanceAppliRecupData()
        {
            Process process = new Process();
            process.StartInfo.FileName = Path.Combine(LanceAPP, "NFC.bat");
            process.StartInfo.Arguments = " newCni,CI003054046,19931112,20320128";

            try
            {
                process.WaitForExit(10000);
                process.Start();
                return (true, process.StandardOutput.ReadToEnd());
            }
            catch (Exception ex)
            {
                return (false, "Une erreur s'est produite lors du démarrage de l'application : " + ex.Message);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            panelconsentement.Visible = true;
            panelEnrollement.Visible = false;
            panelVerif.Visible = false;
            panelEmpreinte.Visible = false;
            panelSignaletique.Visible = false;
            panelResultatRecherche.Visible = false;
            label1.Text = "CONSENTEMENT";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                button3.Enabled = true;
            }
            else
            {
                button3.Enabled = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panelconsentement.Visible = false;
            panelEnrollement.Visible = false;
            panelVerif.Visible = false;
            panelEmpreinte.Visible = true;
            panelSignaletique.Visible = false;
            panelResultatRecherche.Visible = false;
            label1.Text = "EMPREINTES";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            panelconsentement.Visible = false;
            panelEnrollement.Visible = false;
            panelVerif.Visible = true;
            panelEmpreinte.Visible = false;
            panelSignaletique.Visible = false;
            panelResultatRecherche.Visible = false;
            label1.Text = "VERIFICATION";
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            panelconsentement.Visible = false;
            panelEnrollement.Visible = false;
            panelVerif.Visible = true;
            panelEmpreinte.Visible = false;
            panelSignaletique.Visible = false;
            panelResultatRecherche.Visible = false;
            label1.Text = "VERIFICATION";

        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            panelconsentement.Visible = false;
            panelEnrollement.Visible = false;
            panelVerif.Visible = true;
            panelEmpreinte.Visible = false;
            panelSignaletique.Visible = false;
            panelResultatRecherche.Visible = false;
            label1.Text = "VERIFICATION";
        }

        private void pB_empr_Click(object sender, EventArgs e)
        {
            pB_empr.Visible = false;
            labelMsgemp.Visible = false;
        }
    }
}
