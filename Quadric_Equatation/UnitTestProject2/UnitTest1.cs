using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quadric_Equatation;
namespace UnitTestProject2
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestDiscrimante()
        {
            Random random = new Random();
            var qe = new QuadricEquatation(random.NextDouble(),random.NextDouble(),random.NextDouble());
            var qr2 = qe.CalculateResults();

            Assert.AreEqual(random.NextDouble(), qr2.D);
        }
    }
}
