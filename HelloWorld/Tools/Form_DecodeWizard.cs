using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.DataFormats;

namespace HideSloth.Tools
{
    public partial class Form_DecodeWizard : Form
    {
        private MainForm form1;
        private Settings form2;
        CancellationTokenSource cts = new CancellationTokenSource();
        public static bool issub = false;
        private string loadedcontainers, outputroute, outputname, pwd;
        private bool ifok = false;
        public static int searchdepth = 1;
        public void AppendTextToRichTextBox(string text)
        {
            // 检查是否需要跨线程调用
            if (richTextBox1.InvokeRequired)
            {
                richTextBox1.Invoke(new Action<string>(AppendTextToRichTextBox), text);
            }
            else
            {
                try
                {
                    richTextBox1.AppendText(text + "\n");
                }
                catch
                {

                }
            }
        }

        public Form_DecodeWizard(MainForm mainForm)
        {
            form1 = mainForm; // 接收并存储对主窗体的引用

            InitializeComponent();
            //pwd = "123"
            if (GlobalVariables.mode == "Normal")
            {
                radio_modenormal.Checked = true;
            }
            else if (GlobalVariables.mode == "Encryptor")
            {
                radio_modeencrypt.Checked = true;
            }
            this.tabControl1.Selecting += new TabControlCancelEventHandler(this.tabControl1_Selecting);

        }
        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            e.Cancel = true;
        }
        private void SwitchTab(int tabIndex)
        {
            // 通过代码切换到指定的选项卡
            tabControl1.Selecting -= tabControl1_Selecting;
            tabControl1.SelectedIndex = tabIndex;
            tabControl1.Selecting += tabControl1_Selecting;
        }

        private void Form_DecodeWizard_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                // 设置对话框的属性，例如Description
                folderBrowserDialog.Description = "Choose the folder of secret files";

                // 显示对话框
                DialogResult result = folderBrowserDialog.ShowDialog();

                // 处理对话框返回的结果
                if (result == DialogResult.OK)
                {
                    loadedcontainers = folderBrowserDialog.SelectedPath;
                    text_loaded.Text = loadedcontainers;
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                // 设置对话框的属性，例如Description
                folderBrowserDialog.Description = "Choose the folder of output";

                // 显示对话框
                DialogResult result = folderBrowserDialog.ShowDialog();

                // 处理对话框返回的结果
                if (result == DialogResult.OK)
                {
                    outputroute = folderBrowserDialog.SelectedPath;
                    text_outroute.Text = outputroute;
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)//output name
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Save the output file";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                outputname = saveFileDialog.FileName;
                text_outname.Text = outputname;
            }

        }

        private async void button_next_Click(object sender, EventArgs e)
        {

            if (tabControl1.SelectedIndex == 6)
            {
                this.Dispose();
            }
            //Normal large
            if (radio_modenormal.Checked && radio_tobig.Checked)
            {
                button_prev.Enabled = true;
                if (tabControl1.SelectedIndex == 0)
                {
                    SwitchTab(1);
                }
                else if (tabControl1.SelectedIndex == 1)
                {
                    loadedcontainers = text_loaded.Text;
                    if (loadedcontainers == "")
                    {
                        MessageBox.Show("Please select directory of containers.", "Error");
                    }
                    else
                    {
                        SwitchTab(3);
                    }
                }
                else if (tabControl1.SelectedIndex == 3)
                {
                    outputname = text_outname.Text;
                    if (outputname == "")
                    {
                        MessageBox.Show("Please select name of output file.", "Error");
                    }
                    else
                    {
                        SwitchTab(4);
                    }
                }
                else if (tabControl1.SelectedIndex == 4)
                {
                    pwd = text_pwd.Text;
                    button_next.Enabled = false;
                    SwitchTab(5);
                    if (check_rerange.Checked)
                    {
                        GlobalVariables.rerange_decode = true;
                    }
                    if (check_all.Checked)
                    {
                        issub = true;
                        searchdepth = (int)numericUpDown1.Value;
                    }
                    try
                    {
                        ifok = await Task.Run(() =>
                        {
                            // 假设Encryptor方法接受一个回调函数
                            return WizardDecode.StegoExtractLarge(pwd, loadedcontainers, outputname, searchdepth,
                                (message) => AppendTextToRichTextBox(message),cts.Token);
                        });
                        progressBar1.Style = ProgressBarStyle.Blocks;
                        button_next.Enabled = true;
                    }
                    catch (Exception ex)
                    {
                        progressBar1.Style = ProgressBarStyle.Blocks;
                        richTextBox1.AppendText(ex.Message);
                    }
                }
                else if (tabControl1.SelectedIndex == 5 && ifok == true)
                {
                    richTextBox2.AppendText("Completed task extract one large secret file from: \n" + loadedcontainers + "\nTo New Route: \n" + outputname);

                    button_next.Text = "Finnish";
                    SwitchTab(6);
                }

            }

            //Encryptor
            if (radio_modeencrypt.Checked)
            {
                button_prev.Enabled = true;
                if (tabControl1.SelectedIndex == 0)
                {
                    label2.Text = "Select Directory of Encrypted Files";
                    SwitchTab(1);
                }
                else if (tabControl1.SelectedIndex == 1)
                {
                    loadedcontainers = text_loaded.Text;
                    if (loadedcontainers == "")
                    {
                        MessageBox.Show("Please select directory of Encrypted Files.", "Error");
                    }
                    else
                    {
                        SwitchTab(2);
                    }
                }
                else if (tabControl1.SelectedIndex == 2)
                {
                    outputroute = text_outroute.Text;
                    if (outputroute == "")
                    {
                        MessageBox.Show("Please select name of output route.", "Error");
                    }
                    else
                    {
                        SwitchTab(4);
                    }
                }
                else if (tabControl1.SelectedIndex == 4)
                {
                    pwd = text_pwd.Text;
                    button_next.Enabled = false;
                    SwitchTab(5);
                    try
                    {
                        ifok = await Task.Run(() =>
                        {
                            // 假设Encryptor方法接受一个回调函数
                            return WizardDecode.Decryptor(pwd, loadedcontainers, outputroute,
                                (message) => AppendTextToRichTextBox(message), cts.Token);
                        });
                        progressBar1.Style = ProgressBarStyle.Blocks;
                        button_next.Enabled = true;

                    }
                    catch (Exception ex)
                    {
                        progressBar1.Style = ProgressBarStyle.Blocks;
                        richTextBox1.AppendText(ex.Message);
                    }
                }
                else if (tabControl1.SelectedIndex == 5 && ifok == true)
                {
                    button_next.Text = "Finnish";
                    richTextBox2.AppendText("Only Decrypted files in route: \n" + loadedcontainers + "\nOutput to: \n" + outputroute);
                    SwitchTab(6);
                }

            }




        }

        private void radio_modeencrypt_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_modeencrypt.Checked)
            {
                radio_tobig.Enabled = false;
                radio_tomany.Enabled = false;
            }
            else if (radio_modenormal.Checked)
            {
                radio_tobig.Enabled = true;
                radio_tomany.Enabled = true;

            }
        }

        private void button_prev_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex > 0)
            {
                if (radio_modenormal.Checked)
                {
                    if (tabControl1.SelectedIndex == 4)
                    {
                        SwitchTab(3);
                    }
                    else if (tabControl1.SelectedIndex == 3)
                    {
                        SwitchTab(1);
                    }
                    else if (tabControl1.SelectedIndex == 1)
                    {
                        SwitchTab(0);
                    }
                }


            }

            if (tabControl1.SelectedIndex > 0)
            {
                if (radio_modeencrypt.Checked)
                {
                    if (tabControl1.SelectedIndex == 4)
                    {
                        SwitchTab(2);
                    }
                    else if (tabControl1.SelectedIndex == 2)
                    {
                        SwitchTab(1);
                    }
                    else if (tabControl1.SelectedIndex == 1)
                    {
                        SwitchTab(0);
                    }
                }


            }


            if (tabControl1.SelectedIndex == 0)
            {
                button_prev.Enabled = false;
            }

        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 5)
            {
                cts.Cancel();
                Thread.Sleep(100);
                if (Directory.Exists(outputroute))
                {
                    // 获取文件夹中所有文件的路径
                    string[] files = Directory.GetFiles(outputroute);

                    // 遍历所有文件并删除
                    foreach (string file in files)
                    {
                        try
                        {
                            File.Delete(file);
                        }
                        catch
                        {
                            //Do nothing
                        }
                    }
                }

                this.Dispose();
            }
            else
            {
                this.Dispose();
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            form2 = new Settings(form1); // 直接使用类级别的成员变量，不需要重新声明
            form2.Show();

        }
    }
}

