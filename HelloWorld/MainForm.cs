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



        public MainForm()
        {
            /*
            CultureInfo currentUICulture = CultureInfo.CurrentUICulture;
            CultureInfo currentCulture = CultureInfo.CurrentCulture;

            // 设置应用程序的用户界面文化
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
        private void UpdateUI(Action updateAction)
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

        private T ReadUI<T>(Func<T> readAction)
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
        private void BoldToLog(string newText, bool iferror)
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
                            byte[] plainsecret_content = BytesStringThings.ReadFileToByteswithName(GlobalVariables.route_secret);
                            UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Secret File Readed\n"));
                            UpdateUI(() => richTextBoxLog.ScrollToCaret());


                            byte[] encryptedData = AesGcmEncryptor.Encrypt(plainsecret_content, GlobalVariables.password, out byte[] salt, out byte[] nonce, out byte[] tag);

                            UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Secret File Encrypted and stored in memory\n"));
                            UpdateUI(() => richTextBoxLog.ScrollToCaret());
                            Bitmap loaded = (Bitmap)Support_Converter.ConvertOthersToPngInMemory(single_container);

                            UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Container Loaded and Converted if necessary, Start to Embed\n"));
                            UpdateUI(() => richTextBoxLog.ScrollToCaret());

                            if (GlobalVariables.Algor == "LSB")
                            {
                                Bitmap result = LSB_Image.embed(Convert.ToBase64String(BytesStringThings.CombineBytes(salt, nonce, tag, encryptedData)), loaded);
                                UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Success to embed, Please select a route and name to save loaded container.\n"));
                                UpdateUI(() => richTextBoxLog.ScrollToCaret());
                                if (check_multi.Checked == false)
                                {
                                    this.Invoke(new Action(() => Outputfile_pngbmp()));
                                }
                                if (GlobalVariables.outputnameandroute != null && check_multi.Checked == false)
                                {
                                    result.Save(GlobalVariables.outputnameandroute, System.Drawing.Imaging.ImageFormat.Png);
                                }
                                if (GlobalVariables.multipal_route != null && check_multi.Checked == true)
                                {
                                    result.Save(Path.Combine(GlobalVariables.multipal_route, Path.GetFileName(single_container)), System.Drawing.Imaging.ImageFormat.Png);
                                }

                                result.Dispose();

                                UpdateUI(() => BoldToLog(DateTime.Now.ToString() + "--- Loaded container saved\n", false));
                                UpdateUI(() => richTextBoxLog.ScrollToCaret());

                            }
                            else if (GlobalVariables.Algor == "Linear")
                            {
                                Bitmap result = Core_Linear_Image.EncodeFileLinear(loaded, BytesStringThings.CombineBytes(salt, nonce, tag, encryptedData));
                                UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Success to embed, Please select a route and name to save loaded container.\n"));
                                UpdateUI(() => richTextBoxLog.ScrollToCaret());

                                if (check_multi.Checked == false)
                                {
                                    this.Invoke(new Action(() => Outputfile_pngbmp()));
                                }
                                if (GlobalVariables.outputnameandroute != null && check_multi.Checked == false)
                                {
                                    result.Save(GlobalVariables.outputnameandroute, System.Drawing.Imaging.ImageFormat.Png);
                                }
                                if (GlobalVariables.multipal_route != null && check_multi.Checked == true)
                                {
                                    result.Save(Path.Combine(GlobalVariables.multipal_route, Path.GetFileName(single_container)), System.Drawing.Imaging.ImageFormat.Png);
                                }

                                result.Dispose();
                                UpdateUI(() => BoldToLog(DateTime.Now.ToString() + "--- Loaded container saved\n", false));
                                UpdateUI(() => richTextBoxLog.ScrollToCaret());

                            }


                            loaded.Dispose();

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

                    Bitmap unloading = new Bitmap(GlobalVariables.route_container);
                    UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Readed Loaded container\n"));
                    UpdateUI(() => richTextBoxLog.ScrollToCaret());

                    try
                    {
                        string encrypted_result = "";
                        byte[]? decrypted_content = new byte[0];
                        if (GlobalVariables.Algor == "LSB")
                        {
                            encrypted_result = LSB_Image.extract(unloading);
                            UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Extracted File to memory Successful\n"));
                            UpdateUI(() => richTextBoxLog.ScrollToCaret());

                            decrypted_content = AesGcmDecryptor.Decrypt(Convert.FromBase64String(encrypted_result), GlobalVariables.password);
                            UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Decrypted File in memory Successful, select a route to save\n"));
                            UpdateUI(() => richTextBoxLog.ScrollToCaret());

                        }
                        else if (GlobalVariables.Algor == "Linear")
                        {
                            byte[]? filecontent = Core_Linear_Image.DecodeFileFromImage(unloading);
                            UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Extracted File to memory Successful\n"));
                            UpdateUI(() => richTextBoxLog.ScrollToCaret());
                            decrypted_content = AesGcmDecryptor.Decrypt(filecontent, GlobalVariables.password);
                            UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Decrypted File in memory Successful, select a route to save\n"));
                            UpdateUI(() => richTextBoxLog.ScrollToCaret());
                            filecontent = null;
                        }

                        int nameserperatorindex = BytesStringThings.FindSeparatorIndex(decrypted_content, GlobalVariables.separator);
                        BytesStringThings.ExtractFileName(decrypted_content, nameserperatorindex);
                        this.Invoke(new Action(() => Outputfile_any()));

                        byte[]? realcontent = BytesStringThings.ExtractFileContent(decrypted_content, nameserperatorindex);
                        decrypted_content = null;
                        if (GlobalVariables.outputnameandroute != null)
                        {
                            BytesStringThings.BytesWritetoFile(GlobalVariables.outputnameandroute, realcontent);
                            realcontent = null;
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
                        unloading.Dispose();

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

                            if (GlobalVariables.Algor == "LSB")
                            {
                                string StringFromFile = BytesStringThings.ReadFileToStringwithName(GlobalVariables.route_secret);
                                UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Readed secret File in memory Successful\n"));
                                UpdateUI(() => richTextBoxLog.ScrollToCaret());

                                Bitmap loaded = (Bitmap)Support_Converter.ConvertOthersToPngInMemory(single_container);
                                UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Loaded container in memory Successful, start to embed.\n"));
                                UpdateUI(() => richTextBoxLog.ScrollToCaret());

                                Bitmap result = LSB_Image.embed(StringFromFile, loaded);
                                UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Embed File in memory Successful, select a route to save\n"));
                                UpdateUI(() => richTextBoxLog.ScrollToCaret());

                                if (check_multi.Checked == false)
                                {
                                    this.Invoke(new Action(() => Outputfile_pngbmp()));
                                }
                                if (GlobalVariables.outputnameandroute != null && check_multi.Checked == false)
                                {
                                    result.Save(GlobalVariables.outputnameandroute, System.Drawing.Imaging.ImageFormat.Png);
                                }
                                if (GlobalVariables.multipal_route != null && check_multi.Checked == true)
                                {
                                    result.Save(Path.Combine(GlobalVariables.multipal_route, Path.GetFileName(single_container)), System.Drawing.Imaging.ImageFormat.Png);
                                }

                                UpdateUI(() => BoldToLog(DateTime.Now.ToString() + "--- Saved Successful\n", false));
                                UpdateUI(() => richTextBoxLog.ScrollToCaret());

                                loaded.Dispose();

                            }
                            else if (GlobalVariables.Algor == "Linear")
                            {
                                Bitmap loaded = (Bitmap)Support_Converter.ConvertOthersToPngInMemory(single_container);
                                UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Loaded container in memory Successful, start to embed.\n"));
                                UpdateUI(() => richTextBoxLog.ScrollToCaret());

                                Bitmap result = Core_Linear_Image.EncodeFileLinear(loaded, BytesStringThings.ReadFileToByteswithName(GlobalVariables.route_secret));
                                UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Readed secret File and embed in container in memory Successful, please save\n"));
                                UpdateUI(() => richTextBoxLog.ScrollToCaret());

                                if (check_multi.Checked == false)
                                {
                                    this.Invoke(new Action(() => Outputfile_pngbmp()));
                                }
                                if (GlobalVariables.outputnameandroute != null && check_multi.Checked == false)
                                {
                                    result.Save(GlobalVariables.outputnameandroute, System.Drawing.Imaging.ImageFormat.Png);
                                }
                                if (GlobalVariables.multipal_route != null && check_multi.Checked == true)
                                {
                                    result.Save(Path.Combine(GlobalVariables.multipal_route, Path.GetFileName(single_container)), System.Drawing.Imaging.ImageFormat.Png);
                                }

                                UpdateUI(() => BoldToLog(DateTime.Now.ToString() + "--- Saved Successful\n", false));
                                UpdateUI(() => richTextBoxLog.ScrollToCaret());

                                loaded.Dispose();

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
                        string result = string.Empty;
                        Bitmap unloading = new Bitmap(GlobalVariables.route_container);
                        UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Readed loaded container\n"));
                        UpdateUI(() => richTextBoxLog.ScrollToCaret());

                        if (GlobalVariables.Algor == "LSB")
                        {
                            result = LSB_Image.extract(unloading);
                            UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Extracted File Successful\n"));
                            UpdateUI(() => richTextBoxLog.ScrollToCaret());

                            byte[] datawithname = Convert.FromBase64String(result);

                            int nameserperatorindex = BytesStringThings.FindSeparatorIndex(datawithname, GlobalVariables.separator);
                            BytesStringThings.ExtractFileName(datawithname, nameserperatorindex);
                            this.Invoke(new Action(() => Outputfile_any()));

                            byte[] realcontent = BytesStringThings.ExtractFileContent(datawithname, nameserperatorindex);
                            if (GlobalVariables.outputnameandroute != null)
                            {
                                BytesStringThings.BytesWritetoFile(GlobalVariables.outputnameandroute, realcontent);
                            }
                            UpdateUI(() => BoldToLog(DateTime.Now.ToString() + "--- Saved Successful\n", false));
                            UpdateUI(() => richTextBoxLog.ScrollToCaret());

                            unloading.Dispose();
                            ShowMessageOnUIThread("Success decode file without encryption from image", "Success");

                        }
                        else if (GlobalVariables.Algor == "Linear")
                        {
                            byte[] filecontent = Core_Linear_Image.DecodeFileFromImage(unloading);
                            UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Extracted File Successful\n"));
                            UpdateUI(() => richTextBoxLog.ScrollToCaret());


                            int nameserperatorindex = BytesStringThings.FindSeparatorIndex(filecontent, GlobalVariables.separator);
                            BytesStringThings.ExtractFileName(filecontent, nameserperatorindex);
                            this.Invoke(new Action(() => Outputfile_any()));

                            byte[] realcontent = BytesStringThings.ExtractFileContent(filecontent, nameserperatorindex);
                            if (GlobalVariables.outputnameandroute != null)
                            {
                                BytesStringThings.BytesWritetoFile(GlobalVariables.outputnameandroute, realcontent);
                            }
                            UpdateUI(() => BoldToLog(DateTime.Now.ToString() + "--- Saved Successful\n", false));
                            UpdateUI(() => richTextBoxLog.ScrollToCaret());

                            unloading.Dispose();
                            ShowMessageOnUIThread("Success decode file without encryption from image", "Success");

                        }


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

                            byte[] plain_bin = Convert.FromBase64String(BytesStringThings.StringtoBase64(Input_PlainText.Text));

                            byte[] salt, nonce, tag;
                            // 加密数据
                            byte[] encryptedData = AesGcmEncryptor.Encrypt(plain_bin, GlobalVariables.password, out salt, out nonce, out tag);
                            UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Encrypted String Successful\n"));
                            UpdateUI(() => richTextBoxLog.ScrollToCaret());


                            if (GlobalVariables.Algor == "LSB")
                            {
                                Bitmap loaded = (Bitmap)Support_Converter.ConvertOthersToPngInMemory(single_container);
                                UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Container Loaded Successful, start to embed\n"));
                                UpdateUI(() => richTextBoxLog.ScrollToCaret());

                                Bitmap result = LSB_Image.embed(Convert.ToBase64String(BytesStringThings.CombineBytes(salt, nonce, tag, encryptedData)), loaded);
                                UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Embed string Successful, please save loaded container\n"));
                                UpdateUI(() => richTextBoxLog.ScrollToCaret());

                                if (check_multi.Checked == false)
                                {
                                    this.Invoke(new Action(() => Outputfile_pngbmp()));
                                }
                                if (GlobalVariables.outputnameandroute != null && check_multi.Checked == false)
                                {
                                    result.Save(GlobalVariables.outputnameandroute, System.Drawing.Imaging.ImageFormat.Png);
                                }
                                if (GlobalVariables.multipal_route != null && check_multi.Checked == true)
                                {
                                    result.Save(Path.Combine(GlobalVariables.multipal_route, Path.GetFileName(single_container)), System.Drawing.Imaging.ImageFormat.Png);
                                }
                                loaded.Dispose();

                                UpdateUI(() => BoldToLog(DateTime.Now.ToString() + "--- Saved Successful\n", false));
                                UpdateUI(() => richTextBoxLog.ScrollToCaret());
                                result.Dispose();

                            }
                            else if (GlobalVariables.Algor == "Linear")
                            {
                                Bitmap loaded = (Bitmap)Support_Converter.ConvertOthersToPngInMemory(single_container);
                                UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Container Loaded Successful, start to embed\n"));
                                UpdateUI(() => richTextBoxLog.ScrollToCaret());

                                Bitmap result = Core_Linear_Image.EncodeMsgLinearImage(Convert.ToBase64String(BytesStringThings.CombineBytes(salt, nonce, tag, encryptedData)), loaded);
                                UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Embed string Successful, please save loaded container\n"));
                                UpdateUI(() => richTextBoxLog.ScrollToCaret());

                                if (check_multi.Checked == false)
                                {
                                    this.Invoke(new Action(() => Outputfile_pngbmp()));
                                }
                                if (GlobalVariables.outputnameandroute != null && check_multi.Checked == false)
                                {
                                    result.Save(GlobalVariables.outputnameandroute, System.Drawing.Imaging.ImageFormat.Png);
                                }
                                if (GlobalVariables.multipal_route != null && check_multi.Checked == true)
                                {
                                    result.Save(Path.Combine(GlobalVariables.multipal_route, Path.GetFileName(single_container)), System.Drawing.Imaging.ImageFormat.Png);
                                }
                                UpdateUI(() => BoldToLog(DateTime.Now.ToString() + "--- Saved Successful\n", false));
                                UpdateUI(() => richTextBoxLog.ScrollToCaret());
                                loaded.Dispose();
                                result.Dispose();
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

                    Bitmap unloading = new Bitmap(GlobalVariables.route_container);
                    UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Readed Loaded container Successful\n"));
                    UpdateUI(() => richTextBoxLog.ScrollToCaret());

                    string encrypted_result = "";
                    string rawresult = "";
                    string result = "";
                    try
                    {
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
                        result = System.Text.Encoding.UTF8.GetString(AesGcmDecryptor.Decrypt(Convert.FromBase64String(encrypted_result), GlobalVariables.password));
                        UpdateUI(() => BoldToLog(DateTime.Now.ToString() + "--- Decrypted String Successful\n", false));
                        UpdateUI(() => richTextBoxLog.ScrollToCaret());

                        UpdateUI(() => Input_PlainText.Text = result);
                        ShowMessageOnUIThread("Success to decode string with encryption from image", "Success");
                    }
                    catch (Exception ex)
                    {
                        UpdateUI(() => BoldToLog(DateTime.Now.ToString() + "Error" + ex.Message + "\n", true));
                        UpdateUI(() => richTextBoxLog.ScrollToCaret());

                        ShowMessageOnUIThread(ex.Message, "Error");
                    }
                    finally
                    {
                        unloading.Dispose();
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

                            if (GlobalVariables.Algor == "LSB")
                            {
                                Bitmap loaded = (Bitmap)Support_Converter.ConvertOthersToPngInMemory(single_container);
                                UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Loaded container Successful\n"));
                                UpdateUI(() => richTextBoxLog.ScrollToCaret());

                                Bitmap result = LSB_Image.embed(BytesStringThings.StringtoBase64(Input_PlainText.Text), loaded);
                                UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Embed String Successful\n"));
                                UpdateUI(() => richTextBoxLog.ScrollToCaret());

                                if (check_multi.Checked == false)
                                {
                                    this.Invoke(new Action(() => Outputfile_pngbmp()));
                                }
                                if (GlobalVariables.outputnameandroute != null && check_multi.Checked == false)
                                {
                                    result.Save(GlobalVariables.outputnameandroute, System.Drawing.Imaging.ImageFormat.Png);
                                }
                                if (GlobalVariables.multipal_route != null && check_multi.Checked == true)
                                {
                                    result.Save(Path.Combine(GlobalVariables.multipal_route, Path.GetFileName(single_container)), System.Drawing.Imaging.ImageFormat.Png);
                                }
                                UpdateUI(() => BoldToLog(DateTime.Now.ToString() + "--- Saved Successful\n", false));
                                UpdateUI(() => richTextBoxLog.ScrollToCaret());
                                loaded.Dispose();
                                result.Dispose();
                            }
                            else if (GlobalVariables.Algor == "Linear")
                            {
                                Bitmap loaded = (Bitmap)Support_Converter.ConvertOthersToPngInMemory(single_container);
                                UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Loaded container Successful\n"));
                                UpdateUI(() => richTextBoxLog.ScrollToCaret());

                                Bitmap result = Core_Linear_Image.EncodeMsgLinearImage(BytesStringThings.StringtoBase64(Input_PlainText.Text), loaded);
                                UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Embed String Successful\n"));
                                UpdateUI(() => richTextBoxLog.ScrollToCaret());

                                if (check_multi.Checked == false)
                                {
                                    this.Invoke(new Action(() => Outputfile_pngbmp()));
                                }
                                if (GlobalVariables.outputnameandroute != null && check_multi.Checked == false)
                                {
                                    result.Save(GlobalVariables.outputnameandroute, System.Drawing.Imaging.ImageFormat.Png);
                                }
                                if (GlobalVariables.multipal_route != null && check_multi.Checked == true)
                                {
                                    result.Save(Path.Combine(GlobalVariables.multipal_route, Path.GetFileName(single_container)), System.Drawing.Imaging.ImageFormat.Png);
                                }
                                UpdateUI(() => BoldToLog(DateTime.Now.ToString() + "--- Saved Successful\n", false));
                                UpdateUI(() => richTextBoxLog.ScrollToCaret());
                                loaded.Dispose();
                                result.Dispose();

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

                    Bitmap unloading = new Bitmap(GlobalVariables.route_container);
                    UpdateUI(() => richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Readed Loaded container Successful\n"));
                    UpdateUI(() => richTextBoxLog.ScrollToCaret());

                    string result = "";
                    string rawresult = "";
                    try
                    {
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

                        ShowMessageOnUIThread("Success to deocode plain text from image", "Success");
                    }
                    catch (Exception ex)
                    {
                        UpdateUI(() => BoldToLog(DateTime.Now.ToString() + "Error" + ex.Message + "\n", true));
                        UpdateUI(() => richTextBoxLog.ScrollToCaret());

                        ShowMessageOnUIThread(ex.Message, "Error");
                    }
                    finally
                    {
                        unloading.Dispose();
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
            else if (GlobalVariables.mode == "Encryptor")
            {

                if (GlobalVariables.encode && GlobalVariables.isfile)
                {
                    GlobalVariables.defaultname = "";
                    this.Invoke(new Action(() => Outputfile_any()));
                    try
                    {
                        if (GlobalVariables.outputnameandroute != null)
                        {
                            FileAES.EncryptFile(GlobalVariables.route_secret, GlobalVariables.outputnameandroute, GlobalVariables.password);
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
                            FileAES.DecryptFile(GlobalVariables.route_secret, GlobalVariables.outputnameandroute, GlobalVariables.password);
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
                    // 加密数据
                    byte[] encryptedData = AesGcmEncryptor.Encrypt(plain_bin, GlobalVariables.password, out salt, out nonce, out tag);
                    UpdateUI(() => Input_PlainText.Text = (Convert.ToBase64String(BytesStringThings.CombineBytes(salt, nonce, tag, encryptedData))));
                    UpdateUI(() => BoldToLog(DateTime.Now.ToString() + "--- AS ONLY ENCRYPTOR: Encrypted String Successfully" + "\n", false));
                    UpdateUI(() => richTextBoxLog.ScrollToCaret());

                }

                else if (GlobalVariables.decode && GlobalVariables.isstring)
                {
                    try
                    {
                        string result = System.Text.Encoding.UTF8.GetString(AesGcmDecryptor.Decrypt(Convert.FromBase64String(Input_PlainText.Text), GlobalVariables.password));
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
                //openFileDialog.Filter = "文本文件 (*.txt)|*.txt|所有文件 (*.*)|*.*";
                openFileDialog.Title = "Choose a container";
                openFileDialog.Multiselect = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (check_multi.Checked)
                    {
                        string[] selectedFiles = openFileDialog.FileNames;
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
            if (containsSlash == true)
            {
                try
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
                catch (Exception ex)
                {
                    //progressBar1.Value = 0;

                    MessageBox.Show(ex.Message, "Error");

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
                form2 = new Settings(this); // 直接使用类级别的成员变量，不需要重新声明
                form2.FormClosed += (s, args) => this.form2 = null; // 当Form2关闭时，将类级别的引用设置为null
                form2.Show();
                richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Loaded Advanced Settings Form\n");

            }
            else
            {
                form2.BringToFront(); // 如果Form2已经存在，将其带到前台
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
                    richTextBoxLog.AppendText(DateTime.Now.ToString() + "--- Output Name and Route of Loaded Container Selected: " + GlobalVariables.outputnameandroute + "\n");

                }
            }

        }
        private void Outputfile_any()
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
            AboutForm = new AboutBox();
            AboutForm.Show();
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

            // 关闭当前程序实例
            Application.Exit();

        }

        private void mulitpla_output()
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
