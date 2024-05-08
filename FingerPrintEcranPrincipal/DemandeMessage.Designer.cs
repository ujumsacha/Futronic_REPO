namespace FingerPrintEcranPrincipal
{
    partial class DemandeMessage
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
            label7 = new System.Windows.Forms.Label();
            panel5 = new System.Windows.Forms.Panel();
            label1 = new System.Windows.Forms.Label();
            button1 = new System.Windows.Forms.Button();
            button2 = new System.Windows.Forms.Button();
            panel5.SuspendLayout();
            SuspendLayout();
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label7.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            label7.Location = new System.Drawing.Point(205, 12);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(89, 19);
            label7.TabIndex = 1;
            label7.Text = "MESSAGE";
            // 
            // panel5
            // 
            panel5.BackColor = System.Drawing.Color.FromArgb(82, 153, 139);
            panel5.Controls.Add(label7);
            panel5.Location = new System.Drawing.Point(0, 0);
            panel5.Name = "panel5";
            panel5.Size = new System.Drawing.Size(482, 44);
            panel5.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label1.ForeColor = System.Drawing.Color.Red;
            label1.Location = new System.Drawing.Point(12, 72);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(408, 16);
            label1.TabIndex = 6;
            label1.Text = "Identifiant non trouvé voulez vous passer a votre Enrollement ?";
            // 
            // button1
            // 
            button1.BackColor = System.Drawing.Color.FromArgb(82, 153, 139);
            button1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            button1.Location = new System.Drawing.Point(397, 122);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(72, 51);
            button1.TabIndex = 7;
            button1.Text = "OUI";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = System.Drawing.Color.Firebrick;
            button2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            button2.Location = new System.Drawing.Point(33, 122);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(72, 51);
            button2.TabIndex = 8;
            button2.Text = "NON";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // DemandeMessage
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.SystemColors.ButtonHighlight;
            ClientSize = new System.Drawing.Size(481, 185);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label1);
            Controls.Add(panel5);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            Name = "DemandeMessage";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "DemandeMessage";
            FormClosing += DemandeMessage_FormClosing;
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}