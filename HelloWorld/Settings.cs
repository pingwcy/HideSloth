using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HideSloth
{
    public partial class Settings : Form
    {
        private MainForm form1;

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // 检查是否按下了Esc键
            if (keyData == Keys.Escape)
            {
                this.Dispose(); // 关闭并释放窗口
                return true;    // 返回true表示按键已处理
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        public Settings(MainForm form1)
        {
            InitializeComponent();
            this.form1 = form1;

            if (GlobalVariables.mode == "Normal")
            {
                Radio_Normal.Checked = true;
            }
            else if (GlobalVariables.mode == "Encryptor")
            {
                Radio_Encryptor.Checked = true;
            }

            if (GlobalVariables.Algor == "LSB")
            {
                Radio_LSB_PB.Checked = true;
            }
            if (GlobalVariables.Algor == "Linear")
            {
                Radio_Linear_PB.Checked = true;
            }

            if (GlobalVariables.enableencrypt == true)
            {
                Radio_enableenc.Checked = true;
            }
            if (GlobalVariables.disablencrypt == true)
            {
                Radio_disableenc.Checked = true;
            }

            Check_CustIter.Checked = GlobalVariables.CustIter;
            Check_CustHash.Checked = GlobalVariables.CustHash;
            Text_PBKDF2Iter.Text = GlobalVariables.iteration.ToString();
            ComboBox_Hash.SelectedItem = GlobalVariables.Hash;
            combo_entension.SelectedItem = GlobalVariables.outputformat;
            check_meta.Checked = GlobalVariables.copymeta;
            check_keepformat.Checked = GlobalVariables.keepformat;
            check_copymetaother.Checked = GlobalVariables.copyotherfilemeta;
        }

        private void Radio_LSB_PB_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Radio_Normal.Checked)
            {
                GlobalVariables.mode = "Normal";
                form1.Container_Button = true;
                form1.ContainerLabel_Nousage = true;
                form1.ContainerLabel2_Nousage = true;

            }
            else if (Radio_Encryptor.Checked)
            {
                GlobalVariables.mode = "Encryptor";
                //GlobalVariables.route_container = "";
                form1.Container_Button = false;
                form1.ContainerLabel_Nousage = false;
                form1.ContainerLabel2_Nousage = false;


            }

            if (Radio_LSB_PB.Checked)
            {
                GlobalVariables.Algor = "LSB";
            }
            if (Radio_Linear_PB.Checked)
            {
                GlobalVariables.Algor = "Linear";
            }


            if (Radio_enableenc.Checked)
            {
                GlobalVariables.enableencrypt = true;
                GlobalVariables.disablencrypt = false;
                form1.PasswordBOX = true;

            }
            if (Radio_disableenc.Checked)
            {
                GlobalVariables.disablencrypt = true;
                GlobalVariables.enableencrypt = false;
                form1.PasswordBOX = false;
                //Form1.Textbox_Password.Enabled = false;
            }
            GlobalVariables.outputformat = combo_entension.SelectedItem.ToString();

            GlobalVariables.iteration = Int32.Parse(Text_PBKDF2Iter.Text);
#pragma warning disable CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。
#pragma warning disable CS8601 // 引用类型赋值可能为 null。
            GlobalVariables.Hash = (string)ComboBox_Hash.SelectedItem;
#pragma warning restore CS8601 // 引用类型赋值可能为 null。
#pragma warning restore CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。
            GlobalVariables.copymeta = check_meta.Checked;
            GlobalVariables.copyotherfilemeta = check_copymetaother.Checked;
            GlobalVariables.keepformat = check_keepformat.Checked;
            form1.UpdateStatusStrip();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (Check_CustIter.Checked == true)
            {
                Text_PBKDF2Iter.Enabled = true;
                GlobalVariables.CustIter = true;
            }
            else
            {

                Text_PBKDF2Iter.Enabled = false;
                GlobalVariables.CustIter = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (Check_CustHash.Checked == true)
            {
                ComboBox_Hash.Enabled = true;
                GlobalVariables.CustHash = true;
            }
            else
            {

                ComboBox_Hash.Enabled = false;
                GlobalVariables.CustHash = false;

            }

        }

        private void Radio_Encryptor_CheckedChanged(object sender, EventArgs e)
        {
            Radio_Linear_PB.Enabled = false;
            Radio_LSB_PB.Enabled = false;
            Radio_disableenc.Enabled = false;
        }

        private void Radio_Normal_CheckedChanged(object sender, EventArgs e)
        {
            Radio_Linear_PB.Enabled = true;
            Radio_LSB_PB.Enabled = true;
            Radio_disableenc.Enabled = true;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void check_keepformat_CheckedChanged(object sender, EventArgs e)
        {
            if (check_keepformat.Checked)
            {
                combo_entension.Enabled = true;
            }
            else
            {
                combo_entension.Enabled = true;
            }
        }
    }


}
