using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using HideSloth.Crypto;

namespace HideSloth
{
    public partial class Form_RSAGen : Form
    {
        public Form_RSAGen()
        {
            InitializeComponent();
            combo_keysize.SelectedIndex = 0;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "Please choose a route to save keys";
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                text_keyroute.Text = (folderBrowserDialog.SelectedPath);
            }

        }
        static byte[] ExportPrivateKey(RSA rsa)
        {
            // 导出私钥为PKCS#8格式
            var privateKeyBytes = rsa.ExportPkcs8PrivateKey();
            // 将字节转换为Base64编码，以便于存储和传输
            return (privateKeyBytes);
        }

        static string ExportPublicKey(RSA rsa)
        {
            // 导出公钥为X.509格式
            var publicKeyBytes = rsa.ExportSubjectPublicKeyInfo();
            // 将字节转换为Base64编码，以便于存储和传输
            return Convert.ToBase64String(publicKeyBytes);
        }

        private void button_gen_Click(object sender, EventArgs e)
        {
            using (RSA rsa = RSA.Create())
            {
                // 设置密钥大小
                if (combo_keysize.SelectedIndex == 0)
                {
                    rsa.KeySize = 2048;
                }
                else if (combo_keysize.SelectedIndex == 1)
                {
                    rsa.KeySize = 4096;
                }

                // 导出RSA密钥对
                string publicKey = ExportPublicKey(rsa);
                byte[] privateKey_plain = ExportPrivateKey(rsa);

                // 将密钥写入文件
                File.WriteAllText(text_keyroute.Text + "/"+ "publicKey.pem", publicKey);
                GlobalVariables.waitencmaster = true;
                byte[] privateKey_encrypted = Aes_ChaCha_Encryptor.Encrypt(privateKey_plain, text_pwd.Text, out byte[] salt, out byte[] nonce, out byte[] tag);
                GlobalVariables.waitencmaster = false;
                string privateKey = (Convert.ToBase64String(BytesStringThings.CombineBytes(salt, nonce, tag, privateKey_encrypted)));

                File.WriteAllText(text_keyroute.Text + "/" + "privateKey.pem", privateKey);

                MessageBox.Show("Success to generate key pair!","Success!");
                this.Dispose();
            }

        }
    }
}
