using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace second
{
    class CargoPlane : Plane
    {
        public double fuelTankCurrentLevel { get; set; }
        public double fuelTankCapacity { get; set; }

        public CargoPlane()
        {
            fuelTankCurrentLevel = 1000;
            fuelTankCapacity = 1000;                
        }

        public void fly() 
        {
            Console.WriteLine("A cargo plane is flying, weeeee!");
            fuelTankCurrentLevel -= 200;
        }

        public void turn() 
        {
            Console.WriteLine("A cargo plane is turning, zooooom!");    
        }

        public void takeOff() 
        {
            Console.WriteLine("A cargo plane is taking off, fasten your seatbelts!");    
        }

        public void land() 
        {
            Console.WriteLine("A cargo plane is landing, brace yourselves!");    
        }

        public void checkFuelTank()
        {
            Console.WriteLine("The cargo plane's fuel tank is at {0}% ({1}L of fuel out of maximum {2}L)", fuelTankCurrentLevel / fuelTankCapacity * 100, fuelTankCurrentLevel, fuelTankCapacity);
        }
    }
}
