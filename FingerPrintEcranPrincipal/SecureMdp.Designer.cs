namespace FingerPrintEcranPrincipal
{
    partial class SecureMdp
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel5 = new System.Windows.Forms.Panel();
            label7 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            txt_mdp = new System.Windows.Forms.TextBox();
            button1 = new System.Windows.Forms.Button();
            button2 = new System.Windows.Forms.Button();
            label2 = new System.Windows.Forms.Label();
            panel5.SuspendLayout();
            SuspendLayout();
            // 
            // panel5
            // 
            panel5.BackColor = System.Drawing.Color.FromArgb(82, 153, 139);
            panel5.Controls.Add(label7);
            panel5.Location = new System.Drawing.Point(0, 0);
            panel5.Name = "panel5";
            panel5.Size = new System.Drawing.Size(348, 44);
            panel5.TabIndex = 7;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label7.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            label7.Location = new System.Drawing.Point(55, 13);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(239, 19);
            label7.TabIndex = 1;
            label7.Text = "SECURITE ADMINISTRATEUR";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Arial Narrow", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label1.Location = new System.Drawing.Point(57, 62);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(218, 20);
            label1.TabIndex = 8;
            label1.Text = "Veuillez Saisir votre Mot de passe :";
            // 
            // txt_mdp
            // 
            txt_mdp.Location = new System.Drawing.Point(76, 91);
            txt_mdp.Name = "txt_mdp";
            txt_mdp.PasswordChar = '*';
            txt_mdp.PlaceholderText = "Veuillez saisir le Mot de passe";
            txt_mdp.Size = new System.Drawing.Size(172, 23);
            txt_mdp.TabIndex = 9;
            txt_mdp.UseSystemPasswordChar = true;
            // 
            // button1
            // 
            button1.BackColor = System.Drawing.Color.ForestGreen;
            button1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            button1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            button1.Location = new System.Drawing.Point(200, 142);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(75, 35);
            button1.TabIndex = 10;
            button1.Text = "Valider";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = System.Drawing.Color.Gainsboro;
            button2.Location = new System.Drawing.Point(57, 142);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(75, 35);
            button2.TabIndex = 11;
            button2.Text = "Annuler";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label2.ForeColor = System.Drawing.Color.Red;
            label2.Location = new System.Drawing.Point(55, 119);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(229, 15);
            label2.TabIndex = 12;
            label2.Text = "Mot de passe incorrect veuillez ressayer";
            label2.Visible = false;
            // 
            // SecureMdp
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.SystemColors.ButtonHighlight;
            ClientSize = new System.Drawing.Size(346, 200);
            Controls.Add(label2);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(txt_mdp);
            Controls.Add(label1);
            Controls.Add(panel5);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "SecureMdp";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "SecureMdp";
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_mdp;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
    }
}