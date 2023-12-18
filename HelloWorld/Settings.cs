using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HideSloth
{
    public partial class Settings : Form
    {
        public event EventHandler<SettingUpdateUIEventArgs> SettingUpdateUI;
        protected virtual void SubmitSettingsChangedUI(SettingUpdateUIEventArgs e)
        {
            SettingUpdateUI?.Invoke(this, e);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // 检查是否按下了Esc键
            if (keyData == Keys.Escape)
            {
                this.Close(); // 关闭并释放窗口
                return true;    // 返回true表示按键已处理
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        public Settings()
        {
            InitializeComponent();
            combo_imgalg.DataSource = GlobalVariables.listofsupportimagealg;
            if (GlobalVariables.Mode == "Normal")
            {
                Radio_Normal.Checked = true;
            }
            else if (GlobalVariables.Mode == "Encryptor")
            {
                Radio_Encryptor.Checked = true;
            }

            foreach (string i in GlobalVariables.listofsupportimagealg)
            {
                if (GlobalVariables.Algor == i)
                {
                    combo_imgalg.SelectedItem = i;
                }
            }

            if (GlobalVariables.Enableencrypt == true)
            {
                Radio_enableenc.Checked = true;
            }
            if (GlobalVariables.Disablencrypt == true)
            {
                Radio_disableenc.Checked = true;
            }

            Check_CustIter.Checked = GlobalVariables.CustIter;
            Check_CustHash.Checked = GlobalVariables.CustHash;
            Text_PBKDF2Iter.Text = GlobalVariables.Iteration.ToString();
            ComboBox_Hash.SelectedItem = GlobalVariables.Hash;
            combo_entension.SelectedItem = GlobalVariables.Outputformat;
            check_meta.Checked = GlobalVariables.Copymeta;
            check_keepformat.Checked = GlobalVariables.Keepformat;
            check_copymetaother.Checked = GlobalVariables.Copyotherfilemeta;
            numericUpDown1.Value = GlobalVariables.Smallstandard;
            if (GlobalVariables.Encalg == "ChaCha")
            {
                combo_encalg.SelectedItem = "ChaCha20-Poly1305";
            }
            else if (GlobalVariables.Encalg == "AES")
            {
                combo_encalg.SelectedItem = "AES256-GCM";
            }
            check_errorignore.Checked = GlobalVariables.Ignoreextracterror;
        }

        private void Radio_LSB_PB_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Radio_Normal.Checked)
            {
                GlobalVariables.Mode = "Normal";
                //form1.Container_Button = true;
                //form1.ContainerLabel_Nousage = true;
                //form1.ContainerLabel2_Nousage = true;
                //form1.Check_Mult = true;


            }
            else if (Radio_Encryptor.Checked)
            {
                GlobalVariables.Mode = "Encryptor";
                //GlobalVariables.route_container = "";
                //form1.Container_Button = false;
               // form1.ContainerLabel_Nousage = false;
                //form1.ContainerLabel2_Nousage = false;
                //form1.Check_Mult = false;

            }

            GlobalVariables.Algor = combo_imgalg.SelectedItem.ToString();

            if (Radio_enableenc.Checked)
            {
                GlobalVariables.Enableencrypt = true;
                GlobalVariables.Disablencrypt = false;
                //form1.PasswordBOX = true;

            }
            if (Radio_disableenc.Checked)
            {
                GlobalVariables.Disablencrypt = true;
                GlobalVariables.Enableencrypt = false;
                //form1.PasswordBOX = false;
                //Form1.Textbox_Password.Enabled = false;
            }
            GlobalVariables.Outputformat = combo_entension.SelectedItem.ToString();

            GlobalVariables.Iteration = Int32.Parse(Text_PBKDF2Iter.Text);
#pragma warning disable CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。
#pragma warning disable CS8601 // 引用类型赋值可能为 null。
            GlobalVariables.Hash = (string)ComboBox_Hash.SelectedItem;
#pragma warning restore CS8601 // 引用类型赋值可能为 null。
#pragma warning restore CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。
            GlobalVariables.Copymeta = check_meta.Checked;
            GlobalVariables.Copyotherfilemeta = check_copymetaother.Checked;
            GlobalVariables.Keepformat = check_keepformat.Checked;
            GlobalVariables.Smallstandard = (int)numericUpDown1.Value;
            if (combo_encalg.SelectedItem.ToString() == "ChaCha20-Poly1305")
            {
                GlobalVariables.Encalg = "ChaCha";
            }
            else if (combo_encalg.SelectedItem.ToString() == "AES256-GCM")
            {
                GlobalVariables.Encalg = "AES";
            }
            GlobalVariables.Ignoreextracterror = check_errorignore.Checked;
            //form1.UpdateStatusStrip();
            SubmitSettingsChangedUI(new SettingUpdateUIEventArgs(Radio_enableenc.Checked, Radio_Normal.Checked));
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
            combo_imgalg.Enabled = false;
            Radio_disableenc.Enabled = false;
            Radio_enableenc.Checked = true;
        }

        private void Radio_Normal_CheckedChanged(object sender, EventArgs e)
        {
            combo_imgalg.Enabled = true;
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

        private void Radio_disableenc_CheckedChanged(object sender, EventArgs e)
        {
            combo_encalg.Enabled = false;
            Check_CustIter.Enabled = false;
            Check_CustHash.Enabled = false;
        }

        private void Radio_enableenc_CheckedChanged(object sender, EventArgs e)
        {
            combo_encalg.Enabled = true;
            Check_CustIter.Enabled = true;
            Check_CustHash.Enabled = true;

        }
    }
    public class SettingUpdateUIEventArgs : EventArgs
    {
        public bool Isenc { get; set; }
        public bool Modechange { get; set; }

        public SettingUpdateUIEventArgs(bool isenc, bool modechange)
        {
            Isenc = isenc;
            Modechange = modechange;
        }
    }

}
