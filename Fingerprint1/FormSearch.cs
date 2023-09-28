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
            device = accessor.AccessFingerprintDevice();
            device.StartFingerDetection();
            if (device.IsFingerPresent)
            {
                device.SwitchLedState(true, false);
                var ber = device.ReadFingerprint();
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
                        this.label8.Text = rd.GetString(1);
                        this.label9.Text = rd.GetString(3);
                        this.label10.Text = rd.GetString(4); ;
                        this.label11.Text = rd.GetString(7);

                    }

                }
                catch (Exception ex)
                {

                    MessageBox.Show("Erreur systeme veuillez contacter l'administrateur");
                }
                finally { con.Close(); }
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
                        MessageBox.Show("Aucun Client avec ce Numero Unique n'a été trouvé");
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
                finally { con.Close(); }
            }
        }

        private void recupempreinte(object? sender, EventArgs e)
        {
            try
            {
                this.label8.Text = "";
                this.label9.Text = "";
                this.label10.Text = "";
                this.label11.Text = "";

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
                    MessageBox.Show($"Aucune correspondance avec les empreintes de la base de donnée {res.Item3}");
                    //device.Dispose();
                }
                else
                {
                    //**************************************recupere l'empreinte l'utilisateur dans la BD********************************
                    string[] retarray = res.Item3.Split("\\");
                    getinfoUserFromDatabase(retarray.Last());
                    //**************************************recupere l'empreinte l'utilisateur dans la BD********************************
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Erreur dans a detection de l'empreinte veuillez contacter l'administrateur {ex.Message}");
            }
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            //fromConsole();
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
                    string el1 = rd.GetString(1);
                    string el2 = rd.GetString(3);
                    string el3 = rd.GetString(4); ;
                    string el4 = rd.GetString(7);
                    rd.Close();

                    Thread thread = new Thread(() =>
                    {
                        // Effectuez votre travail long ici

                        // Créez un délégué pour mettre à jour l'interface utilisateur avec des paramètres
                        Action<string> updateDelegate = new Action<string>((message) =>
                        {
                            // Appelez la méthode de mise à jour de l'interface utilisateur avec le message
                            UpdateUIFromThread(el1, el2, el3, el4);
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
                MessageBox.Show("Erreur systeme veuillez contacter l'administrateur");
            }
            finally { con.Close(); }
        }
        private void UpdateUIFromThread(string elmt1, string elmt2, string elmt3, string elmt4)
        {
            this.label8.Text = elmt1;
            this.label9.Text = elmt2;
            this.label10.Text = elmt3; ;
            this.label11.Text = elmt4;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            var accessor = new DeviceAccessor();

            var device = accessor.AccessFingerprintDevice();
            device.SwitchLedState(true, false);
            //device.StartFingerDetection();
            Bitmap ber = device.ReadFingerprint();
            this.pictureBox1.Image = ber;
            device.SwitchLedState(false, false);
        }

        private void FormSearch_FormClosing(object sender, FormClosingEventArgs e)
        {
            device.FingerDetected -= recupempreinte;
            _isDetecteMode = false;
            button1.BackColor = Color.Red;
            device.StopFingerDetection();
            device.Dispose();
        }
    }
}
