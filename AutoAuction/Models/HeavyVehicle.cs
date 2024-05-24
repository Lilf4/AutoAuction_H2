using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAuction.Models {
    public abstract class HeavyVehicle : Vehicle {

        //Shared properties for heavy vehicles
        public float Height { get; set; }
        public float Length { get; set; }
        public float Weight { get; set; }

        public HeavyVehicle(int id, string name, int kmDriven, string regCode, int year, bool towHook, double kmPerUnit, FuelTypes fueltType, float height, float length, float weight) 
        : base(id, name, kmDriven, regCode, year, towHook, kmPerUnit, fueltType) { 
            this.Weight = weight;
            this.Height = height;
            this.Length = length;
        }


        public override string ToString() {
            return base.ToString() + $", Vehicle Class: HeavyVehicle";
        }
    }
}
