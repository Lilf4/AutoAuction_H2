using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAuction.Models {
    public abstract class Vehicle {
        public int ID { get; set; }

        //Should throw exception on null set
        private string name = String.Empty;
        public string Name {
            get => name; 
            set { 
                if (value == null) {
                    throw new NoNullAllowedException("Name cannot be null");
                }
                name = value;
            } 
        }

        //Should throw exception on negative set
        private int kmDriven;
        public int KmDriven {
            get => kmDriven;
            set {
                if (value < 0) {
                    throw new ArgumentOutOfRangeException("KmDriven cannot be negative");
                }
                kmDriven = value;
            } 
        }

        //Must consist of 2 characters and 5 digits
        //On get replace first and last 2 characters with *
        //Must throw exception on invalid set
        private string regCode = String.Empty;
        public string RegCode {
            get { 
                string censored = regCode;
                censored = censored.Remove(0, 2);
                censored = censored.Remove(censored.Length - 2, 2);
                censored = censored.PadLeft(2, '*');
                censored = censored.PadRight(2, '*');
                return censored;
            } 
            set {
                if(value.Length != 7) {
                    throw new ArgumentException("RegCode must be 7 characters long");
                }
                if(!value.Substring(0, 2).All(char.IsLetter) && !value.Substring(2, 5).All(char.IsDigit)) {
                    throw new ArgumentException("RegCode must consist of 2 letters and 5 digits");
                }
                regCode = value;
            }
        }

        //Must be readonly
        public int Year { get; }

        //No comment
        public bool TowHook { get; set; }

        //No comment
        public LicenseTypes LicenseType { get; set; }

        //Allowed motor size for personal cars is 0.7 to 10.0
        //Allowed motor size for trucks and busses is 4.2 to 15.0
        //Must throw exception on invalid set
        public abstract double MotorSize { get; set; }

        //No comment
        public double KmPerUnit { get; set; }

        //Trucks and busses must have a fuel type of Diesel
        public FuelTypes FuelType { get; set; }

        //Vehicles before 2010 gets energyclass calculated from:
        // Fueltype Diesel:
        // A kmPerLiter >= 23
        // B 18 <= kmPerLiter < 23
        // C 13 <= kmPerLiter < 18
        // D kmPerLiter < 13

        // Fueltype Benzin:
        // A kmPerLiter >= 18
        // B 14 <= kmPerLiter < 18
        // C 10 <= kmPerLiter < 14
        // D kmPerLiter < 10

        //Vehicles after 2010 gets energyclass calculated from:
        // Fueltype Diesel:
        // A kmPerLiter >= 25
        // B 20 <= kmPerLiter < 25
        // C 15 <= kmPerLiter < 20
        // D kmPerLiter < 15

        // Fueltype Benzin:
        // A kmPerLiter >= 20
        // B 16 <= kmPerLiter < 20
        // C 12 <= kmPerLiter < 16
        // D kmPerLiter < 12
        public EnergyClasses EnergyClass {
            get {
                if (Year < 2010) {
                    return FuelType switch {
                        FuelTypes.Diesel => KmPerUnit switch {
                            >= 23 => EnergyClasses.A,
                            >= 18 => EnergyClasses.B,
                            >= 13 => EnergyClasses.C,
                            _ => EnergyClasses.D,
                        },
                        FuelTypes.Benzin => KmPerUnit switch {
                            >= 18 => EnergyClasses.A,
                            >= 14 => EnergyClasses.B,
                            >= 10 => EnergyClasses.C,
                            _ => EnergyClasses.D,
                        },
                        _ => EnergyClasses.A,
                    };
                }
                else {
                    return FuelType switch {
                        FuelTypes.Diesel => KmPerUnit switch {
                            >= 25 => EnergyClasses.A,
                            >= 20 => EnergyClasses.B,
                            >= 15 => EnergyClasses.C,
                            _ => EnergyClasses.D,
                        },
                        FuelTypes.Benzin => KmPerUnit switch {
                            >= 20 => EnergyClasses.A,
                            >= 16 => EnergyClasses.B,
                            >= 12 => EnergyClasses.C,
                            _ => EnergyClasses.D,
                        },
                        _ => EnergyClasses.A,
                    };
                }
            }
        }
        public Vehicle() { }
        public Vehicle(int id, string name, int kmDriven, string regCode, int year, bool towHook, double kmPerUnit, FuelTypes fuelType) {
            this.ID = id;
            this.Name = name;
            this.KmDriven = kmDriven;
            this.RegCode = regCode;
            this.Year = year;
            this.TowHook = towHook;
            this.KmPerUnit = kmPerUnit;
            this.FuelType = fuelType;
        }

        public override string ToString() {
            return $"Vehicle: {this.Name}";
        }

        public enum LicenseTypes {
            A,
            B,
            C,
            D,
            BE,
            CE,
            DE
        }

        public enum FuelTypes {
            Benzin,
            Diesel,
            Electric,
            Hydrogen,
        }

        public enum EnergyClasses {
            A,
            B,
            C,
            D,
            E,
            F,
            G,
        }
    }
}
