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

namespace Fingerprint1
{
    public partial class FormSearch : Form
    {
        private DeviceAccessor accessor = new();
        private FingerprintDevice device;
        private bool _isDetecteMode = false;
        private NpgsqlConnection con = new NpgsqlConnection(connectionString: Outils.recup().DatabaseString);
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
            device = accessor.AccessFingerprintDevice();
            device.SwitchLedState(false, false);

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

        private void button1_Click(object sender, EventArgs e)
        {
            //est en non detection et doit passer en detection 
            if (!_isDetecteMode)
            {

                _isDetecteMode = true;
            }
            //est en detetction et dois passer en non detection
            else
            {
                _isDetecteMode = false;
            }
        }

        private void recupempreinte(object? sender, EventArgs e)
        {
            try
            {
                var ber = device.ReadFingerprint();
                pictureBox1.Image = ber;

                var tempFile = Guid.NewGuid().ToString();
                var tempFileall = Path.Combine(Outils.recup().tempfolder, tempFile);
                var tmpBmpFile = Path.ChangeExtension(tempFileall, "bmp");
                ber.Save(tmpBmpFile);

                device.FingerDetected -= recupempreinte;
                _isDetecteMode = false;
                button1.BackColor = Color.Red;



                //check into file to see if is goood empreinte
                (bool, double, string) res = BiometricVerification.Verify(tempFileall).Result;
                if (!res.Item1)
                {
                    MessageBox.Show("Aucune correspondance avec les empreinte de la base de donnée");
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

                MessageBox.Show("Erreur dans a detection de l'empreinte veuillez contacter l'administrateur");
            }
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (!_isDetecteMode)
            {
                device.FingerDetected += recupempreinte;
                _isDetecteMode = true;
                button1.BackColor = Color.Green;
            }
            else {
                device.FingerDetected -= recupempreinte;
                _isDetecteMode = false;
                button1.BackColor = Color.Red;
            }
            

        }
        private void getinfoUserFromDatabase(string info)
        {

        }
    }
}
