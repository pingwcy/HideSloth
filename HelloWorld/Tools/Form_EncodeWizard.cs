using System.Data;
using static HideSloth.Tools.WizardEncode;
namespace HideSloth.Tools
{
    public partial class Form_EncodeWizard : Form
    {
        private Settings form2;
#pragma warning disable CS0649 // 从未对字段“Form_EncodeWizard.form1”赋值，字段将一直保持其默认值 null
        private MainForm form1;
#pragma warning restore CS0649 // 从未对字段“Form_EncodeWizard.form1”赋值，字段将一直保持其默认值 null
        private bool ifok = false;
        private bool checkedcapacity = false;
        private int containercount = 0;
        private string routesecretfiles, routeofoutput, pwd, largonesecret, containers;
#pragma warning disable CS0169 // 从不使用字段“Form_EncodeWizard.fileSizeInBytes”
        double fileSizeInBytes, capacity;
#pragma warning restore CS0169 // 从不使用字段“Form_EncodeWizard.fileSizeInBytes”
        private List<string> fileNamesList;
        List<int> AssgCapacityList = new List<int>();
        CancellationTokenSource cts = new CancellationTokenSource();
        public static bool copynonimage = false;
        public static bool keepstrcuture = false;
        public static bool searchdeep = false;
        public List<string> otherfile;
        public List<string> wholestrcuture;
        public int maxfloder;
        public List<string> ALLfilePaths = new List<string>();

        private double CalculateTotalSizeFromList(List<ImageInfo> imageList)
        {
            double totalSize = 0;
            foreach (var image in imageList)
            {
                containercount++;
                totalSize += Convert.ToInt32(image.Dimensions.Remove(image.Dimensions.Length - 3));
            }
            return totalSize;
        }

        private List<string> GetFileNamesFromImageList(List<ImageInfo> imageList)
        {
            List<string> fileNames = imageList.Select(image => image.FileName).ToList();
            return fileNames;
        }


        private void InitializeListView()
        {
            list_capacity.View = View.Details;
            list_capacity.Columns.Add("File Name", -2, HorizontalAlignment.Left);
            list_capacity.Columns.Add("Capacity", -2, HorizontalAlignment.Left);
        }
        /*
        private void LoadImagesToListView(string folderPath)
        {
            List<ImageInfo> imageList = WizardEncode.CheckCapacity(folderPath);
            foreach (var image in imageList)
            {
                ListViewItem item = new ListViewItem(image.FileName);
                item.SubItems.Add(image.Dimensions);
                list_capacity.Items.Add(item);
            }
        }
        */
        public void AppendTextToRichTextBox(string text)
        {
            // 检查是否需要跨线程调用
            if (richTextBox3.InvokeRequired)
            {
                richTextBox3.Invoke(new Action<string>(AppendTextToRichTextBox), text);
            }
            else
            {
                richTextBox3.AppendText(text + "\n");
            }
        }


        public void ShowMessageBox(string message, string caption)
        {
            // Check if we're on the UI thread
            if (this.InvokeRequired)
            {
                // We're not on the UI thread, so use Invoke to run this method on the UI thread
                this.Invoke(new Action<string, string>(ShowMessageBox), message, caption);
            }
            else
            {
                // We're on the UI thread, so it's safe to show the MessageBox
                MessageBox.Show(message, caption);
            }
        }

        public Form_EncodeWizard(MainForm mainForm)
        {
            form1 = mainForm; // 接收并存储对主窗体的引用

            InitializeComponent();
            InitializeListView();
            if (GlobalVariables.mode == "Normal")
            {
                Radio_normalw.Checked = true;
            }
            else if (GlobalVariables.mode == "Encryptor")
            {
                Radio_Encryptorw.Checked = true;
            }
            button_pre.Enabled = false;
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

        public async void button_next_Click(object sender, EventArgs e)
        {
            button_pre.Enabled = true;
            if (button_next.Text == "Finnish")
            {
                this.Dispose();
            }
            //Encryptor
            if (tabControl1.SelectedIndex == 0)
            {
                if (Radio_Encryptorw.Checked)
                {
                    SwitchTab(5);

                }
            }
            else if (tabControl1.SelectedIndex == 5)
            {
                if (Radio_Encryptorw.Checked)
                {
                    if (text_routemanysecrets.Text != "")
                    {
                        routesecretfiles = text_routemanysecrets.Text;
                        SwitchTab(8);
                    }
                    else
                    {
                        MessageBox.Show("Please Select Secret Files' Route", "Error");
                    }
                }

            }

            else if (tabControl1.SelectedIndex == 8)
            {
                if (Radio_Encryptorw.Checked)
                {
                    if (text_outputroute.Text != "")
                    {
                        routeofoutput = text_outputroute.Text;
                        SwitchTab(9);
                    }
                    else
                    {
                        MessageBox.Show("Please Select Output Route", "Error");

                    }
                }
            }
            else if (tabControl1.SelectedIndex == 9)
            {
                if (Radio_Encryptorw.Checked)
                {
                    pwd = text_pwd.Text;
                    box_summary.Clear();
                    box_summary.AppendText("Mode: Bulk File Encryptor\nDirectory of Secret Files: " + routesecretfiles + "\nRoute of Output Directory: " + routeofoutput + "\nPBKDF2 Iterations: " + GlobalVariables.iteration.ToString() + "\nPBKDF2 Hash: " + GlobalVariables.Hash);
                    SwitchTab(6);
                }
            }
            else if (tabControl1.SelectedIndex == 6)
            {
                if (Radio_Encryptorw.Checked)
                {
                    SwitchTab(4);
                    ifok = await Task.Run(() =>
                    {
                        // 假设Encryptor方法接受一个回调函数
                        return WizardEncode.Encryptor(pwd, routesecretfiles, routeofoutput,
                            (message) => AppendTextToRichTextBox(message));
                    });

                }
            }
            else if (tabControl1.SelectedIndex == 4)
            {
                if (Radio_Encryptorw.Checked && ifok)
                {
                    richTextBox1.AppendText("Only Encrypted files in route: \n" + routesecretfiles + "\nOutput to: \n" + routeofoutput);
                    SwitchTab(7);
                }
            }



            //Normal one to many
            if (tabControl1.SelectedIndex == 0)
            {
                if (Radio_normalw.Checked && Radio_onetomany.Checked)
                {
                    SwitchTab(1);

                }
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                if (Radio_normalw.Checked && Radio_onetomany.Checked)
                {
                    largonesecret = text_lagrone.Text;
                    if (largonesecret == "")
                    {
                        MessageBox.Show("Please select secret file.", "Error");
                    }
                    else
                    {
                        SwitchTab(2);
                    }
                }

            }

            else if (tabControl1.SelectedIndex == 2)
            {
                containers = text_containers.Text;
                if (containers == "")
                {
                    MessageBox.Show("Please select containers.", "Error");

                }
                else
                {
                    if (Radio_normalw.Checked && Radio_onetomany.Checked)
                    {
                        button5.Enabled = false;//disable advanced setting
                        SwitchTab(3);
                        button_next.Enabled = false;
                        button_pre.Enabled = false;

                        if (check_searchdeepcontainer.Checked)
                        {
                            searchdeep = true;
                            maxfloder = (int)numericUpDown1.Value;
                            //Now we need to know the folder structure
                            //MessageBox.Show("执行了深度搜索");
                            // 获取文件路径
                            GetFilePaths(containers, ALLfilePaths, maxfloder);

                        }
                        else if (check_searchdeepcontainer.Checked == false)
                        {
                            searchdeep = false;
                        }
                        var list = await Task.Run(() => CheckCapacity(containers, ALLfilePaths));
                        otherfile = await Task.Run(() => WizardEncode.otherfiles(containers, ALLfilePaths));
                        fileNamesList = list.Select(image => image.FileName).ToList();

                        foreach (var image in list)
                        {
                            if (checkedcapacity)
                            {
                                list_capacity.Items.Clear();
                                AssgCapacityList.Clear();
                                checkedcapacity = false;
                            }
                            else
                            {
                                ListViewItem item = new ListViewItem(image.FileName);
                                item.SubItems.Add(image.Dimensions);
                                list_capacity.Items.Add(item);
                            }
                        }
                        list_capacity.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                        list_capacity.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

                        capacity = CalculateTotalSizeFromList(list);



                        FileInfo fileInfo = new FileInfo(largonesecret);
                        long fileSizeInBytes = fileInfo.Length;
                        long sum = fileSizeInBytes + 44 * containercount;


                        foreach (var image in list)
                        {
                            double individualSize = Convert.ToDouble(image.Dimensions.Remove(image.Dimensions.Length - 3));
                            int calculatedValue = (int)Math.Ceiling(individualSize / (capacity) * fileSizeInBytes);
                            AssgCapacityList.Add(calculatedValue);
                        }

                        progressBar2.Style = ProgressBarStyle.Blocks;

                        if (fileSizeInBytes / 1024 > capacity)
                        {
                            MessageBox.Show("The secret file is: " + (fileSizeInBytes / 1024).ToString() + " larger than total capacity: " + capacity.ToString(), "Error");
                            button_next.Enabled = (false);
                            button_pre.Enabled = true;
                        }
                        else
                        {
                            MessageBox.Show("The secret file is: " + (fileSizeInBytes / 1024).ToString() + " less than total capacity: " + capacity.ToString(), "Success");
                            button_next.Enabled = true;
                            button_pre.Enabled = true;

                        }
                        checkedcapacity = true;
                    }
                }
            }
            else if (tabControl1.SelectedIndex == 3)
            {
                if (Radio_normalw.Checked && Radio_onetomany.Checked)
                {
                    SwitchTab(8);
                }
            }
            else if (tabControl1.SelectedIndex == 8)
            {
                if (Radio_normalw.Checked && Radio_onetomany.Checked)
                {
                    routeofoutput = text_outputroute.Text;
                    if (routeofoutput != "")
                    {
                        SwitchTab(9);
                    }
                    else
                    {
                        MessageBox.Show("Please Select output route", "Error");
                    }
                }
            }
            else if (tabControl1.SelectedIndex == 9)
            {
                if (Radio_normalw.Checked && Radio_onetomany.Checked)
                {
                    pwd = text_pwd.Text;
                    box_summary.AppendText("Mode: One Large file To List of Containers\nRoute of Secret Files: " + largonesecret + "\nRoute of Containers: " + containers + "\nRoute of Output Directory: " + routeofoutput + "\nPBKDF2 Iterations: " + GlobalVariables.iteration.ToString() + "\nPBKDF2 Hash: " + GlobalVariables.Hash);

                    SwitchTab(6);
                }
            }
            else if (tabControl1.SelectedIndex == 6)
            {
                if (Radio_normalw.Checked && Radio_onetomany.Checked)
                {
                    button_next.Enabled = false;
                    SwitchTab(4);
                    //Task here
                    if (check_copynonimage.Checked)
                    {
                        copynonimage = true;
                    }
                    else if (check_copynonimage.Checked == false)
                    {
                        copynonimage = false;

                    }
                    if (check_searchdeepcontainer.Checked && radio_outputkeepstructure.Checked)
                    {
                        keepstrcuture = true;
                    }
                    else if (radio_outputkeepstructure.Checked == false)
                    {
                        keepstrcuture = false;

                    }
                    try
                    {
                        ifok = await Task.Run(() =>
                        {
                            // 假设Encryptor方法接受一个回调函数
                            return WizardEncode.StegoLarge(pwd, AssgCapacityList, containers, largonesecret, routeofoutput, fileNamesList, otherfile, ALLfilePaths,
                                (message) => AppendTextToRichTextBox(message), cts.Token);
                        });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");

                    }
                    if (ifok == false)
                    {
                        MessageBox.Show("", "Error");
                    }
                    else
                    {
                        richTextBox3.AppendText("The large file has been embeded to images successfully.");
                        button_next.Enabled = true;
                    }
                }
            }
            else if (tabControl1.SelectedIndex == 4)
            {
                if (Radio_normalw.Checked && Radio_onetomany.Checked && ifok)
                {
                    richTextBox1.AppendText("Completed task to split and embed one large secret file: \n" + largonesecret + "\nTo containers: \n" + containers + "\nAnd output to:\n" + routeofoutput);
                    SwitchTab(7);
                }
            }






            if (tabControl1.SelectedIndex == 7)
            {
                button_next.Text = "Finnish";
                button_pre.Enabled = false;
            }
        }

        private void button_pre_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex > 0)
            {
                button_next.Enabled = true;
                //Encryptor
                if (tabControl1.SelectedIndex == 6)
                {
                    if (Radio_Encryptorw.Checked)
                    {
                        SwitchTab(9);

                    }
                }
                else if (tabControl1.SelectedIndex == 9)
                {
                    if (Radio_Encryptorw.Checked)
                    {
                        SwitchTab(8);
                    }

                }

                else if (tabControl1.SelectedIndex == 8)
                {
                    if (Radio_Encryptorw.Checked)
                    {
                        SwitchTab(5);
                    }
                }
                else if (tabControl1.SelectedIndex == 5)
                {
                    if (Radio_Encryptorw.Checked)
                    {
                        SwitchTab(0);
                    }
                }

                //Large file mode

                if (tabControl1.SelectedIndex == 9)
                {
                    if (Radio_normalw.Checked)
                    {
                        SwitchTab(8);

                    }
                }
                else if (tabControl1.SelectedIndex == 8)
                {
                    if (Radio_normalw.Checked)
                    {
                        SwitchTab(3);
                    }

                }

                else if (tabControl1.SelectedIndex == 3)
                {
                    if (Radio_normalw.Checked)
                    {
                        SwitchTab(2);
                    }
                }
                else if (tabControl1.SelectedIndex == 2)
                {
                    if (Radio_normalw.Checked)
                    {
                        SwitchTab(1);
                    }
                }
                else if (tabControl1.SelectedIndex == 1)
                {
                    if (Radio_normalw.Checked)
                    {
                        SwitchTab(0);
                    }
                }

            }



            if (tabControl1.SelectedIndex == 0)
            {
                button_pre.Enabled = false;
            }

        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button_pre.Enabled = tabControl1.SelectedIndex > 0;
            button_next.Text = (tabControl1.SelectedIndex == tabControl1.TabCount - 1) ? "完成" : "下一步";
        }

        private void Radio_Encryptorw_CheckedChanged(object sender, EventArgs e)
        {
            if (Radio_Encryptorw.Checked)
            {
                Radio_manytomany.Enabled = false;
                Radio_onetomany.Enabled = false;
            }
        }

        private void Radio_normalw_CheckedChanged(object sender, EventArgs e)
        {
            if (Radio_normalw.Checked)
            {
                Radio_manytomany.Enabled = true;
                Radio_onetomany.Enabled = true;
            }

        }

        private void form2_Closed(object sender, EventArgs e)
        {
            if (GlobalVariables.mode == "Normal")
            {
                Radio_normalw.Checked = true;
            }
            else
            {
                Radio_Encryptorw.Checked = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            form2 = new Settings(form1); // 直接使用类级别的成员变量，不需要重新声明
            form2.Closed += form2_Closed;

            form2.Show();


        }

        private void button3_Click(object sender, EventArgs e)
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
                    routesecretfiles = folderBrowserDialog.SelectedPath;
                    text_routemanysecrets.Text = routesecretfiles;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
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
                    routeofoutput = folderBrowserDialog.SelectedPath;
                    text_outputroute.Text = routeofoutput;
                }
            }

        }

        private void tabPage10_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            {
                OpenFileDialog saveFileDialog = new OpenFileDialog();
                saveFileDialog.Title = "Save the Single large secret file";
                saveFileDialog.ShowDialog();

                if (saveFileDialog.FileName != "")
                {
                    largonesecret = saveFileDialog.FileName;
                    text_lagrone.Text = largonesecret;
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
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
                    containers = folderBrowserDialog.SelectedPath;
                    text_containers.Text = containers;
                }
            }

        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 4)
            {
                cts.Cancel();
                Thread.Sleep(100);
                if (Directory.Exists(routeofoutput))
                {
                    // 获取文件夹中所有文件的路径
                    string[] files = Directory.GetFiles(routeofoutput);

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

        private void check_deepcontainer_CheckedChanged(object sender, EventArgs e)
        {
            if (check_searchdeepcontainer.Checked)
            {
                numericUpDown1.Enabled = true;
                radio_outputkeepstructure.Enabled = true;
            }
            else if (check_searchdeepcontainer.Checked == false)
            {
                numericUpDown1.Enabled = false;
                radio_outputkeepstructure.Enabled = false;

            }
        }
    }
}
