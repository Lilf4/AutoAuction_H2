using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAuction.Models
{
	public abstract class PersonalCar : Vehicle
	{

		//Shared properties for Personal cars
		public int NumberOfSeats { get; set; }
		public int BootSize { get; set; }

		public PersonalCar(int id, string name, int kmDriven, string regCode, int year, bool towHook, double kmPerUnit, FuelTypes fueltType, int bootSize, int numberOfSeats)
		: base(id, name, kmDriven, regCode, year, towHook, kmPerUnit, fueltType)
		{
			this.NumberOfSeats = numberOfSeats;
			this.BootSize = bootSize;
		}


		public override string ToString()
		{
			return base.ToString() + $", Vehicle Class: PersonalCar";
		}
	}
}
