using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassNo3;
using System;

namespace ClassNo3Tests
{
    [TestClass]
    public class UnitTest4
    {
        [TestMethod]
        [DataTestMethod]
        [DataRow(123, "liczba jest parzysta")]
        [DataRow(124, "liczba nie jest parzysta")]
        [DataRow(1, "podana liczba nie jest trzycyfrowa")]
        [DataRow(-123, "podana liczba nie jest dodatnia")]
        public void TestMethod1(int input, string expectedOutput)
        {
            string output = ExerciseNo4.sumaCyfr(input);
            Assert.AreEqual(expectedOutput, output);
        }
    }
}
