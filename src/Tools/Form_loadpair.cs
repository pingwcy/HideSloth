using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HideSloth.Tools
{
    public partial class Form_loadpair : Form
    {
        public Form_loadpair()
        {
            InitializeComponent();
            combo_size.SelectedIndex = 0;
        }

        private void button_pri_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Choose Private Key";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    text_pri.Text = openFileDialog.FileName;
                }
            }
            FileInfo fileInfo = new FileInfo(text_pri.Text);
            if (fileInfo.Length > 2400 ) { combo_size.SelectedIndex = 1; }

        }

        private void button_pub_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Choose Public Key";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    text_pub.Text = openFileDialog.FileName;
                }
            }
            FileInfo fileInfo = new FileInfo(text_pub.Text);
            if (fileInfo.Length > 500) { combo_size.SelectedIndex = 1; }


        }

        private void button_load_Click(object sender, EventArgs e)
        {
            GlobalVariables.rsasize = Convert.ToInt32(combo_size.SelectedItem);
            GlobalVariables.pubkey = File.ReadAllText(text_pub.Text);
            if (!check_onlypub.Checked)
            {
                GlobalVariables.privatekeyenced = File.ReadAllText(text_pri.Text);
            }
            MessageBox.Show("Suceess to Load Key(s)", "Success");
            this.Dispose();
            //MessageBox.Show(GlobalVariables.pubkey);
            //MessageBox.Show(GlobalVariables.privatekeyenced);

        }

        private void check_onlypub_CheckedChanged(object sender, EventArgs e)
        {
            if (check_onlypub.Checked)
            {
                text_pri.Enabled = false;
                button_pri.Enabled = false;
            }
            else
            {
                text_pri.Enabled = true;
                button_pri.Enabled = true;
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
