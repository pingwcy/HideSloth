namespace HideSloth.Tools
{
    partial class Form_DecodeWizard
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
            pictureBox1 = new PictureBox();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            groupBox2 = new GroupBox();
            radio_tomany = new RadioButton();
            radio_tobig = new RadioButton();
            groupBox1 = new GroupBox();
            radio_modeencrypt = new RadioButton();
            radio_modenormal = new RadioButton();
            label1 = new Label();
            tabPage2 = new TabPage();
            numericUpDown1 = new NumericUpDown();
            check_all = new CheckBox();
            check_rerange = new CheckBox();
            button1 = new Button();
            text_loaded = new TextBox();
            label2 = new Label();
            tabPage3 = new TabPage();
            button2 = new Button();
            text_outroute = new TextBox();
            label3 = new Label();
            tabPage4 = new TabPage();
            button3 = new Button();
            text_outname = new TextBox();
            label4 = new Label();
            tabPage5 = new TabPage();
            text_pwd = new TextBox();
            label5 = new Label();
            tabPage6 = new TabPage();
            progressBar1 = new ProgressBar();
            richTextBox1 = new RichTextBox();
            label6 = new Label();
            tabPage7 = new TabPage();
            richTextBox2 = new RichTextBox();
            label7 = new Label();
            button4 = new Button();
            button_prev = new Button();
            button_next = new Button();
            button_cancel = new Button();
            panel1 = new Panel();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            tabPage3.SuspendLayout();
            tabPage4.SuspendLayout();
            tabPage5.SuspendLayout();
            tabPage6.SuspendLayout();
            tabPage7.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.wizard2;
            pictureBox1.Location = new Point(6, 31);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(180, 368);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Controls.Add(tabPage5);
            tabControl1.Controls.Add(tabPage6);
            tabControl1.Controls.Add(tabPage7);
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.Location = new Point(188, 5);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(557, 398);
            tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = SystemColors.Control;
            tabPage1.Controls.Add(groupBox2);
            tabPage1.Controls.Add(groupBox1);
            tabPage1.Controls.Add(label1);
            tabPage1.Location = new Point(4, 19);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(549, 375);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "tabPage1";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(radio_tomany);
            groupBox2.Controls.Add(radio_tobig);
            groupBox2.Location = new Point(169, 216);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(334, 120);
            groupBox2.TabIndex = 6;
            groupBox2.TabStop = false;
            groupBox2.Text = "Methods";
            // 
            // radio_tomany
            // 
            radio_tomany.AutoSize = true;
            radio_tomany.Location = new Point(55, 78);
            radio_tomany.Name = "radio_tomany";
            radio_tomany.Size = new Size(143, 21);
            radio_tomany.TabIndex = 1;
            radio_tomany.Text = "Extract to many files";
            radio_tomany.UseVisualStyleBackColor = true;
            // 
            // radio_tobig
            // 
            radio_tobig.AutoSize = true;
            radio_tobig.Checked = true;
            radio_tobig.Location = new Point(55, 35);
            radio_tobig.Name = "radio_tobig";
            radio_tobig.Size = new Size(147, 21);
            radio_tobig.TabIndex = 0;
            radio_tobig.TabStop = true;
            radio_tobig.Text = "Extract to a large file";
            radio_tobig.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(radio_modeencrypt);
            groupBox1.Controls.Add(radio_modenormal);
            groupBox1.Location = new Point(169, 74);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(334, 120);
            groupBox1.TabIndex = 5;
            groupBox1.TabStop = false;
            groupBox1.Text = "Work Mode";
            // 
            // radio_modeencrypt
            // 
            radio_modeencrypt.AutoSize = true;
            radio_modeencrypt.Enabled = false;
            radio_modeencrypt.Location = new Point(55, 75);
            radio_modeencrypt.Name = "radio_modeencrypt";
            radio_modeencrypt.Size = new Size(82, 21);
            radio_modeencrypt.TabIndex = 1;
            radio_modeencrypt.TabStop = true;
            radio_modeencrypt.Text = "Encryptor";
            radio_modeencrypt.UseVisualStyleBackColor = true;
            radio_modeencrypt.CheckedChanged += radio_modeencrypt_CheckedChanged;
            // 
            // radio_modenormal
            // 
            radio_modenormal.AutoSize = true;
            radio_modenormal.Enabled = false;
            radio_modenormal.Location = new Point(55, 35);
            radio_modenormal.Name = "radio_modenormal";
            radio_modenormal.Size = new Size(70, 21);
            radio_modenormal.TabIndex = 0;
            radio_modenormal.TabStop = true;
            radio_modenormal.Text = "Normal";
            radio_modenormal.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft YaHei UI", 18F, FontStyle.Bold);
            label1.Location = new Point(23, 18);
            label1.Name = "label1";
            label1.Size = new Size(210, 31);
            label1.TabIndex = 4;
            label1.Text = "Select Functions";
            // 
            // tabPage2
            // 
            tabPage2.BackColor = SystemColors.Control;
            tabPage2.Controls.Add(numericUpDown1);
            tabPage2.Controls.Add(check_all);
            tabPage2.Controls.Add(check_rerange);
            tabPage2.Controls.Add(button1);
            tabPage2.Controls.Add(text_loaded);
            tabPage2.Controls.Add(label2);
            tabPage2.Location = new Point(4, 19);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(549, 375);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "tabPage2";
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(294, 171);
            numericUpDown1.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(120, 23);
            numericUpDown1.TabIndex = 5;
            numericUpDown1.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // check_all
            // 
            check_all.AutoSize = true;
            check_all.Location = new Point(23, 173);
            check_all.Name = "check_all";
            check_all.Size = new Size(265, 21);
            check_all.TabIndex = 4;
            check_all.Text = "Include SubFloders with Maximum Depth";
            check_all.UseVisualStyleBackColor = true;
            // 
            // check_rerange
            // 
            check_rerange.AutoSize = true;
            check_rerange.Location = new Point(23, 137);
            check_rerange.Name = "check_rerange";
            check_rerange.Size = new Size(215, 21);
            check_rerange.TabIndex = 3;
            check_rerange.Text = "Using Sparse Files if Compatible";
            check_rerange.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Image = Properties.Resources.file;
            button1.Location = new Point(501, 89);
            button1.Name = "button1";
            button1.Size = new Size(30, 30);
            button1.TabIndex = 2;
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // text_loaded
            // 
            text_loaded.Location = new Point(23, 93);
            text_loaded.Name = "text_loaded";
            text_loaded.Size = new Size(451, 23);
            text_loaded.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft YaHei UI", 18F, FontStyle.Bold);
            label2.Location = new Point(23, 18);
            label2.Name = "label2";
            label2.Size = new Size(471, 31);
            label2.TabIndex = 0;
            label2.Text = "Select Directory of Loaded Containers";
            // 
            // tabPage3
            // 
            tabPage3.BackColor = SystemColors.Control;
            tabPage3.Controls.Add(button2);
            tabPage3.Controls.Add(text_outroute);
            tabPage3.Controls.Add(label3);
            tabPage3.Location = new Point(4, 19);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(549, 375);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "tabPage3";
            // 
            // button2
            // 
            button2.Image = Properties.Resources.file;
            button2.Location = new Point(501, 89);
            button2.Name = "button2";
            button2.Size = new Size(30, 30);
            button2.TabIndex = 2;
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // text_outroute
            // 
            text_outroute.Location = new Point(23, 93);
            text_outroute.Name = "text_outroute";
            text_outroute.Size = new Size(451, 23);
            text_outroute.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft YaHei UI", 18F, FontStyle.Bold);
            label3.Location = new Point(23, 18);
            label3.Name = "label3";
            label3.Size = new Size(299, 31);
            label3.TabIndex = 0;
            label3.Text = "Select Output Directory";
            // 
            // tabPage4
            // 
            tabPage4.BackColor = SystemColors.Control;
            tabPage4.Controls.Add(button3);
            tabPage4.Controls.Add(text_outname);
            tabPage4.Controls.Add(label4);
            tabPage4.Location = new Point(4, 5);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new Padding(3);
            tabPage4.Size = new Size(549, 389);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "tabPage4";
            // 
            // button3
            // 
            button3.Image = Properties.Resources.file;
            button3.Location = new Point(483, 68);
            button3.Name = "button3";
            button3.Size = new Size(30, 30);
            button3.TabIndex = 3;
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // text_outname
            // 
            text_outname.Location = new Point(29, 72);
            text_outname.Name = "text_outname";
            text_outname.Size = new Size(451, 23);
            text_outname.TabIndex = 2;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft YaHei UI", 18F, FontStyle.Bold);
            label4.Location = new Point(23, 18);
            label4.Name = "label4";
            label4.Size = new Size(259, 31);
            label4.TabIndex = 1;
            label4.Text = "Select Output Name";
            // 
            // tabPage5
            // 
            tabPage5.BackColor = SystemColors.Control;
            tabPage5.Controls.Add(text_pwd);
            tabPage5.Controls.Add(label5);
            tabPage5.Location = new Point(4, 5);
            tabPage5.Name = "tabPage5";
            tabPage5.Padding = new Padding(3);
            tabPage5.Size = new Size(549, 389);
            tabPage5.TabIndex = 4;
            tabPage5.Text = "tabPage5";
            // 
            // text_pwd
            // 
            text_pwd.Location = new Point(17, 67);
            text_pwd.Name = "text_pwd";
            text_pwd.Size = new Size(451, 23);
            text_pwd.TabIndex = 2;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Microsoft YaHei UI", 18F, FontStyle.Bold);
            label5.Location = new Point(23, 18);
            label5.Name = "label5";
            label5.Size = new Size(213, 31);
            label5.TabIndex = 1;
            label5.Text = "Enter Passwords";
            // 
            // tabPage6
            // 
            tabPage6.BackColor = SystemColors.Control;
            tabPage6.Controls.Add(progressBar1);
            tabPage6.Controls.Add(richTextBox1);
            tabPage6.Controls.Add(label6);
            tabPage6.Location = new Point(4, 5);
            tabPage6.Name = "tabPage6";
            tabPage6.Padding = new Padding(3);
            tabPage6.Size = new Size(549, 389);
            tabPage6.TabIndex = 5;
            tabPage6.Text = "tabPage6";
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(33, 338);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(486, 23);
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.TabIndex = 4;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(33, 61);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(486, 266);
            richTextBox1.TabIndex = 3;
            richTextBox1.Text = "";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Microsoft YaHei UI", 18F, FontStyle.Bold);
            label6.Location = new Point(23, 18);
            label6.Name = "label6";
            label6.Size = new Size(210, 31);
            label6.TabIndex = 2;
            label6.Text = "Start Processing";
            // 
            // tabPage7
            // 
            tabPage7.BackColor = SystemColors.Control;
            tabPage7.Controls.Add(richTextBox2);
            tabPage7.Controls.Add(label7);
            tabPage7.Location = new Point(4, 5);
            tabPage7.Name = "tabPage7";
            tabPage7.Padding = new Padding(3);
            tabPage7.Size = new Size(549, 389);
            tabPage7.TabIndex = 6;
            tabPage7.Text = "tabPage7";
            // 
            // richTextBox2
            // 
            richTextBox2.Location = new Point(49, 103);
            richTextBox2.Name = "richTextBox2";
            richTextBox2.Size = new Size(425, 180);
            richTextBox2.TabIndex = 2;
            richTextBox2.Text = "";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Microsoft YaHei UI", 18F, FontStyle.Bold);
            label7.Location = new Point(23, 18);
            label7.Name = "label7";
            label7.Size = new Size(114, 31);
            label7.TabIndex = 1;
            label7.Text = "Success!";
            // 
            // button4
            // 
            button4.Location = new Point(188, 405);
            button4.Name = "button4";
            button4.Size = new Size(125, 33);
            button4.TabIndex = 2;
            button4.Text = "Advanced Settings";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button_prev
            // 
            button_prev.Enabled = false;
            button_prev.Location = new Point(328, 405);
            button_prev.Name = "button_prev";
            button_prev.Size = new Size(125, 33);
            button_prev.TabIndex = 3;
            button_prev.Text = "Previous";
            button_prev.UseVisualStyleBackColor = true;
            button_prev.Click += button_prev_Click;
            // 
            // button_next
            // 
            button_next.Location = new Point(468, 405);
            button_next.Name = "button_next";
            button_next.Size = new Size(125, 33);
            button_next.TabIndex = 4;
            button_next.Text = "Next";
            button_next.UseVisualStyleBackColor = true;
            button_next.Click += button_next_Click;
            // 
            // button_cancel
            // 
            button_cancel.Location = new Point(608, 405);
            button_cancel.Name = "button_cancel";
            button_cancel.Size = new Size(125, 33);
            button_cancel.TabIndex = 5;
            button_cancel.Text = "Cancel";
            button_cancel.UseVisualStyleBackColor = true;
            button_cancel.Click += button_cancel_Click;
            // 
            // panel1
            // 
            panel1.Location = new Point(176, 5);
            panel1.Name = "panel1";
            panel1.Size = new Size(557, 20);
            panel1.TabIndex = 6;
            // 
            // Form_DecodeWizard
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(757, 450);
            Controls.Add(panel1);
            Controls.Add(button_cancel);
            Controls.Add(button_next);
            Controls.Add(button_prev);
            Controls.Add(button4);
            Controls.Add(tabControl1);
            Controls.Add(pictureBox1);
            Name = "Form_DecodeWizard";
            StartPosition = FormStartPosition.CenterParent;
            Text = "DecodeWizard";
            Load += Form_DecodeWizard_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            tabPage4.ResumeLayout(false);
            tabPage4.PerformLayout();
            tabPage5.ResumeLayout(false);
            tabPage5.PerformLayout();
            tabPage6.ResumeLayout(false);
            tabPage6.PerformLayout();
            tabPage7.ResumeLayout(false);
            tabPage7.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private TabPage tabPage5;
        private Label label1;
        private GroupBox groupBox1;
        private RadioButton radio_modenormal;
        private GroupBox groupBox2;
        private RadioButton radio_tomany;
        private RadioButton radio_tobig;
        private RadioButton radio_modeencrypt;
        private Label label2;
        private Button button1;
        private TextBox text_loaded;
        private Label label3;
        private Button button2;
        private TextBox text_outroute;
        private Label label4;
        private Label label5;
        private Button button3;
        private TextBox text_outname;
        private TextBox text_pwd;
        private TabPage tabPage6;
        private Label label6;
        private TabPage tabPage7;
        private ProgressBar progressBar1;
        private RichTextBox richTextBox1;
        private Button button4;
        private Button button_prev;
        private Button button_next;
        private Button button_cancel;
        private RichTextBox richTextBox2;
        private Label label7;
        private CheckBox check_rerange;
        private Panel panel1;
        private NumericUpDown numericUpDown1;
        private CheckBox check_all;
    }
}