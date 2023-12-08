namespace HideSloth.Tools
{
    partial class Form_EncodeWizard
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
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            label1 = new Label();
            groupBox2 = new GroupBox();
            label11 = new Label();
            Radio_Encryptorw = new RadioButton();
            Radio_normalw = new RadioButton();
            groupBox1 = new GroupBox();
            Radio_manytomany = new RadioButton();
            Radio_onetomany = new RadioButton();
            tabPage2 = new TabPage();
            button1 = new Button();
            text_lagrone = new TextBox();
            label2 = new Label();
            tabPage3 = new TabPage();
            numericUpDown1 = new NumericUpDown();
            check_searchdeepcontainer = new CheckBox();
            button2 = new Button();
            text_containers = new TextBox();
            label3 = new Label();
            tabPage4 = new TabPage();
            progressBar2 = new ProgressBar();
            list_capacity = new ListView();
            label4 = new Label();
            tabPage5 = new TabPage();
            richTextBox3 = new RichTextBox();
            progressBar1 = new ProgressBar();
            label5 = new Label();
            tabPage6 = new TabPage();
            button3 = new Button();
            text_routemanysecrets = new TextBox();
            label7 = new Label();
            tabPage7 = new TabPage();
            box_summary = new RichTextBox();
            label8 = new Label();
            tabPage8 = new TabPage();
            richTextBox1 = new RichTextBox();
            label6 = new Label();
            tabPage9 = new TabPage();
            radio_outputkeepstructure = new RadioButton();
            radio_alltoonefolder = new RadioButton();
            check_copynonimage = new CheckBox();
            button4 = new Button();
            text_outputroute = new TextBox();
            label9 = new Label();
            tabPage10 = new TabPage();
            text_pwd = new TextBox();
            label10 = new Label();
            button_pre = new Button();
            button_next = new Button();
            button_cancel = new Button();
            pictureBox1 = new PictureBox();
            button5 = new Button();
            panel1 = new Panel();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            tabPage2.SuspendLayout();
            tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            tabPage4.SuspendLayout();
            tabPage5.SuspendLayout();
            tabPage6.SuspendLayout();
            tabPage7.SuspendLayout();
            tabPage8.SuspendLayout();
            tabPage9.SuspendLayout();
            tabPage10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
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
            tabControl1.Controls.Add(tabPage8);
            tabControl1.Controls.Add(tabPage9);
            tabControl1.Controls.Add(tabPage10);
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.Location = new Point(188, 5);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(557, 398);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = SystemColors.Control;
            tabPage1.Controls.Add(label1);
            tabPage1.Controls.Add(groupBox2);
            tabPage1.Controls.Add(groupBox1);
            tabPage1.Location = new Point(4, 19);
            tabPage1.Margin = new Padding(0);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(549, 375);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "t1";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft YaHei UI", 18F, FontStyle.Bold);
            label1.Location = new Point(23, 18);
            label1.Name = "label1";
            label1.Size = new Size(210, 31);
            label1.TabIndex = 3;
            label1.Text = "Select Functions";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(label11);
            groupBox2.Controls.Add(Radio_Encryptorw);
            groupBox2.Controls.Add(Radio_normalw);
            groupBox2.Location = new Point(169, 74);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(334, 120);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Working Mode";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(14, 25);
            label11.Name = "label11";
            label11.Size = new Size(306, 17);
            label11.TabIndex = 2;
            label11.Text = "Please Use Advanced Setting to Change this Mode.";
            // 
            // Radio_Encryptorw
            // 
            Radio_Encryptorw.AutoSize = true;
            Radio_Encryptorw.Enabled = false;
            Radio_Encryptorw.Location = new Point(47, 84);
            Radio_Encryptorw.Name = "Radio_Encryptorw";
            Radio_Encryptorw.Size = new Size(82, 21);
            Radio_Encryptorw.TabIndex = 1;
            Radio_Encryptorw.TabStop = true;
            Radio_Encryptorw.Text = "Encryptor";
            Radio_Encryptorw.UseVisualStyleBackColor = true;
            Radio_Encryptorw.CheckedChanged += Radio_Encryptorw_CheckedChanged;
            // 
            // Radio_normalw
            // 
            Radio_normalw.AutoSize = true;
            Radio_normalw.Enabled = false;
            Radio_normalw.Location = new Point(47, 48);
            Radio_normalw.Name = "Radio_normalw";
            Radio_normalw.Size = new Size(70, 21);
            Radio_normalw.TabIndex = 0;
            Radio_normalw.TabStop = true;
            Radio_normalw.Text = "Normal";
            Radio_normalw.UseVisualStyleBackColor = true;
            Radio_normalw.CheckedChanged += Radio_normalw_CheckedChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(Radio_manytomany);
            groupBox1.Controls.Add(Radio_onetomany);
            groupBox1.Location = new Point(169, 216);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(334, 120);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Methods";
            // 
            // Radio_manytomany
            // 
            Radio_manytomany.AutoSize = true;
            Radio_manytomany.Location = new Point(47, 75);
            Radio_manytomany.Name = "Radio_manytomany";
            Radio_manytomany.Size = new Size(200, 21);
            Radio_manytomany.TabIndex = 1;
            Radio_manytomany.Text = "Many files to many containers";
            Radio_manytomany.UseVisualStyleBackColor = true;
            // 
            // Radio_onetomany
            // 
            Radio_onetomany.AutoSize = true;
            Radio_onetomany.Checked = true;
            Radio_onetomany.Location = new Point(47, 37);
            Radio_onetomany.Name = "Radio_onetomany";
            Radio_onetomany.Size = new Size(220, 21);
            Radio_onetomany.TabIndex = 0;
            Radio_onetomany.TabStop = true;
            Radio_onetomany.Text = "One large file to many containers";
            Radio_onetomany.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.BackColor = SystemColors.Control;
            tabPage2.Controls.Add(button1);
            tabPage2.Controls.Add(text_lagrone);
            tabPage2.Controls.Add(label2);
            tabPage2.Location = new Point(4, 5);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(549, 389);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "t2";
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
            // text_lagrone
            // 
            text_lagrone.Location = new Point(23, 93);
            text_lagrone.Name = "text_lagrone";
            text_lagrone.Size = new Size(451, 23);
            text_lagrone.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft YaHei UI", 18F, FontStyle.Bold);
            label2.Location = new Point(23, 18);
            label2.Name = "label2";
            label2.Size = new Size(335, 31);
            label2.TabIndex = 0;
            label2.Text = "Select the Single Large File";
            // 
            // tabPage3
            // 
            tabPage3.BackColor = SystemColors.Control;
            tabPage3.Controls.Add(numericUpDown1);
            tabPage3.Controls.Add(check_searchdeepcontainer);
            tabPage3.Controls.Add(button2);
            tabPage3.Controls.Add(text_containers);
            tabPage3.Controls.Add(label3);
            tabPage3.Location = new Point(4, 5);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(549, 389);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "t3";
            // 
            // numericUpDown1
            // 
            numericUpDown1.Enabled = false;
            numericUpDown1.Location = new Point(319, 156);
            numericUpDown1.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(120, 23);
            numericUpDown1.TabIndex = 5;
            numericUpDown1.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // check_searchdeepcontainer
            // 
            check_searchdeepcontainer.AutoSize = true;
            check_searchdeepcontainer.Location = new Point(23, 157);
            check_searchdeepcontainer.Name = "check_searchdeepcontainer";
            check_searchdeepcontainer.Size = new Size(290, 21);
            check_searchdeepcontainer.TabIndex = 3;
            check_searchdeepcontainer.Text = "Serach Subfolders with the Max Folder depth";
            check_searchdeepcontainer.UseVisualStyleBackColor = true;
            check_searchdeepcontainer.CheckedChanged += check_deepcontainer_CheckedChanged;
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
            // text_containers
            // 
            text_containers.Location = new Point(23, 93);
            text_containers.Name = "text_containers";
            text_containers.Size = new Size(451, 23);
            text_containers.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft YaHei UI", 18F, FontStyle.Bold);
            label3.Location = new Point(23, 18);
            label3.Name = "label3";
            label3.Size = new Size(422, 31);
            label3.TabIndex = 0;
            label3.Text = "Select the Directory of Containers";
            // 
            // tabPage4
            // 
            tabPage4.BackColor = SystemColors.Control;
            tabPage4.Controls.Add(progressBar2);
            tabPage4.Controls.Add(list_capacity);
            tabPage4.Controls.Add(label4);
            tabPage4.Location = new Point(4, 19);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new Padding(3);
            tabPage4.Size = new Size(549, 375);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "t4";
            // 
            // progressBar2
            // 
            progressBar2.Location = new Point(22, 339);
            progressBar2.Name = "progressBar2";
            progressBar2.Size = new Size(507, 23);
            progressBar2.Style = ProgressBarStyle.Marquee;
            progressBar2.TabIndex = 2;
            // 
            // list_capacity
            // 
            list_capacity.Location = new Point(22, 60);
            list_capacity.Name = "list_capacity";
            list_capacity.Size = new Size(507, 272);
            list_capacity.TabIndex = 1;
            list_capacity.UseCompatibleStateImageBehavior = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft YaHei UI", 18F, FontStyle.Bold);
            label4.Location = new Point(23, 18);
            label4.Name = "label4";
            label4.Size = new Size(196, 31);
            label4.TabIndex = 0;
            label4.Text = "Capacity Check";
            // 
            // tabPage5
            // 
            tabPage5.BackColor = SystemColors.Control;
            tabPage5.Controls.Add(richTextBox3);
            tabPage5.Controls.Add(progressBar1);
            tabPage5.Controls.Add(label5);
            tabPage5.Location = new Point(4, 5);
            tabPage5.Name = "tabPage5";
            tabPage5.Padding = new Padding(3);
            tabPage5.Size = new Size(549, 389);
            tabPage5.TabIndex = 4;
            tabPage5.Text = "t5";
            // 
            // richTextBox3
            // 
            richTextBox3.Location = new Point(49, 103);
            richTextBox3.Name = "richTextBox3";
            richTextBox3.Size = new Size(469, 173);
            richTextBox3.TabIndex = 2;
            richTextBox3.Text = "";
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(49, 321);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(469, 23);
            progressBar1.TabIndex = 1;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Microsoft YaHei UI", 18F, FontStyle.Bold);
            label5.Location = new Point(23, 18);
            label5.Name = "label5";
            label5.Size = new Size(210, 31);
            label5.TabIndex = 0;
            label5.Text = "Start Processing";
            // 
            // tabPage6
            // 
            tabPage6.BackColor = SystemColors.Control;
            tabPage6.Controls.Add(button3);
            tabPage6.Controls.Add(text_routemanysecrets);
            tabPage6.Controls.Add(label7);
            tabPage6.Location = new Point(4, 5);
            tabPage6.Name = "tabPage6";
            tabPage6.Padding = new Padding(3);
            tabPage6.Size = new Size(549, 389);
            tabPage6.TabIndex = 5;
            tabPage6.Text = "t6";
            // 
            // button3
            // 
            button3.Image = Properties.Resources.file;
            button3.Location = new Point(501, 89);
            button3.Name = "button3";
            button3.Size = new Size(30, 30);
            button3.TabIndex = 2;
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // text_routemanysecrets
            // 
            text_routemanysecrets.Location = new Point(23, 93);
            text_routemanysecrets.Name = "text_routemanysecrets";
            text_routemanysecrets.Size = new Size(451, 23);
            text_routemanysecrets.TabIndex = 1;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Microsoft YaHei UI", 18F, FontStyle.Bold);
            label7.Location = new Point(23, 18);
            label7.Name = "label7";
            label7.Size = new Size(425, 31);
            label7.TabIndex = 0;
            label7.Text = "Select the Directory of Secret Files";
            // 
            // tabPage7
            // 
            tabPage7.BackColor = SystemColors.Control;
            tabPage7.Controls.Add(box_summary);
            tabPage7.Controls.Add(label8);
            tabPage7.Location = new Point(4, 5);
            tabPage7.Name = "tabPage7";
            tabPage7.Padding = new Padding(3);
            tabPage7.Size = new Size(549, 389);
            tabPage7.TabIndex = 6;
            tabPage7.Text = "t7";
            // 
            // box_summary
            // 
            box_summary.Location = new Point(49, 103);
            box_summary.Name = "box_summary";
            box_summary.Size = new Size(425, 180);
            box_summary.TabIndex = 1;
            box_summary.Text = "";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Microsoft YaHei UI", 18F, FontStyle.Bold);
            label8.Location = new Point(23, 18);
            label8.Name = "label8";
            label8.Size = new Size(237, 31);
            label8.TabIndex = 0;
            label8.Text = "Summary of Tasks";
            // 
            // tabPage8
            // 
            tabPage8.BackColor = SystemColors.Control;
            tabPage8.Controls.Add(richTextBox1);
            tabPage8.Controls.Add(label6);
            tabPage8.Location = new Point(4, 5);
            tabPage8.Name = "tabPage8";
            tabPage8.Padding = new Padding(3);
            tabPage8.Size = new Size(549, 389);
            tabPage8.TabIndex = 7;
            tabPage8.Text = "t8";
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(49, 103);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(425, 180);
            richTextBox1.TabIndex = 1;
            richTextBox1.Text = "";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Microsoft YaHei UI", 18F, FontStyle.Bold);
            label6.Location = new Point(23, 18);
            label6.Name = "label6";
            label6.Size = new Size(114, 31);
            label6.TabIndex = 0;
            label6.Text = "Success!";
            // 
            // tabPage9
            // 
            tabPage9.BackColor = SystemColors.Control;
            tabPage9.Controls.Add(radio_outputkeepstructure);
            tabPage9.Controls.Add(radio_alltoonefolder);
            tabPage9.Controls.Add(check_copynonimage);
            tabPage9.Controls.Add(button4);
            tabPage9.Controls.Add(text_outputroute);
            tabPage9.Controls.Add(label9);
            tabPage9.Location = new Point(4, 5);
            tabPage9.Name = "tabPage9";
            tabPage9.Padding = new Padding(3);
            tabPage9.Size = new Size(549, 389);
            tabPage9.TabIndex = 8;
            tabPage9.Text = "t9";
            // 
            // radio_outputkeepstructure
            // 
            radio_outputkeepstructure.AutoSize = true;
            radio_outputkeepstructure.Enabled = false;
            radio_outputkeepstructure.Location = new Point(23, 183);
            radio_outputkeepstructure.Name = "radio_outputkeepstructure";
            radio_outputkeepstructure.Size = new Size(324, 21);
            radio_outputkeepstructure.TabIndex = 7;
            radio_outputkeepstructure.Text = "Output Loaded Containers with Directory Structure ";
            radio_outputkeepstructure.UseVisualStyleBackColor = true;
            // 
            // radio_alltoonefolder
            // 
            radio_alltoonefolder.AutoSize = true;
            radio_alltoonefolder.Checked = true;
            radio_alltoonefolder.Location = new Point(23, 145);
            radio_alltoonefolder.Name = "radio_alltoonefolder";
            radio_alltoonefolder.Size = new Size(260, 21);
            radio_alltoonefolder.TabIndex = 6;
            radio_alltoonefolder.TabStop = true;
            radio_alltoonefolder.Text = "Output All Loaded Images to one folder";
            radio_alltoonefolder.UseVisualStyleBackColor = true;
            // 
            // check_copynonimage
            // 
            check_copynonimage.AutoSize = true;
            check_copynonimage.Location = new Point(23, 252);
            check_copynonimage.Name = "check_copynonimage";
            check_copynonimage.Size = new Size(261, 21);
            check_copynonimage.TabIndex = 4;
            check_copynonimage.Text = "Copy non containers to output directory";
            check_copynonimage.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Image = Properties.Resources.file;
            button4.Location = new Point(501, 89);
            button4.Name = "button4";
            button4.Size = new Size(30, 30);
            button4.TabIndex = 2;
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // text_outputroute
            // 
            text_outputroute.Location = new Point(23, 93);
            text_outputroute.Name = "text_outputroute";
            text_outputroute.Size = new Size(451, 23);
            text_outputroute.TabIndex = 1;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Microsoft YaHei UI", 18F, FontStyle.Bold);
            label9.Location = new Point(23, 18);
            label9.Name = "label9";
            label9.Size = new Size(346, 31);
            label9.TabIndex = 0;
            label9.Text = "Select the Output Directory";
            // 
            // tabPage10
            // 
            tabPage10.BackColor = SystemColors.Control;
            tabPage10.Controls.Add(text_pwd);
            tabPage10.Controls.Add(label10);
            tabPage10.Location = new Point(4, 5);
            tabPage10.Name = "tabPage10";
            tabPage10.Padding = new Padding(3);
            tabPage10.Size = new Size(549, 389);
            tabPage10.TabIndex = 9;
            tabPage10.Text = "t10";
            tabPage10.Click += tabPage10_Click;
            // 
            // text_pwd
            // 
            text_pwd.Location = new Point(23, 93);
            text_pwd.Name = "text_pwd";
            text_pwd.Size = new Size(451, 23);
            text_pwd.TabIndex = 1;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Microsoft YaHei UI", 18F, FontStyle.Bold);
            label10.Location = new Point(23, 18);
            label10.Name = "label10";
            label10.Size = new Size(236, 31);
            label10.TabIndex = 0;
            label10.Text = "Password Settings";
            // 
            // button_pre
            // 
            button_pre.Location = new Point(328, 405);
            button_pre.Name = "button_pre";
            button_pre.Size = new Size(125, 33);
            button_pre.TabIndex = 1;
            button_pre.Text = "Previous Step";
            button_pre.UseVisualStyleBackColor = true;
            button_pre.Click += button_pre_Click;
            // 
            // button_next
            // 
            button_next.Location = new Point(468, 405);
            button_next.Name = "button_next";
            button_next.Size = new Size(125, 33);
            button_next.TabIndex = 2;
            button_next.Text = "Next Step";
            button_next.UseVisualStyleBackColor = true;
            button_next.Click += button_next_Click;
            // 
            // button_cancel
            // 
            button_cancel.Location = new Point(608, 405);
            button_cancel.Name = "button_cancel";
            button_cancel.Size = new Size(125, 33);
            button_cancel.TabIndex = 3;
            button_cancel.Text = "Cancel";
            button_cancel.UseVisualStyleBackColor = true;
            button_cancel.Click += button_cancel_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.wizard;
            pictureBox1.Location = new Point(6, 31);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(180, 368);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // button5
            // 
            button5.Location = new Point(188, 405);
            button5.Name = "button5";
            button5.Size = new Size(125, 33);
            button5.TabIndex = 5;
            button5.Text = "Advanced Settings";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // panel1
            // 
            panel1.Location = new Point(146, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(610, 23);
            panel1.TabIndex = 6;
            // 
            // Form_EncodeWizard
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(757, 450);
            Controls.Add(panel1);
            Controls.Add(button5);
            Controls.Add(pictureBox1);
            Controls.Add(button_cancel);
            Controls.Add(button_next);
            Controls.Add(button_pre);
            Controls.Add(tabControl1);
            Name = "Form_EncodeWizard";
            StartPosition = FormStartPosition.CenterParent;
            Text = "EncodeWizard";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            tabPage4.ResumeLayout(false);
            tabPage4.PerformLayout();
            tabPage5.ResumeLayout(false);
            tabPage5.PerformLayout();
            tabPage6.ResumeLayout(false);
            tabPage6.PerformLayout();
            tabPage7.ResumeLayout(false);
            tabPage7.PerformLayout();
            tabPage8.ResumeLayout(false);
            tabPage8.PerformLayout();
            tabPage9.ResumeLayout(false);
            tabPage9.PerformLayout();
            tabPage10.ResumeLayout(false);
            tabPage10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Button button_pre;
        private Button button_next;
        private Button button_cancel;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private TabPage tabPage5;
        private TabPage tabPage6;
        private GroupBox groupBox1;
        private RadioButton Radio_manytomany;
        private RadioButton Radio_onetomany;
        private GroupBox groupBox2;
        private RadioButton Radio_Encryptorw;
        private RadioButton Radio_normalw;
        private Label label1;
        private PictureBox pictureBox1;
        private Label label2;
        private Button button1;
        private TextBox text_lagrone;
        private Label label3;
        private Label label4;
        private TabPage tabPage7;
        private TabPage tabPage8;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private RichTextBox richTextBox1;
        private Button button2;
        private TextBox text_containers;
        private RichTextBox box_summary;
        private TextBox text_routemanysecrets;
        private Button button3;
        private ProgressBar progressBar1;
        private TabPage tabPage9;
        private Label label9;
        private Button button4;
        private TextBox text_outputroute;
        private TabPage tabPage10;
        private Label label10;
        private Label label11;
        private Button button5;
        private TextBox text_pwd;
        public RichTextBox richTextBox3;
        private ListView list_capacity;
        private ProgressBar progressBar2;
        private Panel panel1;
        private CheckBox check_searchdeepcontainer;
        private CheckBox check_copynonimage;
        private NumericUpDown numericUpDown1;
        private RadioButton radio_outputkeepstructure;
        private RadioButton radio_alltoonefolder;
    }
}