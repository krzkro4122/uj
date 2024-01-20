using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassNo3;
using System;

namespace ClassNo3Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            const int expectedX1 = -3;
            const int expectedX2 = 1;
            float[] output = ExerciseNo1.funkcjaKwadratowa(1, 2, -3);
            Assert.AreEqual(expectedX1, output[0]);
            Assert.AreEqual(expectedX2, output[1]);
        }
        
        [TestMethod]
        public void TestMethod2()
        {
            float[] output = ExerciseNo1.funkcjaKwadratowa(1, 2, -3);
            Assert.AreNotEqual(-99, output[0]);
            Assert.AreNotEqual(-99, output[1]);
        }

        [TestMethod]
        public void TestMethod3()
        {
            const int expectedX0 = -1;
            float[] output = ExerciseNo1.funkcjaKwadratowa(1, 2, 1);
            Assert.AreEqual(expectedX0, output[0]);
        }

        [TestMethod]
        public void TestMethod4()
        {
            float[] output = ExerciseNo1.funkcjaKwadratowa(1, 2, 1);
            Assert.AreNotEqual(-99, output[0]);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestMethod5()
        {
            ExerciseNo1.funkcjaKwadratowa(2, 1, 1);            
        }
    }
}
