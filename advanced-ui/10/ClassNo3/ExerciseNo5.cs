using System;

namespace ClassNo3
{
    public class ExerciseNo5
    {
        public static int[] zamienElementy(int[] input, int start, int stop, int mode)
        {
            int[] output = new int[input.Length];
            input.CopyTo(output, 0);

            if (start >= input.Length || stop >= input.Length)
                throw new ArgumentException("indeks spoza zakresu");

            if (mode > 0)
            {
                int temporary = output[start];
                output[start] = output[stop];
                output[stop] = temporary;
                return output;
            } else
            {
                return output;
            }
        }

        public static void run()
        {
            int[] input = { 1, 2, 3, 4, 5 };
            int[] output1 = zamienElementy(input, 0, 1, 1);
            Console.WriteLine($"input=[{string.Join(", ", input)}]\tstart=0\tstop=1\tmode=1\toutput=[{string.Join(",", output1)}]");
            int[] output2 = zamienElementy(input, 0, 1, 0);
            Console.WriteLine($"input=[{string.Join(", ", input)}]\tstart=0\tstop=1\tmode=0\toutput=[{string.Join(",", output2)}]");
            Console.WriteLine($"input=[{string.Join(", ", input)}]\tstart=0\tstop=10\tmode=1\tWill throw an exception...");
            int[] output3 = zamienElementy(input, 0, 10, 1);
            Console.Read();
        }
    }
}
