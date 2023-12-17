using System.Windows.Forms;
using System.Drawing;
using System.Security;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Security.Cryptography.Xml;
using System.Security.Policy;
using System.IO;
using System.Text;
using System.ComponentModel;
using System.Web;
using HideSloth.Properties;
using HideSloth.Crypto;
using HideSloth.Steganography;
using HideSloth.Tools;
using System.Diagnostics;
using System;
using System.Linq;
using System.Globalization;

namespace HideSloth
{

    public partial class MainForm : Form
    {
        private Settings form2;
        private AboutBox AboutForm;
        private Form_Benchmark formBenchmark;
        private Form_EncodeWizard WizardEncode;
        private Form_DecodeWizard WizardDecode;
        //public event EventHandler<ControlActionEventArgs> UpdateUIControlEvent;
        private Logic logic;
        string selecte_secret = "";
        /*
        public class ControlActionEventArgs : EventArgs
        {
            public Action ControlAction { get; set; }
        }

        protected virtual void OnUpdateUIControl(ControlActionEventArgs e)
        {
            UpdateUIControlEvent?.Invoke(this, e);
        }
        public void TriggerControlAction(Action action)
        {
            OnUpdateUIControl(new ControlActionEventArgs { ControlAction = action });
        }

        private void MainForm_UpdateUIControlEvent(object sender, ControlActionEventArgs e)
        {
            // 这里调用你的 UI 更新方法
            if (InvokeRequired)
            {
                Invoke(e.ControlAction);
            }
            else
            {
                e.ControlAction?.Invoke();
            }
        }

        public void UpdateUIControl(Action controlAction)
        {
            if (InvokeRequired)
            {
                Invoke(controlAction);
            }
            else
            {
                controlAction();
            }
        }


        public string log
        {
            get {return richTextBoxLog.Text; }
            set { richTextBoxLog.AppendText(value); }
        }

        public string outputstr
        {
            get { return Input_PlainText.Text; }
            set { Input_PlainText.Text = value; }
        }
        */
        public bool PasswordBOX
        {
            get { return Textbox_Password.Enabled; }
            set { Textbox_Password.Enabled = value; }
        }

        public bool Container_Button
        {
            get { return button3.Visible; }
            set { button3.Visible = value; }
        }

        public bool ContainerLabel_Nousage
        {
            get { return Label_RouteofContainer.Visible; }
            set { Label_RouteofContainer.Visible = value; }
        }

        public bool Check_Mult
        {
            get { return check_multi.Visible; }
            set { check_multi.Visible = value; }
        }

        public bool is_Mult
        {
            get { return check_multi.Checked; }
            set { check_multi.Checked = value; }
        }


        public bool ContainerLabel2_Nousage
        {
            get { return label2.Visible; }
            set { label2.Visible = value; }
        }
        public void UpdateStatusStrip()
        {
            toolStripStatusLabel1.Text = GlobalVariables.mode + " Mode";
            toolStripStatusLabel2.Text = "Algorithm: " + GlobalVariables.Algor;
            toolStripStatusLabel3.Text = "Encryption: " + GlobalVariables.enableencrypt.ToString().ToUpper() + ", Iteration: " + GlobalVariables.iteration.ToString() + ", Hash: " + GlobalVariables.Hash.ToString();

        }
        private void InitializeStatusStrip()
        {
            this.separator1 = new ToolStripSeparator();
            this.separator2 = new ToolStripSeparator();
            this.separator3 = new ToolStripSeparator();
            this.toolStripStatusLabel1 = new ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new ToolStripStatusLabel();
            this.bar1 = new ToolStripProgressBar();
            bar1.Size = new Size(240, 16);
            bar1.Alignment = ToolStripItemAlignment.Right;

            statusStrip1.Items.AddRange(new ToolStripItem[]
            {
                toolStripStatusLabel1,
                separator1,
                toolStripStatusLabel2,
                separator2,
                toolStripStatusLabel3,
                separator3,
                bar1
            });
            this.Controls.Add(statusStrip1);
            toolStripStatusLabel1.Text = GlobalVariables.mode + " Mode";
            toolStripStatusLabel2.Text = "Algorithm: " + GlobalVariables.Algor;
            toolStripStatusLabel3.Text = "Encryption: " + GlobalVariables.enableencrypt.ToString().ToUpper() + ", Iteration: " + GlobalVariables.iteration.ToString() + ", Hash: " + GlobalVariables.Hash.ToString();

        }
        private void UpdateUIForCulture()
        {
            action.Text = Properties.Resources.SubmitButton;
            label1.Text = Properties.Resources.InputLabel;
            label4.Text = Properties.Resources.PasswordLabel;
            label2.Text = Properties.Resources.ContainerLabel;
            check_multi.Text = Properties.Resources.MultpLabel;
            label5.Text = Properties.Resources.SecretLabel;
            Label_CapacityInfo.Text = Properties.Resources.CapainforLabel;
            Button_CheckCapacity.Text = Properties.Resources.ButtonCheck;
            Radio_Encode.Text = Properties.Resources.EncodeRadio;
            Radio_Decode.Text = Properties.Resources.DecodeRadio;
            Radio_File.Text = Properties.Resources.FileLabel;
            Radio_String.Text = Properties.Resources.StringLabel;
            Label_OutputString.Text = Properties.Resources.OutputConsole;
            groupBox1.Text = Properties.Resources.EndeFrame;
            groupBox3.Text = Properties.Resources.FilestringFrame;
            Button_Advanced.Text = Properties.Resources.ButtonAdvanced;
            Label_Capacity.Text = Properties.Resources.CapacityInfo;
            toolStripMenuItem1.Text = Properties.Resources.FileMenu;
            optionsToolStripMenuItem.Text = Properties.Resources.OptionMenu;
            toolsTToolStripMenuItem.Text = Properties.Resources.ToolMenu;
            helpToolStripMenuItem.Text = Properties.Resources.HelpMenu;
            // 更新其他 UI 元素...
        }
        private void Logic_ProgressChanged(object sender, ProgressEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler<ProgressEventArgs>(Logic_ProgressChanged), sender, e);
                return;
            }
            if (e.Progress == 0) // Normal New Progess
            {
                richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- "+e.Message+"\n");
                richTextBoxLog.ScrollToCaret();
            }
            else if (e.Progress == 1) // Bold Finnished Signal
            {
                BoldToLog(DateTime.Now.ToString() + "--- " + e.Message+"\n", false);
                richTextBoxLog.ScrollToCaret();
                ShowMessageOnUIThread(e.Message, "Success");

            }
            else if (e.Progress == 2)//Error
            {
                BoldToLog(DateTime.Now.ToString() + "--- " + e.Message+"\n", true);
                richTextBoxLog.ScrollToCaret();
                ShowMessageOnUIThread(e.Message, "Error");
            }
            else if (e.Progress == 3)//Transform Output string
            {
                Input_PlainText.Text = e.Message;
                BoldToLog(DateTime.Now.ToString() + "--- " + "Success to extract string\n", false);
                ShowMessageOnUIThread("Success to extract string! ", "Success");
            }
        }


        private void Logic_RequestFileSave(object sender, FileSaveRequestEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler<FileSaveRequestEventArgs>(Logic_RequestFileSave), sender, e);
            }
            else
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                // 配置 SaveFileDialog...
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    e.SetPath(saveFileDialog.FileName);
                }
                else
                {
                    e.SetPath(""); // 用户取消操作时，可以传递 null 或适当的默认值
                }
            }
        }

        private void Logic_RequestRouteSave(object sender, RouteOutputRequestEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler<RouteOutputRequestEventArgs>(Logic_RequestRouteSave), sender, e);
            }
            else
            {
                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                folderBrowserDialog.Description = "请选择文件夹";
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    e.SetPath(folderBrowserDialog.SelectedPath);
                }
                else
                {
                    e.SetPath(""); // 用户取消操作时，可以传递 null 或适当的默认值
                }
            }
        }

        private void Logic_RequestExtractedSave(object sender, SaveExtractedFileEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler<SaveExtractedFileEventArgs>(Logic_RequestExtractedSave), sender, e);
            }
            else
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Title = "Save the decoded file";
                    saveFileDialog.FileName = GlobalVariables.defaultname; // 设置默认文件名

                    if (saveFileDialog.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(saveFileDialog.FileName))
                    {
                        //GlobalVariables.outputnameandroute = saveFileDialog.FileName;
                        e.SetPath(saveFileDialog.FileName);
                        //richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Output Name and Route of Extracted File Selected: " + GlobalVariables.outputnameandroute + "\n");
                    }
                }
            }
        }

        private void Settings_UpdateGUIMainform (object sender, SettingUpdateUIEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler<ProgressEventArgs>(Logic_ProgressChanged), sender, e);
                return;
            }
            //MessageBox.Show(e.Encalg);
            if (e.Isenc)
            {
                this.PasswordBOX = true;
            }
            else
            {
                this.PasswordBOX = false;
            }
            if (e.Modechange)
            {
                this.Container_Button = true;
                this.ContainerLabel_Nousage = true;
                this.ContainerLabel2_Nousage = true;
                this.Check_Mult = true;

            }
            else
            {
                this.Container_Button = false;
                this.ContainerLabel_Nousage = false;
                this.ContainerLabel2_Nousage = false;
                this.Check_Mult = false;

            }
            this.UpdateStatusStrip();
        }

        public MainForm()
        {
            InitializeComponent();

            //UpdateUIControlEvent += MainForm_UpdateUIControlEvent;
            logic = new Logic();
            logic.ProgressChanged += Logic_ProgressChanged;
            logic.RequestFileSave += Logic_RequestFileSave;
            logic.RequestRouteSave += Logic_RequestRouteSave;
            logic.RequestExtractedSave += Logic_RequestExtractedSave;
            form2 = new Settings();
            form2.SettingUpdateUI += Settings_UpdateGUIMainform;
            /*
            CultureInfo currentUICulture = CultureInfo.CurrentUICulture;
            CultureInfo currentCulture = CultureInfo.CurrentCulture;

            // 设置应用程序的用户界面文化
            Thread.CurrentThread.CurrentUICulture = currentUICulture;
            Thread.CurrentThread.CurrentCulture = currentCulture;
            */


            UpdateUIForCulture();
            this.Text = Resources.MainFormTitle;
            richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Initialization successful\n");
            richTextBoxLog.ScrollToCaret();
            InitializeStatusStrip();
        }


        public void ShowMessageOnUIThread(string message, string msgtyp)
        {
            if (InvokeRequired)
            {
                // 如果不在 UI 线程，则使用 Invoke 在 UI 线程上执行
                Invoke((MethodInvoker)delegate
                {
                    MessageBox.Show(message, msgtyp);
                });
            }
            else
            {
                // 如果已经在 UI 线程，直接显示消息框
                MessageBox.Show(message, msgtyp);
            }
        }
        public void UpdateUI(Action updateAction)
        {
            if (InvokeRequired)
            {
                // 如果当前不在 UI 线程，则通过 Invoke 在 UI 线程上执行操作
                Invoke(updateAction);
            }
            else
            {
                // 如果已经在 UI 线程，则直接执行操作
                updateAction();
            }
        }

        public T ReadUI<T>(Func<T> readAction)
        {
            if (InvokeRequired)
            {
                // 如果当前不在 UI 线程，则通过 Invoke 在 UI 线程上执行读取操作
                return (T)Invoke(readAction);
            }
            else
            {
                // 如果已经在 UI 线程，则直接执行读取操作
                return readAction();
            }
        }
        public void BoldToLog(string newText, bool iferror)
        {
            int insertPos = richTextBoxLog.TextLength;

            // 设置插入点
            richTextBoxLog.SelectionStart = insertPos;

            // 插入文本
            richTextBoxLog.AppendText(newText);

            // 选中刚刚插入的文本
            richTextBoxLog.SelectionStart = insertPos;
            richTextBoxLog.SelectionLength = newText.Length;

            // 创建并应用加粗样式
            Font boldFont = new Font(richTextBoxLog.Font, FontStyle.Bold);
            richTextBoxLog.SelectionFont = boldFont;
            if (iferror)
            {
                richTextBoxLog.SelectionColor = Color.Red;

            }
            richTextBoxLog.ScrollToCaret();
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            action.Enabled = false;
            pictureBox1.Image = Resources.Running;
            GlobalVariables.check_result = (ReadUI(() => (Label_RouteofContainer.Text).Contains("png")) || ReadUI(() => (Label_RouteofContainer.Text).Contains("bmp")));

            GlobalVariables.encode = ReadUI(() => Radio_Encode.Checked);
            GlobalVariables.decode = ReadUI(() => Radio_Decode.Checked);
            GlobalVariables.isfile = ReadUI(() => Radio_File.Checked);
            GlobalVariables.isstring = ReadUI(() => Radio_String.Checked);

            GlobalVariables.route_container = ReadUI(() => Label_RouteofContainer.Text);
            string password = ReadUI(() => Textbox_Password.Text);
            GlobalVariables.route_secret = ReadUI(() => Label_RouteofSecret.Text);
            GlobalVariables.stringinfo = ReadUI(() => Input_PlainText.Text);
            UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Process Started\n"));
            UpdateUI(() => richTextBoxLog.ScrollToCaret());
            //var logic = new Logic();
            await logic.CallMethodAsync(check_multi.Checked, selecte_secret, check_audio.Checked, password);
            pictureBox1.Image = Resources.Idle;
            action.Enabled = true;


        }
        private void button3_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                //openFileDialog.Filter = "文本文件 (*.txt)|*.txt|所有文件 (*.*)|*.*";
                openFileDialog.Title = "Choose a container";
                openFileDialog.Multiselect = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (check_multi.Checked)
                    {
                        string[] selectedFiles = openFileDialog.FileNames;
                        GlobalVariables.route_containers.Clear();
                        foreach (string i in selectedFiles)
                        {
                            GlobalVariables.route_containers.Add(i);
                        }
                        Label_RouteofContainer.Text = String.Join("; ", selectedFiles);
                        richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- MulitpalContainer's Name and Route Selected: " + Label_RouteofContainer.Text + "\n");

                    }
                    else
                    {
                        GlobalVariables.route_containers.Clear();
                        string selecte_containerroute = openFileDialog.FileName;
                        GlobalVariables.route_container = selecte_containerroute;
                        GlobalVariables.route_containers.Add(selecte_containerroute);
                        Label_RouteofContainer.Text = selecte_containerroute;
                        richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Container's Name and Route Selected: " + selecte_containerroute + "\n");

                    }

                }
            }

        }

        private void Button_SelectSecret_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                //openFileDialog.Filter = "文本文件 (*.txt)|*.txt|所有文件 (*.*)|*.*";
                openFileDialog.Title = "Choose a scret file";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    selecte_secret = openFileDialog.FileName;
                    GlobalVariables.route_secret = selecte_secret;
                    Label_RouteofSecret.Text = selecte_secret;
                    richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Input Secret File's Name and Route Selected: " + selecte_secret + "\n");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Input_PlainText.Text = "";
            Label_RouteofContainer.Text = "";
            Label_RouteofSecret.Text = "";
        }


        private void Button_CheckCapacity_Click(object sender, EventArgs e)
        {
            string outputcapacity = "";
            string containfile = Label_RouteofContainer.Text;
            bool containsSlash = containfile.Contains(@"\");
            if (containsSlash == true || GlobalVariables.multipal_route != null)
            {
                try
                {
                    if (check_multi.Checked && GlobalVariables.route_containers != null)
                    {
                        List<double> imagesizes = new List<double>();
                        foreach (string single in GlobalVariables.route_containers)
                        {
                            Image img = Image.FromFile(single);
                            double singlesize = 0.0;
                            // 获取分辨率
                            if (GlobalVariables.Algor == "LSB")
                            {
                                singlesize = Math.Round(img.Width * img.Height * 3 / 8 * 0.89 / 1024 / 1.34);
                                imagesizes.Add(singlesize);
                            }
                            else if (GlobalVariables.Algor == "Linear")
                            {
                                singlesize = Math.Round(img.Width * img.Height / 1024 * 0.97);
                                imagesizes.Add(singlesize);

                            }

                            img.Dispose();

                        }
                        outputcapacity = "The smallest container's capacity is: " + (imagesizes.Min().ToString()) + " KB;";
                        imagesizes.Clear();

                    }
                    else if (check_multi.Checked != true)
                    {
                        Image image = Image.FromFile(Label_RouteofContainer.Text);
                        double size = 0.0;
                        // 获取分辨率
                        if (GlobalVariables.Algor == "LSB")
                        {
                            size = Math.Round(image.Width * image.Height * 3 / 8 * 0.89 / 1024 / 1.34);
                        }
                        else if (GlobalVariables.Algor == "Linear")
                        {
                            size = Math.Round(image.Width * image.Height / 1024 * 0.97);
                        }

                        image.Dispose();
                        outputcapacity = "The container's capacity is: " + (size) + " KB;";
                    }
                }
                catch (Exception ex)
                {
                    //progressBar1.Value = 0;

                    ShowMessageOnUIThread(ex.Message, "Error");

                }
            }
            string secretfile = Label_RouteofSecret.Text;
            bool secretSlah = secretfile.Contains(@"\");
            if (secretSlah == true)
            {
                FileInfo fileInfo = new FileInfo(Label_RouteofSecret.Text);

                // 获取文件大小
                long fileSizeInBytes = fileInfo.Length;
                outputcapacity += " The secret file's size is " + fileSizeInBytes / 1024 + " KB.";
            }

            Label_Capacity.Text = outputcapacity;

        }


        private void Button_Advanced_Click(object sender, EventArgs e)
        {
            
            if (form2 == null || form2.IsDisposed)
            {
                form2 = new Settings(); // 直接使用类级别的成员变量，不需要重新声明
                form2.FormClosed += (s, args) => this.form2 = null; // 当Form2关闭时，将类级别的引用设置为null
                form2.SettingUpdateUI += Settings_UpdateGUIMainform;
                form2.Show();
                richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Loaded Advanced Settings Form\n");

            }
            else
            {
                //form2 = new Settings();
                form2.Show();

                form2.BringToFront(); // 如果Form2已经存在，将其带到前台
            }
            
        }


        public void Outputfile_pngbmp()
        {
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PNG Image|*.png|Bitmap Image|*.bmp";
                saveFileDialog.Title = "Save an Image File";
                saveFileDialog.ShowDialog();

                if (saveFileDialog.FileName != "")
                {
                    GlobalVariables.outputnameandroute = saveFileDialog.FileName;
                    if (Path.GetExtension(GlobalVariables.outputnameandroute) != ".png" && Path.GetExtension(GlobalVariables.outputnameandroute) != ".bmp")
                    {
                        ShowMessageOnUIThread("You entered a format out of png or bmp, you can save it anyway, but it may cause data damage if you open them with other application.", "Warning");
                    }
                    richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Output Name and Route of Loaded Container Selected: " + GlobalVariables.outputnameandroute + "\n");

                }
            }

        }


        public void Outputfile_wav()
        {
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "WAV Audio|*.wav";
                saveFileDialog.Title = "Save an Audio File";
                saveFileDialog.ShowDialog();

                if (saveFileDialog.FileName != "")
                {
                    GlobalVariables.outputnameandroute = saveFileDialog.FileName;
                    richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Output Name and Route of Loaded Container Selected: " + GlobalVariables.outputnameandroute + "\n");

                }
            }

        }

        public void Outputfile_any()
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Title = "Save the decoded file";
                //saveFileDialog.Filter = "文本文件 (*.txt)|*.txt|所有文件 (*.*)|*.*"; // 根据需要调整文件类型
                saveFileDialog.FileName = GlobalVariables.defaultname; // 设置默认文件名

                if (saveFileDialog.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(saveFileDialog.FileName))
                {
                    GlobalVariables.outputnameandroute = saveFileDialog.FileName;
                    richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Output Name and Route of Extracted File Selected: " + GlobalVariables.outputnameandroute + "\n");
                }
            }
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var AboutForm = new AboutBox())
            {
                AboutForm.ShowDialog();

            }
        }

        private void benchmarkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formBenchmark = new Form_Benchmark();
            formBenchmark.Show();
        }

        private void bulkEmbeddingWizardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WizardEncode = new Form_EncodeWizard();
            WizardEncode.SettingUpdateUI3 += Settings_UpdateGUIMainform;
            WizardEncode.Show();
        }

        private void batchExtractionWizardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WizardDecode = new Form_DecodeWizard(this);
            WizardDecode.SettingUpdateUI2 += Settings_UpdateGUIMainform;
            WizardDecode.Show();
        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Application.ExecutablePath);

            // 关闭当前程序实例
            Application.Exit();

        }

        public void mulitpla_output()
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "请选择文件夹";
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                GlobalVariables.multipal_route = folderBrowserDialog.SelectedPath;
            }

        }

        private void Radio_Decode_CheckedChanged(object sender, EventArgs e)
        {
            if (Radio_Decode.Checked)
            {
                check_multi.Enabled = false;
            }
            else if (Radio_Decode.Checked == false)
            {
                check_multi.Enabled = true;
            }

        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                //Input_PlainText.Height = 300;

                richTextBoxLog.Height = 350;
            }
            if (this.WindowState == FormWindowState.Normal)
            {
                richTextBoxLog.Height = 131;
            }

        }


        private void chineseSingaporeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chineseSingaporeToolStripMenuItem.Checked = true;
            englishUSToolStripMenuItem.Checked = false;

            Thread.CurrentThread.CurrentCulture = new CultureInfo("zh-SG");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-SG");
            /*
            foreach (Control c in this.Controls)
            {
                ComponentResourceManager resources = new ComponentResourceManager(typeof(MainForm));
                resources.ApplyResources(c, c.Name, new CultureInfo("zh-SG"));
            }*/
            this.Text = Resources.MainFormTitle;
            UpdateUIForCulture();
            this.Invalidate();
            this.Update();

        }

        private void englishUSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            englishUSToolStripMenuItem.Checked = true;
            chineseSingaporeToolStripMenuItem.Checked = false;

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
            this.Text = Resources.MainFormTitle;
            UpdateUIForCulture();
            this.Invalidate();
            this.Update();

        }
    }

}
