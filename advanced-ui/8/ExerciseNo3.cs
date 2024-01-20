using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassNo3
{
    class ExerciseNo3
    {
        private static int objetoscSzescianu(int sideLength)
        {
            if (sideLength <= 0)
            {
                throw new ArgumentException("bok musi byc wiekszy od 0");

            }
            return sideLength * sideLength * sideLength;
        }

        public static void run()
        {
            Console.WriteLine($"side=1, volume={objetoscSzescianu(1)}");
            Console.WriteLine($"side=2, volume={objetoscSzescianu(2)}");
            Console.WriteLine($"side=3, volume={objetoscSzescianu(3)}");
            Console.WriteLine("side=0, Exception incoming...");
            objetoscSzescianu(0);

            Console.Read();
        }
    }
}
