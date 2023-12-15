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


        private void ShowMessageOnUIThread(string message, string msgtyp)
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
        private void UpdateUI(Action updateAction)
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

        private T ReadUI<T>(Func<T> readAction)
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
        private void BoldToLog(string newText, bool iferror)
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

        }
        private async void button1_Click(object sender, EventArgs e)
        {
            action.Enabled = false;
            pictureBox1.Image = Resources.Running;
            await Task.Run(() => LongRunningOperation());
            pictureBox1.Image = Resources.Idle;
            action.Enabled = true;
        }

        private void LongRunningOperation()
        {
            GlobalVariables.check_result = (ReadUI(() => (Label_RouteofContainer.Text).Contains("png")) || ReadUI(() => (Label_RouteofContainer.Text).Contains("bmp")));

            GlobalVariables.encode = ReadUI(() => Radio_Encode.Checked);
            GlobalVariables.decode = ReadUI(() => Radio_Decode.Checked);
            GlobalVariables.isfile = ReadUI(() => Radio_File.Checked);
            GlobalVariables.isstring = ReadUI(() => Radio_String.Checked);

            GlobalVariables.route_container = ReadUI(() => Label_RouteofContainer.Text);
            GlobalVariables.password = ReadUI(() => Textbox_Password.Text);
            GlobalVariables.route_secret = ReadUI(() => Label_RouteofSecret.Text);

            UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Process Started\n"));
            UpdateUI(() => richTextBoxLog.ScrollToCaret());
            if (GlobalVariables.mode == "Normal")
            {

                if (GlobalVariables.encode && GlobalVariables.enableencrypt && GlobalVariables.isfile)//Encode encrypted file
                {

                    try
                    {
                        if (check_multi.Checked)
                        {

                            this.Invoke(new Action(() => mulitpla_output()));//for duplicate copy, choose route first
                        }
                        //ShowMessageOnUIThread(GlobalVariables.route_containers[0], "");

                        foreach (string single_container in GlobalVariables.route_containers)
                        {
                            byte[] secretData = BytesStringThings.ReadFileToByteswithName(GlobalVariables.route_secret);
                            UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Secret File Readed\n"));
                            UpdateUI(() => richTextBoxLog.ScrollToCaret());
                            DateTime lastaccess = new DateTime(2021, 8, 15);

                            secretData = Aes_ChaCha_Encryptor.Encrypt(secretData, GlobalVariables.password, out byte[] salt, out byte[] nonce, out byte[] tag);

                            UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Secret File Encrypted and stored in memory\n"));
                            UpdateUI(() => richTextBoxLog.ScrollToCaret());
                            if (GlobalVariables.copymeta)
                            {
                                lastaccess = File.GetLastAccessTime(single_container);
                            }
                            string newroutename = "";

                            if (check_audio.Checked == false)
                            {
                                Bitmap loaded = (Bitmap)Support_Converter.ConvertOthersToPngInMemory(single_container);
                                UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Container Loaded and Converted if necessary, Start to Embed\n"));
                                UpdateUI(() => richTextBoxLog.ScrollToCaret());

                                Bitmap result = null;

                                if (GlobalVariables.Algor == "LSB")
                                {
                                    result = LSB_Image.embed(Convert.ToBase64String(BytesStringThings.CombineBytes(salt, nonce, tag, secretData)), loaded);

                                }
                                else if (GlobalVariables.Algor == "Linear")
                                {
                                    result = Core_Linear_Image.EncodeFileLinear(loaded, BytesStringThings.CombineBytes(salt, nonce, tag, secretData));
                                }
                                UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Success to embed, Please select a route and name to save loaded container.\n"));
                                UpdateUI(() => richTextBoxLog.ScrollToCaret());

                                if (check_multi.Checked == false)
                                {
                                    this.Invoke(new Action(() => Outputfile_pngbmp()));
                                }
                                if (GlobalVariables.outputnameandroute != null && check_multi.Checked == false)
                                {
                                    newroutename = GlobalVariables.outputnameandroute;
                                    result.Save(newroutename, Support_Converter.SaveFormatImage(GlobalVariables.outputformat));
                                    loaded.Dispose();
                                    result.Dispose();
                                }
                                if (GlobalVariables.multipal_route != null && check_multi.Checked == true)
                                {
                                    if (GlobalVariables.keepformat)
                                    {
                                        newroutename = Path.Combine(GlobalVariables.multipal_route, (Path.GetFileName(single_container)));
                                        result.Save(newroutename, Support_Converter.SaveFormatImage(GlobalVariables.outputformat));
                                        loaded.Dispose();
                                        result.Dispose();
                                    }
                                    else
                                    {
                                        newroutename = Path.Combine(GlobalVariables.multipal_route, (Path.GetFileNameWithoutExtension(single_container)) + GlobalVariables.outputformat);
                                        result.Save(newroutename, Support_Converter.SaveFormatImage(GlobalVariables.outputformat));
                                        loaded.Dispose();
                                        result.Dispose();
                                    }
                                }
                               
                               
                            }
                            else if (check_audio.Checked == true)
                            {
                                if (check_multi.Checked == false)
                                {
                                    this.Invoke(new Action(() => Outputfile_wav()));
                                    Audio_LSB.Encode_Audio(single_container, GlobalVariables.outputnameandroute, BytesStringThings.CombineBytes(salt, nonce, tag, secretData));

                                }
                                if (GlobalVariables.multipal_route != null && check_multi.Checked)
                                {
                                    newroutename = Path.Combine(GlobalVariables.multipal_route, (Path.GetFileName(single_container)));
                                    Audio_LSB.Encode_Audio(single_container, newroutename, BytesStringThings.CombineBytes(salt, nonce, tag, secretData));

                                }
                            }


                            UpdateUI(() => BoldToLog(DateTime.Now.ToString() + "--- Loaded container saved\n", false));
                            UpdateUI(() => richTextBoxLog.ScrollToCaret());


                            if (GlobalVariables.copymeta)
                            {
                                File.SetCreationTime(newroutename, File.GetCreationTime(single_container));
                                File.SetLastAccessTime(newroutename, lastaccess);
                                File.SetLastWriteTime(newroutename, File.GetLastWriteTime(single_container));

                            }


                            

                        }
                        ShowMessageOnUIThread("Success to encode file with encryption to image", "Success");
                    }
                    catch (Exception ex)
                    {
                        ShowMessageOnUIThread(ex.Message, "Error");
                        UpdateUI(() => BoldToLog(DateTime.Now.ToString() + "Error" + ex.Message + "\n", true));
                        UpdateUI(() => richTextBoxLog.ScrollToCaret());
                    }

                }


                if (GlobalVariables.decode && GlobalVariables.enableencrypt && GlobalVariables.isfile)//Decode encrypted file
                {

                    byte[]? extracted_result = new byte[0];
                    byte[]? decrypted_content = new byte[0];

                    try
                    {
                        if (check_audio.Checked == false)
                        {
                            Bitmap unloading = new Bitmap(GlobalVariables.route_container);
                            UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Readed Loaded container\n"));
                            UpdateUI(() => richTextBoxLog.ScrollToCaret());

                            if (GlobalVariables.Algor == "LSB")
                            {
                                extracted_result = Convert.FromBase64String(LSB_Image.extract(unloading));
                            }
                            else if (GlobalVariables.Algor == "Linear")
                            {
                                extracted_result = Core_Linear_Image.DecodeFileFromImage(unloading);
                            }
                            unloading.Dispose();

                        }
                        else if (check_audio.Checked)
                        {
                            //byte[] xx = Audio_LSB.Decode_Audio(GlobalVariables.route_container);
                            extracted_result = Audio_LSB.Decode_Audio(GlobalVariables.route_container);
                                //UpdateUI(() => BoldToLog(DateTime.Now.ToString() + "--- Decrypted String Successful\n", false));
                               // UpdateUI(() => richTextBoxLog.ScrollToCaret());

                        }
                        UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Extracted File to memory Successful\n"));
                            UpdateUI(() => richTextBoxLog.ScrollToCaret());
                            extracted_result = Aes_ChaCha_Decryptor.Decrypt(extracted_result, GlobalVariables.password);
                            UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Decrypted File in memory Successful, select a route to save\n"));
                            UpdateUI(() => richTextBoxLog.ScrollToCaret());
                            //extracted_result = null;

                            int nameserperatorindex = BytesStringThings.FindSeparatorIndex(extracted_result, GlobalVariables.separator);
                            BytesStringThings.ExtractFileName(extracted_result, nameserperatorindex);
                            this.Invoke(new Action(() => Outputfile_any()));

                            extracted_result = BytesStringThings.ExtractFileContent(extracted_result, nameserperatorindex);
                            //extracted_result = null;

                            if (GlobalVariables.outputnameandroute != null)
                            {
                                BytesStringThings.BytesWritetoFile(GlobalVariables.outputnameandroute, extracted_result);
                                extracted_result = null;
                            }
                            UpdateUI(() => BoldToLog(DateTime.Now.ToString() + "--- Decrypted File Saved Successful\n", false));
                            UpdateUI(() => richTextBoxLog.ScrollToCaret());

                            ShowMessageOnUIThread("Success to decode file with encryption from image", "Success");
                        

                    }

                    catch (Exception ex)
                    {
                        UpdateUI(() => BoldToLog(DateTime.Now.ToString() + "Error" + ex.Message + "\n", true));
                        UpdateUI(() => richTextBoxLog.ScrollToCaret());

                        ShowMessageOnUIThread(ex.Message, "Error");
                    }
                    finally
                    {
                        

                    }

                }


                if (GlobalVariables.encode && GlobalVariables.disablencrypt && GlobalVariables.isfile)//Encode plain file
                {
                    try
                    {
                        if (check_multi.Checked)
                        {

                            this.Invoke(new Action(() => mulitpla_output()));//for duplicate copy, choose route first
                        }
                        foreach (string single_container in GlobalVariables.route_containers)
                        {
                            string newroutename = "";
                            DateTime lastaccess = new DateTime(2021, 8, 15);
                            if (GlobalVariables.copymeta)
                            {
                                lastaccess = File.GetLastAccessTime(single_container);
                            }
                            if (check_audio.Checked == false)
                            {
                                Bitmap loaded = (Bitmap)Support_Converter.ConvertOthersToPngInMemory(single_container);
                                Bitmap result = null;
                                if (GlobalVariables.Algor == "LSB")
                                {
                                    string StringFromFile = BytesStringThings.ReadFileToStringwithName(GlobalVariables.route_secret);
                                    UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Readed secret File in memory Successful\n"));
                                    UpdateUI(() => richTextBoxLog.ScrollToCaret());

                                    UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Loaded container in memory Successful, start to embed.\n"));
                                    UpdateUI(() => richTextBoxLog.ScrollToCaret());

                                    result = LSB_Image.embed(StringFromFile, loaded);
                                    UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Embed File in memory Successful, select a route to save\n"));
                                    UpdateUI(() => richTextBoxLog.ScrollToCaret());

                                }
                                else if (GlobalVariables.Algor == "Linear")
                                {
                                    UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Loaded container in memory Successful, start to embed.\n"));
                                    UpdateUI(() => richTextBoxLog.ScrollToCaret());
                                    result = Core_Linear_Image.EncodeFileLinear(loaded, BytesStringThings.ReadFileToByteswithName(GlobalVariables.route_secret));
                                }

                                UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Readed secret File and embed in container in memory Successful, please save\n"));
                                UpdateUI(() => richTextBoxLog.ScrollToCaret());

                                if (check_multi.Checked == false)
                                {
                                    this.Invoke(new Action(() => Outputfile_pngbmp()));
                                }
                                if (GlobalVariables.outputnameandroute != null && check_multi.Checked == false)
                                {
                                    newroutename = GlobalVariables.outputnameandroute;
                                    result.Save(newroutename, Support_Converter.SaveFormatImage(GlobalVariables.outputformat));
                                    loaded.Dispose();

                                }
                                if (GlobalVariables.multipal_route != null && check_multi.Checked == true)
                                {
                                    if (GlobalVariables.keepformat)
                                    {
                                        newroutename = Path.Combine(GlobalVariables.multipal_route, (Path.GetFileName(single_container)));
                                        result.Save(newroutename, Support_Converter.SaveFormatImage(GlobalVariables.outputformat));
                                        loaded.Dispose();

                                    }
                                    else
                                    {
                                        newroutename = Path.Combine(GlobalVariables.multipal_route, (Path.GetFileNameWithoutExtension(single_container)) + GlobalVariables.outputformat);
                                        result.Save(newroutename, Support_Converter.SaveFormatImage(GlobalVariables.outputformat));
                                        loaded.Dispose();
                                    }
                                }

                                UpdateUI(() => BoldToLog(DateTime.Now.ToString() + "--- Saved Successful\n", false));
                                UpdateUI(() => richTextBoxLog.ScrollToCaret());

                               
                            }
                            else if (check_audio.Checked == true)
                            {
                                if (check_multi.Checked == false)
                                {
                                    this.Invoke(new Action(() => Outputfile_wav()));
                                    Audio_LSB.Encode_Audio(single_container, GlobalVariables.outputnameandroute, BytesStringThings.ReadFileToByteswithName(GlobalVariables.route_secret));

                                }
                                if (GlobalVariables.multipal_route != null && check_multi.Checked)
                                {
                                    newroutename = Path.Combine(GlobalVariables.multipal_route, (Path.GetFileName(single_container)));
                                    Audio_LSB.Encode_Audio(single_container, newroutename, BytesStringThings.ReadFileToByteswithName(GlobalVariables.route_secret));

                                }
                            }


                            if (GlobalVariables.copymeta)
                            {
                                File.SetCreationTime(newroutename, File.GetCreationTime(single_container));
                                File.SetLastAccessTime(newroutename, lastaccess);
                                File.SetLastWriteTime(newroutename, File.GetLastWriteTime(single_container));

                            }

                        }
                        ShowMessageOnUIThread("Success to encode file without encryption to image", "Success");

                    }
                    catch (Exception ex)
                    {
                        UpdateUI(() => BoldToLog(DateTime.Now.ToString() + "Error" + ex.Message + "\n", true));
                        UpdateUI(() => richTextBoxLog.ScrollToCaret());

                        ShowMessageOnUIThread(ex.Message, "Error");
                    }
                }


                if (GlobalVariables.decode && GlobalVariables.disablencrypt && GlobalVariables.isfile)//Decode plain file
                {
                    try
                    {
                        byte[] data = new byte[0];
                        if (check_audio.Checked == false)
                        {
                            Bitmap unloading = new Bitmap(GlobalVariables.route_container);

                            UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Readed loaded container\n"));
                            UpdateUI(() => richTextBoxLog.ScrollToCaret());

                            if (GlobalVariables.Algor == "LSB")
                            {
                                data = Convert.FromBase64String(LSB_Image.extract(unloading));
                                UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Extracted File Successful\n"));
                                UpdateUI(() => richTextBoxLog.ScrollToCaret());

                            }
                            else if (GlobalVariables.Algor == "Linear")
                            {
                                data = Core_Linear_Image.DecodeFileFromImage(unloading);
                                UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Extracted File Successful\n"));
                                UpdateUI(() => richTextBoxLog.ScrollToCaret());
                            }
                            unloading.Dispose();
                        }
                        else if (check_audio.Checked)
                        {
                            data = Audio_LSB.Decode_Audio(GlobalVariables.route_container);
                            UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Extracted File Successful\n"));
                            UpdateUI(() => richTextBoxLog.ScrollToCaret());

                        }
                        int nameserperatorindex = BytesStringThings.FindSeparatorIndex(data, GlobalVariables.separator);
                        BytesStringThings.ExtractFileName(data, nameserperatorindex);
                        this.Invoke(new Action(() => Outputfile_any()));

                        data = BytesStringThings.ExtractFileContent(data, nameserperatorindex);
                        if (GlobalVariables.outputnameandroute != null)
                        {
                            BytesStringThings.BytesWritetoFile(GlobalVariables.outputnameandroute, data);
                        }
                        UpdateUI(() => BoldToLog(DateTime.Now.ToString() + "--- Saved Successful\n", false));
                        UpdateUI(() => richTextBoxLog.ScrollToCaret());

                        
                        ShowMessageOnUIThread("Success decode file without encryption from image", "Success");




                    }
                    catch (Exception ex)
                    {
                        UpdateUI(() => BoldToLog(DateTime.Now.ToString() + "Error" + ex.Message + "\n", true));
                        UpdateUI(() => richTextBoxLog.ScrollToCaret());

                        ShowMessageOnUIThread(ex.Message, "Error");
                    }

                }

                //Start string
                if (GlobalVariables.encode && GlobalVariables.enableencrypt && GlobalVariables.isstring)//Encode encrypted string
                {
                    try
                    {
                        if (check_multi.Checked)
                        {

                            this.Invoke(new Action(() => mulitpla_output()));//for duplicate copy, choose route first
                        }
                        foreach (string single_container in GlobalVariables.route_containers)
                        {
                            DateTime lastaccess = new DateTime(2021, 8, 15);
                            if (GlobalVariables.copymeta)
                            {
                                lastaccess = File.GetLastAccessTime(single_container);
                            }

                            string newroutename = "";
                            byte[] plain_bin = Convert.FromBase64String(BytesStringThings.StringtoBase64(Input_PlainText.Text));

                            byte[] salt, nonce, tag;
                            // ��������
                            byte[] encryptedData = Aes_ChaCha_Encryptor.Encrypt(plain_bin, GlobalVariables.password, out salt, out nonce, out tag);
                            UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Encrypted String Successful\n"));
                            UpdateUI(() => richTextBoxLog.ScrollToCaret());
                            if (check_audio.Checked == false)
                            {
                                Bitmap loaded = (Bitmap)Support_Converter.ConvertOthersToPngInMemory(single_container);
                                UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Container Loaded Successful, start to embed\n"));
                                UpdateUI(() => richTextBoxLog.ScrollToCaret());
                                Bitmap result = null;

                                if (GlobalVariables.Algor == "LSB")
                                {

                                    result = LSB_Image.embed(Convert.ToBase64String(BytesStringThings.CombineBytes(salt, nonce, tag, encryptedData)), loaded);
                                    UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Embed string Successful, please save loaded container\n"));
                                    UpdateUI(() => richTextBoxLog.ScrollToCaret());

                                }
                                else if (GlobalVariables.Algor == "Linear")
                                {

                                    result = Core_Linear_Image.EncodeMsgLinearImage(Convert.ToBase64String(BytesStringThings.CombineBytes(salt, nonce, tag, encryptedData)), loaded);
                                    UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Embed string Successful, please save loaded container\n"));
                                    UpdateUI(() => richTextBoxLog.ScrollToCaret());
                                }
                                if (check_multi.Checked == false)
                                {
                                    this.Invoke(new Action(() => Outputfile_pngbmp()));
                                }
                                if (GlobalVariables.outputnameandroute != null && check_multi.Checked == false)
                                {
                                    newroutename = GlobalVariables.outputnameandroute;
                                    result.Save(newroutename, Support_Converter.SaveFormatImage(GlobalVariables.outputformat));

                                }
                                if (GlobalVariables.multipal_route != null && check_multi.Checked == true)
                                {
                                    if (GlobalVariables.keepformat)
                                    {
                                        newroutename = Path.Combine(GlobalVariables.multipal_route, (Path.GetFileName(single_container)));
                                        result.Save(newroutename, Support_Converter.SaveFormatImage(GlobalVariables.outputformat));

                                    }
                                    else
                                    {
                                        newroutename = Path.Combine(GlobalVariables.multipal_route, (Path.GetFileNameWithoutExtension(single_container)) + GlobalVariables.outputformat);
                                        result.Save(newroutename, Support_Converter.SaveFormatImage(GlobalVariables.outputformat));
                                    }
                                }
                                UpdateUI(() => BoldToLog(DateTime.Now.ToString() + "--- Saved Successful\n", false));
                                UpdateUI(() => richTextBoxLog.ScrollToCaret());
                                loaded.Dispose();
                                result.Dispose();
                            }
                            else if (check_audio.Checked == true)
                            {
                                if (check_multi.Checked == false)
                                {
                                    this.Invoke(new Action(() => Outputfile_wav()));
                                    Audio_LSB.Encode_Audio(single_container, GlobalVariables.outputnameandroute, BytesStringThings.CombineBytes(salt, nonce, tag, encryptedData));

                                }
                                if (GlobalVariables.multipal_route != null && check_multi.Checked)
                                {
                                    newroutename = Path.Combine(GlobalVariables.multipal_route, (Path.GetFileName(single_container)));
                                    Audio_LSB.Encode_Audio(single_container, newroutename, BytesStringThings.CombineBytes(salt, nonce, tag, encryptedData));

                                }
                            }
                            if (GlobalVariables.copymeta)
                            {
                                File.SetCreationTime(newroutename, File.GetCreationTime(single_container));
                                File.SetLastAccessTime(newroutename, lastaccess);
                                File.SetLastWriteTime(newroutename, File.GetLastWriteTime(single_container));

                            }

                        }
                        ShowMessageOnUIThread("Success to encode string with encryption to image", "Success");

                    }
                    catch (Exception ex)
                    {
                        UpdateUI(() => BoldToLog(DateTime.Now.ToString() + "Error" + ex.Message + "\n", true));
                        UpdateUI(() => richTextBoxLog.ScrollToCaret());

                        ShowMessageOnUIThread(ex.Message, "Error");
                    }
                }


                if (GlobalVariables.decode && GlobalVariables.enableencrypt && GlobalVariables.isstring)//Decode encrypted string
                {

                    try
                    {
                        if (check_audio.Checked == false)
                        {
                            Bitmap unloading = new Bitmap(GlobalVariables.route_container);
                            UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Readed Loaded container Successful\n"));
                            UpdateUI(() => richTextBoxLog.ScrollToCaret());

                            string encrypted_result = "";
                            string rawresult = "";
                            string result = "";

                            if (GlobalVariables.Algor == "LSB")
                            {
                                encrypted_result = LSB_Image.extract(unloading);
                                UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Extracted String Successful\n"));
                                UpdateUI(() => richTextBoxLog.ScrollToCaret());

                            }
                            else if (GlobalVariables.Algor == "Linear")
                            {
                                rawresult = Core_Linear_Image.DecodeMsgLinearImage(unloading);
                                UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Extracted String Successful\n"));
                                UpdateUI(() => richTextBoxLog.ScrollToCaret());

                                int nullCharIndex = rawresult.IndexOf('\0');
                                if (nullCharIndex != -1)
                                {
                                    encrypted_result = rawresult.Substring(0, nullCharIndex);
                                }

                            }
                            result = System.Text.Encoding.UTF8.GetString(Aes_ChaCha_Decryptor.Decrypt(Convert.FromBase64String(encrypted_result), GlobalVariables.password));
                            UpdateUI(() => BoldToLog(DateTime.Now.ToString() + "--- Decrypted String Successful\n", false));
                            UpdateUI(() => richTextBoxLog.ScrollToCaret());
                            unloading.Dispose();
                            UpdateUI(() => Input_PlainText.Text = result);
                            ShowMessageOnUIThread("Success to decode string with encryption from image", "Success");
                        }
                        else if (check_audio.Checked)
                        {
                            //byte[] xx = Audio_LSB.Decode_Audio(GlobalVariables.route_container);
                            string result_audio = System.Text.Encoding.UTF8.GetString(Aes_ChaCha_Decryptor.Decrypt(Audio_LSB.Decode_Audio(GlobalVariables.route_container), GlobalVariables.password));
                            UpdateUI(() => BoldToLog(DateTime.Now.ToString() + "--- Decrypted String Successful\n", false));
                            UpdateUI(() => richTextBoxLog.ScrollToCaret());

                            UpdateUI(() => Input_PlainText.Text = result_audio);
                            ShowMessageOnUIThread("Success to decode string with encryption from wav audio", "Success");

                        }

                    }
                    catch (Exception ex)
                    {
                        UpdateUI(() => BoldToLog(DateTime.Now.ToString() + "Error" + ex.Message + "\n", true));
                        UpdateUI(() => richTextBoxLog.ScrollToCaret());

                        ShowMessageOnUIThread(ex.Message, "Error");
                    }

                }


                if (GlobalVariables.encode && GlobalVariables.disablencrypt && GlobalVariables.isstring)//Encode plain string
                {




                    try
                    {
                        if (check_multi.Checked)
                        {

                            this.Invoke(new Action(() => mulitpla_output()));//for duplicate copy, choose route first
                        }
                        foreach (string single_container in GlobalVariables.route_containers)
                        {
                            DateTime lastaccess = new DateTime(2021, 8, 15);
                            string newroutename = "";

                            if (GlobalVariables.copymeta)
                            {
                                lastaccess = File.GetLastAccessTime(single_container);
                            }
                            if (check_audio.Checked == false)
                            {

                                Bitmap loaded = (Bitmap)Support_Converter.ConvertOthersToPngInMemory(single_container);
                                Bitmap result = null;

                                if (GlobalVariables.Algor == "LSB")
                                {
                                    UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Loaded container Successful\n"));
                                    UpdateUI(() => richTextBoxLog.ScrollToCaret());

                                    result = LSB_Image.embed(BytesStringThings.StringtoBase64(Input_PlainText.Text), loaded);
                                    UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Embed String Successful\n"));
                                    UpdateUI(() => richTextBoxLog.ScrollToCaret());
                                }
                                else if (GlobalVariables.Algor == "Linear")
                                {
                                    UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Loaded container Successful\n"));
                                    UpdateUI(() => richTextBoxLog.ScrollToCaret());

                                    result = Core_Linear_Image.EncodeMsgLinearImage(BytesStringThings.StringtoBase64(Input_PlainText.Text), loaded);
                                    UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Embed String Successful\n"));
                                    UpdateUI(() => richTextBoxLog.ScrollToCaret());
                                }
                                if (check_multi.Checked == false)
                                {
                                    this.Invoke(new Action(() => Outputfile_pngbmp()));
                                }
                                if (GlobalVariables.outputnameandroute != null && check_multi.Checked == false)
                                {
                                    newroutename = GlobalVariables.outputnameandroute;
                                    result.Save(newroutename, Support_Converter.SaveFormatImage(GlobalVariables.outputformat));

                                }
                                if (GlobalVariables.multipal_route != null && check_multi.Checked == true)
                                {
                                    if (GlobalVariables.keepformat)
                                    {
                                        newroutename = Path.Combine(GlobalVariables.multipal_route, (Path.GetFileName(single_container)));
                                        result.Save(newroutename, Support_Converter.SaveFormatImage(GlobalVariables.outputformat));

                                    }
                                    else
                                    {
                                        newroutename = Path.Combine(GlobalVariables.multipal_route, (Path.GetFileNameWithoutExtension(single_container)) + GlobalVariables.outputformat);
                                        result.Save(newroutename, Support_Converter.SaveFormatImage(GlobalVariables.outputformat));
                                    }
                                }
                                UpdateUI(() => BoldToLog(DateTime.Now.ToString() + "--- Saved Successful\n", false));
                                UpdateUI(() => richTextBoxLog.ScrollToCaret());
                                loaded.Dispose();
                                result.Dispose();
                            }
                            else if (check_audio.Checked == true)
                            {
                                if (check_multi.Checked == false)
                                {
                                    this.Invoke(new Action(() => Outputfile_wav()));
                                    Audio_LSB.Encode_Audio(single_container, GlobalVariables.outputnameandroute, Encoding.UTF8.GetBytes(Input_PlainText.Text));

                                }
                                if (GlobalVariables.multipal_route != null && check_multi.Checked)
                                {
                                    newroutename = Path.Combine(GlobalVariables.multipal_route, (Path.GetFileName(single_container)));
                                    Audio_LSB.Encode_Audio(single_container, newroutename, Encoding.UTF8.GetBytes(Input_PlainText.Text));

                                }
                            }

                            if (GlobalVariables.copymeta)
                            {
                                File.SetCreationTime(newroutename, File.GetCreationTime(single_container));
                                File.SetLastAccessTime(newroutename, lastaccess);
                                File.SetLastWriteTime(newroutename, File.GetLastWriteTime(single_container));

                            }

                        }
                        ShowMessageOnUIThread("Success to encode plain text to image", "Success");

                    }
                    catch (Exception ex)
                    {
                        UpdateUI(() => BoldToLog(DateTime.Now.ToString() + "Error" + ex.Message + "\n", true));
                        UpdateUI(() => richTextBoxLog.ScrollToCaret());

                        ShowMessageOnUIThread(ex.Message, "Error");

                    }
                }


                if (GlobalVariables.decode && GlobalVariables.disablencrypt && GlobalVariables.isstring)//Decode plain string
                {


                    string result = "";
                    string rawresult = "";
                    try
                    {
                        if (check_audio.Checked == false)
                        {
                            Bitmap unloading = new Bitmap(GlobalVariables.route_container);
                            UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Readed Loaded container Successful\n"));
                            UpdateUI(() => richTextBoxLog.ScrollToCaret());

                            if (GlobalVariables.Algor == "LSB")
                            {
                                result = LSB_Image.extract(unloading);
                                UpdateUI(() => BoldToLog(DateTime.Now.ToString() + "--- Extracted String Successful\n", false));
                                UpdateUI(() => richTextBoxLog.ScrollToCaret());

                            }
                            else if (GlobalVariables.Algor == "Linear")
                            {
                                rawresult = Core_Linear_Image.DecodeMsgLinearImage(unloading);
                                UpdateUI(() => BoldToLog(DateTime.Now.ToString() + "--- Extracted String Successful\n", false));
                                UpdateUI(() => richTextBoxLog.ScrollToCaret());

                                int nullCharIndex = rawresult.IndexOf('\0');
                                if (nullCharIndex != -1)
                                {
                                    result = rawresult.Substring(0, nullCharIndex);
                                }
                            }
                            UpdateUI(() => Input_PlainText.Text = BytesStringThings.Base64toString(result));
                            unloading.Dispose();
                            ShowMessageOnUIThread("Success to deocode plain text from image", "Success");
                        }
                        else if (check_audio.Checked)
                        {
                            //byte[] xx = Audio_LSB.Decode_Audio(GlobalVariables.route_container);
                            string result_audio = System.Text.Encoding.UTF8.GetString(Audio_LSB.Decode_Audio(GlobalVariables.route_container));
                            UpdateUI(() => BoldToLog(DateTime.Now.ToString() + "--- Decrypted String Successful\n", false));
                            UpdateUI(() => richTextBoxLog.ScrollToCaret());

                            UpdateUI(() => Input_PlainText.Text = result_audio);
                            ShowMessageOnUIThread("Success to decode string with encryption from wav audio", "Success");

                        }

                    }
                    catch (Exception ex)
                    {
                        UpdateUI(() => BoldToLog(DateTime.Now.ToString() + "Error" + ex.Message + "\n", true));
                        UpdateUI(() => richTextBoxLog.ScrollToCaret());

                        ShowMessageOnUIThread(ex.Message, "Error");
                    }
                    finally
                    {
                        
                    }
                }


                if ((GlobalVariables.encode == false && GlobalVariables.decode == false) || (GlobalVariables.enableencrypt == false && GlobalVariables.disablencrypt == false) || (Radio_File.Checked == false && Radio_String.Checked == false))//
                {
                    UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Lack of Choice\n"));
                    UpdateUI(() => richTextBoxLog.ScrollToCaret());

                    ShowMessageOnUIThread("Lack of one or more Choices", "Error");
                }


                GC.Collect();

            }
            else if (GlobalVariables.mode == "Encryptor" )
            {

                if (GlobalVariables.encode && GlobalVariables.isfile)
                {
                    GlobalVariables.defaultname = "";
                    this.Invoke(new Action(() => Outputfile_any()));
                    try
                    {
                        if (GlobalVariables.outputnameandroute != null)
                        {
                            FileEnc.EncryptFile(GlobalVariables.route_secret, GlobalVariables.outputnameandroute, GlobalVariables.password);
                        }
                        UpdateUI(() => BoldToLog(DateTime.Now.ToString() + "--- AS ONLY ENCRYPTOR: Encrypted File And Saved Successfully" + "\n", false));
                        UpdateUI(() => richTextBoxLog.ScrollToCaret());

                    }
                    catch (Exception ex)
                    {
                        UpdateUI(() => BoldToLog(DateTime.Now.ToString() + "Error" + ex.Message + "\n", true));
                        UpdateUI(() => richTextBoxLog.ScrollToCaret());

                    }
                }
                else if (GlobalVariables.decode && GlobalVariables.isfile)
                {
                    GlobalVariables.defaultname = "";
                    this.Invoke(new Action(() => Outputfile_any()));
                    try
                    {
                        if (GlobalVariables.outputnameandroute != null)
                        {
                            FileEnc.DecryptFile(GlobalVariables.route_secret, GlobalVariables.outputnameandroute, GlobalVariables.password);
                        }
                        UpdateUI(() => BoldToLog(DateTime.Now.ToString() + "--- AS ONLY ENCRYPTOR: Decrypted File And Saved Successfully" + "\n", false));
                        UpdateUI(() => richTextBoxLog.ScrollToCaret());

                    }
                    catch (Exception ex)
                    {
                        UpdateUI(() => BoldToLog(DateTime.Now.ToString() + "Error" + ex.Message + "\n", true));
                        UpdateUI(() => richTextBoxLog.ScrollToCaret());

                    }

                }

                else if (GlobalVariables.encode && GlobalVariables.isstring)
                {
                    byte[] plain_bin = Convert.FromBase64String(BytesStringThings.StringtoBase64(Input_PlainText.Text));

                    byte[] salt, nonce, tag;
                    // ��������
                    byte[] encryptedData = Aes_ChaCha_Encryptor.Encrypt(plain_bin, GlobalVariables.password, out salt, out nonce, out tag);
                    UpdateUI(() => Input_PlainText.Text = (Convert.ToBase64String(BytesStringThings.CombineBytes(salt, nonce, tag, encryptedData))));
                    UpdateUI(() => BoldToLog(DateTime.Now.ToString() + "--- AS ONLY ENCRYPTOR: Encrypted String Successfully" + "\n", false));
                    UpdateUI(() => richTextBoxLog.ScrollToCaret());

                }

                else if (GlobalVariables.decode && GlobalVariables.isstring)
                {
                    try
                    {
                        string result = System.Text.Encoding.UTF8.GetString(Aes_ChaCha_Decryptor.Decrypt(Convert.FromBase64String(Input_PlainText.Text), GlobalVariables.password));
                        UpdateUI(() => Input_PlainText.Text = result);
                        UpdateUI(() => BoldToLog(DateTime.Now.ToString() + "--- AS ONLY ENCRYPTOR: Decrypted String Successfully" + "\n", false));
                        UpdateUI(() => richTextBoxLog.ScrollToCaret());

                    }
                    catch (Exception ex)
                    {
                        UpdateUI(() => BoldToLog(DateTime.Now.ToString() + "s--- Error:" + ex.Message + "\n", true));
                        UpdateUI(() => richTextBoxLog.ScrollToCaret());

                    }
                }


            }



            GC.Collect(2, GCCollectionMode.Forced);
            GC.WaitForPendingFinalizers();

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

        private void Outputfile_pngbmp()
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


        private void Outputfile_wav()
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

        private void Outputfile_any()
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

        private void mulitpla_output()
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
