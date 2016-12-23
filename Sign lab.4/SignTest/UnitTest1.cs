using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using CryptoLibrary;
using System.Text;

namespace SignTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void SignAreTrue()
        {
            CryptoLibrary.CypherRSA rsa = new CryptoLibrary.CypherRSA();
            
            string sourceDataString = "Test sign";
            MemoryStream input = new MemoryStream(Encoding.Default.GetBytes(sourceDataString));
            MemoryStream output = new MemoryStream();     
            rsa.Sign(input, output);

            Assert.IsTrue(rsa.Verify(output));
        }
    }
}
