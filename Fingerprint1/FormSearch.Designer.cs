namespace Fingerprint1
{
    partial class FormSearch
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
            groupBox1 = new System.Windows.Forms.GroupBox();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            button1 = new System.Windows.Forms.Button();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(pictureBox1);
            groupBox1.Controls.Add(button1);
            groupBox1.Location = new System.Drawing.Point(12, 28);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(200, 274);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "Recherche";
            // 
            // pictureBox1
            // 
            pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pictureBox1.Location = new System.Drawing.Point(19, 31);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(149, 176);
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(37, 213);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(120, 34);
            button1.TabIndex = 0;
            button1.Text = "Recherche";
            button1.UseVisualStyleBackColor = true;
            // 
            // FormSearch
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(893, 484);
            Controls.Add(groupBox1);
            Name = "FormSearch";
            Text = "FormSearch";
            groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
    }
}