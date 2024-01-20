using NUnit.Framework;
using ClassNo3;
using System;

namespace ClassNo3Tests
{    
    public class UnitTest3
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase(1, 1)]
        [TestCase(2, 8)]
        [TestCase(3, 27)]
        public void TestMethod1(int input, int expectedOutput)
        {
            int output = ExerciseNo3.objetoscSzescianu(input);
            Assert.AreEqual(expectedOutput, output);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void TestMethod2(int input)
        {
            Assert.Throws<ArgumentException>(() => ExerciseNo3.objetoscSzescianu(input));                        
        }
    }
}
