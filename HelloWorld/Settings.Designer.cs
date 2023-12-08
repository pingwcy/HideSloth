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
            groupBox5 = new GroupBox();
            Radio_Linear_PB = new RadioButton();
            Radio_LSB_PB = new RadioButton();
            button1 = new Button();
            button2 = new Button();
            groupBox1 = new GroupBox();
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
            combo_entension = new ComboBox();
            label1 = new Label();
            groupBox5.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(Radio_Linear_PB);
            groupBox5.Controls.Add(Radio_LSB_PB);
            groupBox5.Location = new Point(12, 171);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(274, 83);
            groupBox5.TabIndex = 36;
            groupBox5.TabStop = false;
            groupBox5.Text = "Algorithm";
            // 
            // Radio_Linear_PB
            // 
            Radio_Linear_PB.AutoSize = true;
            Radio_Linear_PB.Checked = true;
            Radio_Linear_PB.Location = new Point(26, 49);
            Radio_Linear_PB.Name = "Radio_Linear_PB";
            Radio_Linear_PB.Size = new Size(218, 21);
            Radio_Linear_PB.TabIndex = 1;
            Radio_Linear_PB.TabStop = true;
            Radio_Linear_PB.Text = "PNG/BMP - Linear (For large file)";
            Radio_Linear_PB.UseVisualStyleBackColor = true;
            // 
            // Radio_LSB_PB
            // 
            Radio_LSB_PB.AutoSize = true;
            Radio_LSB_PB.Location = new Point(26, 22);
            Radio_LSB_PB.Name = "Radio_LSB_PB";
            Radio_LSB_PB.Size = new Size(181, 21);
            Radio_LSB_PB.TabIndex = 0;
            Radio_LSB_PB.Text = "PNG/BMP - Traditiona LSB";
            Radio_LSB_PB.UseVisualStyleBackColor = true;
            Radio_LSB_PB.CheckedChanged += Radio_LSB_PB_CheckedChanged;
            // 
            // button1
            // 
            button1.Location = new Point(91, 367);
            button1.Name = "button1";
            button1.Size = new Size(165, 42);
            button1.TabIndex = 37;
            button1.Text = "Save and Quit";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(348, 367);
            button2.Name = "button2";
            button2.Size = new Size(165, 42);
            button2.TabIndex = 38;
            button2.Text = "Cancel and Quit";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // groupBox1
            // 
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
            Check_CustHash.Location = new Point(25, 121);
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
            Text_PBKDF2Iter.Location = new Point(47, 81);
            Text_PBKDF2Iter.Name = "Text_PBKDF2Iter";
            Text_PBKDF2Iter.Size = new Size(121, 23);
            Text_PBKDF2Iter.TabIndex = 3;
            // 
            // Check_CustIter
            // 
            Check_CustIter.AutoSize = true;
            Check_CustIter.Location = new Point(25, 54);
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
            groupBox3.Controls.Add(combo_entension);
            groupBox3.Controls.Add(label1);
            groupBox3.Location = new Point(12, 270);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(274, 76);
            groupBox3.TabIndex = 41;
            groupBox3.TabStop = false;
            groupBox3.Text = "Loaded Containers in Bulk Process";
            // 
            // combo_entension
            // 
            combo_entension.DropDownStyle = ComboBoxStyle.DropDownList;
            combo_entension.FormattingEnabled = true;
            combo_entension.Items.AddRange(new object[] { ".png", ".bmp" });
            combo_entension.Location = new Point(161, 34);
            combo_entension.Name = "combo_entension";
            combo_entension.Size = new Size(83, 25);
            combo_entension.TabIndex = 1;
            combo_entension.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 37);
            label1.Name = "label1";
            label1.Size = new Size(146, 17);
            label1.TabIndex = 0;
            label1.Text = "Output Extension Name";
            // 
            // Settings
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(590, 421);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(groupBox5);
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
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox5;
        private RadioButton Radio_Linear_PB;
        private RadioButton Radio_LSB_PB;
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
    }
}