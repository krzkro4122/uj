using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassNo3;
using System;

namespace ClassNo3Tests
{
    [TestClass]
    public class UnitTest5
    {
        [TestMethod]
        [DataTestMethod]
        [DataRow(new[] { 1, 2, 3, 4, 5 }, 0, 1, 1, new[] { 2, 1, 3, 4, 5 })]
        [DataRow(new[] { 1, 2, 3, 4, 5 }, 0, 1, 0, new[] { 1, 2, 3, 4, 5 })]        
        public void TestMethod1(int[] input, int start, int stop, int mode, int[] expectedOutput)
        {
            int[] output = ExerciseNo5.zamienElementy(input, start, stop, mode);
            CollectionAssert.AreEqual(expectedOutput, output);
        }

        [TestMethod]
        [DataTestMethod]        
        [DataRow(new[] { 1, 2, 3, 4, 5 }, 0, 10, 0, new[] { 1, 2, 3, 4, 5 })]
        [ExpectedException(typeof(ArgumentException))]
        public void TestMethod2(int[] input, int start, int stop, int mode, int expectedOutput)
        {
            ExerciseNo5.zamienElementy(input, start, stop, mode);
        }
    }
}
