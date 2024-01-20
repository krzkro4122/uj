using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassNo3;
using System;

namespace ClassNo3Tests
{
    [TestClass]
    public class UnitTest3
    {
        [TestMethod]
        [DataTestMethod]
        [DataRow(1, 1)]
        [DataRow(2, 8)]
        [DataRow(3, 27)]
        public void TestMethod1(int input, int expectedOutput)
        {
            int output = ExerciseNo3.objetoscSzescianu(input);
            Assert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        [ExpectedException(typeof(ArgumentException))]
        public void TestMethod2(int input)
        {
            ExerciseNo3.objetoscSzescianu(input);
        }
    }
}
