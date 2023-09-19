//using Futronic.Infrastructure.Services;
using Futronic.Models;
using Futronic.Scanners.FS26X80;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Drawing.Design;
using System.Drawing.Imaging;
using System.IO;
using System.Data.SqlClient;
using Npgsql;

namespace FutronicFingerPrint.Forms
{
    [Serializable]
    public partial class UserRegistrationForm : Form
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public FingerPrint LeftHand { get; set; }
        [DataMember]
        public DoigtDroit RightHand { get; set; }

        private List<byte[]> listEmpreinte;

        private byte[] Thumb;
        private byte[] IndexFinger;
        private byte[] MiddleFinger;
        private byte[] RingFinger;
        private byte[] LittleFinger;

        private FingerprintDevice _device;
        private DeviceAccessor _accessor = new DeviceAccessor();


        private NpgsqlConnection con = new NpgsqlConnection(connectionString: "Server=172.10.10.103;Port=5434;User Id=vitbank;Password=vitbank;Database=Bd_enrollement;");

        string cheminImagethumb;
        string cheminImageIndex;
        string cheminImagemajeur;
        string cheminImageAnnulaire;
        string cheminImageAuriculaire;


        //private IRepository<long, User> _userRepository;
        //private IRepository<long, AttendanceLog> _attendanceRepository;

        public UserRegistrationForm()
        {
            InitializeComponent();
            InstantiateRepository();


            _device = _accessor.AccessFingerprintDevice();

        }
        public UserRegistrationForm(long userId)
        {
            InitializeComponent();
            InstantiateRepository();

        }

        private void InstantiateRepository()
        {

        }

        private void UserRegistrationForm_Load(object sender, EventArgs e)
        {

        }

        private void btnUpdateData_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private void btnEnrollLeft_Click(object sender, EventArgs e)
        {
            FingerprintForm form = new();
            form.ShowDialog();
            if (form.ShowDialog() == DialogResult.OK)
            {
                var fingerprints = form.GetFingerPrints();
                this.LeftHand = fingerprints;
                Task.Run(CheckRegisteredLeftFingers);
            }
        }
        private void CheckRegisteredLeftFingers()
        {
            this.Invoke(new MethodInvoker(delegate ()
            {
                checkBoxLT.Checked = LeftHand.Thumb != null;
                checkBoxLI.Checked = LeftHand.IndexFinger != null;
                checkBoxLM.Checked = LeftHand.MiddleFinger != null;
                checkBoxLR.Checked = LeftHand.RingFinger != null;
                checkBoxLL.Checked = LeftHand.LittleFinger != null;
            }));
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            this.UserName = txtUsername.Text;
        }

        private void txtFirstname_TextChanged(object sender, EventArgs e)
        {
            this.FirstName = txtFirstname.Text;
        }

        private void txtLastname_TextChanged(object sender, EventArgs e)
        {
            this.LastName = txtLastname.Text;
        }

        private void picturePassport_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new();
            openFile.Multiselect = false;
            openFile.Title = "Select Image";
            openFile.Filter = "*.jpg|*.png";
            openFile.DefaultExt = "jpg";
            openFile.AddExtension = true;
            openFile.InitialDirectory = SpecialDirectories.MyPictures;
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                this.picturePassport.Image = Image.FromFile(openFile.FileName);
            }
        }

        private void btnCaptureMiddle_Click(object sender, EventArgs e)
        {
            var accessor = new DeviceAccessor();

            using (var device = accessor.AccessFingerprintDevice())
            {
                device.SwitchLedState(true, false);
                //device.StartFingerDetection();
                Bitmap ber = device.ReadFingerprint();
                this.pictureMiddle.Image = ber;
                device.SwitchLedState(false, false);
            }
            this.MiddleFinger = GetImageBytes(this.pictureMiddle.Image);
            //listEmpreinte.Add(this.MiddleFinger);
        }

        private byte[] GetImageBytes(Image image)
        {
            MemoryStream ms = new();
            image.Save(ms, ImageFormat.Png);
            return ms.ToArray();
        }

        private void btnCaptureThumb_Click(object sender, EventArgs e)
        {
            try
            {
                var accessor = new DeviceAccessor();

                using (var device = accessor.AccessFingerprintDevice())
                {
                    device.SwitchLedState(true, false);
                    //device.StartFingerDetection();
                    Bitmap ber = device.ReadFingerprint();
                    this.pictureThumb.Image = ber;
                    device.SwitchLedState(false, false);
                }
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

        private void btnCaptureIndex_Click(object sender, EventArgs e)
        {
            var accessor = new DeviceAccessor();

            using (var device = accessor.AccessFingerprintDevice())
            {
                device.SwitchLedState(true, false);
                //device.StartFingerDetection();
                Bitmap ber = device.ReadFingerprint();
                this.pictureIndex.Image = ber;
                device.SwitchLedState(false, false);
            }
            this.IndexFinger = GetImageBytes(this.pictureIndex.Image);
            //listEmpreinte.Add(this.IndexFinger);
        }

        private void btnCaptureRing_Click(object sender, EventArgs e)
        {
            var accessor = new DeviceAccessor();

            using (var device = accessor.AccessFingerprintDevice())
            {
                device.SwitchLedState(true, false);
                //device.StartFingerDetection();
                Bitmap ber = device.ReadFingerprint();
                this.pictureRing.Image = ber;
                device.SwitchLedState(false, false);
            }
            this.RingFinger = GetImageBytes(this.pictureRing.Image);
            //listEmpreinte.Add(this.RingFinger);
        }

        private void btnCaptureLittle_Click(object sender, EventArgs e)
        {
            var accessor = new DeviceAccessor();

            using (var device = accessor.AccessFingerprintDevice())
            {
                device.SwitchLedState(true, false);
                //device.StartFingerDetection();
                Bitmap ber = device.ReadFingerprint();
                this.pictureLittle.Image = ber;
                device.SwitchLedState(false, false);
            }
            this.LittleFinger = GetImageBytes(this.pictureLittle.Image);
            //listEmpreinte.Add(this.LittleFinger);
        }

        private void button1_Click(object sender, EventArgs e)
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

        public async Task savedata()
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
                cmd1.CommandText = requette.Replace("\r","");
                cmd1.CommandText = cmd1.CommandText.Replace("\n","");
                cmd1.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show($"Execute non query {ex.Message}");
            }
            
        
        }



        public class DoigtDroit
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (con.State==ConnectionState.Open)
                con.Close();

        }
    }
}
