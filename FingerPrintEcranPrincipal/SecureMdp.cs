using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FingerPrintEcranPrincipal
{
    public partial class SecureMdp : Form
    {
        public bool is_ok { get; set; } = false;
        public SecureMdp()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txt_mdp.Text == "Admin@123")
            {
                is_ok = true;
                this.Close();
            }
            else
            {
                is_ok = false;
                label2.Visible = true;
                Task.Run(() =>
                {
                    Task.Delay(5000);
                    Action<string> visibletread = new Action<string>((message) =>
                    {
                        // Appelez la méthode de mise à jour de l'interface utilisateur avec le message
                        this.label2.Visible = false;
                        this.txt_mdp.Text=string.Empty;

                    });

                    this.Invoke(visibletread, "Mise à jour depuis le thread secondaire avec des paramètres");

                });
                

            }
        }
    

    private void button2_Click(object sender, EventArgs e)
    {
        this.Close();
    }
}
}
