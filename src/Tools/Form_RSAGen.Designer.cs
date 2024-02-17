namespace HideSloth
{
    partial class Form_RSAGen
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
            groupBox1 = new GroupBox();
            text_pwd = new TextBox();
            label6 = new Label();
            button1 = new Button();
            text_keyroute = new TextBox();
            combo_keysize = new ComboBox();
            label3 = new Label();
            label2 = new Label();
            button_gen = new Button();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(28, 29);
            label1.Name = "label1";
            label1.Size = new Size(56, 17);
            label1.TabIndex = 0;
            label1.Text = "Key Size";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(text_pwd);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(text_keyroute);
            groupBox1.Controls.Add(combo_keysize);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(26, 21);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(743, 211);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Parameters";
            // 
            // text_pwd
            // 
            text_pwd.Location = new Point(125, 113);
            text_pwd.Name = "text_pwd";
            text_pwd.Size = new Size(551, 23);
            text_pwd.TabIndex = 9;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(28, 116);
            label6.Name = "label6";
            label6.Size = new Size(64, 17);
            label6.TabIndex = 8;
            label6.Text = "Password";
            // 
            // button1
            // 
            button1.Location = new Point(695, 63);
            button1.Name = "button1";
            button1.Size = new Size(30, 30);
            button1.TabIndex = 5;
            button1.Text = "...";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // text_keyroute
            // 
            text_keyroute.Location = new Point(125, 67);
            text_keyroute.Name = "text_keyroute";
            text_keyroute.Size = new Size(550, 23);
            text_keyroute.TabIndex = 4;
            // 
            // combo_keysize
            // 
            combo_keysize.DropDownStyle = ComboBoxStyle.DropDownList;
            combo_keysize.FormattingEnabled = true;
            combo_keysize.Items.AddRange(new object[] { "RSA2048", "RSA4096" });
            combo_keysize.Location = new Point(125, 26);
            combo_keysize.Name = "combo_keysize";
            combo_keysize.Size = new Size(121, 25);
            combo_keysize.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(28, 157);
            label3.Name = "label3";
            label3.Size = new Size(647, 17);
            label3.TabIndex = 2;
            label3.Text = "The private key is confidential information, in order to keep it secure, encryption of private key is mandatory. ";
            label3.Click += label3_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(28, 73);
            label2.Name = "label2";
            label2.Size = new Size(84, 17);
            label2.TabIndex = 1;
            label2.Text = "Export Route";
            // 
            // button_gen
            // 
            button_gen.Location = new Point(292, 238);
            button_gen.Name = "button_gen";
            button_gen.Size = new Size(209, 57);
            button_gen.TabIndex = 2;
            button_gen.Text = "Generate";
            button_gen.UseVisualStyleBackColor = true;
            button_gen.Click += button_gen_Click;
            // 
            // Form_RSAGen
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 307);
            Controls.Add(button_gen);
            Controls.Add(groupBox1);
            Name = "Form_RSAGen";
            Text = "RSA Key Pair Generator";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private GroupBox groupBox1;
        private Label label3;
        private Label label2;
        private ComboBox combo_keysize;
        private TextBox text_keyroute;
        private Button button1;
        private Button button_gen;
        private TextBox text_pwd;
        private Label label6;
    }
}