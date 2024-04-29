using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FingerPrintEcranPrincipal
{
    public partial class AvertissementPopup : Form
    {

        public AvertissementPopup(string val)
        {
            InitializeComponent();
            label1.Text = val;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
