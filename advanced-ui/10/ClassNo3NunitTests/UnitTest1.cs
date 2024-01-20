using NUnit.Framework;
using ClassNo3;
using System;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }


        [Test]
        [TestCase(1, 2, -3, -3, 1)]
        public void Test1(int a, int b, int c, int expectedX1, int expectedX2)
        {
            float[] output = ExerciseNo1.funkcjaKwadratowa(a, b, c);
            Assert.AreEqual(expectedX1, output[0]);
            Assert.AreEqual(expectedX2, output[1]);
        }

        [Test]
        [TestCase(1, 2, -3, -99, -99)]
        public void Test2(int a, int b, int c, int expectedX1, int expectedX2)
        {
            float[] output = ExerciseNo1.funkcjaKwadratowa(a, b, c);
            Assert.AreNotEqual(expectedX1, output[0]);
            Assert.AreNotEqual(expectedX2, output[1]);
        }

        [Test]
        [TestCase(1, 2, 1, -1)]
        public void Test3(int a, int b, int c, int expectedX0)
        {
            float[] output = ExerciseNo1.funkcjaKwadratowa(a, b, c);
            Assert.AreEqual(expectedX0, output[0]);
        }

        [Test]
        [TestCase(1, 2, 1, -99)]
        public void Test4(int a, int b, int c, int expectedX0)
        {
            float[] output = ExerciseNo1.funkcjaKwadratowa(a, b, c);
            Assert.AreNotEqual(expectedX0, output[0]);
        }

        [Test]        
        public void Test5()
        {
            Assert.Throws<ArgumentException>(() => ExerciseNo1.funkcjaKwadratowa(2, 1, 1));            
        }
    }
}