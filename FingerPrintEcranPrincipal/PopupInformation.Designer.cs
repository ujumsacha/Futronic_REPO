namespace FingerPrintEcranPrincipal
{
    partial class PopupInformation
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
            groupBox1 = new System.Windows.Forms.GroupBox();
            label6 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            comboBox1 = new System.Windows.Forms.ComboBox();
            txt_numpiece = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            Dt_naissance = new System.Windows.Forms.DateTimePicker();
            Dt_Exp = new System.Windows.Forms.DateTimePicker();
            button1 = new System.Windows.Forms.Button();
            button2 = new System.Windows.Forms.Button();
            panel5.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // panel5
            // 
            panel5.BackColor = System.Drawing.Color.FromArgb(82, 153, 139);
            panel5.Controls.Add(label7);
            panel5.Location = new System.Drawing.Point(0, 0);
            panel5.Name = "panel5";
            panel5.Size = new System.Drawing.Size(563, 44);
            panel5.TabIndex = 4;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label7.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            label7.Location = new System.Drawing.Point(226, 12);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(89, 19);
            label7.TabIndex = 1;
            label7.Text = "PORTEUR";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(comboBox1);
            groupBox1.Controls.Add(txt_numpiece);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(Dt_naissance);
            groupBox1.Controls.Add(Dt_Exp);
            groupBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            groupBox1.Location = new System.Drawing.Point(14, 52);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(536, 222);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Information(s)";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.ForeColor = System.Drawing.Color.Red;
            label6.Location = new System.Drawing.Point(365, 178);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(107, 21);
            label6.TabIndex = 9;
            label6.Text = "Date incorrect";
            label6.Visible = false;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.ForeColor = System.Drawing.Color.Red;
            label5.Location = new System.Drawing.Point(365, 132);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(107, 21);
            label5.TabIndex = 8;
            label5.Text = "Date incorrect";
            label5.Visible = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(6, 48);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(103, 21);
            label4.TabIndex = 7;
            label4.Text = "Type de Piece";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new System.Drawing.Point(158, 45);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new System.Drawing.Size(189, 29);
            comboBox1.TabIndex = 1;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // txt_numpiece
            // 
            txt_numpiece.Location = new System.Drawing.Point(158, 86);
            txt_numpiece.Name = "txt_numpiece";
            txt_numpiece.Size = new System.Drawing.Size(189, 29);
            txt_numpiece.TabIndex = 2;
            txt_numpiece.TextChanged += txt_numpiece_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(6, 88);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(136, 21);
            label3.TabIndex = 4;
            label3.Text = "Numero de Piece :";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(6, 172);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(135, 21);
            label2.TabIndex = 3;
            label2.Text = "Date d’expiration :";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(6, 129);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(144, 21);
            label1.TabIndex = 2;
            label1.Text = "Date de Naissance :";
            // 
            // Dt_naissance
            // 
            Dt_naissance.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            Dt_naissance.Location = new System.Drawing.Point(158, 126);
            Dt_naissance.Name = "Dt_naissance";
            Dt_naissance.Size = new System.Drawing.Size(189, 29);
            Dt_naissance.TabIndex = 3;
            Dt_naissance.Value = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            Dt_naissance.ValueChanged += Dt_naissance_ValueChanged;
            // 
            // Dt_Exp
            // 
            Dt_Exp.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            Dt_Exp.Location = new System.Drawing.Point(158, 172);
            Dt_Exp.Name = "Dt_Exp";
            Dt_Exp.Size = new System.Drawing.Size(190, 29);
            Dt_Exp.TabIndex = 4;
            Dt_Exp.Value = new System.DateTime(1800, 1, 1, 0, 0, 0, 0);
            // 
            // button1
            // 
            button1.BackColor = System.Drawing.Color.FromArgb(82, 153, 139);
            button1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            button1.Location = new System.Drawing.Point(422, 285);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(125, 28);
            button1.TabIndex = 6;
            button1.Text = "Valider";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = System.Drawing.Color.Firebrick;
            button2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            button2.Location = new System.Drawing.Point(14, 284);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(125, 28);
            button2.TabIndex = 7;
            button2.Text = "Annuler";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // PopupInformation
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            BackColor = System.Drawing.SystemColors.ButtonHighlight;
            ClientSize = new System.Drawing.Size(562, 332);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(groupBox1);
            Controls.Add(panel5);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "PopupInformation";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "PopupInformation";
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker Dt_Exp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker Dt_naissance;
        private System.Windows.Forms.TextBox txt_numpiece;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}