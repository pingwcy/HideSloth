namespace HideSloth.Tools
{
    partial class Form_loadpair
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
            label1 = new Label();
            label2 = new Label();
            text_pri = new TextBox();
            groupBox1 = new GroupBox();
            combo_size = new ComboBox();
            label4 = new Label();
            check_onlypub = new CheckBox();
            label3 = new Label();
            button_pub = new Button();
            text_pub = new TextBox();
            button_pri = new Button();
            button_load = new Button();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(23, 37);
            label1.Name = "label1";
            label1.Size = new Size(110, 17);
            label1.TabIndex = 0;
            label1.Text = "Private Key Route";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(23, 83);
            label2.Name = "label2";
            label2.Size = new Size(105, 17);
            label2.TabIndex = 1;
            label2.Text = "Public Key Route";
            // 
            // text_pri
            // 
            text_pri.Location = new Point(153, 31);
            text_pri.Name = "text_pri";
            text_pri.Size = new Size(521, 23);
            text_pri.TabIndex = 2;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(combo_size);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(check_onlypub);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(button_pub);
            groupBox1.Controls.Add(text_pub);
            groupBox1.Controls.Add(button_pri);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(text_pri);
            groupBox1.Controls.Add(label2);
            groupBox1.Location = new Point(33, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(738, 242);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "Parameters";
            // 
            // combo_size
            // 
            combo_size.DropDownStyle = ComboBoxStyle.DropDownList;
            combo_size.FormattingEnabled = true;
            combo_size.Items.AddRange(new object[] { "2048", "4096" });
            combo_size.Location = new Point(153, 125);
            combo_size.Name = "combo_size";
            combo_size.Size = new Size(121, 25);
            combo_size.TabIndex = 9;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(23, 128);
            label4.Name = "label4";
            label4.Size = new Size(83, 17);
            label4.TabIndex = 8;
            label4.Text = "RSA Key Size";
            label4.Click += label4_Click;
            // 
            // check_onlypub
            // 
            check_onlypub.AutoSize = true;
            check_onlypub.Location = new Point(23, 166);
            check_onlypub.Name = "check_onlypub";
            check_onlypub.Size = new Size(235, 21);
            check_onlypub.TabIndex = 7;
            check_onlypub.Text = "Only Load Public Key for Encryption";
            check_onlypub.UseVisualStyleBackColor = true;
            check_onlypub.CheckedChanged += check_onlypub_CheckedChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(23, 201);
            label3.Name = "label3";
            label3.Size = new Size(582, 17);
            label3.TabIndex = 6;
            label3.Text = "The loaded privated key is encrypted, you will need to enter password when using the private key!";
            // 
            // button_pub
            // 
            button_pub.Location = new Point(694, 76);
            button_pub.Name = "button_pub";
            button_pub.Size = new Size(30, 30);
            button_pub.TabIndex = 5;
            button_pub.Text = "...";
            button_pub.UseVisualStyleBackColor = true;
            button_pub.Click += button_pub_Click;
            // 
            // text_pub
            // 
            text_pub.Location = new Point(153, 80);
            text_pub.Name = "text_pub";
            text_pub.Size = new Size(521, 23);
            text_pub.TabIndex = 3;
            // 
            // button_pri
            // 
            button_pri.Location = new Point(694, 27);
            button_pri.Name = "button_pri";
            button_pri.Size = new Size(30, 30);
            button_pri.TabIndex = 4;
            button_pri.Text = "...";
            button_pri.UseVisualStyleBackColor = true;
            button_pri.Click += button_pri_Click;
            // 
            // button_load
            // 
            button_load.Location = new Point(310, 260);
            button_load.Name = "button_load";
            button_load.Size = new Size(188, 64);
            button_load.TabIndex = 4;
            button_load.Text = "Load Key Pair";
            button_load.UseVisualStyleBackColor = true;
            button_load.Click += button_load_Click;
            // 
            // Form_loadpair
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 336);
            Controls.Add(button_load);
            Controls.Add(groupBox1);
            Name = "Form_loadpair";
            Text = "Load Key Pair";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox text_pri;
        private GroupBox groupBox1;
        private Button button_pub;
        private TextBox text_pub;
        private Button button_pri;
        private Button button_load;
        private Label label3;
        private CheckBox check_onlypub;
        private Label label4;
        private ComboBox combo_size;
    }
}