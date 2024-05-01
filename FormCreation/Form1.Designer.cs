namespace FormCreation
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            panelRecapitulatif = new System.Windows.Forms.Panel();
            textBox1 = new System.Windows.Forms.TextBox();
            button11 = new System.Windows.Forms.Button();
            btn_precedent = new System.Windows.Forms.Button();
            panelRecapitulatif.SuspendLayout();
            SuspendLayout();
            // 
            // panelRecapitulatif
            // 
            panelRecapitulatif.Controls.Add(btn_precedent);
            panelRecapitulatif.Controls.Add(button11);
            panelRecapitulatif.Controls.Add(textBox1);
            panelRecapitulatif.Location = new System.Drawing.Point(12, 26);
            panelRecapitulatif.Name = "panelRecapitulatif";
            panelRecapitulatif.Size = new System.Drawing.Size(1036, 476);
            panelRecapitulatif.TabIndex = 0;
            // 
            // textBox1
            // 
            textBox1.Location = new System.Drawing.Point(19, 16);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(999, 388);
            textBox1.TabIndex = 0;
            // 
            // button11
            // 
            button11.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            button11.Image = (System.Drawing.Image)resources.GetObject("button11.Image");
            button11.Location = new System.Drawing.Point(950, 409);
            button11.Name = "button11";
            button11.Size = new System.Drawing.Size(68, 46);
            button11.TabIndex = 55;
            button11.UseVisualStyleBackColor = true;
            // 
            // btn_precedent
            // 
            btn_precedent.Location = new System.Drawing.Point(19, 420);
            btn_precedent.Name = "btn_precedent";
            btn_precedent.Size = new System.Drawing.Size(85, 36);
            btn_precedent.TabIndex = 60;
            btn_precedent.Text = "Precedent";
            btn_precedent.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1060, 572);
            Controls.Add(panelRecapitulatif);
            Name = "Form1";
            Text = "Form1";
            panelRecapitulatif.ResumeLayout(false);
            panelRecapitulatif.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Button btn_precedent;
        private System.Windows.Forms.Panel panelRecapitulatif;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button11;
    }
}
