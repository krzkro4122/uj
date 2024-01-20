using System;
using System.Linq;

namespace ClassNo3
{
    public class ExerciseNo4
    {
        public static string sumaCyfr(int number)
        {
            string parsedNumber = number.ToString();

            if (number <= 0)
                return "podana liczba nie jest dodatnia";

            if (parsedNumber.Length != 3)
                return "podana liczba nie jest trzycyfrowa";

            int[] digits =
            {
                Int32.Parse(parsedNumber[0].ToString()),
                Int32.Parse(parsedNumber[1].ToString()),
                Int32.Parse(parsedNumber[2].ToString()),
            };

            int sum = digits.Sum();

            if (sum % 2 == 0)
                return "liczba jest parzysta";
            else
                return "liczba nie jest parzysta";
        }

        public static void run()
        {
            Console.WriteLine($"number=123\t{sumaCyfr(123)}");
            Console.WriteLine($"number=124\t{sumaCyfr(124)}");
            Console.WriteLine($"number=1\t{sumaCyfr(1)}");
            Console.WriteLine($"number=-123\t{sumaCyfr(-123)}");

            Console.Read();
        }
    }
}
