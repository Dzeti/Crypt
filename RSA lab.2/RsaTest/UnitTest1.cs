using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Numerics;
using System.Collections.Generic;
using System.Text;

namespace RsaTest
{
    [TestClass]
    public class UnitTest1
    {
     

        [TestMethod]
        public void ConstantAndFinalEqual()
        {
            string s = "ТЕКСТ";
            RSA_Lib.RSA rsa = new RSA_Lib.RSA();
            MemoryStream sourceDataStream = new MemoryStream(Encoding.Default.GetBytes(s));
            MemoryStream encryptedDataStream = new MemoryStream();
            MemoryStream decryptedDataStream = new MemoryStream();
            rsa.RSALib(sourceDataStream, decryptedDataStream);
            byte[] a = new byte[decryptedDataStream.Length];
            decryptedDataStream.Position = 0;
            decryptedDataStream.Read(a, 0, a.Length);
            string line = System.Text.Encoding.Default.GetString(a);
            Assert.AreEqual(s, line);
        }

        [TestMethod]
        public void StartAndFinalEqual()
        {
            string s = "ТЕКСТ";
            RSA_Lib.RSA rsa = new RSA_Lib.RSA();
            MemoryStream sourceDataStream = new MemoryStream(Encoding.Default.GetBytes(s));
            MemoryStream encryptedDataStream = new MemoryStream();
            MemoryStream decryptedDataStream = new MemoryStream();
            rsa.RSALib(sourceDataStream,decryptedDataStream);
            byte[] a = new byte[5];
            decryptedDataStream.Position = 0;
            decryptedDataStream.Read(a, 0, a.Length);
            string line = System.Text.Encoding.Default.GetString(a);
            Assert.AreEqual(s, line);
        }
    }
}
