using FingerPrintEcranPrincipal.Reponses;
using FingerPrintEcranPrincipal.Request;
using Futronic.Scanners.FS26X80;
using Newtonsoft.Json;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
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

        private int _doigt = 0;

        //***********************Gestion d'array de byte pour les empreinte*******************************
        private byte[] Thumb;
        private byte[] IndexFinger;
        private byte[] MiddleFinger;
        private byte[] RingFinger;
        private byte[] LittleFinger;
        //***********************Gestion d'array de byte pour les empreinte*******************************
        private List<KeyValuePair<string, string>> keyValuePairs = new List<KeyValuePair<string, string>>();
        private List<KeyValuePair<string, string>> listTypePiece = new List<KeyValuePair<string, string>>();
        private ILogger _logger;
        private AdminsystemeParam ParamApp = Outils.recupAdminParam();
        private DtoEnroll _dtoEnroll;
        private DtoEmpreinte _empreinte = new DtoEmpreinte();
        private readonly string URIBD = Outils.recup().baseUriApi.ToString();
        private string LanceAPP = Outils.recup().NFCappLaunch.ToString();
        public frm_Acceuil()
        {
            InitializeComponent();

            _logger = Log.ForContext<frm_Acceuil>();
            keyValuePairs.Add(new KeyValuePair<string, string>("M", "Homme"));
            keyValuePairs.Add(new KeyValuePair<string, string>("F", "Femme"));
        }
        private void frm_Acceuil_Load(object sender, EventArgs e)
        {
            try
            {
                if (ParamApp.Use_empreinte)
                {
                    device = accessor.AccessFingerprintDevice();

                    if (device == null)
                    {
                        AvertissementPopup AVp = new AvertissementPopup("Lecteur d'empreinte non connecter l'application va s'arreter");
                        AVp.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        Task.Run(() =>
                        {
                            device.StartFingerDetection();
                            device.SwitchLedState(false, true);
                            device.SwitchLedState(false, false);
                        });
                    }

                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur veuillez connecter tous les lecteurs SVP");
            }




            lbl_messageinput.Visible = true;
            lbl_messageinput.Text = "Veuillez renseigner le numero CNI SVP ...";
            rd_numPiece.Checked = true;


            OuverturePanelVerification();

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

                        if (!ParamApp.Use_empreinte)
                        {
                            AvertissementPopup Avpp = new AvertissementPopup("Votre configuration ne vous permet pas d'utiliser cette recherche");
                            Avpp.ShowDialog();
                            rd_numPiece.Checked = true;
                            lbl_messageinput.Visible = true;
                            textBox1.Visible = true;
                            pictureBox2.Visible = false;
                            label2.Visible = false;
                            button1.Visible = true;
                            lbl_messageinput.Text = "Veuillez renseigner le numero CNI SVP ...";
                            return;
                        }
                        else
                        {
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
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                PopupInformation Pop = new PopupInformation();
                Pop.ShowDialog();
                string Numpiece = "";
                string dateNaiss = "";
                string dateFin = "";
                string typePiece = "";
                if (Pop.isvalid)
                {
                    Numpiece = Pop.numeroPIECE;
                    dateNaiss = Pop._datenaissance.Date.ToString("yyyyMMdd");
                    dateFin = Pop.dateExpire.Date.ToString("yyyyMMdd"); ;
                    typePiece = Pop.Typedepiece;
                    (bool, object) res = Outils.Executerbatch(typePiece, dateNaiss, dateFin, Numpiece);

                    //**************************************************************************
                    if (res.Item1)
                    {
                        _logger.Information("retour Vrai de la fonction executer Batch 1-1");
                        OuverturePanelSaisieEnrollement();
                        _logger.Information("retour Vrai de la fonction executer Batch 1-2" + res.Item2);
                        remplirAvecInformationPiece((DataCarteNfc)res.Item2);
                        _logger.Information("retour Vrai de la fonction executer Batch 1-3");
                    }
                    else
                    {

                        _logger.Information("Erreur " + res.Item1);
                        AvertissementPopup Ap = new AvertissementPopup((string)res.Item2);
                        Ap.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);

                AvertissementPopup Ap = new AvertissementPopup("Erreur Systeme veuillez contacter l'administrateur");
                Ap.ShowDialog();
            }


            //**************************************************************************

            //*******************************************AJOUTE*****************************************

            //*******************************************AJOUTE*****************************************
        }
        //**********************************VERIFICATION DES RETOUR AFIN DE CALL LE PANEL ADEQUAT***********************************************
        private async void button1_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text == string.Empty) || (textBox1.Text.Count() < 3))
            {
                AvertissementPopup AVSp = new AvertissementPopup("Veuillez renseigner les valeur correct");
                AVSp.ShowDialog();
            }
            else
            {
                try
                {
                    pictureBox3.Visible = true;
                    panelVerif.Enabled = false;
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
                                    pictureBox3.Visible = false;
                                    panelVerif.Enabled = true;
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
                finally
                {
                    pictureBox3.Visible = false;
                    panelVerif.Enabled = true;

                }
            }
           
        }
        //**********************************VERIFICATION DES RETOUR AFIN DE CALL LE PANEL ADEQUAT***********************************************



        private async void pictureThumb_Click(object sender, EventArgs e)
        {
            labelMsgemp.Text = "Veuillez poser votre pouce sur le Capteur ...";
            labelMsgemp.Visible = true;
            pB_empr.Visible = true;
            activedetectionFingerprintEnrollement(1);


        }

        private async void activedetectionFingerprintEnrollement(int doigt)
        {
            this._doigt = doigt;
            device.FingerDetected += recupempreinteEnrollement;
            _isDetecteMode = true;
            Action<string> affichagedumessageutilisateurA = new Action<string>((message) =>
            {
                // Appelez la méthode de mise à jour de l'interface utilisateur avec le message
                this.labelMsgemp.Text = "Veuillez poser votre empreinte sur le capteur"; //cni

            });
        }
        private async void DesactivedetectionFingerprintEnrollement()
        {

            device.FingerDetected -= recupempreinteEnrollement;
            _isDetecteMode = true;
            Action<string> affichagedumessageutilisateurA = new Action<string>((message) =>
            {
                // Appelez la méthode de mise à jour de l'interface utilisateur avec le message
                this.labelMsgemp.Visible = true;
                this.pB_empr.Visible = false;
                this.labelMsgemp.Text = "Veuillez retirer votre empreinte";

            });
            this.Invoke(affichagedumessageutilisateurA, "Validation");
        }


        private async void recupempreinteEnrollement(object? sender, EventArgs e)
        {
            try
            {
                var ber = device.ReadFingerprint();
                //*********************************desactivation de la detection automatique dee l'empreinte***************************************

                //*********************************desactivation de la detection automatique dee l'empreinte***************************************

                var tempFile = Guid.NewGuid().ToString();
                var tempFileall = Path.Combine(Outils.recup().tempfolder, tempFile);
                var tmpBmpFile = Path.ChangeExtension(tempFileall, "bmp");
                ber.Save(tmpBmpFile);
                DesactivedetectionFingerprintEnrollement();

                Action<string> AffichageEmpreinte = new Action<string>((message) =>
                {
                    switch (this._doigt)
                    {
                        case 1:
                            pictureThumb.Image = ber;
                            this.Thumb = GetImageBytes(ber);
                            _empreinte.pouce = Convert.ToBase64String(this.Thumb);
                            break;
                        case 2:
                            pictureIndex.Image = ber;
                            this.IndexFinger = GetImageBytes(ber);
                            _empreinte.index = Convert.ToBase64String(this.Thumb);
                            break;
                        case 3:
                            pictureMiddle.Image = ber;
                            this.MiddleFinger = GetImageBytes(ber);
                            _empreinte.majeur = Convert.ToBase64String(this.Thumb);
                            break;
                        case 4:
                            pictureRing.Image = ber;
                            this.RingFinger = GetImageBytes(ber);
                            _empreinte.annulaire = Convert.ToBase64String(this.RingFinger);

                            break;
                        case 5:
                            pictureLittle.Image = ber;
                            this.LittleFinger = GetImageBytes(ber);
                            _empreinte.auriculaire = Convert.ToBase64String(this.Thumb);
                            break;
                    }

                });
                this.Invoke(AffichageEmpreinte, "Mise à jour depuis le thread secondaire avec des paramètres");
                //return tmpBmpFile;
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }


        private void remplirComboSexe()
        {

            // Assurez-vous que le ComboBox est vide avant de le remplir
            comboSex.Items.Clear();

            // Parcourez chaque paire clé-valeur dans la liste et ajoutez-la au ComboBox
            foreach (var kvp in keyValuePairs)
            {
                comboSex.Items.Add(kvp);
            }

            // Définissez la propriété DisplayMember pour afficher la valeur
            comboSex.DisplayMember = "Value";

        }

        private async void pictureIndex_Click(object sender, EventArgs e)
        {
            labelMsgemp.Text = "Veuillez poser index pouce sur le Capteur ...";
            labelMsgemp.Visible = true;
            pB_empr.Visible = true;
            activedetectionFingerprintEnrollement(2);
        }
        private async void pictureMiddle_Click(object sender, EventArgs e)
        {
            labelMsgemp.Text = "Veuillez poser votre Majeur sur le Capteur ...";
            labelMsgemp.Visible = true;
            pB_empr.Visible = true;
            activedetectionFingerprintEnrollement(3);
        }
        private async void pictureRing_Click(object sender, EventArgs e)
        {
            labelMsgemp.Text = "Veuillez poser votre Annulaire sur le Capteur ...";
            labelMsgemp.Visible = true;
            pB_empr.Visible = true;
            activedetectionFingerprintEnrollement(4);
        }
        private async void pictureLittle_Click(object sender, EventArgs e)
        {
            labelMsgemp.Text = "Veuillez poser votre Auriculaire sur le Capteur ...";
            labelMsgemp.Visible = true;
            pB_empr.Visible = true;
            activedetectionFingerprintEnrollement(5);
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
            OuverturePanelVerification();
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
                    OuverturePanelSaisieEnrollement();
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

                    PopupInformation Pi = new PopupInformation();

                    Pi.ShowDialog();

                    if (Pi.isvalid)
                    {

                    }


                    //********************************************PAR NFC **************************************************************
                    //LanceAPP

                    //    @"@echo off
                    //        java -jar ""C:\Users\sacha.ogou\source\repos\Futronic_REPO\FingerPrintEcranPrincipal\bin\Debug\net5.0-windows\APP NFC\scan.jar"" ""newCni,CI003054046,19931112,20320128""

                    //    pause"

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
            //************************************SI activation empreinte valide alors appeler la lecture de l'empreinte sinon passer au recapitulatif**************
            _dtoEnroll = new DtoEnroll
            {
                txt_cni = txt_numpiece.Text,
                txt_lieu_emission = txt_lieuemission.Text,
                txt_lieu_naissance = txtLieuNaissance.Text,
                txt_nationnalite = txt_nationnalite.Text,
                txt_nni = txt_NNi.Text,
                txt_nom = textBox7.Text,
                txt_num_unique = txt_numuniq.Text,
                txt_prenom = textBox8.Text,
                txt_profession = txt_proffession.Text,
                txt_taille = textBox9.Text,
                txt_sexe = (comboSex.SelectedIndex == 0) ? 'M' : 'F',
                date_emiss_cni = string.Format("{0:yyyy/MM/dd}", dateTimePicker2.Value.Date),
                date_expir_cni = string.Format("{0:yyyy/MM/dd}", dateTimePicker3.Value.Date),
                date_naiss = string.Format("{0:yyyy/MM/dd}", dateTimePicker1.Value.Date)


            };

            if (ParamApp.Use_empreinte)
            {
                OuverturePanelConsentement();
            }
            else
            {
                //********************************Ouverture Panel recapitulatif*****************************************
                //renseigner les données du panel recapitulatif
                OuverturePanelRecapitulatif();
                textBox22.AppendText(Outils.ListPropertiesAndValuesEnroll(_dtoEnroll));

            }



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
            OuverturePanelEmpreinte();
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
            panelRecapitulatif.Visible = false;
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
            panelRecapitulatif.Visible = false;
            panelCondutil.Visible = true;
            panelconsentement.Visible = false;
            panelEnrollement.Visible = false;
            panelVerif.Visible = false;
            panelEmpreinte.Visible = false;
            panelSignaletique.Visible = false;
            panelResultatRecherche.Visible = false;
            chkConsend.Checked = false;
            btn_suivant.Enabled = false;
            label1.Text = "CONDITIONS D'UTILISATION";
        }
        public void OuverturePanelEnrollement()
        {
            panelRecapitulatif.Visible = false;
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
            panelRecapitulatif.Visible = false;
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
            panelRecapitulatif.Visible = false;
            panelCondutil.Visible = false;
            panelconsentement.Visible = false;
            panelEnrollement.Visible = false;
            panelVerif.Visible = false;
            panelEmpreinte.Visible = false;
            panelSignaletique.Visible = true;
            panelResultatRecherche.Visible = false;
            label1.Text = "SAISIE DES INFORMATION SIGNALETIQUE";
        }
        public void OuverturePanelConsentement()
        {
            panelRecapitulatif.Visible = false;
            panelCondutil.Visible = false;
            panelconsentement.Visible = true;
            panelEnrollement.Visible = false;
            panelVerif.Visible = false;
            panelEmpreinte.Visible = false;
            panelSignaletique.Visible = false;
            panelResultatRecherche.Visible = false;
            label1.Text = "CONSENTEMENT";
        }
        public void OuverturePanelRecapitulatif()
        {
            panelRecapitulatif.Visible = true;
            panelCondutil.Visible = false;
            panelconsentement.Visible = false;
            panelEnrollement.Visible = false;
            panelVerif.Visible = false;
            panelEmpreinte.Visible = false;
            panelSignaletique.Visible = false;
            panelResultatRecherche.Visible = false;
            label1.Text = "RECAPITULATIF";
        }
        public void OuverturePanelEmpreinte()
        {
            panelRecapitulatif.Visible = false;
            panelCondutil.Visible = false;
            panelconsentement.Visible = false;
            panelEnrollement.Visible = false;
            panelVerif.Visible = false;
            panelEmpreinte.Visible = true;
            panelSignaletique.Visible = false;
            panelResultatRecherche.Visible = false;

            label1.Text = "EMPREINTES";
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
            _dtoEnroll = new DtoEnroll();
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

        private void button10_Click(object sender, EventArgs e)
        {
            OuverturePanelEnrollement();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OuverturePanelVerification();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            SecureMdp Smdp = new SecureMdp();
            Smdp.ShowDialog();

            if (Smdp.is_ok)
            {
                Parametre pr = new Parametre();
                pr.Show();
                ParamApp = Outils.recupAdminParam();
            }
            else
            {

            }
        }

        private void btn_precedent_Click(object sender, EventArgs e)
        {
            textBox22.Text = string.Empty;
            if (ParamApp.Use_empreinte)
            {
                OuverturePanelEmpreinte();
            }
            else
            {
                //********************************Ouverture Panel recapitulatif*****************************************
                OuverturePanelSaisieEnrollement();
            }
        }

        private void btn_annuler_Click(object sender, EventArgs e)
        {
            OuverturePanelSaisieEnrollement();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            OuverturePanelRecapitulatif();
            textBox22.AppendText(Outils.ListPropertiesAndValuesEnroll(_dtoEnroll));
        }

        private void comboSex_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private byte[]? GetImageBytes(Image image)
        {
            try
            {
                MemoryStream ms = new();
                image.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        private async void button13_Click(object sender, EventArgs e)
        {
            try
            {
                _dtoEnroll.empreintes = _empreinte;
                await PostDataToserver(_dtoEnroll);
                AvertissementPopup Avp = new AvertissementPopup("Enrolement effectué avec Succes");
                Avp.ShowDialog();
                textBox22.Text = string.Empty;
                OuverturePanelVerification();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erreur Systeme veuillez contacter l'administrateur");
            }
        }


        public async Task PostDataToserver(DtoEnroll sendEmpreinte)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string _Uri = Outils.recup().baseUriApi + "/Enrolle";
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(sendEmpreinte), Encoding.UTF8, "application/json");
                    HttpResponseMessage _response = await client.PostAsync(_Uri, content);

                    string parseur = _response.Content.ReadAsStringAsync().Result;
                    GeneraleResponse ResultJson = JsonConvert.DeserializeObject<GeneraleResponse>(parseur);
                    //logger.Information($" Http status code {_response.StatusCode} Code retour {ResultJson?.code} Données presentes sont : {ResultJson?.data}");
                    switch (_response.StatusCode)
                    {
                        case System.Net.HttpStatusCode.OK:
                            AvertissementPopup Avp = new AvertissementPopup("Enorllmeent effectué avec Succes");
                            Avp.ShowDialog();
                            OuverturePanelVerification();
                            break;
                        case System.Net.HttpStatusCode.InternalServerError:
                            switch (ResultJson.code)
                            {
                                case "ERR0018 ":
                                    MessageBox.Show("erreur dans la sauvegarde de l'empreinte");
                                    break;
                                case "ERR0016 ":
                                    MessageBox.Show("erreur de récupération des paramètre de la Base de données");
                                    break;
                                case "ERR0020":
                                    MessageBox.Show("Erreur système veuillez contacter l'administrateur");
                                    break;
                            }

                            break;
                        case System.Net.HttpStatusCode.BadRequest:
                            switch (ResultJson.code)
                            {
                                case "ERR0010 ":
                                    MessageBox.Show("Valeur invalide pour l'un des champs");
                                    break;
                                case "ERR0011 ":
                                    MessageBox.Show("Format invalide pour la date de naissance");
                                    break;
                                case "ERR0012":
                                    MessageBox.Show("Format invalide pour la date d'émission de la CNI");
                                    break;
                                case "ERR0013":
                                    MessageBox.Show("Format invalide pour la date d'expiration de la CNI");
                                    break;
                                case "ERR0015":
                                    MessageBox.Show("Valeur sexe invalide");
                                    break;
                                case "ERR0017":
                                    MessageBox.Show(ResultJson.descrition);
                                    break;
                            }
                            break;

                        default:
                            MessageBox.Show("Erreur Systeme veuillez contacter l'administrateur");
                            break;
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Erreur Systeme veuillez contacter l'administrateur");
                }

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //Outils.WriteContenueFichierBat();
        }


        private void remplirAvecInformationPiece(DataCarteNfc Dtnfc)
        {
            txt_numpiece.Text = Dtnfc.numPiece;
            txt_lieuemission.Text = Dtnfc.lieuEmission;
            txtLieuNaissance.Text = Dtnfc.lieuNaissance;
            txt_nationnalite.Text = Dtnfc.nationnalite;
            txt_NNi.Text = "";
            textBox7.Text = Dtnfc.nom;
            txt_numuniq.Text = "";
            textBox8.Text = Dtnfc.prenom;
            txt_proffession.Text = Dtnfc.profession;
            textBox9.Text = Dtnfc.taille;
            dateTimePicker2.Value = DateTime.ParseExact(Dtnfc.dateEmission, "dd/MM/yyyy", null);
            dateTimePicker3.Value = DateTime.ParseExact(Dtnfc.dateExpire, "dd/MM/yyyy", null); ;
            dateTimePicker1.Value = DateTime.ParseExact(Dtnfc.dateNaissance, "dd/MM/yyyy", null);
            comboSex.ValueMember = Dtnfc.genre;

        }

        private void txt_sexe_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
