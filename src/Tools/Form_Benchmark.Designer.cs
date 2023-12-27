namespace HideSloth
{
    partial class Form_Benchmark
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Benchmark));
            tabControl1 = new TabControl();
            Tab_Ste = new TabPage();
            listBox1 = new ListView();
            Button_testcancecl = new Button();
            Button_Teststart = new Button();
            groupBox1 = new GroupBox();
            Combo_Cycle = new ComboBox();
            Combo_buff = new ComboBox();
            label2 = new Label();
            Combo_Alg = new ComboBox();
            label1 = new Label();
            Tab_Encryption = new TabPage();
            listView1 = new ListView();
            button2 = new Button();
            button1 = new Button();
            groupBox2 = new GroupBox();
            label4 = new Label();
            label3 = new Label();
            combo_enccycle = new ComboBox();
            combo_encbuff = new ComboBox();
            combo_encalg = new ComboBox();
            Tab_KDF = new TabPage();
            listView2 = new ListView();
            button_kdfcan = new Button();
            button_kdf = new Button();
            groupBox3 = new GroupBox();
            combo_sha = new ComboBox();
            combo_iter = new ComboBox();
            label6 = new Label();
            combo_kdf = new ComboBox();
            label5 = new Label();
            progressBar1 = new ProgressBar();
            tabControl1.SuspendLayout();
            Tab_Ste.SuspendLayout();
            groupBox1.SuspendLayout();
            Tab_Encryption.SuspendLayout();
            groupBox2.SuspendLayout();
            Tab_KDF.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(Tab_Ste);
            tabControl1.Controls.Add(Tab_Encryption);
            tabControl1.Controls.Add(Tab_KDF);
            tabControl1.Location = new Point(9, 5);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(413, 376);
            tabControl1.TabIndex = 0;
            // 
            // Tab_Ste
            // 
            Tab_Ste.Controls.Add(listBox1);
            Tab_Ste.Controls.Add(Button_testcancecl);
            Tab_Ste.Controls.Add(Button_Teststart);
            Tab_Ste.Controls.Add(groupBox1);
            Tab_Ste.Location = new Point(4, 26);
            Tab_Ste.Name = "Tab_Ste";
            Tab_Ste.Padding = new Padding(3);
            Tab_Ste.Size = new Size(405, 346);
            Tab_Ste.TabIndex = 0;
            Tab_Ste.Text = "Steganography";
            Tab_Ste.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            listBox1.Location = new Point(30, 162);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(354, 160);
            listBox1.TabIndex = 6;
            listBox1.UseCompatibleStateImageBehavior = false;
            // 
            // Button_testcancecl
            // 
            Button_testcancecl.Location = new Point(210, 122);
            Button_testcancecl.Name = "Button_testcancecl";
            Button_testcancecl.Size = new Size(83, 23);
            Button_testcancecl.TabIndex = 5;
            Button_testcancecl.Text = "Cancel Test";
            Button_testcancecl.UseVisualStyleBackColor = true;
            Button_testcancecl.Click += Button_testcancecl_Click;
            // 
            // Button_Teststart
            // 
            Button_Teststart.Location = new Point(98, 122);
            Button_Teststart.Name = "Button_Teststart";
            Button_Teststart.Size = new Size(83, 23);
            Button_Teststart.TabIndex = 4;
            Button_Teststart.Text = "Start Test";
            Button_Teststart.UseVisualStyleBackColor = true;
            Button_Teststart.Click += Button_Teststart_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(Combo_Cycle);
            groupBox1.Controls.Add(Combo_buff);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(Combo_Alg);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(30, 6);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(354, 110);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Algorithm Choose";
            // 
            // Combo_Cycle
            // 
            Combo_Cycle.DropDownStyle = ComboBoxStyle.DropDownList;
            Combo_Cycle.FormattingEnabled = true;
            Combo_Cycle.Items.AddRange(new object[] { "1", "5", "10", "50", "100", "500" });
            Combo_Cycle.Location = new Point(242, 64);
            Combo_Cycle.Name = "Combo_Cycle";
            Combo_Cycle.Size = new Size(71, 25);
            Combo_Cycle.TabIndex = 4;
            // 
            // Combo_buff
            // 
            Combo_buff.DropDownStyle = ComboBoxStyle.DropDownList;
            Combo_buff.FormattingEnabled = true;
            Combo_buff.Items.AddRange(new object[] { "100KB", "500KB", "1MB", "5MB", "10MB", "50MB", "100MB" });
            Combo_buff.Location = new Point(169, 64);
            Combo_buff.Name = "Combo_buff";
            Combo_buff.Size = new Size(67, 25);
            Combo_buff.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 67);
            label2.Name = "label2";
            label2.Size = new Size(158, 17);
            label2.TabIndex = 2;
            label2.Text = "Test Buffer Size and Cycle";
            // 
            // Combo_Alg
            // 
            Combo_Alg.DropDownStyle = ComboBoxStyle.DropDownList;
            Combo_Alg.FormattingEnabled = true;
            Combo_Alg.Location = new Point(169, 28);
            Combo_Alg.Name = "Combo_Alg";
            Combo_Alg.Size = new Size(144, 25);
            Combo_Alg.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 31);
            label1.Name = "label1";
            label1.Size = new Size(157, 17);
            label1.TabIndex = 1;
            label1.Text = "Steganography algorithm";
            // 
            // Tab_Encryption
            // 
            Tab_Encryption.Controls.Add(listView1);
            Tab_Encryption.Controls.Add(button2);
            Tab_Encryption.Controls.Add(button1);
            Tab_Encryption.Controls.Add(groupBox2);
            Tab_Encryption.Location = new Point(4, 26);
            Tab_Encryption.Name = "Tab_Encryption";
            Tab_Encryption.Padding = new Padding(3);
            Tab_Encryption.Size = new Size(405, 346);
            Tab_Encryption.TabIndex = 1;
            Tab_Encryption.Text = "Encryption";
            Tab_Encryption.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            listView1.Location = new Point(30, 162);
            listView1.Name = "listView1";
            listView1.Size = new Size(354, 160);
            listView1.TabIndex = 3;
            listView1.UseCompatibleStateImageBehavior = false;
            // 
            // button2
            // 
            button2.Location = new Point(210, 122);
            button2.Name = "button2";
            button2.Size = new Size(83, 23);
            button2.TabIndex = 2;
            button2.Text = "Cancel Test";
            button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(98, 122);
            button1.Name = "button1";
            button1.Size = new Size(83, 23);
            button1.TabIndex = 1;
            button1.Text = "Start Test";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(combo_enccycle);
            groupBox2.Controls.Add(combo_encbuff);
            groupBox2.Controls.Add(combo_encalg);
            groupBox2.Location = new Point(30, 6);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(354, 110);
            groupBox2.TabIndex = 0;
            groupBox2.TabStop = false;
            groupBox2.Text = "Algorithm Choose";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 67);
            label4.Name = "label4";
            label4.Size = new Size(158, 17);
            label4.TabIndex = 4;
            label4.Text = "Test Buffer Size and Cycle";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 31);
            label3.Name = "label3";
            label3.Size = new Size(157, 17);
            label3.TabIndex = 3;
            label3.Text = "Steganography algorithm";
            // 
            // combo_enccycle
            // 
            combo_enccycle.DropDownStyle = ComboBoxStyle.DropDownList;
            combo_enccycle.FormattingEnabled = true;
            combo_enccycle.Items.AddRange(new object[] { "1", "5", "10", "50", "100" });
            combo_enccycle.Location = new Point(242, 64);
            combo_enccycle.Name = "combo_enccycle";
            combo_enccycle.Size = new Size(71, 25);
            combo_enccycle.TabIndex = 2;
            // 
            // combo_encbuff
            // 
            combo_encbuff.DropDownStyle = ComboBoxStyle.DropDownList;
            combo_encbuff.FormattingEnabled = true;
            combo_encbuff.Items.AddRange(new object[] { "100 KB", "500 KB", "1 MB", "5 MB", "50 MB", "100 MB", "500 MB" });
            combo_encbuff.Location = new Point(169, 64);
            combo_encbuff.Name = "combo_encbuff";
            combo_encbuff.Size = new Size(67, 25);
            combo_encbuff.TabIndex = 1;
            // 
            // combo_encalg
            // 
            combo_encalg.DropDownStyle = ComboBoxStyle.DropDownList;
            combo_encalg.FormattingEnabled = true;
            combo_encalg.Items.AddRange(new object[] { "AES256-GCM", "ChaCha20-Poly1305", "All" });
            combo_encalg.Location = new Point(169, 28);
            combo_encalg.Name = "combo_encalg";
            combo_encalg.Size = new Size(144, 25);
            combo_encalg.TabIndex = 0;
            // 
            // Tab_KDF
            // 
            Tab_KDF.Controls.Add(listView2);
            Tab_KDF.Controls.Add(button_kdfcan);
            Tab_KDF.Controls.Add(button_kdf);
            Tab_KDF.Controls.Add(groupBox3);
            Tab_KDF.Location = new Point(4, 26);
            Tab_KDF.Name = "Tab_KDF";
            Tab_KDF.Size = new Size(405, 346);
            Tab_KDF.TabIndex = 2;
            Tab_KDF.Text = "KDF";
            Tab_KDF.UseVisualStyleBackColor = true;
            // 
            // listView2
            // 
            listView2.Location = new Point(30, 162);
            listView2.Name = "listView2";
            listView2.Size = new Size(354, 160);
            listView2.TabIndex = 3;
            listView2.UseCompatibleStateImageBehavior = false;
            // 
            // button_kdfcan
            // 
            button_kdfcan.Location = new Point(210, 122);
            button_kdfcan.Name = "button_kdfcan";
            button_kdfcan.Size = new Size(83, 23);
            button_kdfcan.TabIndex = 2;
            button_kdfcan.Text = "Cancel Test";
            button_kdfcan.UseVisualStyleBackColor = true;
            // 
            // button_kdf
            // 
            button_kdf.Location = new Point(98, 122);
            button_kdf.Name = "button_kdf";
            button_kdf.Size = new Size(83, 23);
            button_kdf.TabIndex = 1;
            button_kdf.Text = "Start Test";
            button_kdf.UseVisualStyleBackColor = true;
            button_kdf.Click += button_kdf_Click;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(combo_sha);
            groupBox3.Controls.Add(combo_iter);
            groupBox3.Controls.Add(label6);
            groupBox3.Controls.Add(combo_kdf);
            groupBox3.Controls.Add(label5);
            groupBox3.Location = new Point(30, 6);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(354, 110);
            groupBox3.TabIndex = 0;
            groupBox3.TabStop = false;
            groupBox3.Text = "KDF Algorithm";
            // 
            // combo_sha
            // 
            combo_sha.DropDownStyle = ComboBoxStyle.DropDownList;
            combo_sha.FormattingEnabled = true;
            combo_sha.Items.AddRange(new object[] { "SHA256", "SHA384", "SHA512" });
            combo_sha.Location = new Point(232, 63);
            combo_sha.Name = "combo_sha";
            combo_sha.Size = new Size(81, 25);
            combo_sha.TabIndex = 8;
            // 
            // combo_iter
            // 
            combo_iter.FormattingEnabled = true;
            combo_iter.Items.AddRange(new object[] { "100000", "200000", "300000", "400000" });
            combo_iter.Location = new Point(113, 63);
            combo_iter.Name = "combo_iter";
            combo_iter.Size = new Size(109, 25);
            combo_iter.TabIndex = 7;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(6, 66);
            label6.Name = "label6";
            label6.Size = new Size(101, 17);
            label6.TabIndex = 6;
            label6.Text = "KDF Parameters";
            // 
            // combo_kdf
            // 
            combo_kdf.DropDownStyle = ComboBoxStyle.DropDownList;
            combo_kdf.FormattingEnabled = true;
            combo_kdf.Items.AddRange(new object[] { "PBKDF2" });
            combo_kdf.Location = new Point(169, 28);
            combo_kdf.Name = "combo_kdf";
            combo_kdf.Size = new Size(144, 25);
            combo_kdf.TabIndex = 5;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(6, 31);
            label5.Name = "label5";
            label5.Size = new Size(144, 17);
            label5.TabIndex = 4;
            label5.Text = "Key Derivation Function";
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(13, 387);
            progressBar1.MarqueeAnimationSpeed = 50;
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(405, 23);
            progressBar1.TabIndex = 1;
            // 
            // Form_Benchmark
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(434, 422);
            Controls.Add(progressBar1);
            Controls.Add(tabControl1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form_Benchmark";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Benchmark";
            tabControl1.ResumeLayout(false);
            Tab_Ste.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            Tab_Encryption.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            Tab_KDF.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage Tab_Ste;
        private TabPage Tab_Encryption;
        private TabPage Tab_KDF;
        private GroupBox groupBox1;
        private ComboBox Combo_Alg;
        private Label label1;
        private Button Button_testcancecl;
        private Button Button_Teststart;
        private ListView listBox1;
        private Label label2;
        private ComboBox Combo_Cycle;
        private ComboBox Combo_buff;
        private ProgressBar progressBar1;
        private GroupBox groupBox2;
        private ComboBox combo_encalg;
        private ComboBox combo_enccycle;
        private ComboBox combo_encbuff;
        private Label label4;
        private Label label3;
        private Button button2;
        private Button button1;
        private ListView listView1;
        private GroupBox groupBox3;
        private ComboBox combo_kdf;
        private Label label5;
        private Label label6;
        private ComboBox combo_sha;
        private ComboBox combo_iter;
        private Button button_kdfcan;
        private Button button_kdf;
        private ListView listView2;
    }
}