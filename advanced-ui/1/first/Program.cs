using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace first
{
    class Program
    {        
        static void Main(string[] args)
        {            
            Console.WriteLine("Input a value for coefficient 'a': ");
            double a = Convert.ToDouble(Console.ReadLine());

            if (a == 0)
            {
                Console.WriteLine("Not a quadratic equation!");
                System.Environment.Exit(1);
            }

            Console.WriteLine("Input a value for coefficient 'b': ");
            double b = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Input a value for coefficient 'c': ");
            double c = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Quadratic equation to solve: {0}x^2 + {1}x + {2}", a, b, c);

            double delta = b * b - 4 * a * c;
            double squared_delta = Math.Sqrt(delta);

            if (delta < 0)
            {
                Console.WriteLine("No real solutions found!");
                System.Environment.Exit(1);
            }
            else if (delta > 0)
            {
                double x1 = (-b + squared_delta) / (2 * a);
                double x2 = (-b - squared_delta) / (2 * a);
                Console.WriteLine("The possible solutions are: x1={0} and x2={1}", x1, x2);
            }
            else
            {
                double x0 = -b / (2 * a);
                Console.WriteLine("The solution is: x0={0}", x0);
            }
        }
    }
}
