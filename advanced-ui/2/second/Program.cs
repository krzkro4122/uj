using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace second
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("==========1st PLANE==========");
            Airliner boeing737 = new Airliner();
            boeing737.checkFuelTank();
            boeing737.takeOff();
            boeing737.fly();
            boeing737.turn();
            boeing737.fly();
            boeing737.land();
            boeing737.checkFuelTank();

            Console.WriteLine("==========2nd PLANE==========");
            CargoPlane example_name_of_a_cargo_plane = new CargoPlane();
            example_name_of_a_cargo_plane.checkFuelTank();
            example_name_of_a_cargo_plane.takeOff();
            example_name_of_a_cargo_plane.fly();
            example_name_of_a_cargo_plane.turn();
            example_name_of_a_cargo_plane.fly();
            example_name_of_a_cargo_plane.land();
            example_name_of_a_cargo_plane.checkFuelTank();
        }
    }
}
