using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text;

namespace TestDes
{
    [TestClass]
    public class TestDes
    {

        [TestMethod]
        public void SourseAndFinalAreEqual()
        {
            string source = "АБВГ";
            string encrypt;
            DesLibrary.DESLib crypt = new DesLibrary.DESLib();
            Stream sourceData = new MemoryStream(Encoding.Default.GetBytes(source));
            Stream encryptedData = new MemoryStream();
            crypt.EncryptData(sourceData, encryptedData);
            Stream decryptedData = new MemoryStream();
            crypt.DecryptData(encryptedData, decryptedData);

            byte[] bufRead1 = new byte[sourceData.Length];
            sourceData.Read(bufRead1, 0, Convert.ToInt32(sourceData.Length));
            byte[] bufRead2 = new byte[sourceData.Length];
            sourceData.Read(bufRead2, 0, Convert.ToInt32(sourceData.Length));
            source = Encoding.Default.GetString(bufRead1);
            encrypt = Encoding.Default.GetString(bufRead2);

            Assert.AreEqual(source, encrypt);

        }


    }

}