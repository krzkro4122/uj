using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassNo3
{    
    class ExerciseNo2
    {
        private static T[] zapiszWTablicy<T>(T[] input)
        {
            T[] output = new T[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                output[i] = input[input.Length - 1 - i];
            }

            return output;
        }

        public static void run()
        {
            int[] input1 = { 1, 2, 3, 4 };
            int[] output1 = zapiszWTablicy(input1);
            Console.WriteLine($"input=[{string.Join(", ", input1)}], output=[{string.Join(", ", output1)}]");


            string[] input2 = { "a", "b", "c", "d" };
            string[] output2 = zapiszWTablicy(input2);
            Console.WriteLine($"input=[{string.Join(", ", input2)}], output=[{string.Join(", ", output2)}]");

            Console.Read();
        }
    }
}
