using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Crypto
{
    public partial class main : Form
    {
        string strFin, s, encrypt1;
        public main()
        {
            InitializeComponent();
            textBox10.BackColor = Color.Red;
            textBox11.BackColor = Color.Red;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string encrypt;
            string source = Convert.ToString(textBox1.Text);
            Library.DESLib crypt = new Library.DESLib();
            Stream sourceData = new MemoryStream(Encoding.Default.GetBytes(source));
            Stream encryptedData = new MemoryStream();
            crypt.EncryptData(sourceData, encryptedData);
            Stream decryptedData = new MemoryStream();
            crypt.DecryptData(encryptedData, decryptedData);
            byte[] bufRead2 = new byte[encryptedData.Length];
            encryptedData.Position = 0;
            encryptedData.Read(bufRead2, 0, Convert.ToInt32(encryptedData.Length));
            encrypt = Convert.ToBase64String(bufRead2);
            textBox2.Text = encrypt;
            byte[] bufRead1 = new byte[encryptedData.Length];
            decryptedData.Position = 0;
            decryptedData.Read(bufRead1, 0, Convert.ToInt32(decryptedData.Length));
            string decrypt = Encoding.Default.GetString(bufRead1);
            textBox3.Text = decrypt;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            RSA_Lib.RSA crypt = new RSA_Lib.RSA();
            string encrypt;
            string source = Convert.ToString(textBox4.Text);

            Stream sourceData = new MemoryStream(Encoding.Default.GetBytes(source));
            Stream encryptedData = new MemoryStream();
            string s = crypt.RSALib(sourceData, encryptedData);
            textBox5.Text = s;
            byte[] bufRead2 = new byte[encryptedData.Length];
            encryptedData.Position = 0;
            encryptedData.Read(bufRead2, 0, Convert.ToInt32(encryptedData.Length));
            encrypt = System.Text.Encoding.Default.GetString(bufRead2);
            textBox6.Text = encrypt;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Sha2.Sha2 sha = new Sha2.Sha2();
            string sourceDataString = textBox7.Text;
            string strFin1;
            MemoryStream sourceDataStream = new MemoryStream(Encoding.Default.GetBytes(sourceDataString));
            MemoryStream HashStream = new MemoryStream();
            sha.HashFile(sourceDataStream, HashStream);
            byte[] a = new byte[32];
            HashStream.Position = 0;
            HashStream.Read(a, 0, a.Length);
            strFin1 = Sha2.Util.ArrayToString(a);
            textBox8.Text = strFin1;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Sha2.Sha2 sha = new Sha2.Sha2();
            string sourceDataString = textBox9.Text;
            MemoryStream sourceDataStream = new MemoryStream(Encoding.Default.GetBytes(sourceDataString));
            MemoryStream HashStream = new MemoryStream();
            sha.HashFile(sourceDataStream, HashStream);
            byte[] a = new byte[32];
            HashStream.Position = 0;
            HashStream.Read(a, 0, a.Length);
            strFin = Sha2.Util.ArrayToString(a);
            RSA_Lib.RSA crypt = new RSA_Lib.RSA();
            Stream sourceData = new MemoryStream(Encoding.Default.GetBytes(strFin));
            Stream encryptedData = new MemoryStream();
            s = crypt.RSALib(sourceData, encryptedData);
            byte[] bufRead2 = new byte[encryptedData.Length];
            encryptedData.Position = 0;
            encryptedData.Read(bufRead2, 0, Convert.ToInt32(encryptedData.Length));
            encrypt1 = System.Text.Encoding.Default.GetString(bufRead2);
            textBox10.Text = "Сообщение подписано";
            textBox10.BackColor = Color.Green;

        
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (strFin == encrypt1)
            {
                textBox11.Text = "Подпись верна";
                textBox11.BackColor = Color.Green;
            }
            else
            {
                textBox11.Text = "Подпись неверна";
              
            }

        }
    }
}
