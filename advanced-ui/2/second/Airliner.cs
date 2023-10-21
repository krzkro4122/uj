using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace second
{
    class Airliner : Plane
    {
        public double fuelTankCurrentLevel { get; set; }
        public double fuelTankCapacity { get; set; }

        public Airliner()
        {
            fuelTankCurrentLevel = 500;
            fuelTankCapacity = 500;   
        }

        public void fly() 
        {
            Console.WriteLine("An airliner is flying, weeeee!");
            fuelTankCurrentLevel -= 100;
        }

        public void turn() 
        {
            Console.WriteLine("An airliner is turning, zooooom!");    
        }

        public void takeOff() 
        {
            Console.WriteLine("An airliner is taking off, fasten your seatbelts!");    
        }

        public void land() 
        {
            Console.WriteLine("An airliner is landing, brace yourselves!");    
        }

        public void checkFuelTank()
        {
            Console.WriteLine("The airliner's fuel tank is at {0}% ({1}L of fuel out of maximum {2}L)", fuelTankCurrentLevel / fuelTankCapacity * 100, fuelTankCurrentLevel, fuelTankCapacity);
        }
    }
}
