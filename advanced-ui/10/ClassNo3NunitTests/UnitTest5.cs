using NUnit.Framework;
using ClassNo3;
using System;

namespace ClassNo3Tests
{
    public class UnitTest5
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase(new[] { 1, 2, 3, 4, 5 }, 0, 1, 1, new[] { 2, 1, 3, 4, 5 })]
        [TestCase(new[] { 1, 2, 3, 4, 5 }, 0, 1, 0, new[] { 1, 2, 3, 4, 5 })]        
        public void TestMethod1(int[] input, int start, int stop, int mode, int[] expectedOutput)
        {
            int[] output = ExerciseNo5.zamienElementy(input, start, stop, mode);
            CollectionAssert.AreEqual(expectedOutput, output);
        }

        [Test]
        [TestCase(new[] { 1, 2, 3, 4, 5 }, 0, 10, 0)]
        public void TestMethod2(int[] input, int start, int stop, int mode)
        {
            Assert.Throws<ArgumentException>(() => ExerciseNo5.zamienElementy(input, start, stop, mode));        
        }
    }
}
