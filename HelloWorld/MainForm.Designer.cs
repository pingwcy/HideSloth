using System.Windows.Forms;

namespace HideSloth
{
    partial class MainForm
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            action = new Button();
            button2 = new Button();
            label1 = new Label();
            button3 = new Button();
            label2 = new Label();
            Radio_Encode = new RadioButton();
            Radio_Decode = new RadioButton();
            Radio_File = new RadioButton();
            Radio_String = new RadioButton();
            Input_PlainText = new TextBox();
            groupBox1 = new GroupBox();
            groupBox3 = new GroupBox();
            Textbox_Password = new TextBox();
            label4 = new Label();
            label5 = new Label();
            Button_SelectSecret = new Button();
            Label_CapacityInfo = new Label();
            Label_Capacity = new Label();
            Label_OutputString = new Label();
            Button_CheckCapacity = new Button();
            Button_Advanced = new Button();
            richTextBoxLog = new RichTextBox();
            pictureBox1 = new PictureBox();
            contextMenuStrip1 = new ContextMenuStrip(components);
            menuStrip1 = new MenuStrip();
            toolStripMenuItem1 = new ToolStripMenuItem();
            openContainerToolStripMenuItem = new ToolStripMenuItem();
            selectSecretFileToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            restartToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            optionsToolStripMenuItem = new ToolStripMenuItem();
            advancedSettingsToolStripMenuItem = new ToolStripMenuItem();
            languagesToolStripMenuItem = new ToolStripMenuItem();
            englishUSToolStripMenuItem = new ToolStripMenuItem();
            chineseSingaporeToolStripMenuItem = new ToolStripMenuItem();
            toolsTToolStripMenuItem = new ToolStripMenuItem();
            benchmarkToolStripMenuItem1 = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            bulkEmbeddingWizardToolStripMenuItem = new ToolStripMenuItem();
            batchExtractionWizardToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            userManualToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            statusStrip1 = new StatusStrip();
            Label_RouteofContainer = new TextBox();
            Label_RouteofSecret = new TextBox();
            check_multi = new CheckBox();
            groupBox1.SuspendLayout();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // action
            // 
            resources.ApplyResources(action, "action");
            action.Name = "action";
            action.UseVisualStyleBackColor = true;
            action.Click += button1_Click;
            // 
            // button2
            // 
            resources.ApplyResources(button2, "button2");
            button2.Name = "button2";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // button3
            // 
            resources.ApplyResources(button3, "button3");
            button3.Image = Properties.Resources.file;
            button3.Name = "button3";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // Radio_Encode
            // 
            resources.ApplyResources(Radio_Encode, "Radio_Encode");
            Radio_Encode.Name = "Radio_Encode";
            Radio_Encode.TabStop = true;
            Radio_Encode.UseVisualStyleBackColor = true;
            // 
            // Radio_Decode
            // 
            resources.ApplyResources(Radio_Decode, "Radio_Decode");
            Radio_Decode.Name = "Radio_Decode";
            Radio_Decode.TabStop = true;
            Radio_Decode.UseVisualStyleBackColor = true;
            Radio_Decode.CheckedChanged += Radio_Decode_CheckedChanged;
            // 
            // Radio_File
            // 
            resources.ApplyResources(Radio_File, "Radio_File");
            Radio_File.Name = "Radio_File";
            Radio_File.TabStop = true;
            Radio_File.UseVisualStyleBackColor = true;
            // 
            // Radio_String
            // 
            resources.ApplyResources(Radio_String, "Radio_String");
            Radio_String.Name = "Radio_String";
            Radio_String.TabStop = true;
            Radio_String.UseVisualStyleBackColor = true;
            // 
            // Input_PlainText
            // 
            resources.ApplyResources(Input_PlainText, "Input_PlainText");
            Input_PlainText.Name = "Input_PlainText";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(Radio_Encode);
            groupBox1.Controls.Add(Radio_Decode);
            resources.ApplyResources(groupBox1, "groupBox1");
            groupBox1.Name = "groupBox1";
            groupBox1.TabStop = false;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(Radio_File);
            groupBox3.Controls.Add(Radio_String);
            resources.ApplyResources(groupBox3, "groupBox3");
            groupBox3.Name = "groupBox3";
            groupBox3.TabStop = false;
            // 
            // Textbox_Password
            // 
            resources.ApplyResources(Textbox_Password, "Textbox_Password");
            Textbox_Password.Name = "Textbox_Password";
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            label4.Name = "label4";
            // 
            // label5
            // 
            resources.ApplyResources(label5, "label5");
            label5.Name = "label5";
            // 
            // Button_SelectSecret
            // 
            resources.ApplyResources(Button_SelectSecret, "Button_SelectSecret");
            Button_SelectSecret.Image = Properties.Resources.file;
            Button_SelectSecret.Name = "Button_SelectSecret";
            Button_SelectSecret.UseVisualStyleBackColor = true;
            Button_SelectSecret.Click += Button_SelectSecret_Click;
            // 
            // Label_CapacityInfo
            // 
            resources.ApplyResources(Label_CapacityInfo, "Label_CapacityInfo");
            Label_CapacityInfo.Name = "Label_CapacityInfo";
            // 
            // Label_Capacity
            // 
            resources.ApplyResources(Label_Capacity, "Label_Capacity");
            Label_Capacity.Name = "Label_Capacity";
            // 
            // Label_OutputString
            // 
            resources.ApplyResources(Label_OutputString, "Label_OutputString");
            Label_OutputString.Name = "Label_OutputString";
            // 
            // Button_CheckCapacity
            // 
            resources.ApplyResources(Button_CheckCapacity, "Button_CheckCapacity");
            Button_CheckCapacity.Name = "Button_CheckCapacity";
            Button_CheckCapacity.UseVisualStyleBackColor = true;
            Button_CheckCapacity.Click += Button_CheckCapacity_Click;
            // 
            // Button_Advanced
            // 
            resources.ApplyResources(Button_Advanced, "Button_Advanced");
            Button_Advanced.Name = "Button_Advanced";
            Button_Advanced.UseVisualStyleBackColor = true;
            Button_Advanced.Click += Button_Advanced_Click;
            // 
            // richTextBoxLog
            // 
            resources.ApplyResources(richTextBoxLog, "richTextBoxLog");
            richTextBoxLog.Name = "richTextBoxLog";
            richTextBoxLog.ReadOnly = true;
            // 
            // pictureBox1
            // 
            resources.ApplyResources(pictureBox1, "pictureBox1");
            pictureBox1.Image = Properties.Resources.Idle;
            pictureBox1.Name = "pictureBox1";
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Name = "contextMenuStrip1";
            resources.ApplyResources(contextMenuStrip1, "contextMenuStrip1");
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1, optionsToolStripMenuItem, toolsTToolStripMenuItem, helpToolStripMenuItem });
            resources.ApplyResources(menuStrip1, "menuStrip1");
            menuStrip1.Name = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { openContainerToolStripMenuItem, selectSecretFileToolStripMenuItem, toolStripSeparator2, restartToolStripMenuItem, exitToolStripMenuItem });
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // openContainerToolStripMenuItem
            // 
            openContainerToolStripMenuItem.Name = "openContainerToolStripMenuItem";
            resources.ApplyResources(openContainerToolStripMenuItem, "openContainerToolStripMenuItem");
            openContainerToolStripMenuItem.Click += button3_Click;
            // 
            // selectSecretFileToolStripMenuItem
            // 
            selectSecretFileToolStripMenuItem.Name = "selectSecretFileToolStripMenuItem";
            resources.ApplyResources(selectSecretFileToolStripMenuItem, "selectSecretFileToolStripMenuItem");
            selectSecretFileToolStripMenuItem.Click += Button_SelectSecret_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(toolStripSeparator2, "toolStripSeparator2");
            // 
            // restartToolStripMenuItem
            // 
            restartToolStripMenuItem.Name = "restartToolStripMenuItem";
            resources.ApplyResources(restartToolStripMenuItem, "restartToolStripMenuItem");
            restartToolStripMenuItem.Click += restartToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            resources.ApplyResources(exitToolStripMenuItem, "exitToolStripMenuItem");
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // optionsToolStripMenuItem
            // 
            optionsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { advancedSettingsToolStripMenuItem, languagesToolStripMenuItem });
            optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            resources.ApplyResources(optionsToolStripMenuItem, "optionsToolStripMenuItem");
            // 
            // advancedSettingsToolStripMenuItem
            // 
            advancedSettingsToolStripMenuItem.Name = "advancedSettingsToolStripMenuItem";
            resources.ApplyResources(advancedSettingsToolStripMenuItem, "advancedSettingsToolStripMenuItem");
            advancedSettingsToolStripMenuItem.Click += Button_Advanced_Click;
            // 
            // languagesToolStripMenuItem
            // 
            languagesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { englishUSToolStripMenuItem, chineseSingaporeToolStripMenuItem });
            languagesToolStripMenuItem.Name = "languagesToolStripMenuItem";
            resources.ApplyResources(languagesToolStripMenuItem, "languagesToolStripMenuItem");
            // 
            // englishUSToolStripMenuItem
            // 
            englishUSToolStripMenuItem.Name = "englishUSToolStripMenuItem";
            resources.ApplyResources(englishUSToolStripMenuItem, "englishUSToolStripMenuItem");
            englishUSToolStripMenuItem.Click += englishUSToolStripMenuItem_Click;
            // 
            // chineseSingaporeToolStripMenuItem
            // 
            chineseSingaporeToolStripMenuItem.Name = "chineseSingaporeToolStripMenuItem";
            resources.ApplyResources(chineseSingaporeToolStripMenuItem, "chineseSingaporeToolStripMenuItem");
            chineseSingaporeToolStripMenuItem.Click += chineseSingaporeToolStripMenuItem_Click;
            // 
            // toolsTToolStripMenuItem
            // 
            toolsTToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { benchmarkToolStripMenuItem1, toolStripSeparator1, bulkEmbeddingWizardToolStripMenuItem, batchExtractionWizardToolStripMenuItem });
            toolsTToolStripMenuItem.Name = "toolsTToolStripMenuItem";
            resources.ApplyResources(toolsTToolStripMenuItem, "toolsTToolStripMenuItem");
            // 
            // benchmarkToolStripMenuItem1
            // 
            benchmarkToolStripMenuItem1.Name = "benchmarkToolStripMenuItem1";
            resources.ApplyResources(benchmarkToolStripMenuItem1, "benchmarkToolStripMenuItem1");
            benchmarkToolStripMenuItem1.Click += benchmarkToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(toolStripSeparator1, "toolStripSeparator1");
            // 
            // bulkEmbeddingWizardToolStripMenuItem
            // 
            bulkEmbeddingWizardToolStripMenuItem.Name = "bulkEmbeddingWizardToolStripMenuItem";
            resources.ApplyResources(bulkEmbeddingWizardToolStripMenuItem, "bulkEmbeddingWizardToolStripMenuItem");
            bulkEmbeddingWizardToolStripMenuItem.Click += bulkEmbeddingWizardToolStripMenuItem_Click;
            // 
            // batchExtractionWizardToolStripMenuItem
            // 
            batchExtractionWizardToolStripMenuItem.Name = "batchExtractionWizardToolStripMenuItem";
            resources.ApplyResources(batchExtractionWizardToolStripMenuItem, "batchExtractionWizardToolStripMenuItem");
            batchExtractionWizardToolStripMenuItem.Click += batchExtractionWizardToolStripMenuItem_Click;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { userManualToolStripMenuItem, aboutToolStripMenuItem });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            resources.ApplyResources(helpToolStripMenuItem, "helpToolStripMenuItem");
            // 
            // userManualToolStripMenuItem
            // 
            userManualToolStripMenuItem.Name = "userManualToolStripMenuItem";
            resources.ApplyResources(userManualToolStripMenuItem, "userManualToolStripMenuItem");
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            resources.ApplyResources(aboutToolStripMenuItem, "aboutToolStripMenuItem");
            aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
            // 
            // statusStrip1
            // 
            resources.ApplyResources(statusStrip1, "statusStrip1");
            statusStrip1.Name = "statusStrip1";
            statusStrip1.ItemClicked += statusStrip1_ItemClicked;
            // 
            // Label_RouteofContainer
            // 
            resources.ApplyResources(Label_RouteofContainer, "Label_RouteofContainer");
            Label_RouteofContainer.Name = "Label_RouteofContainer";
            // 
            // Label_RouteofSecret
            // 
            resources.ApplyResources(Label_RouteofSecret, "Label_RouteofSecret");
            Label_RouteofSecret.Name = "Label_RouteofSecret";
            Label_RouteofSecret.TextChanged += Label_RouteofSecret_TextChanged;
            // 
            // check_multi
            // 
            resources.ApplyResources(check_multi, "check_multi");
            check_multi.Name = "check_multi";
            check_multi.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonFace;
            Controls.Add(check_multi);
            Controls.Add(Label_RouteofSecret);
            Controls.Add(Label_RouteofContainer);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            Controls.Add(pictureBox1);
            Controls.Add(richTextBoxLog);
            Controls.Add(Button_Advanced);
            Controls.Add(Button_CheckCapacity);
            Controls.Add(Label_OutputString);
            Controls.Add(Label_Capacity);
            Controls.Add(Label_CapacityInfo);
            Controls.Add(Button_SelectSecret);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(Textbox_Password);
            Controls.Add(groupBox3);
            Controls.Add(groupBox1);
            Controls.Add(Input_PlainText);
            Controls.Add(label2);
            Controls.Add(button3);
            Controls.Add(label1);
            Controls.Add(button2);
            Controls.Add(action);
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            Resize += MainForm_Resize;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button action;
        private Button button2;
        private Label label1;
        private Button button3;
        private Label label2;
        private RadioButton Radio_Encode;
        private RadioButton Radio_Decode;
        private RadioButton Radio_File;
        private RadioButton Radio_String;
        private TextBox Input_PlainText;
        private GroupBox groupBox1;
        private GroupBox groupBox3;
        private Label label4;
        private Label label5;
        private Button Button_SelectSecret;
        private Label Label_CapacityInfo;
        private Label Label_Capacity;
        private Label Label_OutputString;
        private Button Button_CheckCapacity;
        private Button Button_Advanced;
        public TextBox Textbox_Password;
        private RichTextBox richTextBoxLog;
        private PictureBox pictureBox1;
        private ContextMenuStrip contextMenuStrip1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem openContainerToolStripMenuItem;
        private ToolStripMenuItem selectSecretFileToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem optionsToolStripMenuItem;
        private ToolStripMenuItem advancedSettingsToolStripMenuItem;
        private ToolStripMenuItem languagesToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem userManualToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1, toolStripStatusLabel2, toolStripStatusLabel3;
        private ToolStripSeparator separator1, separator2, separator3;
        private ToolStripProgressBar bar1;
        private TextBox Label_RouteofContainer;
        private TextBox Label_RouteofSecret;
        private ToolStripMenuItem toolsTToolStripMenuItem;
        private ToolStripMenuItem benchmarkToolStripMenuItem1;
        private ToolStripMenuItem bulkEmbeddingWizardToolStripMenuItem;
        private ToolStripMenuItem batchExtractionWizardToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem restartToolStripMenuItem;
        private CheckBox check_multi;
        private ToolStripMenuItem chineseSingaporeToolStripMenuItem;
        private ToolStripMenuItem englishUSToolStripMenuItem;
    }
}
