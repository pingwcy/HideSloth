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
        public event EventHandler<ControlActionEventArgs> UpdateUIControlEvent;

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
            // ���������� UI ���·���
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
            // �������� UI Ԫ��...
        }



        public MainForm()
        {
            UpdateUIControlEvent += MainForm_UpdateUIControlEvent;

            /*
            CultureInfo currentUICulture = CultureInfo.CurrentUICulture;
            CultureInfo currentCulture = CultureInfo.CurrentCulture;

            // ����Ӧ�ó�����û������Ļ�
            Thread.CurrentThread.CurrentUICulture = currentUICulture;
            Thread.CurrentThread.CurrentCulture = currentCulture;
            */

            InitializeComponent();

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
                // ������� UI �̣߳���ʹ�� Invoke �� UI �߳���ִ��
                Invoke((MethodInvoker)delegate
                {
                    MessageBox.Show(message, msgtyp);
                });
            }
            else
            {
                // ����Ѿ��� UI �̣߳�ֱ����ʾ��Ϣ��
                MessageBox.Show(message, msgtyp);
            }
        }
        public void UpdateUI(Action updateAction)
        {
            if (InvokeRequired)
            {
                // �����ǰ���� UI �̣߳���ͨ�� Invoke �� UI �߳���ִ�в���
                Invoke(updateAction);
            }
            else
            {
                // ����Ѿ��� UI �̣߳���ֱ��ִ�в���
                updateAction();
            }
        }

        public T ReadUI<T>(Func<T> readAction)
        {
            if (InvokeRequired)
            {
                // �����ǰ���� UI �̣߳���ͨ�� Invoke �� UI �߳���ִ�ж�ȡ����
                return (T)Invoke(readAction);
            }
            else
            {
                // ����Ѿ��� UI �̣߳���ֱ��ִ�ж�ȡ����
                return readAction();
            }
        }
        public void BoldToLog(string newText, bool iferror)
        {
            int insertPos = richTextBoxLog.TextLength;

            // ���ò����
            richTextBoxLog.SelectionStart = insertPos;

            // �����ı�
            richTextBoxLog.AppendText(newText);

            // ѡ�иող�����ı�
            richTextBoxLog.SelectionStart = insertPos;
            richTextBoxLog.SelectionLength = newText.Length;

            // ������Ӧ�üӴ���ʽ
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
            GlobalVariables.password = ReadUI(() => Textbox_Password.Text);
            GlobalVariables.route_secret = ReadUI(() => Label_RouteofSecret.Text);
            GlobalVariables.stringinfo = ReadUI(() => Input_PlainText.Text);
            UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Process Started\n"));
            UpdateUI(() => richTextBoxLog.ScrollToCaret());
            if (check_audio.Checked)
            {
                GlobalVariables.audioorimage = "audio";
            }
            else if (!check_audio.Checked)
            {
                GlobalVariables.audioorimage = "image";

            }
            var logic = new Logic(this);
            await logic.CallMethodAsync();
            pictureBox1.Image = Resources.Idle;
            action.Enabled = true;


        }
        private void button3_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                //openFileDialog.Filter = "�ı��ļ� (*.txt)|*.txt|�����ļ� (*.*)|*.*";
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
                //openFileDialog.Filter = "�ı��ļ� (*.txt)|*.txt|�����ļ� (*.*)|*.*";
                openFileDialog.Title = "Choose a scret file";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string selecte_secret = openFileDialog.FileName;
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
                            // ��ȡ�ֱ���
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
                        // ��ȡ�ֱ���
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

                // ��ȡ�ļ���С
                long fileSizeInBytes = fileInfo.Length;
                outputcapacity += " The secret file's size is " + fileSizeInBytes / 1024 + " KB.";
            }

            Label_Capacity.Text = outputcapacity;

        }


        private void Button_Advanced_Click(object sender, EventArgs e)
        {
            if (form2 == null || form2.IsDisposed)
            {
                form2 = new Settings(this); // ֱ��ʹ���༶��ĳ�Ա����������Ҫ��������
                form2.FormClosed += (s, args) => this.form2 = null; // ��Form2�ر�ʱ�����༶�����������Ϊnull
                form2.Show();
                richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Loaded Advanced Settings Form\n");

            }
            else
            {
                form2.BringToFront(); // ���Form2�Ѿ����ڣ��������ǰ̨
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
                //saveFileDialog.Filter = "�ı��ļ� (*.txt)|*.txt|�����ļ� (*.*)|*.*"; // ������Ҫ�����ļ�����
                saveFileDialog.FileName = GlobalVariables.defaultname; // ����Ĭ���ļ���

                if (saveFileDialog.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(saveFileDialog.FileName))
                {
                    GlobalVariables.outputnameandroute = saveFileDialog.FileName;
                    richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Output Name and Route of Extracted File Selected: " + GlobalVariables.outputnameandroute + "\n");
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

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
            WizardEncode = new Form_EncodeWizard(this);
            WizardEncode.Show();
        }

        private void batchExtractionWizardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WizardDecode = new Form_DecodeWizard(this);
            WizardDecode.Show();
        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Application.ExecutablePath);

            // �رյ�ǰ����ʵ��
            Application.Exit();

        }

        public void mulitpla_output()
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "��ѡ���ļ���";
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

        private void Label_RouteofSecret_TextChanged(object sender, EventArgs e)
        {

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
