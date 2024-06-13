using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAuction.Models
{
    internal class PrivatePersonalCar : PersonalCar
    {
        private double motorSize;
        public override double MotorSize
        {
            get
            {
                return motorSize;
            }
            set
            {
                if (value < 0.7 || value > 10.0)
                {
                    throw new ArgumentOutOfRangeException("Motor size must be between 0.7 and 10.0");
                }
                motorSize = value;
            }
        }

        public bool Isofix { get; set; }

        public PrivatePersonalCar(int id, string name, int kmDriven, string regCode, int year, bool towhook, bool isofix, double kmPerUnit, FuelTypes fuelType, int bootSize, int LoadCapacity, int numberOfSeats)
        : base(id, name, kmDriven, regCode, year, towhook, kmPerUnit, fuelType, bootSize, numberOfSeats)
        {
            this.Isofix = isofix;
            LicenseType = LicenseTypes.B;
        }

        public override string ToString()
        {
            return base.ToString() + $", Vehicle Type: PrivatePersonalCar";
        }
    }
}