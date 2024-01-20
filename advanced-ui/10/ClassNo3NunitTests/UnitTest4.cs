using NUnit.Framework;
using ClassNo3;
using System;

namespace ClassNo3Tests
{    
    public class UnitTest4
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase(123, "liczba jest parzysta")]
        [TestCase(124, "liczba nie jest parzysta")]
        [TestCase(1, "podana liczba nie jest trzycyfrowa")]
        [TestCase(-123, "podana liczba nie jest dodatnia")]
        public void TestMethod1(int input, string expectedOutput)
        {
            string output = ExerciseNo4.sumaCyfr(input);
            Assert.AreEqual(expectedOutput, output);
        }
    }
}
