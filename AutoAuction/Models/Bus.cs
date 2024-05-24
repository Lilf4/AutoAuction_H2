using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAuction.Models {
    public class Bus : HeavyVehicle {

        private double motorSize;
        public override double MotorSize { 
            get { return motorSize; }
            set {
                if (value < 4.2 || value > 15.0)
                {
                    throw new ArgumentOutOfRangeException("Motor size must be between 4.2 and 15.0");
                }
                motorSize = value;
            } 
        }

        public int Seats { get; set; }
        public int StandingPlaces { get; set; }
        public bool Toilets { get; set; }


        //TODO sort by necessary start set fields
        public Bus(int id, string name, int kmDriven, string regCode, int year, bool towHook, double kmPerUnit, FuelTypes fueltType, float height, float length, float weight, int seats, int standingPlaces, bool toilets) : 
            base(id, name, kmDriven, regCode, year, towHook, kmPerUnit, fueltType, height, length, weight) {
            this.Seats = seats;
            this.StandingPlaces = standingPlaces;
            this.Toilets = toilets;

            if(towHook) {
                LicenseType = LicenseTypes.DE;
            }
            else {
                LicenseType = LicenseTypes.D;
            }
        }

        public override string ToString() {
            return base.ToString() + $", Vehicle Type: Bus";
        }
    }
}
