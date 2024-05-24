using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAuction.Models {
    internal class Truck : HeavyVehicle {
        private double motorSize;
        public override double MotorSize {
            get {
                return motorSize;
            }
            set {
                if (value < 4.2 || value > 15.0) {
                    throw new ArgumentOutOfRangeException("Motor size must be between 4.2 and 15.0");
                }
                motorSize = value;
            }
        }

        public int LoadCapacity { get; set; }

        public Truck(int id, string name, int kmDriven, string regCode, int year, bool towHook, double kmPerUnit, FuelTypes fueltType, float height, float length, float weight, int LoadCapacity)
        : base(id, name, kmDriven, regCode, year, towHook, kmPerUnit, fueltType, height, length, weight) {
            this.LoadCapacity = LoadCapacity;

            if (towHook) {
                LicenseType = LicenseTypes.CE;
            }
            else {
                LicenseType = LicenseTypes.C;
            }
        }

        public override string ToString() {
            return base.ToString() + $", Vehicle Type: Truck";
        }
    }
}
