using FingerPrintEcranPrincipal.Reponses;
using FingerPrintEcranPrincipal.Request;
using Futronic.Scanners.FS26X80;
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
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FingerPrintEcranPrincipal
{
    public partial class frm_Acceuil : Form
    {
        //**********************GESTION EMPREINTE DIGITALE ****************************************
        private DeviceAccessor accessor = new();
        private FingerprintDevice device;
        private bool _isDetecteMode = false;
        //**********************GESTION EMPREINTE DIGITALE ****************************************





        private readonly string URIBD = Outils.recup().baseUriApi.ToString();
        private string LanceAPP = Outils.recup().NFCappLaunch.ToString();
        public frm_Acceuil()
        {
            InitializeComponent();
            device = accessor.AccessFingerprintDevice();
            Task.Run(() =>
            {
                device.StartFingerDetection();
                device.SwitchLedState(false, true);

                device.SwitchLedState(false, false);
            });
        }




        private void frm_Acceuil_Load(object sender, EventArgs e)
        {
            lbl_messageinput.Visible = true;
            lbl_messageinput.Text = "Veuillez renseigner le numero CNI SVP ...";
            rd_numPiece.Checked = true;


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
        private async void radiobutton_CheckedChanged(object sender, EventArgs e)
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

                        //***************GESTION ACTIVATION DE L'EMPREINTE *************************
                        if (rd_empreinte.Checked)
                        {
                            activedetectionFingerprint();
                        }

                        else
                        {
                            desactivedetectionFingerprint();
                        }
                        break;

                    default:
                        break;

                }
                //***************GESTION ACTIVATION DE L'EMPREINTE *************************
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
            textBox8.Text = "OGOU SACHA MAXIME";
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
                    string url = URIBD + "/Getone";
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
                    HttpResponseMessage response = await client.PostAsync(url, content);

                    switch (response.StatusCode)
                    {
                        case System.Net.HttpStatusCode.OK:
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
                                    OuverturePanelConditionUtilisation();
                                }
                                else
                                {
                                    textBox1.Text = String.Empty;
                                }
                            }
                            else
                            {
                                textBox1.Text = string.Empty;
                                DTORetCompImage[]? dtretemp;
                                dtretemp = JsonConvert.DeserializeObject<DTORetCompImage[]>(GN.data.ToString());
                                DTORetCompImage? dtre = dtretemp[0];
                                //**********************************Appeler la method pour renseigner les differents valeur retrouver sur l'interface*****************
                                //**********************************Appeler la method pour renseigner les differents valeur retrouver sur l'interface*****************
                                //OuverturePanelSaisieEnrollement();

                                //affichage du pannel
                                OuverturePanelResultatrecherche();
                                //Remise a zero des champs
                                voidMiseAblancSearchTiers();
                                //Remplir les champs avec le resultat
                                RempliChampsderecherche(dtre);
                            }

                            break;
                        default:
                            MessageBox.Show("Erreur Systeme veuillez contacter l'administrateur");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur Systeme veuillez contacter l'administrateur");
            }

        }
        //**********************************VERIFICATION DES RETOUR AFIN DE CALL LE PANEL ADEQUAT***********************************************



        private async void pictureThumb_Click(object sender, EventArgs e)
        {
            labelMsgemp.Text = "Veuillez poser votre pouce sur le Capteur ...";
            labelMsgemp.Visible = true;
            pB_empr.Visible = true;
            
        }

        private async void activedetectionFingerprintEnrollement(object? sender, EventArgs e)
        {
            device.FingerDetected += recupempreinteEnrollement;
            _isDetecteMode = true;
            Action<string> affichagedumessageutilisateurA = new Action<string>((message) =>
            {
                // Appelez la méthode de mise à jour de l'interface utilisateur avec le message
                this.label2.Text = "Veuillez poser votre empreinte sur le capteur"; //cni

            });
        }

        private async void recupempreinteEnrollement(object? sender, EventArgs e)
        {
            try
            {
                var ber = device.ReadFingerprint();
                //*********************************desactivation de la detection automatique dee l'empreinte***************************************
                //desactivedetectionEnrollement();
                //*********************************desactivation de la detection automatique dee l'empreinte***************************************

                var tempFile = Guid.NewGuid().ToString();
                var tempFileall = Path.Combine(Outils.recup().tempfolder, tempFile);
                var tmpBmpFile = Path.ChangeExtension(tempFileall, "bmp");
                ber.Save(tmpBmpFile);
                //return tmpBmpFile;
            }
            catch (Exception ex)
            {

            }
            finally 
            { 

            }
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
            OuverturePanelVerification();
        }
        private void pB_empr_Click(object sender, EventArgs e)
        {
            pB_empr.Visible = false;
            labelMsgemp.Visible = false;
        }
        private async void recupempreinte(object? sender, EventArgs e)
        {
            try
            {
                var ber = device.ReadFingerprint();
                //*********************************desactivation de la detection automatique dee l'empreinte***************************************
                desactivedetectionFingerprint();
                //*********************************desactivation de la detection automatique dee l'empreinte***************************************

                var tempFile = Guid.NewGuid().ToString();
                var tempFileall = Path.Combine(Outils.recup().tempfolder, tempFile);
                var tmpBmpFile = Path.ChangeExtension(tempFileall, "bmp");
                ber.Save(tmpBmpFile);


                //******************convertir en base 64 *******************
                string base64String = ConvertToBase64(tmpBmpFile);
                //******************convertir en base 64 *******************

                using (HttpClient client = new HttpClient())
                {
                    string _Uri = URIBD + "/Postimage";
                    DTOsendCompImage dtoSendCNI = new DTOsendCompImage { image = base64String };
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(dtoSendCNI), Encoding.UTF8, "application/json");

                    HttpResponseMessage _response = await client.PostAsync(_Uri, content);

                    switch (_response.StatusCode)
                    {
                        case System.Net.HttpStatusCode.OK:
                            string parseur = _response.Content.ReadAsStringAsync().Result;
                            GeneraleResponse ResultJson = JsonConvert.DeserializeObject<GeneraleResponse>(parseur);
                            if (ResultJson.data == "[  ]")
                            {
                                MessageBox.Show("Aucun resultat trouvé pour le numero Unique ");
                            }
                            else
                            {

                                DTORetCompImage? dtretemp;
                                dtretemp = JsonConvert.DeserializeObject<DTORetCompImage>(ResultJson.data.ToString());
                                //affichage du pannel
                                OuverturePanelResultatrecherche();
                                //Remise a zero des champs
                                voidMiseAblancSearchTiers();
                                //Remplir les champs avec le resultat
                                RempliChampsderecherche(dtretemp);

                            }
                            break;
                        default:
                            MessageBox.Show("Erreur Systeme veuillez contacter l'administrateur");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Erreur dans a detection de l'empreinte veuillez contacter l'administrateur {ex.Message}");
            }
            finally
            {
                activedetectionFingerprint();
            }
        }
         private async void recupempreinteEnroll(object? sender, EventArgs e)
        {
            try
            {
                var ber = device.ReadFingerprint();
                //*********************************desactivation de la detection automatique dee l'empreinte***************************************
                    desactivedetectionFingerprint();
                //*********************************desactivation de la detection automatique dee l'empreinte***************************************

                var tempFile = Guid.NewGuid().ToString();
                var tempFileall = Path.Combine(Outils.recup().tempfolder, tempFile);
                var tmpBmpFile = Path.ChangeExtension(tempFileall, "bmp");
                ber.Save(tmpBmpFile);
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Erreur dans a detection de l'empreinte veuillez contacter l'administrateur {ex.Message}");
            }
            finally
            {
                activedetectionFingerprint();
            }
        }
        public string ConvertToBase64(string data)
        {
            // Chemin de l'image à convertir en base64
            string imagePath = data;

            // Lecture de l'image en tant qu'array de bytes
            byte[] imageBytes = File.ReadAllBytes(imagePath);

            // Conversion de l'array de bytes en une chaîne Base64
            string base64String = Convert.ToBase64String(imageBytes);

            // Affichage de la chaîne Base64
            return base64String;
        }
        //************************************OUVERTURE DES DIFFERENTES PAGES ********************************************
        public void OuverturePanelResultatrecherche()
        {
            panelCondutil.Visible = false;
            panelconsentement.Visible = false;
            panelEnrollement.Visible = false;
            panelVerif.Visible = false;
            panelEmpreinte.Visible = false;
            panelSignaletique.Visible = false;
            panelResultatRecherche.Visible = true;
            label1.Text = "VERIFICATION DES INFORMATION SIGNALETIQUE";
        }
        public void OuverturePanelConditionUtilisation()
        {
            panelCondutil.Visible = true;
            panelconsentement.Visible = false;
            panelEnrollement.Visible = false;
            panelVerif.Visible = false;
            panelEmpreinte.Visible = false;
            panelSignaletique.Visible = false;
            panelResultatRecherche.Visible = false;
            label1.Text = "CONDITIONS D'UTILISATION";
        }
        public void OuverturePanelEnrollement()
        {
            panelCondutil.Visible = false;
            panelconsentement.Visible = false;
            panelEnrollement.Visible = true;
            panelVerif.Visible = false;
            panelEmpreinte.Visible = false;
            panelSignaletique.Visible = false;
            panelResultatRecherche.Visible = false;
            label1.Text = "ENROLLEMENT";
        }
        public void OuverturePanelVerification()
        {
            panelCondutil.Visible = false;
            panelconsentement.Visible = false;
            panelEnrollement.Visible = false;
            panelVerif.Visible = true;
            panelEmpreinte.Visible = false;
            panelSignaletique.Visible = false;
            panelResultatRecherche.Visible = false;
            label1.Text = "VERIFICATION";
        }
        public void OuverturePanelSaisieEnrollement()
        {
            panelCondutil.Visible = false;
            panelconsentement.Visible = false;
            panelEnrollement.Visible = false;
            panelVerif.Visible = false;
            panelEmpreinte.Visible = false;
            panelSignaletique.Visible = true;
            panelResultatRecherche.Visible = true;
            label1.Text = "SAISIE DES INFORMATION SIGNALETIQUE";
        }
        //************************************OUVERTURE DES DIFFERENTES PAGES ********************************************
        public async void voidMiseAblancSearchTiers()
        {
            Action<string> miseaublancDelegate = new Action<string>((message) =>
            {
                // Appelez la méthode de mise à jour de l'interface utilisateur avec le message
                this.label31.Text = "CIXXXXXXXXXXXX"; //cni
                this.label29.Text = "XXXXXXXXXXXXXXXXXXXX"; //Nom
                this.label21.Text = "XXX XXXXXXX XXXXXX XXXXX"; //Prenom
                this.label16.Text = "XXXXXXXXXXXXXXXX"; //Nationnalite
                this.label18.Text = "JJ/MM/AAAA"; //date de naissance
                this.label15.Text = "M / F";//sexe
                this.label12.Text = "XXX";//taille

                this.label11.Text = "";
                this.label13.Text = "XXXXXXXXXXXXXXXX";//lieu de naissance
                this.label25.Text = "JJ/MM/AAAA";//date d'expiration
                this.label53.Text = "XXXXXXXXXXXXXXXX";//NNI
                this.label51.Text = "XXXXXXXXXXXXXXXX";//Profession
                this.label34.Text = "JJ/MM/AAAA";//DATE Emission
                this.label40.Text = "XXXXXXXXXXXXXXXX";//lieu d'emission
            });

            this.Invoke(miseaublancDelegate, "Mise à jour depuis le thread secondaire avec des paramètres");
        }
        public async void RempliChampsderecherche(DTORetCompImage? dtretemp)
        {
            Action<string> remplirRetRequetteUI = new Action<string>((message) =>
            {

                label31.Text = dtretemp.r_num_cni ?? "";
                label29.Text = dtretemp.r_nom ?? "";
                label21.Text = dtretemp.r_prenom ?? "";
                label15.Text = (dtretemp.r_sexe.ToString() == "F") ? "Femme" : "Homme";
                label12.Text = dtretemp.r_taille.ToString() + " Cm" ?? "";
                label16.Text = dtretemp.r_nationnalite ?? "";
                label13.Text = dtretemp.r_lieu_de_naissance;
                label53.Text = dtretemp.r_NNI ?? "";
                label51.Text = dtretemp.r_profession ?? "";
                label25.Text = dtretemp.r_num_unique;
                label34.Text = FormatMyDate(dtretemp.r_date_emission);
                label18.Text = FormatMyDate(dtretemp.r_date_naissance);
                label25.Text = FormatMyDate(dtretemp.r_date_expiration);
                label40.Text = dtretemp.r_lieu_emission;
            });

            this.Invoke(remplirRetRequetteUI, "MISE A JOUR DE L'INTERFACE GRAPHIQUE");

        }
        private string FormatMyDate(string dateString)
        {
            string formattedDate = "";

            if (DateTime.TryParseExact(dateString, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out var dateValue))
            {
                formattedDate = dateValue.ToString("dd/MM/yyyy");
                //Console.WriteLine(formattedDate); // Output: 25/01/2023
            }

            return formattedDate;
        }
        private async Task desactivedetectionFingerprint()
        {
            device.FingerDetected -= recupempreinte;
            _isDetecteMode = false;
            Action<string> affichagedumessageutilisateur = new Action<string>((message) =>
            {
                // Appelez la méthode de mise à jour de l'interface utilisateur avec le message
                this.label2.Text = "Vous pouvez retirer votre doigt du lecteur";
                this.label3.ForeColor = Color.Red;
                device.SwitchLedState(false, true);//cni

            });

            this.Invoke(affichagedumessageutilisateur, "Mise à jour depuis le thread secondaire avec des paramètres");
        }
        private async Task activedetectionFingerprint()
        {
            device.FingerDetected += recupempreinte;
            _isDetecteMode = true;
            Action<string> affichagedumessageutilisateurA = new Action<string>((message) =>
            {
                // Appelez la méthode de mise à jour de l'interface utilisateur avec le message
                this.label2.Text = "Veuillez poser votre empreinte sur le capteur"; //cni

            });

            this.Invoke(affichagedumessageutilisateurA, "Mise à jour depuis le thread secondaire avec des paramètres");
        }
        private void btn_suivant_Click(object sender, EventArgs e)
        {
            OuverturePanelEnrollement();
        }
        private void chkConsend_CheckedChanged(object sender, EventArgs e)
        {
            if (chkConsend.Checked)
            {
                btn_suivant.Enabled = true;
            }
            else
            { btn_suivant.Enabled = false; }
        }
    }
}
