namespace HideSloth
{
    partial class Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            groupBox5 = new GroupBox();
            label4 = new Label();
            combo_imgalg = new ComboBox();
            button1 = new Button();
            button2 = new Button();
            groupBox1 = new GroupBox();
            combo_encalg = new ComboBox();
            ComboBox_Hash = new ComboBox();
            Check_CustHash = new CheckBox();
            Text_PBKDF2Iter = new TextBox();
            Check_CustIter = new CheckBox();
            Radio_disableenc = new RadioButton();
            Radio_enableenc = new RadioButton();
            groupBox2 = new GroupBox();
            Radio_Encryptor = new RadioButton();
            Radio_Normal = new RadioButton();
            groupBox3 = new GroupBox();
            check_keepformat = new CheckBox();
            check_copymetaother = new CheckBox();
            check_meta = new CheckBox();
            combo_entension = new ComboBox();
            label1 = new Label();
            groupBox4 = new GroupBox();
            check_errorignore = new CheckBox();
            label3 = new Label();
            numericUpDown1 = new NumericUpDown();
            label2 = new Label();
            groupBox5.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            SuspendLayout();
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(label4);
            groupBox5.Controls.Add(combo_imgalg);
            groupBox5.Location = new Point(12, 171);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(274, 83);
            groupBox5.TabIndex = 36;
            groupBox5.TabStop = false;
            groupBox5.Text = "Algorithm";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(26, 25);
            label4.Name = "label4";
            label4.Size = new Size(45, 17);
            label4.TabIndex = 1;
            label4.Text = "Image";
            // 
            // combo_imgalg
            // 
            combo_imgalg.DropDownStyle = ComboBoxStyle.DropDownList;
            combo_imgalg.FormattingEnabled = true;
            combo_imgalg.Location = new Point(79, 22);
            combo_imgalg.Name = "combo_imgalg";
            combo_imgalg.Size = new Size(173, 25);
            combo_imgalg.TabIndex = 0;
            // 
            // button1
            // 
            button1.Location = new Point(91, 499);
            button1.Name = "button1";
            button1.Size = new Size(165, 42);
            button1.TabIndex = 37;
            button1.Text = "Save and Quit";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(348, 499);
            button2.Name = "button2";
            button2.Size = new Size(165, 42);
            button2.TabIndex = 38;
            button2.Text = "Cancel and Quit";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(combo_encalg);
            groupBox1.Controls.Add(ComboBox_Hash);
            groupBox1.Controls.Add(Check_CustHash);
            groupBox1.Controls.Add(Text_PBKDF2Iter);
            groupBox1.Controls.Add(Check_CustIter);
            groupBox1.Controls.Add(Radio_disableenc);
            groupBox1.Controls.Add(Radio_enableenc);
            groupBox1.Location = new Point(301, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(267, 242);
            groupBox1.TabIndex = 39;
            groupBox1.TabStop = false;
            groupBox1.Text = "Encryption";
            // 
            // combo_encalg
            // 
            combo_encalg.DropDownStyle = ComboBoxStyle.DropDownList;
            combo_encalg.FormattingEnabled = true;
            combo_encalg.Items.AddRange(new object[] { "AES256-GCM", "ChaCha20-Poly1305" });
            combo_encalg.Location = new Point(104, 23);
            combo_encalg.Name = "combo_encalg";
            combo_encalg.Size = new Size(147, 25);
            combo_encalg.TabIndex = 6;
            // 
            // ComboBox_Hash
            // 
            ComboBox_Hash.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBox_Hash.Enabled = false;
            ComboBox_Hash.FormattingEnabled = true;
            ComboBox_Hash.Items.AddRange(new object[] { "SHA256", "SHA384", "SHA512" });
            ComboBox_Hash.Location = new Point(47, 148);
            ComboBox_Hash.Name = "ComboBox_Hash";
            ComboBox_Hash.Size = new Size(121, 25);
            ComboBox_Hash.TabIndex = 5;
            // 
            // Check_CustHash
            // 
            Check_CustHash.AutoSize = true;
            Check_CustHash.Location = new Point(25, 122);
            Check_CustHash.Name = "Check_CustHash";
            Check_CustHash.Size = new Size(169, 21);
            Check_CustHash.TabIndex = 4;
            Check_CustHash.Text = "Customize PBKDF2 Hash";
            Check_CustHash.UseVisualStyleBackColor = true;
            Check_CustHash.CheckedChanged += checkBox2_CheckedChanged;
            // 
            // Text_PBKDF2Iter
            // 
            Text_PBKDF2Iter.Enabled = false;
            Text_PBKDF2Iter.Location = new Point(47, 90);
            Text_PBKDF2Iter.Name = "Text_PBKDF2Iter";
            Text_PBKDF2Iter.Size = new Size(121, 23);
            Text_PBKDF2Iter.TabIndex = 3;
            // 
            // Check_CustIter
            // 
            Check_CustIter.AutoSize = true;
            Check_CustIter.Location = new Point(25, 64);
            Check_CustIter.Name = "Check_CustIter";
            Check_CustIter.Size = new Size(195, 21);
            Check_CustIter.TabIndex = 2;
            Check_CustIter.Text = "Customize PBKDF2 Iterations";
            Check_CustIter.UseVisualStyleBackColor = true;
            Check_CustIter.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // Radio_disableenc
            // 
            Radio_disableenc.AutoSize = true;
            Radio_disableenc.Location = new Point(25, 197);
            Radio_disableenc.Name = "Radio_disableenc";
            Radio_disableenc.Size = new Size(77, 21);
            Radio_disableenc.TabIndex = 1;
            Radio_disableenc.TabStop = true;
            Radio_disableenc.Text = "Disabled";
            Radio_disableenc.UseVisualStyleBackColor = true;
            Radio_disableenc.CheckedChanged += Radio_disableenc_CheckedChanged;
            // 
            // Radio_enableenc
            // 
            Radio_enableenc.AutoSize = true;
            Radio_enableenc.Location = new Point(25, 27);
            Radio_enableenc.Name = "Radio_enableenc";
            Radio_enableenc.Size = new Size(73, 21);
            Radio_enableenc.TabIndex = 0;
            Radio_enableenc.TabStop = true;
            Radio_enableenc.Text = "Enabled";
            Radio_enableenc.UseVisualStyleBackColor = true;
            Radio_enableenc.CheckedChanged += Radio_enableenc_CheckedChanged;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(Radio_Encryptor);
            groupBox2.Controls.Add(Radio_Normal);
            groupBox2.Location = new Point(12, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(274, 142);
            groupBox2.TabIndex = 40;
            groupBox2.TabStop = false;
            groupBox2.Text = "Working Mode";
            // 
            // Radio_Encryptor
            // 
            Radio_Encryptor.AutoSize = true;
            Radio_Encryptor.Location = new Point(26, 97);
            Radio_Encryptor.Name = "Radio_Encryptor";
            Radio_Encryptor.Size = new Size(148, 21);
            Radio_Encryptor.TabIndex = 2;
            Radio_Encryptor.Text = "Simple File Encryptor";
            Radio_Encryptor.UseVisualStyleBackColor = true;
            Radio_Encryptor.CheckedChanged += Radio_Encryptor_CheckedChanged;
            // 
            // Radio_Normal
            // 
            Radio_Normal.Checked = true;
            Radio_Normal.Location = new Point(26, 27);
            Radio_Normal.Name = "Radio_Normal";
            Radio_Normal.Size = new Size(226, 48);
            Radio_Normal.TabIndex = 0;
            Radio_Normal.TabStop = true;
            Radio_Normal.Text = "Normal Steganography and optional Encryption";
            Radio_Normal.UseVisualStyleBackColor = true;
            Radio_Normal.CheckedChanged += Radio_Normal_CheckedChanged;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(check_keepformat);
            groupBox3.Controls.Add(check_copymetaother);
            groupBox3.Controls.Add(check_meta);
            groupBox3.Controls.Add(combo_entension);
            groupBox3.Controls.Add(label1);
            groupBox3.Location = new Point(12, 270);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(556, 109);
            groupBox3.TabIndex = 41;
            groupBox3.TabStop = false;
            groupBox3.Text = "Properties of Output";
            // 
            // check_keepformat
            // 
            check_keepformat.AutoSize = true;
            check_keepformat.Location = new Point(26, 73);
            check_keepformat.Name = "check_keepformat";
            check_keepformat.Size = new Size(260, 21);
            check_keepformat.TabIndex = 4;
            check_keepformat.Text = "Keep Extension Name in Bulk Processes";
            check_keepformat.UseVisualStyleBackColor = true;
            check_keepformat.CheckedChanged += check_keepformat_CheckedChanged;
            // 
            // check_copymetaother
            // 
            check_copymetaother.AutoSize = true;
            check_copymetaother.Location = new Point(314, 73);
            check_copymetaother.Name = "check_copymetaother";
            check_copymetaother.Size = new Size(194, 21);
            check_copymetaother.TabIndex = 3;
            check_copymetaother.Text = "Copy metadata to other files";
            check_copymetaother.UseVisualStyleBackColor = true;
            // 
            // check_meta
            // 
            check_meta.AutoSize = true;
            check_meta.Location = new Point(314, 38);
            check_meta.Name = "check_meta";
            check_meta.Size = new Size(241, 21);
            check_meta.TabIndex = 2;
            check_meta.Text = "Copy metadata to loaded containers";
            check_meta.UseVisualStyleBackColor = true;
            // 
            // combo_entension
            // 
            combo_entension.DropDownStyle = ComboBoxStyle.DropDownList;
            combo_entension.FormattingEnabled = true;
            combo_entension.Items.AddRange(new object[] { ".png", ".bmp" });
            combo_entension.Location = new Point(191, 34);
            combo_entension.Name = "combo_entension";
            combo_entension.Size = new Size(83, 25);
            combo_entension.TabIndex = 1;
            combo_entension.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(26, 37);
            label1.Name = "label1";
            label1.Size = new Size(153, 17);
            label1.TabIndex = 0;
            label1.Text = "Output Container Format";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(check_errorignore);
            groupBox4.Controls.Add(label3);
            groupBox4.Controls.Add(numericUpDown1);
            groupBox4.Controls.Add(label2);
            groupBox4.Location = new Point(12, 393);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(555, 100);
            groupBox4.TabIndex = 42;
            groupBox4.TabStop = false;
            groupBox4.Text = "Properties of Operation";
            // 
            // check_errorignore
            // 
            check_errorignore.AutoSize = true;
            check_errorignore.Location = new Point(26, 64);
            check_errorignore.Name = "check_errorignore";
            check_errorignore.Size = new Size(362, 21);
            check_errorignore.TabIndex = 3;
            check_errorignore.Text = "Ignore individual failure in bulk data extracting processes";
            check_errorignore.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(440, 30);
            label3.Name = "label3";
            label3.Size = new Size(24, 17);
            label3.TabIndex = 2;
            label3.Text = "KB";
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(314, 28);
            numericUpDown1.Maximum = new decimal(new int[] { 1024, 0, 0, 0 });
            numericUpDown1.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(120, 23);
            numericUpDown1.TabIndex = 1;
            numericUpDown1.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(26, 30);
            label2.Name = "label2";
            label2.Size = new Size(279, 17);
            label2.TabIndex = 0;
            label2.Text = "Ignore Images with Capacity Not Higher Than ";
            // 
            // Settings
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(590, 553);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(groupBox5);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Settings";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Advanced Settings";
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox5;
        private Button button1;
        private Button button2;
        private GroupBox groupBox1;
        private CheckBox Check_CustIter;
        private RadioButton Radio_disableenc;
        private RadioButton Radio_enableenc;
        private TextBox Text_PBKDF2Iter;
        private ComboBox ComboBox_Hash;
        private CheckBox Check_CustHash;
        private GroupBox groupBox2;
        private RadioButton Radio_Normal;
        private RadioButton Radio_Encryptor;
        private GroupBox groupBox3;
        private ComboBox combo_entension;
        private Label label1;
        private CheckBox check_meta;
        private CheckBox check_keepformat;
        private CheckBox check_copymetaother;
        private GroupBox groupBox4;
        private Label label3;
        private NumericUpDown numericUpDown1;
        private Label label2;
        private ComboBox combo_encalg;
        private CheckBox check_errorignore;
        private Label label4;
        private ComboBox combo_imgalg;
    }
}