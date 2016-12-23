using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Text;


namespace ShaTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ConstAndHashAreEqual()
        {
            Sha2.Sha2 sha = new Sha2.Sha2();
            string sourceDataString = "АБВГ";
            string strFin, consta = "72d2a70d03005439de209bbe9ffc050fafd891082e9f3150f05a61054d25990f";
            MemoryStream sourceDataStream = new MemoryStream(Encoding.Default.GetBytes(sourceDataString));
            MemoryStream HashStream = new MemoryStream();
            sha.HashFile(sourceDataStream, HashStream);
            byte[] a = new byte[32];
            HashStream.Position = 0;
            HashStream.Read(a, 0, a.Length);
            strFin = Sha2.Util.ArrayToString(a);
            Assert.AreEqual(consta, strFin);

        }
    }
}
