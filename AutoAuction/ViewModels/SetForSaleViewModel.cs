using AutoAuction.Models;
using ReactiveUI;
using System;
using System.Diagnostics;
using System.Linq;

namespace AutoAuction.ViewModels {
    public class SetForSaleViewModel : ViewModelBase {

        public string[] fuelTypes { get; } = Enum.GetNames(typeof(Vehicle.FuelTypes));
        public string[] vehicleTypes { get; } = {
            "Private",
            "Professional",
            "Truck",
            "Bus"
        };
        public int[] seatNums { get; } = Enumerable.Range(2, 4).ToArray();

        //Base Vehicle properties
        private string name = string.Empty;
        public string Name {
            get { return name; }
            set { this.RaiseAndSetIfChanged(ref name, value, "Name"); }
        }
        private int kmDriven;
        public int KmDriven {
            get { return kmDriven; }
            set { this.RaiseAndSetIfChanged(ref kmDriven, value, "KmDriven"); }
        }
        private double kmPerUnit;
        public double KmPerUnit {
            get { return kmPerUnit; }
            set { this.RaiseAndSetIfChanged(ref kmPerUnit, value, "KmPerUnit"); }
        }
        private string fuelType = string.Empty;
        public string FuelType {
            get { return fuelType; }
            set { this.RaiseAndSetIfChanged(ref fuelType, value, "FuelType"); }
        }
        private string regCode = string.Empty;
        public string RegCode {
            get { return regCode; }
            set { this.RaiseAndSetIfChanged(ref regCode, value, "RegCode"); }
        }
        private int year;
        public int Year {
            get { return year; }
            set { this.RaiseAndSetIfChanged(ref year, value, "Year"); }
        }
        private string vehicleType = string.Empty;
        public string VehicleType {
            get { return vehicleType; }
            set { this.RaiseAndSetIfChanged(ref vehicleType, value, "VehicleType"); }
        }
        private bool towHook;
        public bool TowHook {
            get { return towHook; }
            set { this.RaiseAndSetIfChanged(ref towHook, value, "TowHook"); }
        }
        private double motorSize;
        public double MotorSize {
            get { return motorSize; }
            set { this.RaiseAndSetIfChanged(ref motorSize, value, "MotorSize"); }
        }
        private decimal price;
        public decimal Price {
            get { return price; }
            set { this.RaiseAndSetIfChanged(ref price, value, "Price"); }
        }

        //HeavyVehicle properties
        private float height;
        public float Height {
            get { return height; }
            set { this.RaiseAndSetIfChanged(ref height, value, "Height"); }
        }
        private float length;
        public float Length {
            get { return length; }
            set { this.RaiseAndSetIfChanged(ref length, value, "Length"); }
        }
        private int weight;
        public int Weight {
            get { return weight; }
            set { this.RaiseAndSetIfChanged(ref weight, value, "Weight"); }
        }

        //Truck properties
        private int loadCapacity;
        public int LoadCapacity {
            get { return loadCapacity; }
            set { this.RaiseAndSetIfChanged(ref loadCapacity, value, "LoadCapacity"); }
        }

        //Bus properties
        private int seats;
        public int Seats {
            get { return numberOfSeats; }
            set { this.RaiseAndSetIfChanged(ref numberOfSeats, value, "NumberOfSeats"); }
        }
        private int standingPlaces;
        public int StandingPlaces {
            get { return standingPlaces; }
            set { this.RaiseAndSetIfChanged(ref standingPlaces, value, "StandingPlaces"); }
        }
        private bool toilet;
        public bool Toilet {
            get { return toilet; }
            set { this.RaiseAndSetIfChanged(ref toilet, value, "Toilet"); }
        }

        //PersonalVehicle properties
        private int numberOfSeats;
        public int NumberOfSeats {
            get { return numberOfSeats; }
            set { this.RaiseAndSetIfChanged(ref numberOfSeats, value, "NumberOfSeats"); }
        }
        private int bootSize;
        public int BootSize {
            get { return bootSize; }
            set { this.RaiseAndSetIfChanged(ref bootSize, value, "BootSize"); }
        }
        //PrivateVehicle properties
        private bool isoFix;
        public bool IsoFix {
            get { return isoFix; }
            set { this.RaiseAndSetIfChanged(ref isoFix, value, "IsoFix"); }
        }

        //ProffesionalVehicle properties
        private int load;
        public int Load{
            get { return load; }
            set { this.RaiseAndSetIfChanged(ref load, value, "Load"); }
        }
        private bool safetyBar;
        public bool SafetyBar {
            get { return safetyBar; }
            set { this.RaiseAndSetIfChanged(ref safetyBar, value, "SafetyBar"); }
        }
        public SetForSaleViewModel() {
        }

        public void Cancel()
        {
            MainWindowViewModel.Instance.CurrViewModel = new HomeViewModel();
        }

        public void CreateAuction() {
            Vehicle vehicle = null;
            try {

                switch (VehicleType) {
                    case "Private":
                        PrivatePersonalCar privateCar = new PrivatePersonalCar(
                            id: 0,
                            name: Name,
                            kmDriven: KmDriven,
                            regCode: RegCode,
                            year: Year,
                            towhook: TowHook,
                            isofix: IsoFix,
                            kmPerUnit: KmPerUnit,
                            fuelType: (Vehicle.FuelTypes)Enum.Parse(typeof(Vehicle.FuelTypes), FuelType),
                            bootSize: BootSize,
                            numberOfSeats: NumberOfSeats) {
                            MotorSize = MotorSize
                        };
                        vehicle = privateCar;
                        break;
                    case "Professional":
                        ProfessionelPersonalCar professionalCar = new ProfessionelPersonalCar(
                            id: 0,
                            name: Name,
                            kmDriven: KmDriven,
                            regCode: RegCode,
                            year: Year,
                            towhook: TowHook,
                            safetyBar: SafetyBar,
                            kmPerUnit: KmPerUnit,
                            fuelType: (Vehicle.FuelTypes)Enum.Parse(typeof(Vehicle.FuelTypes), FuelType),
                            bootSize: BootSize,
                            LoadCapacity: load) {
                            MotorSize = MotorSize
                        };
                        vehicle = professionalCar;
                        break;
                    case "Truck":
                        Truck truck = new Truck(
                            id: 0,
                            name: Name,
                            kmDriven: KmDriven,
                            regCode: RegCode,
                            year: Year,
                            towHook: TowHook,
                            kmPerUnit: KmPerUnit,
                            fueltType: (Vehicle.FuelTypes)Enum.Parse(typeof(Vehicle.FuelTypes), FuelType),
                            height: Height,
                            length: Length,
                            weight: Weight,
                            LoadCapacity: LoadCapacity) {
                            MotorSize = MotorSize
                        };
                        vehicle = truck;
                        break;
                    case "Bus":
                        Bus bus = new Bus(
                            id: 0,
                            name: Name,
                            kmDriven: KmDriven,
                            regCode: RegCode,
                            year: Year,
                            towHook: TowHook,
                            kmPerUnit: KmPerUnit,
                            fueltType: (Vehicle.FuelTypes)Enum.Parse(typeof(Vehicle.FuelTypes), FuelType),
                            height: Height,
                            length: Length,
                            weight: Weight,
                            seats: Seats,
                            standingPlaces: StandingPlaces,
                            toilets: Toilet) {
                            MotorSize = MotorSize
                        };
                        vehicle = bus;
                        break;
                }
                //CREATE AUCTION
                Auction auction = new Auction(
                    auctionID: 0,
                    seller: MainWindowViewModel.Instance.User,
                    vehicle: vehicle,
                    minimumPrice: Price
                    );
                MainWindowViewModel.Instance._IAuction.CreateAuction(auction, VehicleType);
            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);
            }
        }

        public int[] YearRange
        {
            get
            {
                int x = 1900;
                int y = DateTime.Now.Year;
                int[] numbers = Enumerable.Range(x, y - x + 1).ToArray();
                return numbers;
            }
        }

    }
}
