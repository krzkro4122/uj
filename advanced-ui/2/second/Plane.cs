using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace second
{
    interface Plane
    {
        double fuelTankCurrentLevel { get; set; }
        double fuelTankCapacity { get; set; }
        void fly();
        void turn();
        void takeOff();
        void land();
        void checkFuelTank();
    }
}
