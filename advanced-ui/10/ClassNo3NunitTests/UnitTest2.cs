using NUnit.Framework;
using ClassNo3;
using System;

namespace ClassNo3Tests
{
    public class UnitTest2
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase(new[] { 1, 2, 3, 4 }, new[] { 4, 3, 2, 1 })]        
        public void TestMethod1(int[] input, int[] expectedOutput)
        {
            int[] output = ExerciseNo2.zapiszWTablicy<int>(input);
            CollectionAssert.AreEqual(expectedOutput, output);
        }

        [Test]
        [TestCase(new[] { 'a', 'b', 'c', 'd' }, new[] { 'd', 'c', 'b', 'a' })]
        public void TestMethod2(char[] input, char[] expectedOutput)
        {
            char[] output = ExerciseNo2.zapiszWTablicy<char>(input);
            CollectionAssert.AreEqual(expectedOutput, output);
        }
    }
}
