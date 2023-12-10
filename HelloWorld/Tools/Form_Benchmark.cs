using HideSloth.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HideSloth
{
    public partial class Form_Benchmark : Form
    {
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

        public Form_Benchmark()
        {
            InitializeComponent();
            Combo_Alg.SelectedItem = "PNG/BMP: Linear";
            Combo_buff.SelectedItem = "100KB";
            Combo_Cycle.SelectedItem = "1";
            combo_encalg.SelectedItem = "AES256-GCM";
            combo_encbuff.SelectedItem = "100 KB";
            combo_enccycle.SelectedItem = "1";
            combo_kdf.SelectedItem = "PBKDF2";
            combo_iter.SelectedItem = "100000";
            combo_sha.SelectedItem = "SHA256";
            listBox1.View = View.Details;
            listBox1.Columns.Add("Algorithm", -2, HorizontalAlignment.Left); // 列标题和宽度
            listBox1.Columns.Add("Average Time", -2, HorizontalAlignment.Left); // 列标题和宽度
            listBox1.Columns.Add("Max Time", -2, HorizontalAlignment.Left); // 列标题和宽度
            listBox1.Columns.Add("Min Time", -2, HorizontalAlignment.Left); // 列标题和宽度

            listView1.View = View.Details;
            listView1.Columns.Add("Algorithm", -2, HorizontalAlignment.Left); // 列标题和宽度
            listView1.Columns.Add("Average Speed", -2, HorizontalAlignment.Left); // 列标题和宽度
            listView1.Columns.Add("Max Speed", -2, HorizontalAlignment.Left); // 列标题和宽度
            listView1.Columns.Add("Min Speed", -2, HorizontalAlignment.Left); // 列标题和宽度

            listView2.View = View.Details;
            listView2.Columns.Add("Algorithm", -2, HorizontalAlignment.Left);
            listView2.Columns.Add("Time", -2, HorizontalAlignment.Left); // 列标题和宽度

        }

        private async void Button_Teststart_Click(object sender, EventArgs e)
        {
            // 设置 ListView 控件的属性以显示列和行
            progressBar1.Style = ProgressBarStyle.Marquee;
            // 添加数据项和子项
            int size, count;
            string Alg = Combo_Alg.SelectedItem.ToString();
            List<List<double>> time = new List<List<double>>();

            count = Int32.Parse(Combo_Cycle.SelectedItem.ToString());

            switch (Combo_buff.SelectedItem.ToString())
            {
                case "100KB":
                    size = 100 * 1024;
                    break;
                case "500KB":
                    size = 500 * 1024;
                    break;
                case "1MB":
                    size = 1024 * 1024;
                    break;
                case "5MB":
                    size = 5 * 1024 * 1024;
                    break;
                case "10MB":
                    size = 10 * 1024 * 1024;
                    break;
                case "50MB":
                    size = 50 * 1024 * 1024;
                    break;
                case "100MB":
                    size = 100 * 1024 * 1024;
                    break;

                default:
                    throw new ArgumentException("Invalid Input");
            }

            time = await Task.Run(() => Benchmark.AlgBench(Alg, size, count));
            //MessageBox.Show(Alg);
            List<double> resultlist = new List<double>();
            if (Alg == "PNG/BMP: LSB")
            {
                resultlist = time[0];
                ListViewItem item1 = new ListViewItem("LSB");
                item1.SubItems.Add(resultlist.Average().ToString() + " s");
                item1.SubItems.Add(resultlist.Max().ToString() + " s");
                item1.SubItems.Add(resultlist.Min().ToString() + " s");

                listBox1.Items.Add(item1);

            }
            if (Alg == "PNG/BMP: Linear")
            {
                resultlist = time[0];
                ListViewItem item2 = new ListViewItem("Linear");
                item2.SubItems.Add(resultlist.Average().ToString() + " s");
                item2.SubItems.Add(resultlist.Max().ToString() + " s");
                item2.SubItems.Add(resultlist.Min().ToString() + " s");

                listBox1.Items.Add(item2);

            }
            if (Alg == "PNG/BMP: ALL")
            {
                resultlist = time[0];
                ListViewItem item1 = new ListViewItem("LSB");
                item1.SubItems.Add(resultlist.Average().ToString() + " s");
                item1.SubItems.Add(resultlist.Max().ToString() + " s");
                item1.SubItems.Add(resultlist.Min().ToString() + " s");

                listBox1.Items.Add(item1);


                resultlist = time[1];
                ListViewItem item2 = new ListViewItem("Linear");
                item2.SubItems.Add(resultlist.Average().ToString() + " s");
                item2.SubItems.Add(resultlist.Max().ToString() + " s");
                item2.SubItems.Add(resultlist.Min().ToString() + " s");

                listBox1.Items.Add(item2);


            }
            // 确保列能够填充 ListView 控件的宽度
            listBox1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listBox1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            progressBar1.Style = ProgressBarStyle.Continuous;
        }

        private void Button_testcancecl_Click(object sender, EventArgs e)
        {
            //this.Dispose();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            progressBar1.Style = ProgressBarStyle.Marquee;
            // 添加数据项和子项
            int size, count;
            string Alg = combo_encalg.SelectedItem.ToString();
            List<List<double>> time = new List<List<double>>();

            count = Int32.Parse(combo_enccycle.SelectedItem.ToString());

            switch (combo_encbuff.SelectedItem.ToString())
            {
                case "100 KB":
                    size = 100 * 1024;
                    break;
                case "500 KB":
                    size = 500 * 1024;
                    break;
                case "1 MB":
                    size = 1024 * 1024;
                    break;
                case "5 MB":
                    size = 5 * 1024 * 1024;
                    break;
                case "50 MB":
                    size = 50 * 1024 * 1024;
                    break;
                case "100 MB":
                    size = 100 * 1024 * 1024;
                    break;
                case "500 MB":
                    size = 500 * 1024 * 1024;
                    break;
                case "1 GB":
                    size = 1024 * 1024 * 1024;
                    break;

                default:
                    throw new ArgumentException("Invalid Input");
            }

            time = await Task.Run(() => Benchmark.EncAlgBench(Alg, size, count));
            //MessageBox.Show(Alg);
            List<double> resultlist = new List<double>();
            if (Alg == "AES256-GCM")
            {
                resultlist = time[0];
                ListViewItem item1 = new ListViewItem("AES256-GCM-Enc");
                item1.SubItems.Add((size / resultlist.Average() / 1024 / 1024 / 1024).ToString() + " GB/s");
                item1.SubItems.Add((size / resultlist.Min() / 1024 / 1024 / 1024).ToString() + " GB/s");
                item1.SubItems.Add((size / resultlist.Max() / 1024 / 1024 / 1024).ToString().ToString() + " GB/s");

                listView1.Items.Add(item1);

                resultlist = time[1];
                ListViewItem item2 = new ListViewItem("AES256-GCM-Dec");
                item2.SubItems.Add((size / resultlist.Average() / 1024 / 1024 / 1024).ToString() + " GB/s");
                item2.SubItems.Add((size / resultlist.Min() / 1024 / 1024 / 1024).ToString() + " GB/s");
                item2.SubItems.Add((size / resultlist.Max() / 1024 / 1024 / 1024).ToString().ToString() + " GB/s");

                listView1.Items.Add(item2);

            }
            else if (Alg == "ChaCha20-Poly1305")
            {
                resultlist = time[0];
                ListViewItem item1 = new ListViewItem("ChaCha20-Poly1305-Enc");
                item1.SubItems.Add((size / resultlist.Average() / 1024 / 1024 / 1024).ToString() + " GB/s");
                item1.SubItems.Add((size / resultlist.Min() / 1024 / 1024 / 1024).ToString() + " GB/s");
                item1.SubItems.Add((size / resultlist.Max() / 1024 / 1024 / 1024).ToString().ToString() + " GB/s");

                listView1.Items.Add(item1);

                resultlist = time[1];
                ListViewItem item2 = new ListViewItem("ChaCha20-Poly1305-Dec");
                item2.SubItems.Add((size / resultlist.Average() / 1024 / 1024 / 1024).ToString() + " GB/s");
                item2.SubItems.Add((size / resultlist.Min() / 1024 / 1024 / 1024).ToString() + " GB/s");
                item2.SubItems.Add((size / resultlist.Max() / 1024 / 1024 / 1024).ToString().ToString() + " GB/s");

                listView1.Items.Add(item2);

            }
            else if (Alg == "All")
            {
                resultlist = time[0];
                ListViewItem item1 = new ListViewItem("AES256-GCM-Enc");
                item1.SubItems.Add((size / resultlist.Average() / 1024 / 1024 / 1024).ToString() + " GB/s");
                item1.SubItems.Add((size / resultlist.Min() / 1024 / 1024 / 1024).ToString() + " GB/s");
                item1.SubItems.Add((size / resultlist.Max() / 1024 / 1024 / 1024).ToString().ToString() + " GB/s");

                listView1.Items.Add(item1);

                resultlist = time[1];
                ListViewItem item2 = new ListViewItem("AES256-GCM-Dec");
                item2.SubItems.Add((size / resultlist.Average() / 1024 / 1024 / 1024).ToString() + " GB/s");
                item2.SubItems.Add((size / resultlist.Min() / 1024 / 1024 / 1024).ToString() + " GB/s");
                item2.SubItems.Add((size / resultlist.Max() / 1024 / 1024 / 1024).ToString().ToString() + " GB/s");

                listView1.Items.Add(item2);

                resultlist = time[2];
                ListViewItem item3 = new ListViewItem("ChaCha20-Poly1305-Enc");
                item3.SubItems.Add((size / resultlist.Average() / 1024 / 1024 / 1024).ToString() + " GB/s");
                item3.SubItems.Add((size / resultlist.Min() / 1024 / 1024 / 1024).ToString() + " GB/s");
                item3.SubItems.Add((size / resultlist.Max() / 1024 / 1024 / 1024).ToString().ToString() + " GB/s");

                listView1.Items.Add(item3);

                resultlist = time[3];
                ListViewItem item4 = new ListViewItem("ChaCha20-Poly1305-Dec");
                item4.SubItems.Add((size / resultlist.Average() / 1024 / 1024 / 1024).ToString() + " GB/s");
                item4.SubItems.Add((size / resultlist.Min() / 1024 / 1024 / 1024).ToString() + " GB/s");
                item4.SubItems.Add((size / resultlist.Max() / 1024 / 1024 / 1024).ToString().ToString() + " GB/s");

                listView1.Items.Add(item4);

            }

            // 确保列能够填充 ListView 控件的宽度
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            progressBar1.Style = ProgressBarStyle.Continuous;
        }

        private void button_kdf_Click(object sender, EventArgs e)
        {
            if (combo_kdf.SelectedItem.ToString() == "PBKDF2")
            {
                List < List<double> > time = Benchmark.KDFBench("PBKDF2", Int32.Parse(combo_iter.SelectedItem.ToString()),combo_sha.SelectedItem.ToString());
                List<double>  resultlist = time[0];
                ListViewItem item1 = new ListViewItem("PBKDF2");
                item1.SubItems.Add(( resultlist.Average() *1000 ).ToString() + " ms");
                listView2.Items.Add(item1);
            }
            listBox1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listBox1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            progressBar1.Style = ProgressBarStyle.Continuous;

        }
    }
}

