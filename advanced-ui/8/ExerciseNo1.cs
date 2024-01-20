using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassNo3
{
    class ExerciseNo1
    {
        private static float[] funkcjaKwadratowa(int a, int b, int c)
        {
            float[] miejscaZerowe = new float[2];

            // b^2 - 4ac
            int delta = b * b - 4 * a * c;

            if (delta == 0)
            {
                //x0 = -b / 2a
                float x0 = (float)((b * -1) / (2 * a));
                miejscaZerowe[0] = x0;
            }
            else if (delta > 0)
            {
                //x1 = (-b - sqrt(delta) ) / 2a
                float x1 = (float)((-b - Math.Sqrt(delta)) / (2 * a));
                miejscaZerowe[0] = x1;

                //x2 = (-b + sqrt(delta) ) / 2a
                float x2 = (float)((-b + Math.Sqrt(delta)) / (2 * a));
                miejscaZerowe[1] = x2;
            }
            else
            {
                throw new Exception("Nie ma prawdziwych miejsc zerowych!");
            }

            return miejscaZerowe;
        }

        public static void run()
        {
            Console.WriteLine($"x1={funkcjaKwadratowa(1, 2, -3)[0]} x2={funkcjaKwadratowa(1, 2, -3)[1]}");
            Console.WriteLine($"x1={funkcjaKwadratowa(2, 8, -10)[0]} x2={funkcjaKwadratowa(2, 8, -10)[1]}");

            Console.WriteLine($"x0={funkcjaKwadratowa(1, 2, 1)[0]}");

            Console.WriteLine($"Exception incoming!!!");
            funkcjaKwadratowa(2, 1, 1);

            Console.Read();
        }
    }
}
