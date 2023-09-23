﻿
namespace FutronicFingerPrint.Forms
{
    partial class UserRegistrationForm
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
            groupBox2 = new System.Windows.Forms.GroupBox();
            txt_dat_exp_cni = new System.Windows.Forms.DateTimePicker();
            txt_date_emission = new System.Windows.Forms.DateTimePicker();
            txt_lieu_emission = new System.Windows.Forms.TextBox();
            txt_sexe = new System.Windows.Forms.ComboBox();
            txt_date_naissance = new System.Windows.Forms.DateTimePicker();
            label2 = new System.Windows.Forms.Label();
            label23 = new System.Windows.Forms.Label();
            txt_profession = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            label19 = new System.Windows.Forms.Label();
            label22 = new System.Windows.Forms.Label();
            txt_nni = new System.Windows.Forms.TextBox();
            label21 = new System.Windows.Forms.Label();
            txt_nationnalite = new System.Windows.Forms.TextBox();
            label17 = new System.Windows.Forms.Label();
            label16 = new System.Windows.Forms.Label();
            label13 = new System.Windows.Forms.Label();
            label20 = new System.Windows.Forms.Label();
            txt_nom = new System.Windows.Forms.TextBox();
            txt_lieu_naissance = new System.Windows.Forms.TextBox();
            label18 = new System.Windows.Forms.Label();
            nom = new System.Windows.Forms.Label();
            label12 = new System.Windows.Forms.Label();
            txt_taille = new System.Windows.Forms.TextBox();
            txt_cni = new System.Windows.Forms.TextBox();
            txt_prenom = new System.Windows.Forms.TextBox();
            label15 = new System.Windows.Forms.Label();
            txt_num_unique = new System.Windows.Forms.TextBox();
            button1 = new System.Windows.Forms.Button();
            btnCaptureLittle = new System.Windows.Forms.Button();
            btnCaptureRing = new System.Windows.Forms.Button();
            btnCaptureMiddle = new System.Windows.Forms.Button();
            btnCaptureIndex = new System.Windows.Forms.Button();
            btnCaptureThumb = new System.Windows.Forms.Button();
            pictureLittle = new System.Windows.Forms.PictureBox();
            pictureRing = new System.Windows.Forms.PictureBox();
            pictureMiddle = new System.Windows.Forms.PictureBox();
            pictureIndex = new System.Windows.Forms.PictureBox();
            pictureThumb = new System.Windows.Forms.PictureBox();
            label7 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            label11 = new System.Windows.Forms.Label();
            button2 = new System.Windows.Forms.Button();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureLittle).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureRing).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureMiddle).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureIndex).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureThumb).BeginInit();
            SuspendLayout();
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(txt_dat_exp_cni);
            groupBox2.Controls.Add(txt_date_emission);
            groupBox2.Controls.Add(txt_lieu_emission);
            groupBox2.Controls.Add(txt_sexe);
            groupBox2.Controls.Add(txt_date_naissance);
            groupBox2.Controls.Add(label2);
            groupBox2.Controls.Add(label23);
            groupBox2.Controls.Add(txt_profession);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(label19);
            groupBox2.Controls.Add(label22);
            groupBox2.Controls.Add(txt_nni);
            groupBox2.Controls.Add(label21);
            groupBox2.Controls.Add(txt_nationnalite);
            groupBox2.Controls.Add(label17);
            groupBox2.Controls.Add(label16);
            groupBox2.Controls.Add(label13);
            groupBox2.Controls.Add(label20);
            groupBox2.Controls.Add(txt_nom);
            groupBox2.Controls.Add(txt_lieu_naissance);
            groupBox2.Controls.Add(label18);
            groupBox2.Controls.Add(nom);
            groupBox2.Controls.Add(label12);
            groupBox2.Controls.Add(txt_taille);
            groupBox2.Controls.Add(txt_cni);
            groupBox2.Controls.Add(txt_prenom);
            groupBox2.Controls.Add(label15);
            groupBox2.Controls.Add(txt_num_unique);
            groupBox2.Controls.Add(button1);
            groupBox2.Location = new System.Drawing.Point(12, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(776, 229);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Données Personnels";
            // 
            // txt_dat_exp_cni
            // 
            txt_dat_exp_cni.Location = new System.Drawing.Point(306, 165);
            txt_dat_exp_cni.Name = "txt_dat_exp_cni";
            txt_dat_exp_cni.Size = new System.Drawing.Size(138, 23);
            txt_dat_exp_cni.TabIndex = 31;
            // 
            // txt_date_emission
            // 
            txt_date_emission.Location = new System.Drawing.Point(552, 135);
            txt_date_emission.Name = "txt_date_emission";
            txt_date_emission.Size = new System.Drawing.Size(128, 23);
            txt_date_emission.TabIndex = 30;
            // 
            // txt_lieu_emission
            // 
            txt_lieu_emission.AcceptsReturn = true;
            txt_lieu_emission.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            txt_lieu_emission.Location = new System.Drawing.Point(553, 162);
            txt_lieu_emission.Name = "txt_lieu_emission";
            txt_lieu_emission.Size = new System.Drawing.Size(127, 23);
            txt_lieu_emission.TabIndex = 29;
            // 
            // txt_sexe
            // 
            txt_sexe.FormattingEnabled = true;
            txt_sexe.Items.AddRange(new object[] { "Homme", "Femme" });
            txt_sexe.Location = new System.Drawing.Point(638, 23);
            txt_sexe.Name = "txt_sexe";
            txt_sexe.Size = new System.Drawing.Size(97, 23);
            txt_sexe.TabIndex = 28;
            // 
            // txt_date_naissance
            // 
            txt_date_naissance.Location = new System.Drawing.Point(132, 61);
            txt_date_naissance.Name = "txt_date_naissance";
            txt_date_naissance.Size = new System.Drawing.Size(133, 23);
            txt_date_naissance.TabIndex = 27;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(450, 166);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(89, 15);
            label2.TabIndex = 26;
            label2.Text = "Lieu d'emission";
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Location = new System.Drawing.Point(222, 138);
            label23.Name = "label23";
            label23.Size = new System.Drawing.Size(62, 15);
            label23.TabIndex = 24;
            label23.Text = "Profession";
            // 
            // txt_profession
            // 
            txt_profession.AcceptsReturn = true;
            txt_profession.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            txt_profession.Location = new System.Drawing.Point(306, 134);
            txt_profession.Name = "txt_profession";
            txt_profession.Size = new System.Drawing.Size(105, 23);
            txt_profession.TabIndex = 23;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(446, 138);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(91, 15);
            label1.TabIndex = 25;
            label1.Text = "Date d'emission";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new System.Drawing.Point(557, 68);
            label19.Name = "label19";
            label19.Size = new System.Drawing.Size(72, 15);
            label19.TabIndex = 16;
            label19.Text = "Nationnalite";
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Location = new System.Drawing.Point(17, 171);
            label22.Name = "label22";
            label22.Size = new System.Drawing.Size(28, 15);
            label22.TabIndex = 22;
            label22.Text = "NNI";
            // 
            // txt_nni
            // 
            txt_nni.AcceptsReturn = true;
            txt_nni.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            txt_nni.Location = new System.Drawing.Point(93, 167);
            txt_nni.Name = "txt_nni";
            txt_nni.Size = new System.Drawing.Size(105, 23);
            txt_nni.TabIndex = 21;
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new System.Drawing.Point(222, 167);
            label21.Name = "label21";
            label21.Size = new System.Drawing.Size(76, 15);
            label21.TabIndex = 20;
            label21.Text = "Date Exp CNI";
            // 
            // txt_nationnalite
            // 
            txt_nationnalite.AcceptsReturn = true;
            txt_nationnalite.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            txt_nationnalite.Location = new System.Drawing.Point(638, 64);
            txt_nationnalite.Name = "txt_nationnalite";
            txt_nationnalite.Size = new System.Drawing.Size(105, 23);
            txt_nationnalite.TabIndex = 15;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new System.Drawing.Point(599, 26);
            label17.Name = "label17";
            label17.Size = new System.Drawing.Size(31, 15);
            label17.TabIndex = 12;
            label17.Text = "Sexe";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new System.Drawing.Point(13, 64);
            label16.Name = "label16";
            label16.Size = new System.Drawing.Size(103, 15);
            label16.TabIndex = 10;
            label16.Text = "Date de Naissance";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new System.Drawing.Point(226, 196);
            label13.Name = "label13";
            label13.Size = new System.Drawing.Size(75, 15);
            label13.TabIndex = 4;
            label13.Text = "Num Unique";
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new System.Drawing.Point(289, 64);
            label20.Name = "label20";
            label20.Size = new System.Drawing.Size(101, 15);
            label20.TabIndex = 18;
            label20.Text = "Lieu de Naissance";
            // 
            // txt_nom
            // 
            txt_nom.AcceptsReturn = true;
            txt_nom.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            txt_nom.Location = new System.Drawing.Point(132, 22);
            txt_nom.Name = "txt_nom";
            txt_nom.Size = new System.Drawing.Size(105, 23);
            txt_nom.TabIndex = 5;
            // 
            // txt_lieu_naissance
            // 
            txt_lieu_naissance.AcceptsReturn = true;
            txt_lieu_naissance.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            txt_lieu_naissance.Location = new System.Drawing.Point(399, 60);
            txt_lieu_naissance.Name = "txt_lieu_naissance";
            txt_lieu_naissance.Size = new System.Drawing.Size(105, 23);
            txt_lieu_naissance.TabIndex = 17;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new System.Drawing.Point(18, 200);
            label18.Name = "label18";
            label18.Size = new System.Drawing.Size(33, 15);
            label18.TabIndex = 14;
            label18.Text = "Taille";
            // 
            // nom
            // 
            nom.AutoSize = true;
            nom.Location = new System.Drawing.Point(11, 26);
            nom.Name = "nom";
            nom.Size = new System.Drawing.Size(34, 15);
            nom.TabIndex = 6;
            nom.Text = "Nom";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new System.Drawing.Point(9, 139);
            label12.Name = "label12";
            label12.Size = new System.Drawing.Size(57, 15);
            label12.TabIndex = 2;
            label12.Text = "Num CNI";
            // 
            // txt_taille
            // 
            txt_taille.AcceptsReturn = true;
            txt_taille.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            txt_taille.Location = new System.Drawing.Point(94, 196);
            txt_taille.Name = "txt_taille";
            txt_taille.Size = new System.Drawing.Size(105, 23);
            txt_taille.TabIndex = 13;
            // 
            // txt_cni
            // 
            txt_cni.AcceptsReturn = true;
            txt_cni.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            txt_cni.Location = new System.Drawing.Point(93, 135);
            txt_cni.Name = "txt_cni";
            txt_cni.Size = new System.Drawing.Size(105, 23);
            txt_cni.TabIndex = 1;
            // 
            // txt_prenom
            // 
            txt_prenom.AcceptsReturn = true;
            txt_prenom.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            txt_prenom.Location = new System.Drawing.Point(348, 23);
            txt_prenom.Name = "txt_prenom";
            txt_prenom.Size = new System.Drawing.Size(213, 23);
            txt_prenom.TabIndex = 7;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new System.Drawing.Point(286, 26);
            label15.Name = "label15";
            label15.Size = new System.Drawing.Size(49, 15);
            label15.TabIndex = 8;
            label15.Text = "prenom";
            // 
            // txt_num_unique
            // 
            txt_num_unique.AcceptsReturn = true;
            txt_num_unique.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            txt_num_unique.Location = new System.Drawing.Point(306, 192);
            txt_num_unique.Name = "txt_num_unique";
            txt_num_unique.Size = new System.Drawing.Size(105, 23);
            txt_num_unique.TabIndex = 3;
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(656, 192);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(114, 30);
            button1.TabIndex = 0;
            button1.Text = "Enregistrer";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // btnCaptureLittle
            // 
            btnCaptureLittle.Location = new System.Drawing.Point(611, 462);
            btnCaptureLittle.Name = "btnCaptureLittle";
            btnCaptureLittle.Size = new System.Drawing.Size(75, 23);
            btnCaptureLittle.TabIndex = 24;
            btnCaptureLittle.Text = "Capture";
            btnCaptureLittle.UseVisualStyleBackColor = true;
            btnCaptureLittle.Click += btnCaptureLittle_Click;
            // 
            // btnCaptureRing
            // 
            btnCaptureRing.Location = new System.Drawing.Point(474, 423);
            btnCaptureRing.Name = "btnCaptureRing";
            btnCaptureRing.Size = new System.Drawing.Size(75, 23);
            btnCaptureRing.TabIndex = 25;
            btnCaptureRing.Text = "Capture";
            btnCaptureRing.UseVisualStyleBackColor = true;
            btnCaptureRing.Click += btnCaptureRing_Click;
            // 
            // btnCaptureMiddle
            // 
            btnCaptureMiddle.Location = new System.Drawing.Point(344, 404);
            btnCaptureMiddle.Name = "btnCaptureMiddle";
            btnCaptureMiddle.Size = new System.Drawing.Size(75, 23);
            btnCaptureMiddle.TabIndex = 26;
            btnCaptureMiddle.Text = "Capture";
            btnCaptureMiddle.UseVisualStyleBackColor = true;
            btnCaptureMiddle.Click += btnCaptureMiddle_Click;
            // 
            // btnCaptureIndex
            // 
            btnCaptureIndex.Location = new System.Drawing.Point(217, 423);
            btnCaptureIndex.Name = "btnCaptureIndex";
            btnCaptureIndex.Size = new System.Drawing.Size(75, 23);
            btnCaptureIndex.TabIndex = 27;
            btnCaptureIndex.Text = "Capture";
            btnCaptureIndex.UseVisualStyleBackColor = true;
            btnCaptureIndex.Click += btnCaptureIndex_Click;
            // 
            // btnCaptureThumb
            // 
            btnCaptureThumb.Location = new System.Drawing.Point(89, 490);
            btnCaptureThumb.Name = "btnCaptureThumb";
            btnCaptureThumb.Size = new System.Drawing.Size(75, 23);
            btnCaptureThumb.TabIndex = 28;
            btnCaptureThumb.Text = "Capture";
            btnCaptureThumb.UseVisualStyleBackColor = true;
            btnCaptureThumb.Click += btnCaptureThumb_Click;
            // 
            // pictureLittle
            // 
            pictureLittle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pictureLittle.Location = new System.Drawing.Point(597, 326);
            pictureLittle.Name = "pictureLittle";
            pictureLittle.Size = new System.Drawing.Size(106, 130);
            pictureLittle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureLittle.TabIndex = 19;
            pictureLittle.TabStop = false;
            // 
            // pictureRing
            // 
            pictureRing.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pictureRing.Location = new System.Drawing.Point(460, 287);
            pictureRing.Name = "pictureRing";
            pictureRing.Size = new System.Drawing.Size(106, 130);
            pictureRing.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureRing.TabIndex = 20;
            pictureRing.TabStop = false;
            // 
            // pictureMiddle
            // 
            pictureMiddle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pictureMiddle.Location = new System.Drawing.Point(330, 268);
            pictureMiddle.Name = "pictureMiddle";
            pictureMiddle.Size = new System.Drawing.Size(106, 130);
            pictureMiddle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureMiddle.TabIndex = 21;
            pictureMiddle.TabStop = false;
            // 
            // pictureIndex
            // 
            pictureIndex.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pictureIndex.Location = new System.Drawing.Point(203, 287);
            pictureIndex.Name = "pictureIndex";
            pictureIndex.Size = new System.Drawing.Size(106, 130);
            pictureIndex.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureIndex.TabIndex = 22;
            pictureIndex.TabStop = false;
            // 
            // pictureThumb
            // 
            pictureThumb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pictureThumb.Location = new System.Drawing.Point(75, 354);
            pictureThumb.Name = "pictureThumb";
            pictureThumb.Size = new System.Drawing.Size(106, 130);
            pictureThumb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            pictureThumb.TabIndex = 23;
            pictureThumb.TabStop = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label7.Location = new System.Drawing.Point(101, 330);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(50, 19);
            label7.TabIndex = 29;
            label7.Text = "Pouce";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label8.Location = new System.Drawing.Point(231, 264);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(46, 19);
            label8.TabIndex = 30;
            label8.Text = "Index";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label9.Location = new System.Drawing.Point(360, 244);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(56, 19);
            label9.TabIndex = 31;
            label9.Text = "Majeur";
            label9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label10.Location = new System.Drawing.Point(478, 264);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(73, 19);
            label10.TabIndex = 32;
            label10.Text = "Annulaire";
            label10.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label11.Location = new System.Drawing.Point(610, 303);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(82, 19);
            label11.TabIndex = 33;
            label11.Text = "Auriculaire";
            label11.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // button2
            // 
            button2.Location = new System.Drawing.Point(687, 259);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(63, 12);
            button2.TabIndex = 34;
            button2.Text = "button2";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // UserRegistrationForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 541);
            Controls.Add(button2);
            Controls.Add(label11);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(btnCaptureLittle);
            Controls.Add(btnCaptureRing);
            Controls.Add(btnCaptureMiddle);
            Controls.Add(btnCaptureIndex);
            Controls.Add(btnCaptureThumb);
            Controls.Add(pictureLittle);
            Controls.Add(pictureRing);
            Controls.Add(pictureMiddle);
            Controls.Add(pictureIndex);
            Controls.Add(pictureThumb);
            Controls.Add(groupBox2);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Name = "UserRegistrationForm";
            Text = "User Registration";
            Load += UserRegistrationForm_Load;
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureLittle).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureRing).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureMiddle).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureIndex).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureThumb).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dataGridAttendance;
        private System.Windows.Forms.TextBox txtLastname;
        private System.Windows.Forms.TextBox txtFirstname;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox picturePassport;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkBoxLL;
        private System.Windows.Forms.CheckBox checkBoxLR;
        private System.Windows.Forms.CheckBox checkBoxLM;
        private System.Windows.Forms.CheckBox checkBoxLI;
        private System.Windows.Forms.CheckBox checkBoxLT;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnEnrollRight;
        private System.Windows.Forms.Button btnEnrollLeft;
        private System.Windows.Forms.CheckBox checkBoxRL;
        private System.Windows.Forms.CheckBox checkBoxRR;
        private System.Windows.Forms.CheckBox checkBoxRM;
        private System.Windows.Forms.CheckBox checkBoxRI;
        private System.Windows.Forms.CheckBox checkBoxRT;
        private System.Windows.Forms.CheckBox checkBox6;
        private System.Windows.Forms.CheckBox checkBox7;
        private System.Windows.Forms.CheckBox checkBox8;
        private System.Windows.Forms.CheckBox checkBox9;
        private System.Windows.Forms.CheckBox checkBox10;
        private System.Windows.Forms.Button btnUpdateData;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCaptureLittle;
        private System.Windows.Forms.Button btnCaptureRing;
        private System.Windows.Forms.Button btnCaptureMiddle;
        private System.Windows.Forms.Button btnCaptureIndex;
        private System.Windows.Forms.Button btnCaptureThumb;
        private System.Windows.Forms.PictureBox pictureLittle;
        private System.Windows.Forms.PictureBox pictureRing;
        private System.Windows.Forms.PictureBox pictureMiddle;
        private System.Windows.Forms.PictureBox pictureIndex;
        private System.Windows.Forms.PictureBox pictureThumb;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txt_cni;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txt_profession;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txt_nni;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txt_lieu_naissance;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txt_nationnalite;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txt_taille;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txt_prenom;
        private System.Windows.Forms.Label nom;
        private System.Windows.Forms.TextBox txt_nom;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txt_num_unique;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DateTimePicker txt_date_naissance;
        private System.Windows.Forms.ComboBox txt_sexe;
        private System.Windows.Forms.DateTimePicker txt_date_emission;
        private System.Windows.Forms.TextBox txt_lieu_emission;
        private System.Windows.Forms.DateTimePicker txt_dat_exp_cni;
    }
}