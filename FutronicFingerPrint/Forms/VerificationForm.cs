using Futronic.Scanners.FS26X80;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace FutronicFingerPrint.Forms
{
    public partial class VerificationForm : Form
    {

        
        private DeviceAccessor accessor = new();
        private FingerprintDevice device;
        private NpgsqlConnection con = new NpgsqlConnection(connectionString: "Server=172.10.10.103;Port=5434;User Id=vitbank;Password=vitbank;Database=Bd_enrollement;");
        public VerificationForm()
        {

            InitializeComponent();
            InitializeDevice();
            label3.Visible = true;
            lbl_result_conversion.Visible = false;
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
            device.FingerDetected += getimage;
          
        }
        public void getimage(object sender, EventArgs e)
        {
            this.lbl_result_conversion.Visible = false;
            Bitmap ber = device.ReadFingerprint();
            pictureBox1.Image = ber;

          (bool,double) res=  BiometricVerification.Verify(this.pictureBox1.Image, this.pictureBox2.Image).Result;
            
            if (res.Item1)
            {
                
                MessageBox.Show("Comparaison Correct", "VALIDE");
                //this.lbl_result_conversion.BeginInvoke (new Action(() => {
                //    // Code à exécuter dans le thread principal
                //    this.lbl_result_conversion.ForeColor = Color.Green;
                //    this.lbl_result_conversion.Text = "MATCH";
                //    this.lbl_result_conversion.Visible = true;
                //}));
            }
            else
            {
                MessageBox.Show("Comparaison Erroné", "ECHEC");
                //this.lbl_result_conversion.BeginInvoke(new Action(() => {
                //    // Code à exécuter dans le thread principal
                //    lbl_result_conversion.ForeColor = Color.Red;
                //    lbl_result_conversion.Text = "NO MATCH";
                //    lbl_result_conversion.Visible = true;
                //}));
            }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            label3.Visible = false;
            con.Open();
            using var cmd = new NpgsqlCommand();
            cmd.Connection = con;
            //cmd.CommandText = $"Insert into sc_enrollement.t_info_personne values('{id}','{txt_cni.Text}','{txt_num_unique.Text}','{txt_nom.Text}','{txt_prenom.Text}','{sex}',{Convert.ToInt32(txt_taille.Text)},'{txt_nationnalite.Text}','{txt_lieu_naissance.Text}','TO_DATE({txt_dat_exp_cni.Value.ToString().Substring(0,10)},YYYYMMDD)','{txt_nni.Text}','{txt_profession.Text}','TO_DATE({txt_date_emission.Value.ToString().Substring(0,10)},YYYYMMDD)','{txt_lieu_emission.Text}','Syteme','TODATE({DateTime.Now.Date.ToString().Substring(0,10)},YYYYMMDD)','Systeme','TODATE({DateTime.Now.Date.ToString().Substring(0, 10)},YYYYMMDD)',{false},'{string.Empty}','{txt_date_naissance.Value.ToString().Substring(0, 10)}')";
            cmd.CommandText = $"SELECT t2.r_lien,t2.r_id_personne_fk FROM sc_enrollement.t_info_personne as t1 inner join sc_enrollement.t_empreinte as t2 ON t1.r_id=t2.r_id_personne_fk where t2.r_type='{comboBox1.Text}' and t1.r_num_cni='{textBox1.Text}'";
            using (NpgsqlDataReader reader = cmd.ExecuteReader())
            {
                if(!reader.HasRows)
                {
                    label3.Visible = false;
                }
                if(reader.Read())
                {
                    this.pictureBox2.ImageLocation = reader["r_lien"].ToString();
                }
            }
            con.Close();

        }
    }
}
