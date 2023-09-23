using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Futronic.Scanners.FS26X80;
using Microsoft.VisualBasic.FileIO;
using Npgsql;

namespace Fingerprint1
{
    public partial class Form1 : Form
    {
        private DeviceAccessor accessor = new();
        private FingerprintDevice device;
        private List<byte[]> listEmpreinte;

        private bool statButton = false;
        private byte[] Thumb;
        private byte[] IndexFinger;
        private byte[] MiddleFinger;
        private byte[] RingFinger;
        private byte[] LittleFinger;



        private NpgsqlConnection con = new NpgsqlConnection(connectionString: "Server=172.10.10.103;Port=5434;User Id=vitbank;Password=vitbank;Database=Bd_enrollement;");

        string cheminImagethumb;
        string cheminImageIndex;
        string cheminImagemajeur;
        string cheminImageAnnulaire;
        string cheminImageAuriculaire;
        public Form1()
        {

            InitializeComponent();
            InitializeDevice();
            this.pictureLittle.SizeMode = PictureBoxSizeMode.StretchImage;
            this.pictureMiddle.SizeMode = PictureBoxSizeMode.StretchImage;
            this.pictureRing.SizeMode = PictureBoxSizeMode.StretchImage;
            this.pictureThumb.SizeMode = PictureBoxSizeMode.StretchImage;
            this.pictureIndex.SizeMode = PictureBoxSizeMode.StretchImage;
            this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            Task.Run(() =>
            {
                device.StartFingerDetection();
                device.SwitchLedState(false, true);

                device.SwitchLedState(false, false);
            });
        }

        private void InitializeDevice()
        {
            device = accessor.AccessFingerprintDevice();
            device.SwitchLedState(false, false);

        }

        private void FingerReleased(object? sender, EventArgs e)
        {
            Trace.TraceWarning("delete");
        }

        //private void FingerDetected(object? sender, EventArgs e)
        //{
        //    //pictureBox1.Image = null;
        //    Bitmap ber = device.ReadFingerprint();
        //    pictureBox1.Image = ber;
        //}

        private void PouceDetected(object? sender, EventArgs e)
        {
            //pictureBox1.Image = null;
            Bitmap ber = device.ReadFingerprint();
            pictureThumb.Image = ber;
        }
        private void IndexDetected(object? sender, EventArgs e)
        {
            //pictureBox1.Image = null;
            Bitmap ber = device.ReadFingerprint();
            pictureIndex.Image = ber;
        }
        private void MiddleDetected(object? sender, EventArgs e)
        {
            //pictureBox1.Image = null;
            Bitmap ber = device.ReadFingerprint();
            pictureMiddle.Image = ber;
        }
        private void AnnulaireDetected(object? sender, EventArgs e)
        {
            //pictureBox1.Image = null;
            Bitmap ber = device.ReadFingerprint();
            pictureRing.Image = ber;
        }
        private void LitteDetect(object? sender, EventArgs e)
        {
            //pictureBox1.Image = null;
            Bitmap ber = device.ReadFingerprint();
            pictureLittle.Image = ber;
        }

        private void btnCaptureThumb_Click(object sender, EventArgs e)
        {



            try
            {
                var accessor = new DeviceAccessor();

                var device = accessor.AccessFingerprintDevice();
                device.SwitchLedState(true, false);
                //device.StartFingerDetection();
                Bitmap ber = device.ReadFingerprint();
                this.pictureThumb.Image = ber;
                device.SwitchLedState(false, false);

                this.Thumb = GetImageBytes(this.pictureThumb.Image);
                //listEmpreinte.Add(this.Thumb);
            }
            catch (NullReferenceException ec)
            {
                MessageBox.Show("Veuillez posez votre empreinte sur le lecteur");
            }
            catch (Exception ex)
            {

                MessageBox.Show("Erreur Syeteme veuillez contacter l'administrateur");
            }

        }

        private byte[] GetImageBytes(Image image)
        {
            MemoryStream ms = new();
            image.Save(ms, ImageFormat.Png);
            return ms.ToArray();
        }

        private void btnCaptureIndex_Click(object sender, EventArgs e)
        {
            var accessor = new DeviceAccessor();

            var device = accessor.AccessFingerprintDevice();
            device.SwitchLedState(true, false);
            //device.StartFingerDetection();
            Bitmap ber = device.ReadFingerprint();
            this.pictureIndex.Image = ber;
            device.SwitchLedState(false, false);

            this.IndexFinger = GetImageBytes(this.pictureIndex.Image);
            //listEmpreinte.Add(this.IndexFinger);
        }

        private void btnCaptureRing_Click(object sender, EventArgs e)
        {
            var accessor = new DeviceAccessor();

            var device = accessor.AccessFingerprintDevice();
            device.SwitchLedState(true, false);
            //device.StartFingerDetection();
            Bitmap ber = device.ReadFingerprint();
            this.pictureRing.Image = ber;
            device.SwitchLedState(false, false);

            this.RingFinger = GetImageBytes(this.pictureRing.Image);
            //listEmpreinte.Add(this.RingFinger);
        }

        private void btnCaptureLittle_Click(object sender, EventArgs e)
        {
            var accessor = new DeviceAccessor();

            var device = accessor.AccessFingerprintDevice();
            device.SwitchLedState(true, false);
            //device.StartFingerDetection();
            Bitmap ber = device.ReadFingerprint();
            this.pictureLittle.Image = ber;
            device.SwitchLedState(true, true);

            this.LittleFinger = GetImageBytes(this.pictureLittle.Image);
            //listEmpreinte.Add(this.LittleFinger);
        }

        private void btnCaptureMiddle_Click(object sender, EventArgs e)
        {
            var accessor = new DeviceAccessor();

            var device = accessor.AccessFingerprintDevice();
            device.SwitchLedState(true, false);
            //device.StartFingerDetection();
            Bitmap ber = device.ReadFingerprint();
            this.pictureMiddle.Image = ber;
            device.SwitchLedState(true, false);

            this.MiddleFinger = GetImageBytes(this.pictureIndex.Image);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //var accessor = new DeviceAccessor();

            //var device = accessor.AccessFingerprintDevice();
            if (!statButton)
            {
                statButton = true;
                this.button1.BackColor = Color.Green;
                device.FingerDetected += RechercheTodatabase;
            }
            else
            {
                statButton = false;
                this.button1.BackColor = Color.Red;
            }

        }
        private void RechercheTodatabase(object sender, EventArgs e)
        {
            
            Bitmap ber = device.ReadFingerprint();
            device.FingerDetected -= RechercheTodatabase;
            this.button1.BackColor = Color.Red;
            this.statButton = false;
            pictureBox1.Image=ber;

            //******************************************************Go to database ********************************************************************

            //******************************************************Go to database ********************************************************************
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string pathget = Directory.GetCurrentDirectory();
            pathget = Path.Combine(pathget, "empreintePicture");

            string pathutilisateur = Path.Combine(pathget, txt_cni.Text);

            if (!Directory.Exists(pathget))
            {
                Directory.CreateDirectory(pathget);
            }
            if (!Directory.Exists(pathutilisateur))
            {
                Directory.CreateDirectory(pathutilisateur);
            }

            cheminImagethumb = Path.Combine(pathutilisateur, txt_cni.Text + "thumb.bmp");
            cheminImageIndex = Path.Combine(pathutilisateur, txt_cni.Text + "index.bmp");
            cheminImagemajeur = Path.Combine(pathutilisateur, txt_cni.Text + "majeur.bmp");
            cheminImageAnnulaire = Path.Combine(pathutilisateur, txt_cni.Text + "Annulaire.bmp");
            cheminImageAuriculaire = Path.Combine(pathutilisateur, txt_cni.Text + "Auriculaire.bmp");

            File.WriteAllBytes(cheminImagethumb, this.Thumb);
            File.WriteAllBytes(cheminImageIndex, this.IndexFinger);
            File.WriteAllBytes(cheminImagemajeur, this.MiddleFinger);
            File.WriteAllBytes(cheminImageAnnulaire, this.RingFinger);
            File.WriteAllBytes(cheminImageAuriculaire, this.LittleFinger);
            savedata();
            MessageBox.Show("ok");
        }

        private void savedata()
        {
            try
            {
                con.Open();
                using var cmd = new NpgsqlCommand();
                cmd.Connection = con;
                string id = Guid.NewGuid().ToString();
                string sex = (txt_sexe.Text == "Homme") ? "H" : "F";
                //cmd.CommandText = $"Insert into sc_enrollement.t_info_personne values('{id}','{txt_cni.Text}','{txt_num_unique.Text}','{txt_nom.Text}','{txt_prenom.Text}','{sex}',{Convert.ToInt32(txt_taille.Text)},'{txt_nationnalite.Text}','{txt_lieu_naissance.Text}','TO_DATE({txt_dat_exp_cni.Value.ToString().Substring(0,10)},YYYYMMDD)','{txt_nni.Text}','{txt_profession.Text}','TO_DATE({txt_date_emission.Value.ToString().Substring(0,10)},YYYYMMDD)','{txt_lieu_emission.Text}','Syteme','TODATE({DateTime.Now.Date.ToString().Substring(0,10)},YYYYMMDD)','Systeme','TODATE({DateTime.Now.Date.ToString().Substring(0, 10)},YYYYMMDD)',{false},'{string.Empty}','{txt_date_naissance.Value.ToString().Substring(0, 10)}')";
                cmd.CommandText = $"Insert into sc_enrollement.t_info_personne values('{id}','{txt_cni.Text}','{txt_num_unique.Text}','{txt_nom.Text}','{txt_prenom.Text}','{sex}',{Convert.ToInt32(txt_taille.Text)},'{txt_nationnalite.Text}','{txt_lieu_naissance.Text}',now(),'{txt_nni.Text}','{txt_profession.Text}',now(),'{txt_lieu_emission.Text}','Syteme',now(),'Systeme',now(),{false},'{string.Empty}',now())";
                cmd.ExecuteNonQuery();


                using var cmd1 = new NpgsqlCommand();

                string requette = @$"INSERT INTO sc_enrollement.t_empreinte(r_type, r_valeur, r_lien, r_id_personne_fk, r_created_by, r_created_on)VALUES ( 'POUCE', '{this.Thumb}', '{this.cheminImagethumb}', '{id}', 'Systeme', now());
                                    INSERT INTO sc_enrollement.t_empreinte(r_type, r_valeur, r_lien, r_id_personne_fk, r_created_by, r_created_on)VALUES ( 'INDEX', '{this.IndexFinger}', '{this.cheminImageIndex}', '{id}', 'Systeme', now());
                                    INSERT INTO sc_enrollement.t_empreinte(r_type, r_valeur, r_lien, r_id_personne_fk, r_created_by, r_created_on)VALUES ( 'MAJEUR', '{this.MiddleFinger}', '{this.cheminImagemajeur}', '{id}', 'Systeme', now());
                                    INSERT INTO sc_enrollement.t_empreinte(r_type, r_valeur, r_lien, r_id_personne_fk, r_created_by, r_created_on)VALUES ( 'ANNULAIRE', '{this.RingFinger}', '{this.cheminImageAnnulaire}', '{id}', 'Systeme', now());
                                    INSERT INTO sc_enrollement.t_empreinte(r_type, r_valeur, r_lien, r_id_personne_fk, r_created_by, r_created_on)VALUES ( 'AURICULAIRE', '{this.LittleFinger}', '{this.cheminImageAuriculaire}', '{id}', 'Systeme', now());";
                cmd1.Connection = con;
                cmd1.CommandText = requette.Replace("\r", "");
                cmd1.CommandText = cmd1.CommandText.Replace("\n", "");
                cmd1.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show($"Execute non query {ex.Message}");
            }

        }

        private void btn_search_cni_Click(object sender, EventArgs e)
        {

        }
    }
}
