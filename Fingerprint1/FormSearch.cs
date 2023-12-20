using Futronic.Scanners.FS26X80;
using Npgsql;
using SourceAFIS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using System.Threading;

namespace Fingerprint1
{
    public partial class FormSearch : Form
    {
        private DeviceAccessor accessor = new();
        private FingerprintDevice device;
        private bool _isDetecteMode = false;
        private NpgsqlConnection con = new NpgsqlConnection(connectionString: Outils.recup().DatabaseString);
        public void Mymethod(object sender, ElapsedEventArgs e)
        {
            try
            {
                device = accessor.AccessFingerprintDevice();
                device.StartFingerDetection();
                if (device.IsFingerPresent)
                {
                    device.SwitchLedState(true, false);
                    var ber = device.ReadFingerprint();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erreur dans l'initialisation du materiel veuillez contacter l'administrateur");
            }
            
        }
        public FormSearch()
        {
            InitializeComponent();
            chargeall();
            Task.Run(() =>
            {
                device.StartFingerDetection();
                device.SwitchLedState(false, true);

                device.SwitchLedState(false, false);
            });

        }
        private void chargeall()
        {
            //device.StopFingerDetection();
            device = accessor.AccessFingerprintDevice();
            //device.SwitchLedState(true, false);

        }
        private void btn_search_cni_Click(object sender, EventArgs e)
        {
            if (txt_search_cni.Text == string.Empty)
            {
                MessageBox.Show("Veuillez renseigner le numero de CNI a Rechercher");
            }
            else
            {
                try
                {
                    con.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = $"Select * FROM sc_enrollement.t_info_personne where r_num_cni='{txt_search_cni.Text}'";
                    NpgsqlDataReader rd = cmd.ExecuteReader();

                    if (!rd.HasRows)
                    {
                        MessageBox.Show("Aucun Client avec cette CNI n'a été trouvé");
                    }
                    else
                    {
                        rd.Read();
                        //this.label8.Text = rd.GetString(1);
                        //this.label9.Text = rd.GetString(3);
                        //this.label10.Text = rd.GetString(4); ;
                        //this.label11.Text = rd.GetString(7);

                    }

                }
                catch (Exception ex)
                {

                    MessageBox.Show("Erreur systeme veuillez contacter l'administrateur");
                }
                finally {/* con.Close();*/ }
            }
        }
        private void btn_par_numero_unique_Click(object sender, EventArgs e)
        {
            if (txt_numero_unique.Text == string.Empty)
            {
                MessageBox.Show("Veuillez renseigner le numero Unique a Rechercher");
            }
            else
            {
                try
                {
                    con.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = $"Select * FROM sc_enrollement.t_info_personne where r_num_unique='{txt_numero_unique.Text}'";
                    NpgsqlDataReader rd = cmd.ExecuteReader();

                    if (!rd.HasRows)
                    {
                        MessageBox.Show("Aucun Client avec ce Numéro Unique n'a été trouvé");
                    }
                    else
                    {
                        MessageBox.Show("trouvé");

                    }

                }
                catch (Exception ex)
                {

                    MessageBox.Show("Erreur systeme veuillez contacter l'administrateur");
                }
                finally { /*con.Close(); */}
            }
        }
        private void recupempreinte(object? sender, EventArgs e)
        {
            try
            {

                Action<string> miseaublancDelegate = new Action<string>((message) =>
                {
                    // Appelez la méthode de mise à jour de l'interface utilisateur avec le message
                    this.label17.Text = ""; //cni
                    this.label16.Text = ""; //Nom
                    this.label15.Text = ""; //Prenom
                    this.label14.Text = ""; //Nationnalite
                    this.label5.Text = ""; //date de naissance
                    this.label8.Text = "";//sexe
                    this.label9.Text = "";
                    this.label10.Text = "";//taille
                    this.label11.Text = "";
                    this.label13.Text = "";//lieu de naissance
                    this.label25.Text = "";//date d'expiration
                    this.label29.Text = "";//NNI
                    this.label32.Text = "";//Profession
                    this.label34.Text = "";//Emission
                    this.label36.Text = "";//lieu d'emission
                });

                this.Invoke(miseaublancDelegate, "Mise à jour depuis le thread secondaire avec des paramètres");
                //this.label8.Text = "";
                //this.label9.Text = "";
                //this.label10.Text = "";
                //this.label11.Text = "";

                var ber = device.ReadFingerprint();
                var tempFile = Guid.NewGuid().ToString();
                var tempFileall = Path.Combine(Outils.recup().tempfolder, tempFile);
                var tmpBmpFile = Path.ChangeExtension(tempFileall, "bmp");
                ber.Save(tmpBmpFile);
                pictureBox1.Image = ber;

                ////check into file to see if is goood empreinte
                (bool, double, string) res = BiometricVerification.Verify(tmpBmpFile).Result;
                if (!res.Item1)
                {
                    device.FingerDetected -= recupempreinte;
                    _isDetecteMode = false;
                    button1.BackColor = Color.Red;
                    MessageBox.Show($"Aucune correspondance avec les empreintes de la base de donnée {res.Item3}");
                    //device.Dispose();
                }
                else
                {
                    //**************************************recupere l'empreinte l'utilisateur dans la BD********************************
                    string[] retarray = res.Item3.Split("\\");
                    getinfoUserFromDatabase(retarray.Last());
                    device.FingerDetected -= recupempreinte;
                    _isDetecteMode = false;
                    button1.BackColor = Color.Red;

                    //**************************************recupere l'empreinte l'utilisateur dans la BD********************************
                }
            }
            catch (Exception ex)
            {
                device.FingerDetected -= recupempreinte;
                _isDetecteMode = false;
                button1.BackColor = Color.Red;
                MessageBox.Show($"Erreur dans a detection de l'empreinte veuillez contacter l'administrateur {ex.Message}");
            }
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (!_isDetecteMode)
                {
                    device.FingerDetected += recupempreinte;
                    _isDetecteMode = true;
                    button1.BackColor = Color.Green;
                }
                else
                {
                    device.FingerDetected -= recupempreinte;
                    _isDetecteMode = false;
                    button1.BackColor = Color.Red;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erreur Systeme veuillez contacter l'administrateur");
            }
            //fromConsole();
           

        }
        private void getinfoUserFromDatabase(string info)
        {

            try
            {
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = con;
                cmd.CommandText = $"SELECT p1.* FROM sc_enrollement.t_info_personne as p1 inner join sc_enrollement.t_empreinte as p2 on p2.r_id_personne_fk=p1.r_id where p2.r_lien like '%{info}'";
                NpgsqlDataReader rd = cmd.ExecuteReader();

                if (!rd.HasRows)
                {
                    MessageBox.Show("Empreinte existante mais données non trouvé");
                }
                else
                {
                    rd.Read();
                    string _cni = rd.GetString(1);
                    string _numuniq = rd.GetString(2);
                    string _nom = rd.GetString(3);
                    string _prenom = rd.GetString(4); ;
                    string _sex = rd.GetString(5); ;
                    string _taile = rd.GetString(6); ;
                    string _nationnalite = rd.GetString(7);
                    string _lieunaissance = rd.GetString(8);
                    DateTime _dateexpire = rd.GetDateTime(9);
                    string _nni = rd.GetString(10);
                    string _profession = rd.GetString(11);
                    DateTime _dateemission = rd.GetDateTime(12);
                    string _lieuemission = rd.GetString(13);
                    DateTime _datenaissance = rd.GetDateTime(20);
                    rd.Close();



                    

                    Thread thread = new Thread(() =>
                    {
                        // Effectuez votre travail long ici

                        // Créez un délégué pour mettre à jour l'interface utilisateur avec des paramètres
                        Action<string> updateDelegate = new Action<string>((message) =>
                        {
                            // Appelez la méthode de mise à jour de l'interface utilisateur avec le message
                            UpdateUIFromThread(_cni, _numuniq, _nom, _prenom, _sex, _taile, _nationnalite, _lieunaissance, _dateexpire, _nni, _profession, _dateemission, _lieuemission, _datenaissance);
                        });

                        // Appelez Invoke sur le formulaire pour mettre à jour l'interface utilisateur
                        this.Invoke(updateDelegate, "Mise à jour depuis le thread secondaire avec des paramètres");
                    });

                    // Démarrez le thread
                    thread.Start();

                }

            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show("Erreur systeme veuillez contacter l'administrateur");
            }
            finally { /*con.Close();*/ }
        }
        private void UpdateUIFromThread(string _cni, string _numuniq , string _nom , string _prenom , string _sex , string _taile, string _nationnalite, string _lieunaissance, DateTime _dateexpire, string _nni, string _profession, DateTime _dateemission, string _lieuemission, DateTime _datenaissance)
        {
            this.label17.Text = _cni; //cni
            this.label16.Text = _nom; //Nom
            this.label15.Text = _prenom; //Prenom
            this.label14.Text = _nationnalite; //Nationnalite
            this.label5.Text = _datenaissance.ToShortDateString(); //date de naissance
            this.label8.Text = _sex;//sexe
            this.label9.Text = "";
            this.label10.Text = _taile;//taille
            this.label11.Text = "";
            this.label13.Text = _lieunaissance;//lieu de naissance
            this.label25.Text = _dateexpire.ToShortDateString();//date d'expiration
            this.label29.Text = _nni;//NNI
            this.label32.Text = _profession;//Profession
            this.label34.Text = _dateemission.ToShortDateString();//Emission
            this.label36.Text = _lieuemission;//lieu d'emission
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var accessor = new DeviceAccessor();

                var device = accessor.AccessFingerprintDevice();
                device.SwitchLedState(true, false);
                //device.StartFingerDetection();
                Bitmap ber = device.ReadFingerprint();
                this.pictureBox1.Image = ber;
                device.SwitchLedState(false, false);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erreur dans l'initialisation du materiel veuillez contacter l'administrateur");
            }
            
        }
        private void FormSearch_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            { device.FingerDetected -= recupempreinte;
            _isDetecteMode = false;
            button1.BackColor = Color.Red;
            device.StopFingerDetection();
            device.Dispose();

            }
            catch (Exception ex)
            {
                MessageBox.Show("erreur systeme veuillez contacter l'administrateur");
            }
           
        }

        private void FormSearch_Load(object sender, EventArgs e)
        {

        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
