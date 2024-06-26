﻿using FingerPrintEcranPrincipal.Reponses;
using FingerPrintEcranPrincipal.Request;
using Futronic.Scanners.FS26X80;
using Newtonsoft.Json;
using Serilog;
using Serilog.Core;
using System;
using System.Collections;
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
        //private List<KeyValuePair<string, string>> listTypePiece = new List<KeyValuePair<string, string>>();
        private ILogger _logger;
        private AdminsystemeParam ParamApp = Outils.recupAdminParam();
        private DtoEnroll _dtoEnroll;
        private DtoEmpreinte _empreinte = new DtoEmpreinte();
        private readonly string URIBD = Outils.recup().baseUriApi.ToString();
        private string LanceAPP = Outils.recup().NFCappLaunch.ToString();


        private async void chargeDataTypePiece(string _data)
        {
            using (HttpClient _client = new HttpClient())
            {
                try
                {
                    // Envoyer une requête GET à une URL spécifique
                    HttpResponseMessage response = await _client.GetAsync(URIBD + $"/gettypepiece/{_data}");

                    // Vérifier si la réponse est réussie
                    if (response.IsSuccessStatusCode)
                    {
                        // Lire le contenu de la réponse
                        string responseBody = await response.Content.ReadAsStringAsync();

                        GeneraleResponse GN1 = JsonConvert.DeserializeObject<GeneraleResponse>(responseBody);
                        if (GN1.data == "[  ]")
                        {
                            AvertissementPopup Avs = new AvertissementPopup("Erreur Systeme veuillez contacter l'administrateur");
                            Avs.ShowDialog();
                            Application.Exit();
                        }
                        else
                        {
                            BindingList<RetourComboType>? ret_type = JsonConvert.DeserializeObject<BindingList<RetourComboType>>(GN1.data);
                            if (ret_type != null)
                            {
                                txt_typepiece.Items.Clear();
                                txt_typepiece.DataSource = ret_type;
                                // Définissez la propriété DisplayMember pour afficher la valeur de la paire clé/valeur

                                txt_typepiece.DisplayMember = "libelle_type_piece";

                                // Définissez la propriété ValueMember pour spécifier la valeur de la paire clé/valeur
                                txt_typepiece.ValueMember = "id_type_piece";

                                // Sélectionnez un élément par défaut si nécessaire
                                //txt_typepiece.SelectedIndex = 0; // Pour sélectionner le premier élément
                            }
                        }
                    }
                    else
                    {
                        AvertissementPopup vre = new AvertissementPopup("Erreur Systeme de recuperation, Contactez l'administrateur");
                        vre.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    Log.Error($"Erreur systeme =================================================> {ex.Message}");
                    AvertissementPopup vre = new AvertissementPopup("Erreur Systeme veuillez Contactez l'administrateur");
                    vre.ShowDialog();

                }
            }
        }




        public frm_Acceuil()
        {
            InitializeComponent();

            _logger = Log.ForContext<frm_Acceuil>();

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
            //chargeDataTypePiece();
            keyValuePairs.Add(new KeyValuePair<string, string>("M", "Homme"));
            keyValuePairs.Add(new KeyValuePair<string, string>("F", "Femme"));
            toolTip1.SetToolTip(button7, "Aide");
            toolTip2.ToolTipTitle = "Parametre";
            toolTip2.SetToolTip(button16, "Parametre Systeme");

            var dt = DateTime.Now.Date.ToString().Substring(0, 10).Replace("/", "");
            Serilog.Log.Logger = new LoggerConfiguration()
                   .WriteTo.File($"logFolder/log_{dt}_forms.txt") // Écrit les logs dans un fichier
                   .CreateLogger();

            lbl_messageinput.Visible = true;
            lbl_messageinput.Text = "Veuillez renseigner le numero CNI SVP ...";
            rd_numPiece.Checked = true;

            remplirComboSexe();
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
                        //desactivedetectionFingerprint();
                        lbl_messageinput.Visible = true;
                        textBox1.Visible = true;
                        pictureBox2.Visible = false;
                        label2.Visible = false;
                        button1.Visible = true;
                        lbl_messageinput.Text = "Veuillez renseigner le numero CNI SVP ...";
                        break;

                    case 1:
                        //desactivedetectionFingerprint();
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
                                panelVerif.Enabled = false;

                                //ChargementEmpreinte Ch = new ChargementEmpreinte();
                                //Ch.Show();
                                await activedetectionFingerprint();
                                //Ch.Close();
                            }

                            else
                            {
                                desactivedetectionFingerprint();
                            }
                        }

                        break;

                    default:
                        desactivedetectionFingerprint();
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
                panelEnrollement.Enabled = false;
                PopupInformation Pop = new PopupInformation();
                Pop.ShowDialog();
                string Numpiece = "";
                string dateNaiss = "";
                string dateFin = "";
                string typePiece = "";
                panelEnrollement.Enabled = true;
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

                        _logger.Information("Erreur " + res.Item1 + res.Item2);
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
            finally
            {
                panelEnrollement.Enabled = true;
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
                AvertissementPopup AVSp = new AvertissementPopup("Veuillez renseigner des valeurs correctes");
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
            comboSexgenre.Items.Clear();

            // Parcourez chaque paire clé-valeur dans la liste et ajoutez-la au ComboBox
            //foreach (var kvp in keyValuePairs)
            //{
            //    comboSexgenre.Items.Add(kvp);

            //}
            comboSexgenre.DataSource = keyValuePairs;
            comboSexgenre.ValueMember = "Key";
            comboSexgenre.DisplayMember = "Value";


            // Définissez la propriété DisplayMember pour afficher la valeur
            //comboSexgenre.DisplayMember = "Value";

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
                    chargeDataTypePiece("MANUEL");
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



                }
            }

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
            DateTime gt = new DateTime(1900, 01, 01);
            if (textBox7.Text == string.Empty) { AvertissementPopup tt1 = new AvertissementPopup("Veuillez renseigner votre nom"); tt1.ShowDialog(); return; }
            if (textBox8.Text == string.Empty) { AvertissementPopup tt2 = new AvertissementPopup("Veuillez renseigner votre prenom"); tt2.ShowDialog(); return; }
            if (dateTimePicker1.Value < gt) { AvertissementPopup tt3 = new AvertissementPopup("Veuillez renseigner une date de naissance valide"); tt3.ShowDialog(); return; }
            if (dateTimePicker2.Value < gt) { AvertissementPopup tt4 = new AvertissementPopup("Veuillez renseigner une date d'emission valide"); tt4.ShowDialog(); return; }
            if (dateTimePicker3.Value < gt) { AvertissementPopup tt5 = new AvertissementPopup("Veuillez renseigner une date d'expiration valide"); tt5.ShowDialog(); return; }


            if (dateTimePicker1.Value.Date >= dateTimePicker2.Value.Date) { AvertissementPopup tt1 = new AvertissementPopup("Date de naissance non valide"); tt1.ShowDialog(); return; }
            if (dateTimePicker2.Value.Date >= dateTimePicker3.Value.Date) { AvertissementPopup tt1 = new AvertissementPopup("Date d'emission non valide"); tt1.ShowDialog(); return; }
            if (dateTimePicker1.Value.Date >= dateTimePicker3.Value.Date) { AvertissementPopup tt1 = new AvertissementPopup("Date de naissance non valide"); tt1.ShowDialog(); return; }

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

                txt_sexe = (string)comboSexgenre.SelectedValue,

                date_emiss_cni = string.Format("{0:yyyy/MM/dd}", dateTimePicker2.Value.Date),
                date_expir_cni = string.Format("{0:yyyy/MM/dd}", dateTimePicker3.Value.Date),
                date_naiss = string.Format("{0:yyyy/MM/dd}", dateTimePicker1.Value.Date),
                type_piece = (int)txt_typepiece.SelectedValue

            };
            Log.Information($"RECUPERATION COMBO SEXE ==================> INDEX {comboSexgenre.SelectedIndex.ToString()} ValueMember {comboSexgenre.ValueMember} SelectedValue {comboSexgenre.SelectedValue})");
            Log.Information($"RECUPERATION TYPE DE PIECE ==================> INDEX {txt_typepiece.SelectedIndex.ToString()} ValueMember {txt_typepiece.ValueMember} SelectedValue {txt_typepiece.SelectedValue})");
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
            OuverturePanelVerification();
        }
        private void button9_Click_1(object sender, EventArgs e)
        {
            OuverturePanelVerification();

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
            bool Empreinte_is_passed = false;
            try
            {

                miseAjour(true);
                var ber = device.ReadFingerprint();
                //*********************************desactivation de la detection automatique dee l'empreinte***************************************
                desactivedetectionFingerprint();


                //////////////************************AFFICHAGE DU MESSAGE DU RETRAIT DE L'EMPREINTE **************************
                Action<string> TDDMESSAGE = new Action<string>((message) =>
                {
                    // Appelez la méthode de mise à jour de l'interface utilisateur avec le message
                    label88.Text = "Veillez retirer votre main du capteur";
                    label88.Visible = true;


                });

                this.Invoke(TDDMESSAGE, "Mise à jour depuis le thread secondaire avec des paramètres");
                //////////////************************AFFICHAGE DU MESSAGE DU RETRAIT DE L'EMPREINTE **************************




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
                                //////////////************************AFFICHAGE DU MESSAGE DU RETRAIT DE L'EMPREINTE **************************
                                Action<string> TDD1MESSAGE = new Action<string>((message) =>
                                {
                                    // Appelez la méthode de mise à jour de l'interface utilisateur avec le message
                                    OuverturePanelResultatrecherche();
                                    //Remise a zero des champs
                                    voidMiseAblancSearchTiers();
                                    //Remplir les champs avec le resultat
                                    RempliChampsderecherche(dtretemp);

                                });

                                this.Invoke(TDD1MESSAGE, "Mise à jour depuis le thread secondaire avec des paramètres");
                                //////////////************************AFFICHAGE DU MESSAGE DU RETRAIT DE L'EMPREINTE **************************

                            }
                            break;
                        case System.Net.HttpStatusCode.BadRequest:
                            bool bres = false;
                            DemandeMessage Dm = new DemandeMessage();
                            //pictureBox3.Visible = false;
                            //panelVerif.Enabled = true;
                            Dm.ShowDialog();
                            if (Dm.ret)
                            {
                                //////////////************************AFFICHAGE DU MESSAGE DU RETRAIT DE L'EMPREINTE **************************
                                Action<string> TDD2MESSAGE = new Action<string>((message) =>
                                {
                                    // Appelez la méthode de mise à jour de l'interface utilisateur avec le message
                                    OuverturePanelConditionUtilisation();
                                    Empreinte_is_passed = true;
                                });

                                this.Invoke(TDD2MESSAGE, "Mise à jour depuis le thread secondaire avec des paramètres");
                                //////////////************************AFFICHAGE DU MESSAGE DU RETRAIT DE L'EMPREINTE **************************
                            }
                            else
                            {
                                Action<string> TDD3MESSAGE = new Action<string>((message) =>
                                {
                                    // Appelez la méthode de mise à jour de l'interface utilisateur avec le message
                                    textBox1.Text = String.Empty;

                                });

                                this.Invoke(TDD3MESSAGE, "Mise à jour depuis le thread secondaire avec des paramètres");


                            }
                            break;
                        default:
                            //string parseur1 = _response.Content.ReadAsStringAsync().Result;
                            MessageBox.Show("Erreur Systeme veuillez contacter l'administrateur");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                //miseAjour(true);
                Log.Error(ex.Message);
                MessageBox.Show($"Erreur dans a detection de l'empreinte veuillez contacter l'administrateur {ex.Message}");
            }
            finally
            {
               

                Action<string> TDD4MESSAGE = new Action<string>((message) =>
                {
                    miseAjour(false);
                    if (Empreinte_is_passed)
                    {
                        desactivedetectionFingerprint();
                    }
                    else
                    {
                        activedetectionFingerprint();
                    }
                    // Appelez la méthode de mise à jour de l'interface utilisateur avec le message
                    label88.Visible = false;
                    Empreinte_is_passed = false;
                });

              

                this.Invoke(TDD4MESSAGE, "Mise à jour depuis le thread secondaire avec des paramètres");

            }
        }







        private void miseAjour(bool isactive)
        {

            Action<string> miseajourMessage = new Action<string>((message) =>
            {
                if (isactive)
                {
                    pictureBox3.Visible = true;
                    panelVerif.Enabled = false;
                }
                else
                {
                    pictureBox3.Visible = false;
                    panelVerif.Enabled = true;
                }

            });
            this.Invoke(miseajourMessage, "Mise à jour depuis le thread secondaire avec des paramètres");

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
            label1.Text = "VERIFICATION DES INFORMATIONS SIGNALETIQUES";
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
            label1.Text = "ENRÔLEMENT";
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
            label1.Text = "SAISIE DES INFORMATIONS SIGNALETIQUES";
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
            label88.Visible = true;
            pictureBox3.Visible = true;
            panelVerif.Enabled = false;

            device.FingerDetected += recupempreinte;
            _isDetecteMode = true;
            Action<string> affichagedumessageutilisateurA = new Action<string>((message) =>
            {
                // Appelez la méthode de mise à jour de l'interface utilisateur avec le message
                this.label2.Text = "Veuillez poser votre empreinte sur le capteur"; //cni

            });

            this.Invoke(affichagedumessageutilisateurA, "Mise à jour depuis le thread secondaire avec des paramètres");

            label88.Visible = false;
            pictureBox3.Visible = false;
            panelVerif.Enabled = true;
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
            if (ParamApp.Use_empreinte)
            {

                DtoEmpreinte dtoEmpreinte = new DtoEmpreinte
                {
                    pouce = Convert.ToBase64String(Thumb),
                    index = Convert.ToBase64String(IndexFinger),
                    majeur = Convert.ToBase64String(MiddleFinger),
                    auriculaire = Convert.ToBase64String(LittleFinger),
                    annulaire = Convert.ToBase64String(RingFinger)


                };
                _dtoEnroll.empreintes= dtoEmpreinte;
            }
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
                panelRecapitulatif.Enabled = false;
                pictureBox3.Visible = true;
                Log.Information($"Contenue a evoyer au serveur ====>{JsonConvert.SerializeObject(_dtoEnroll)}");
                //_dtoEnroll.empreintes = null;
                (bool, string) mavarret = await PostDataToserver(_dtoEnroll);

                Log.Information($"REt====>{mavarret.Item1} contenue est {mavarret.Item2}");

                if (mavarret.Item1)
                {
                    AvertissementPopup Avp = new AvertissementPopup("Enrôlement effectué avec Succes");
                    Avp.ShowDialog();
                    textBox22.Text = string.Empty;
                    OuverturePanelVerification();

                    ViderlesChampsConcerne();
                }
                else
                {
                    AvertissementPopup Avp = new AvertissementPopup(mavarret.Item2);
                    Avp.ShowDialog();
                }


            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                MessageBox.Show("Erreur Systeme veuillez contacter l'administrateur");
            }
            finally
            {
                panelRecapitulatif.Enabled = true;
                pictureBox3.Visible = false;
            }
        }

        private void ViderlesChampsConcerne()
        {
            textBox1.Text = string.Empty;
            textBox6.Text = string.Empty;
            textBox7.Text = string.Empty;
            textBox8.Text = string.Empty;
            textBox9.Text = string.Empty;

            txtLieuNaissance.Text = string.Empty;
            txt_nationnalite.Text = string.Empty;
            txt_proffession.Text = string.Empty;
            txt_perename.Text = string.Empty;
            txt_merename.Text = string.Empty;
            txt_numpiece.Text = string.Empty;
            txt_numuniq.Text = string.Empty;
            txt_lieuemission.Text = string.Empty;
            txt_NNi.Text = string.Empty;

            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            dateTimePicker3.Value = DateTime.Now;

            checkBox1.Checked = false;
            chkConsend.Checked = false;

            pictureIndex.Image = null;
            pictureMiddle.Image = null;
            pictureRing.Image = null;
            pictureThumb.Image = null;
            pictureLittle.Image = null;



            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
            radioButton5.Checked = false;
            panelporteur.Visible = false;
            panelclient.Visible = false;

        }

        public async Task<(bool, string)> PostDataToserver(DtoEnroll sendEmpreinte)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string _Uri = Outils.recup().baseUriApi + "/Enrolle";
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(sendEmpreinte), Encoding.UTF8, "application/json");
                    HttpResponseMessage _response = await client.PostAsync(_Uri, content);


                    Log.Information($"Contenue de la requette===================================> {JsonConvert.SerializeObject(sendEmpreinte)}");
                    string parseur = _response.Content.ReadAsStringAsync().Result;
                    GeneraleResponse ResultJson = JsonConvert.DeserializeObject<GeneraleResponse>(parseur);



                    Log.Information($"Retour requette HTTP====>{parseur}");
                    //logger.Information($" Http status code {_response.StatusCode} Code retour {ResultJson?.code} Données presentes sont : {ResultJson?.data}");
                    switch (_response.StatusCode)
                    {
                        case System.Net.HttpStatusCode.OK:

                            return (true, "Enrôlement effectué avec Succes");

                        case System.Net.HttpStatusCode.InternalServerError:
                            switch (ResultJson.code)
                            {
                                case "ERR0018 ":
                                    return (false, "erreur dans la sauvegarde de l'empreinte");

                                case "ERR0016 ":
                                    return (false, "erreur de récupération des paramètre de la Base de données");

                                case "ERR0020":
                                    return (false, "Erreur système veuillez contacter l'administrateur");
                                default:
                                    return (false, "Erreur 500 Systeme veuillez contacter l'administrateur");
                            }
                        case System.Net.HttpStatusCode.BadRequest:
                            switch (ResultJson.code)
                            {
                                case "ERR0010 ":
                                    return (false, "Valeur invalide pour l'un des champs");

                                case "ERR0011 ":
                                    return (false, "Format invalide pour la date de naissance");

                                case "ERR0012":
                                    return (false, "Format invalide pour la date d'émission de la Pièce");

                                case "ERR0013":
                                    return (false, "Format invalide pour la date d'expiration de la Pièce");

                                case "ERR0015":
                                    return (false, "Valeur sexe invalide");

                                case "ERR0017":
                                    return (false, ResultJson.descrition);
                                default:
                                    return (false, "Erreur 400 Systeme veuillez contacter l'administrateur ");
                            }

                        default:
                            return (false, "Erreur (default) Systeme veuillez contacter l'administrateur");

                    }
                }
                catch (Exception ex)
                {
                    return (false, "Erreur Systeme veuillez contacter l'administrateur");
                }

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //Outils.WriteContenueFichierBat();
        }


        private void remplirAvecInformationPiece(DataCarteNfc Dtnfc)
        {
            if (radioButton3.Checked)
            {
                chargeDataTypePiece("NFC");
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
                dateTimePicker2.Value = DateTime.ParseExact(((Dtnfc.dateEmission == string.Empty) ? "18000101" : Dtnfc.dateEmission), "yyyyMMdd", null);
                dateTimePicker3.Value = DateTime.ParseExact(((Dtnfc.dateExpire == string.Empty) ? "18000101" : Dtnfc.dateExpire), "yyyyMMdd", null);
                dateTimePicker1.Value = DateTime.ParseExact(((Dtnfc.dateNaissance == string.Empty) ? "18000101" : Dtnfc.dateNaissance), "yyyyMMdd", null);
                txt_NNi.Text = Dtnfc.numNNI;
                comboSexgenre.SelectedValue = Dtnfc.genre;
            }
            else if (radioButton2.Checked)
            {
                chargeDataTypePiece("MANUEL");
            }


        }

        private void txt_sexe_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panelSignaletique_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
